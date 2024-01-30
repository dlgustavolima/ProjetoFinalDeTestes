using Features.Jogos;
using MediatR;
using Moq;

namespace Features.Tests.AutoMock
{
    [Collection(nameof(JogoTestsAutoMockerCollection))]
    public class JogoServiceAutoMockerFixtureTests
    {
        private readonly JogoTestsAutoMockerFixture _jogoTestsAutoMockerFixture;
        private readonly JogoService _jogoService;

        public JogoServiceAutoMockerFixtureTests(JogoTestsAutoMockerFixture jogoTestsAutoMockerFixture)
        {
            _jogoTestsAutoMockerFixture = jogoTestsAutoMockerFixture;
            _jogoService = _jogoTestsAutoMockerFixture.ObterJogoService();
        }

        [Fact(DisplayName = "Novo Jogo Valido com Sucesso")]
        [Trait("Categoria", "Jogo Teste Auto Mock Fixture")]
        public void Jogo_NovoJogo_DeveExecutarComSucesso()
        {
            //Arrange
            var jogo = _jogoTestsAutoMockerFixture.GerarJogoValido();

            // Act
            _jogoService.Adicionar(jogo);

            // Assert
            _jogoTestsAutoMockerFixture.Moker.GetMock<IJogoRepository>().Verify(r => r.Adicionar(jogo), Times.Once);
            _jogoTestsAutoMockerFixture.Moker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Novo Jogo Valido com Falha")]
        [Trait("Categoria", "Jogo Teste Auto Mock Fixture")]
        public void Jogo_NovoJogo_DeveExecutarComFalha()
        {
            //Arrange
            var jogo = _jogoTestsAutoMockerFixture.GerarJogoInvalido();

            // Act
            _jogoService.Adicionar(jogo);

            // Assert
            _jogoTestsAutoMockerFixture.Moker.GetMock<IJogoRepository>().Verify(r => r.Adicionar(jogo), Times.Never);
            _jogoTestsAutoMockerFixture.Moker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Novo Jogos Ativos")]
        [Trait("Categoria", "Jogo Teste Auto Mock Fixture")]
        public void Jogo_ObterTodosJogos_DeveRetornarApenasJogosAtivos()
        {
            //Arrange
            _jogoTestsAutoMockerFixture.Moker.GetMock<IJogoRepository>().Setup(p => p.ObterTodos())
                .Returns(_jogoTestsAutoMockerFixture.ObterJogosVariados());

            // Act
            var jogos = _jogoService.ObterTodosAtivos();

            // Assert
            _jogoTestsAutoMockerFixture.Moker.GetMock<IJogoRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(jogos.Any());
            Assert.False(jogos.Count(c => !c.Ativo) > 0);
        }

    }
}
