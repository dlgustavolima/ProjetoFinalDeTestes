using Bogus;
using Features.Jogos;
using Features.Tests.Fixture;
using Features.Tests.HumanData;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Tests.Mock
{
    [Collection(nameof(JogoCollectionHumanData))]
    public class JogoServiceTests
    {
        private readonly HumanDataTests _jogosTestsHumanData;

        public JogoServiceTests(HumanDataTests jogosTestsHumanData)
        {
            _jogosTestsHumanData = jogosTestsHumanData;
        }

        [Fact(DisplayName = "Novo Jogo Valido com Sucesso")]
        [Trait("Categoria", "Jogo Teste Mock")]
        public void Jogo_NovoJogo_DeveExecutarComSucesso()
        {
            //Arrange
            var jogo = _jogosTestsHumanData.GerarJogoValidoComDadosHumanos();
            var jogoRepo = new Mock<IJogoRepository>();
            var mediator = new Mock<IMediator>();

            var jogoService = new JogoService(jogoRepo.Object, mediator.Object);

            // Act
            jogoService.Adicionar(jogo);

            // Assert
            jogoRepo.Verify(r => r.Adicionar(jogo), Times.Once);
            mediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Novo Jogo Valido com Falha")]
        [Trait("Categoria", "Jogo Teste Mock")]
        public void Jogo_NovoJogo_DeveExecutarComFalha()
        {
            //Arrange
            var jogo = _jogosTestsHumanData.GerarJogoInvalidoComDadosHumanos();
            var jogoRepo = new Mock<IJogoRepository>();
            var mediator = new Mock<IMediator>();

            var jogoService = new JogoService(jogoRepo.Object, mediator.Object);

            // Act
            jogoService.Adicionar(jogo);

            // Assert
            jogoRepo.Verify(r => r.Adicionar(jogo), Times.Never);
            mediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Novo Jogos Ativos")]
        [Trait("Categoria", "Jogo Teste Mock")]
        public void Jogo_ObterTodosJogos_DeveRetornarApenasJogosAtivos()
        {
            //Arrange
            var jogoRepo = new Mock<IJogoRepository>();
            var mediator = new Mock<IMediator>();

            jogoRepo.Setup(p => p.ObterTodos())
                .Returns(_jogosTestsHumanData.ObterJogosVariados());

            var jogoService = new JogoService(jogoRepo.Object, mediator.Object);

            // Act
            var jogos = jogoService.ObterTodosAtivos();

            // Assert
            jogoRepo.Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(jogos.Any());
            Assert.False(jogos.Count(c => !c.Ativo) > 0);
        }
    }
}
