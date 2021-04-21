using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Testes
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[] {800,1150,1400,1250})]
        public void RetornaValorSuperiorMaisProximoDoLeilaoNessaModalidade(double valorDestino, double valorEsperado, double[] ofertas)
        {
            //arranjo - cenario
            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Vanh Gogh", modalidade);
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

        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            //arranjo - cenario
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Vanh Gogh", modalidade);
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
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Vanh Gogh", modalidade);

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
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Vanh Gogh", modalidade);
            leilao.IniciaPregao();

            //act -  execução sob teste
            leilao.TerminaPregao();

            //assert 
            var valorEsperado = 0;
            Assert.Equal(0, leilao.Ganhador.Valor);
        }
    }
}
