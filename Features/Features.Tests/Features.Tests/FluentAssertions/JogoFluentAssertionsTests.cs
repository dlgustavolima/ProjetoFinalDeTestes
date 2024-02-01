using Features.Jogos;
using Features.Tests.HumanData;
using MediatR;
using Moq.AutoMock;
using Moq;
using FluentAssertions;

namespace Features.Tests.FluentAssertions
{
    [Collection(nameof(JogoCollectionHumanData))]
    public class JogoFluentAssertionsTests
    {
        private readonly HumanDataTests _jogosTestsHumanData;

        public JogoFluentAssertionsTests(HumanDataTests jogosTestsHumanData)
        {
            _jogosTestsHumanData = jogosTestsHumanData;
        }

        [Fact(DisplayName = "Novo Jogo Valido com Sucesso")]
        [Trait("Categoria", "Jogo Teste FluentAssertons")]
        public void Jogo_NovoJogo_DeveExecutarComSucesso()
        {
            //Arrange
            var jogo = _jogosTestsHumanData.GerarJogoValidoComDadosHumanos();
            var autoMoker = new AutoMocker();
            var jogoService = autoMoker.CreateInstance<JogoService>();

            // Act
            jogoService.Adicionar(jogo);

            // Assert
            jogo.EhValido().Should().BeTrue();

            autoMoker.GetMock<IJogoRepository>().Verify(r => r.Adicionar(jogo), Times.Once);
            autoMoker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Novo Jogo Valido com Falha")]
        [Trait("Categoria", "Jogo Teste FluentAssertons")]
        public void Jogo_NovoJogo_DeveExecutarComFalha()
        {
            //Arrange
            var jogo = _jogosTestsHumanData.GerarJogoInvalidoComDadosHumanos();
            var autoMoker = new AutoMocker();
            var jogoService = autoMoker.CreateInstance<JogoService>();

            // Act
            jogoService.Adicionar(jogo);

            // Assert
            jogo.EhValido().Should().BeFalse();

            autoMoker.GetMock<IJogoRepository>().Verify(r => r.Adicionar(jogo), Times.Never);
            autoMoker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Novo Jogos Ativos")]
        [Trait("Categoria", "Jogo Teste FluentAssertons")]
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
            jogos.Should().NotContain(p => !p.Ativo);
            jogos.Should().HaveCountGreaterOrEqualTo(1).And.OnlyHaveUniqueItems();
        }
    }
}
