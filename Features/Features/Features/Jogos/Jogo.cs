using Features.Core;
using FluentValidation;

namespace Features.Jogos
{
    public class Jogo : Entity
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string EmailProdutora { get; set; }

        public bool Ativo { get; set; }

        public decimal Valor { get; set; }

        public int Unidade { get; set; }

        public DateTime DataLancamento { get; set; }


        protected Jogo()
        {
        }

        public Jogo(string nome, string descricao, bool ativo, decimal valor, int unidade, DateTime dataLancamento)
        {
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            Unidade = unidade;
            DataLancamento = dataLancamento;
        }

        public void Inativar()
        {
            Ativo = false;
        }

        public override bool EhValido()
        {
            ValidationResult = new JogoValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class JogoValidacao : AbstractValidator<Jogo>
    {
        public JogoValidacao()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Por favor, certifique-se de ter inserido o nome")
                .Length(2, 150).WithMessage("O nome deve ter entre 2 e 150 caracteres");

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("Por favor, certifique-se de ter inserido o sobrenome")
                .Length(2, 3000).WithMessage("O Sobrenome deve ter entre 2 e 3000 caracteres");

            RuleFor(c => c.DataLancamento)
                .NotEmpty()
                .Must(HaveMinimumAge)
                .WithMessage("O cliente deve ter 18 anos ou mais");

            RuleFor(c => c.Valor)
                .NotEmpty()
                .LessThan(0);

            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        public static bool HaveMinimumAge(DateTime dataLancamento)
        {
            return dataLancamento <= DateTime.Now.AddYears(-18);
        }
    }
}