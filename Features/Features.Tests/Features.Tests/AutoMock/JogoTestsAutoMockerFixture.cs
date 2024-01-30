using Bogus;
using Features.Jogos;
using Moq.AutoMock;

namespace Features.Tests.AutoMock
{
    [CollectionDefinition(nameof(JogoTestsAutoMockerCollection))]

    public class JogoTestsAutoMockerCollection : ICollectionFixture<JogoTestsAutoMockerFixture> { }


    public class JogoTestsAutoMockerFixture : IDisposable
    {
        public JogoService JogoService;
        public AutoMocker Moker;

        public Jogo GerarJogoValido()
        {
            return new Faker<Jogo>("pt_BR")
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
        }

        public Jogo GerarJogoInvalido()
        {
            return new Faker<Jogo>("pt_BR")
                .CustomInstantiator(f => new Jogo(  //Para classes que possuem ctor utilizar o (CustomInstantiator)
                    Guid.NewGuid(),
                    null,
                    f.Lorem.Paragraph(1),
                    "",
                    true,
                    f.Finance.Amount(100, 500),
                    1,
                    f.Date.Future(30)
                    ))
                .RuleFor(p => p.EmailProdutora, (f, p) => f.Internet.Email(p.Nome))
                .Generate();
        }

        public IEnumerable<Jogo> GerarJogosValidos(int quantidade, bool ativos)
        {
            return new Faker<Jogo>("pt_BR")
                .CustomInstantiator(f => new Jogo(  //Para classes que possuem ctor utilizar o (CustomInstantiator)
                    Guid.NewGuid(),
                    f.Name.FirstName(),
                    f.Lorem.Paragraph(1),
                    "",
                    ativos,
                    f.Finance.Amount(100, 500),
                    1,
                    f.Date.Recent(30)
                    ))
                .RuleFor(p => p.EmailProdutora, (f, p) => f.Internet.Email(p.Nome))
                .Generate(quantidade);
        }

        public IEnumerable<Jogo> ObterJogosVariados()
        {
            var jogos = new List<Jogo>();

            jogos.AddRange(GerarJogosValidos(50, true).ToList());
            jogos.AddRange(GerarJogosValidos(50, false).ToList());

            return jogos;
        }

        public JogoService ObterJogoService() 
        {
            Moker = new AutoMocker();
            var jogoService = Moker.CreateInstance<JogoService>();

            return jogoService;
        }

        public void Dispose()
        {
        }


    }
}
