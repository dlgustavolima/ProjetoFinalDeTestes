namespace TestesDeUnidade
{
    public class Jogo
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public decimal Valor { get; set; }

        public int Unidade { get; set; }

        public decimal GastoTotalProducao { get; set; }

        public NivelProducao NivelProducao { get; set; }

        public List<string> Plataformas { get; set; } = new List<string>();

        public Jogo()
        {

        }

        public Jogo(string nome, bool ativo, decimal valor, int unidade, decimal gastoTotalProducao)
        {
            Nome = nome;
            Ativo = ativo;
            Valor = valor;
            Unidade = unidade;
            GastoTotalProducao = gastoTotalProducao;

            DefinirGastoProducao(gastoTotalProducao);
            AtribuirTodasPlataformas();
        }

        public decimal CalcularValorUnidade(decimal valor, int unidade)
        {
            return valor * unidade;
        }

        public void AtribuirNomeJogo(string nome)
        {
            Nome = nome;
        }

        public void DefinirGastoProducao(decimal gastoProducao)
        {
            if (gastoProducao < 0) throw new Exception("Valor do jogo não pode ser menor que 0");

            if (gastoProducao < 10000) NivelProducao = NivelProducao.Indie;
            if (gastoProducao >= 10000 && gastoProducao <= 1000000) NivelProducao = NivelProducao.Medio;
            if (gastoProducao > 1000000) NivelProducao = NivelProducao.Alto;
        }

        public void AtribuirTodasPlataformas()
        {
            var plataformas = new string[] { "Playstation", "Xbox", "PC", "Switch" };
            Plataformas.AddRange(plataformas);
        }
    }

    public enum NivelProducao
    {
        Indie,
        Medio,
        Alto
    }
}
