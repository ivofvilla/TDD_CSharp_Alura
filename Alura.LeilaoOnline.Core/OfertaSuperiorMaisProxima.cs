﻿using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
    {
        public double ValorDestino{ get; }

        public OfertaSuperiorMaisProxima(double valorDestino)
        {
            this.ValorDestino = valorDestino;
        }

        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances.DefaultIfEmpty(new Lance(null, 0))
                                 .Where(w => w.Valor > ValorDestino)
                                 .OrderBy(o => o.Valor)
                                 .FirstOrDefault();
        }
    }
}
