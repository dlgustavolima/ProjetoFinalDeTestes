using Features.Core;

namespace Features.Jogos
{
    public interface IJogoRepository : IRepository<Jogo> 
    {
        Jogo ObterPorNome(string nome);
    }
}
