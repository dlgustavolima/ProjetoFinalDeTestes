using MediatR;

namespace Features.Jogos
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IMediator _mediator;

        public JogoService(IJogoRepository jogoRepository,
                           IMediator mediator)
        {
            _jogoRepository = jogoRepository;
            _mediator = mediator;
        }

        public IEnumerable<Jogo> ObterTodosAtivos()
        {
            return _jogoRepository.ObterTodos().Where(c => c.Ativo);
        }

        public void Adicionar(Jogo jogo)
        {
            if (!jogo.EhValido())
                return;

            _jogoRepository.Adicionar(jogo);
            _mediator.Publish(new JogoMailNotification("admin@me.com", jogo.EmailProdutora, "Novo Jogo", "Jogo criado com sucesso!"));
        }

        public void Atualizar(Jogo jogo)
        {
            if (!jogo.EhValido())
                return;

            _jogoRepository.Atualizar(jogo);
            _mediator.Publish(new JogoMailNotification("admin@me.com", jogo.EmailProdutora, "Mudanças", "Jogo atualizado com sucesso!"));
        }

        public void Inativar(Jogo jogo)
        {
            if (!jogo.EhValido())
                return;

            jogo.Inativar();
            _jogoRepository.Atualizar(jogo);
            _mediator.Publish(new JogoMailNotification("admin@me.com", jogo.EmailProdutora, "Até breve", "Jogo inativado com sucesso!"));
        }

        public void Remover(Jogo jogo)
        {
            _jogoRepository.Remover(jogo.Id);
            _mediator.Publish(new JogoMailNotification("admin@me.com", jogo.EmailProdutora, "Adeus", "Jogo apagado com sucesso!"));
        }

        public void Dispose()
        {
            _jogoRepository.Dispose();
        }
    }
}
