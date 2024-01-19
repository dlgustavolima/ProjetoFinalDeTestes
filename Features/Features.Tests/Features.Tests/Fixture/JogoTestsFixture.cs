using Features.Jogos;

namespace Features.Tests.Fixture
{

    [CollectionDefinition(nameof(JogoCollection))]
    public class JogoCollection : ICollectionFixture<JogoTestsFixture> { }

    public class JogoTestsFixture : IDisposable
    {


        public Jogo GerarJogoValido() 
        {
            return new Jogo(Guid.NewGuid(), "Elden Ring", "Teste", "fromsoftware@teste.com", true, 350, 1, DateTime.Now);
        }

        public Jogo GerarJogoInvalido()
        {
            return new Jogo(Guid.NewGuid(), "Elden Ring", "Teste", "fromsoftware@teste.com", true, 350, 1, DateTime.Now.AddYears(11));
        }

        public void Dispose()
        {
        }
    }
}
