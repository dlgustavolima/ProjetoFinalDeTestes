using Features.Jogos;

namespace Features.Tests.Traits
{
    public class JogoTraitTests
    {
        [Fact(DisplayName = "Novo Jogo Valido")]
        [Trait("Categoria", "Jogo Teste Trait")]
        public void Jogo_NovoJogo_JogoDeveEstarValido()
        {
            // Arrange
            var jogo = new Jogo(Guid.NewGuid(),"Elden Ring", "Teste", "fromsoftware@teste.com", true, 350, 1, DateTime.Now);

            // Act
            var result = jogo.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Novo Jogo Invalido")]
        [Trait("Categoria", "Jogo Teste Trait")]
        public void Jogo_NovoJogo_JogoNaoDeveEstarValido()
        {
            // Arrange
            var jogo = new Jogo(Guid.NewGuid(), "Elden Ring", "Teste", "fromsoftware@teste.com", true, 350, 1, DateTime.Now.AddYears(11));

            // Act
            var result = jogo.EhValido();

            // Assert
            Assert.False(result);
        }

    }
}
