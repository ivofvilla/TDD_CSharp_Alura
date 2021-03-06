using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoAntesPegrao,
        LeilaoEmAndamento,
        LeilaoFinalizado
    }

    public class Leilao
    {
        private Interessada _ultimoCliente = null;
        private IList<Lance> _lances;
        private IModalidadeAvaliacao _avaliador { get; }
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }

        public Leilao(string peca, IModalidadeAvaliacao avaliacao)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesPegrao;
            _avaliador = avaliacao;
        }

        private bool NovoLanceAceito(Interessada cliente, double valor)
        {
            return Estado == EstadoLeilao.LeilaoEmAndamento &&
                   cliente != _ultimoCliente;
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
                throw new System.InvalidOperationException("Leilão não iniciado");

            Ganhador = _avaliador.Avalia(this);

            Estado = EstadoLeilao.LeilaoFinalizado;
        }
    }
}