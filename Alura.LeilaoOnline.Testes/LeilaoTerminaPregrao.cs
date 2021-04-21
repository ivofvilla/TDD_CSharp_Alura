using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Testes
{
    public class LeilaoTerminaPregrao
    {
        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            //arranjo - cenario
            var leilao = new Leilao("Vanh Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
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

            //act -  execução sob teste
            leilao.TerminaPregao();

            //assert 
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //arranjo - cenario
            var leilao = new Leilao("Vanh Gogh");

            //assert
            Assert.Throws<InvalidOperationException>(
                () =>
                //Act - metodo sob teste
                leilao.TerminaPregao()
            );
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //arranjo - cenario
            var leilao = new Leilao("Vanh Gogh");
            leilao.IniciaPregao();

            //act -  execução sob teste
            leilao.TerminaPregao();

            //assert 
            var valorEsperado = 0;
            Assert.Equal(0, leilao.Ganhador.Valor);
        }
    }
}
