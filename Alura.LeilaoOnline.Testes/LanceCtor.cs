using System;
using Xunit;
using Alura.LeilaoOnline.Core;

namespace Alura.LeilaoOnline.Testes
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            //arranje
            var valorNegativo = -100;

            //assert
            Assert.Throws<ArgumentException>(
                //Act
                () => new Lance(null, valorNegativo)
            );
        }
    }
}
