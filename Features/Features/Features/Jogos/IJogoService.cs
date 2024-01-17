namespace Features.Jogos
{
    public interface IJogoService : IDisposable
    {
        IEnumerable<Jogo> ObterTodosAtivos();
        void Adicionar(Jogo jogo);
        void Atualizar(Jogo jogo);
        void Remover(Jogo jogo);
        void Inativar(Jogo jogo);
    }
}
