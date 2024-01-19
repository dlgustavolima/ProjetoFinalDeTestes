using Features.Jogos;

namespace Features.Tests.Fixture
{
    [Collection(nameof(JogoCollection))]
    public class JogoTesteInvalido 
    {

        private readonly JogoTestsFixture _jogosTestsFixture;

        public JogoTesteInvalido(JogoTestsFixture jogosTestsFixture)
        {
            _jogosTestsFixture = jogosTestsFixture;
        }

        [Fact(DisplayName = "Novo Jogo Invalido")]
        [Trait("Categoria", "Jogo Teste Fixture")]
        public void Jogo_NovoJogo_JogoNaoDeveEstarValido()
        {
            // Arrange
            var jogo = _jogosTestsFixture.GerarJogoInvalido();

            // Act
            var result = jogo.EhValido();

            // Assert
            Assert.False(result);
        }
    }
}
