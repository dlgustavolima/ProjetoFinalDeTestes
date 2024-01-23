using Bogus;
using Features.Jogos;
using Features.Tests.Fixture;
using System.Collections.Generic;

namespace Features.Tests.HumanData
{
    [CollectionDefinition(nameof(JogoCollectionHumanData))]
    public class JogoCollectionHumanData : ICollectionFixture<HumanDataTests> { }

    public class HumanDataTests
    {
        public Jogo GerarJogoValidoComDadosHumanos()
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

        public Jogo GerarJogoInvalidoComDadosHumanos()
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

        public IEnumerable<Jogo> GerarJogosValidosComDadosHumanos(int quantidade, bool ativos)
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

            jogos.AddRange(GerarJogosValidosComDadosHumanos(50, true).ToList());
            jogos.AddRange(GerarJogosValidosComDadosHumanos(50, false).ToList());

            return jogos;
        }
    }
}
