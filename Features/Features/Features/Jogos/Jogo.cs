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

        public Jogo(Guid id, string nome, string descricao, string emailProdutora, bool ativo, decimal valor, int unidade, DateTime dataLancamento)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            EmailProdutora = emailProdutora;
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
                .Must(HaveMaxAge)
                .WithMessage("A data de lançamento não pode ser maior a 10 anos");

            RuleFor(c => c.Valor)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        public static bool HaveMaxAge(DateTime dataLancamento)
        {
            return dataLancamento <= DateTime.Now.AddYears(10);
        }
    }
}