using Alura.LeilaoOnline.Core;
using System.Linq;
using Xunit;


namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //Arranje
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);

            //Act
            leilao.RecebeLance(fulano, 1000);


            //Assert
            var qtdeEsperado = 1;
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperado, qtdeObtida);
        }

        [Theory]
        [InlineData(4, new double[] {100,1100,1400,1300 })]
        [InlineData(2, new double[] {800,900 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(double qtdeEsperado, double[] ofertas)
        {
            //Arranje
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();
            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i%2)==0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }
           
            leilao.TerminaPregao();

            //Act
            leilao.RecebeLance(fulano, 1000);


            //Assert
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperado, qtdeObtida);
        }

        [Theory]
        [InlineData(1200, new double[] {800,900,1000,1200})]
        [InlineData(1000, new double[] {800,900,1000,900})]
        [InlineData(800, new double[] {800})]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            //Arranje
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();
            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadopregaoNaoIniciado()
        {
            //Arranje
            var leilao = new Leilao("Van Gogh");

            //Assert
            Assert.Throws<System.InvalidOperationException>(
               //Act - Metodo Sob Teste
               () => leilao.TerminaPregao());

        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances() 
        {
            //Arranje
            var leilao = new Leilao("Van Gogh");
            leilao.IniciaPregao();

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }



    }
}
