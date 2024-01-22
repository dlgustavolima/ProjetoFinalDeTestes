using Bogus;
using Features.Jogos;
using Features.Tests.Fixture;

namespace Features.Tests.HumanData
{
    [Collection(nameof(JogoCollection))]
    public class HumanDataTests
    {
        private readonly JogoTestsFixture _jogosTestsFixture;

        public HumanDataTests(JogoTestsFixture jogosTestsFixture)
        {
            _jogosTestsFixture = jogosTestsFixture;
        }

        [Fact(DisplayName = "Novo Jogo Valido Por DadosHumanos")]
        [Trait("Categoria", "Jogo Teste HumanData")]
        public void Jogo_NovoJogo_JogoDeveEstarValido()
        {
            //Arrange
            //var jogo = new Jogo(Guid.NewGuid(), "Elden Ring", "Teste", "fromsoftware@teste.com", true, 350, 1, DateTime.Now);
            var jogo = new Faker<Jogo>("pt_BR")
                .CustomInstantiator(f => new Jogo(  //Para classes que possuem ctor utilizar o (CustomInstantiator)
                    Guid.NewGuid(),
                    f.Name.FirstName(),
                    f.Lorem.Paragraph(1),
                    "",
                    true,
                    f.Finance.Amount(100, 500),
                    1,
                    f.Date.Recent(30)
                    ))
                .RuleFor(p => p.EmailProdutora, (f, p) => f.Internet.Email(p.Nome))
                .Generate();

            // Act
            var result = jogo.EhValido();

            // Assert
            Assert.True(result);
        }


    }
}
