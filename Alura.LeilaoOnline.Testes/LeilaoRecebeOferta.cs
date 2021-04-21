using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Alura.LeilaoOnline.Testes
{
    public class LeilaoRecebeOferta
    {
        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //arranjo - cenario
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Vanh Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();

            leilao.RecebeLance(fulano, 800);

            //act -  execução sob teste

            leilao.RecebeLance(fulano, 1000);

            leilao.TerminaPregao();

            //assert 
            var valorEsperado = 1;
            Assert.Equal(valorEsperado, leilao.Lances.Count());
        }

        [Theory]
        [InlineData(2, new double[] {800,900}) ]
        [InlineData(4, new double[] { 800, 900, 1000, 1500 })]
        [InlineData(3, new double[] { 800, 900, 500 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int valorEsperada, double[] ofertas)
        {
            //arranjo - cenario
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Vanh Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for(int i = 0; i < ofertas.Length; i++)
            {
                if (i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }
            
            leilao.TerminaPregao();

            //act -  execução sob teste
            leilao.RecebeLance(fulano, 1100);

            //assert 
            Assert.Equal(valorEsperada, leilao.Lances.Count());
        }
    }
}
