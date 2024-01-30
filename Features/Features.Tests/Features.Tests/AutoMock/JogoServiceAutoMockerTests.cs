using Features.Jogos;
using Features.Tests.HumanData;
using MediatR;
using Moq;
using Moq.AutoMock;

namespace Features.Tests.AutoMock
{
    [Collection(nameof(JogoCollectionHumanData))]
    public class JogoServiceAutoMockerTests
    {
        private readonly HumanDataTests _jogosTestsHumanData;

        public JogoServiceAutoMockerTests(HumanDataTests jogosTestsHumanData)
        {
            _jogosTestsHumanData = jogosTestsHumanData;
        }

        [Fact(DisplayName = "Novo Jogo Valido com Sucesso")]
        [Trait("Categoria", "Jogo Teste Auto Mock")]
        public void Jogo_NovoJogo_DeveExecutarComSucesso()
        {
            //Arrange
            var jogo = _jogosTestsHumanData.GerarJogoValidoComDadosHumanos();
            var autoMoker = new AutoMocker();
            var jogoService = autoMoker.CreateInstance<JogoService>();

            // Act
            jogoService.Adicionar(jogo);

            // Assert
            autoMoker.GetMock<IJogoRepository>().Verify(r => r.Adicionar(jogo), Times.Once);
            autoMoker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Novo Jogo Valido com Falha")]
        [Trait("Categoria", "Jogo Teste Auto Mock")]
        public void Jogo_NovoJogo_DeveExecutarComFalha()
        {
            //Arrange
            var jogo = _jogosTestsHumanData.GerarJogoInvalidoComDadosHumanos();
            var autoMoker = new AutoMocker();
            var jogoService = autoMoker.CreateInstance<JogoService>();

            // Act
            jogoService.Adicionar(jogo);

            // Assert
            autoMoker.GetMock<IJogoRepository>().Verify(r => r.Adicionar(jogo), Times.Never);
            autoMoker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Novo Jogos Ativos")]
        [Trait("Categoria", "Jogo Teste Auto Mock")]
        public void Jogo_ObterTodosJogos_DeveRetornarApenasJogosAtivos()
        {
            //Arrange
            var autoMoker = new AutoMocker();
            var jogoService = autoMoker.CreateInstance<JogoService>();

            autoMoker.GetMock<IJogoRepository>().Setup(p => p.ObterTodos())
                .Returns(_jogosTestsHumanData.ObterJogosVariados());

            // Act
            var jogos = jogoService.ObterTodosAtivos();

            // Assert
            autoMoker.GetMock<IJogoRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(jogos.Any());
            Assert.False(jogos.Count(c => !c.Ativo) > 0);
        }
    }
}
