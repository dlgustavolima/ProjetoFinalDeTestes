using Features.Jogos;

namespace Features.Tests.Fixture
{
    [Collection(nameof(JogoCollection))]
    public class JogoTesteValido
    {
        private readonly JogoTestsFixture _jogosTestsFixture;

        public JogoTesteValido(JogoTestsFixture jogosTestsFixture)
        {
            _jogosTestsFixture = jogosTestsFixture;
        }

        [Fact(DisplayName = "Novo Jogo Valido")]
        [Trait("Categoria", "Jogo Teste Fixture")]
        public void Jogo_NovoJogo_JogoDeveEstarValido()
        {
            //Arrange
            var jogo = _jogosTestsFixture.GerarJogoValido();

            // Act
            var result = jogo.EhValido();

            // Assert
            Assert.True(result);
        }
    }
}
