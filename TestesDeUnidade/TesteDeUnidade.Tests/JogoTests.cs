using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestesDeUnidade.Tests
{
    public class JogoTests
    {
        [Fact]
        public void Jogo_CalcularValorUnidade_SomaValorxUnidade()
        {
            //Arrange
            var jogo = new Jogo();

            //Act
            var resultado = jogo.CalcularValorUnidade(350, 1);

            //Assert
            Assert.Equal(350, resultado);
        }

        [Theory]
        [InlineData(350, 1, 350)]
        [InlineData(350, 2, 700)]
        public void Jogo_CalcularValorUnidade_SomaValorxUnidadeCorreto(decimal valor, int unidade, decimal total)
        {
            //Arrange
            var jogo = new Jogo();

            //Act
            var resultado = jogo.CalcularValorUnidade(valor, unidade);

            //Assert
            Assert.Equal(total, resultado);
        }

        [Fact]
        public void Jogo_ValidarNome_DeveIgnorarCase()
        {
            //Arrange
            var jogo = new Jogo("Elden Ring", true, 350, 1, 10000000);

            //Act
            var resultado = jogo.Nome;

            //Assert
            Assert.Equal("ELDEN RING", resultado, true);
        }

        [Fact]
        public void Jogo_ValidarNome_DeveConterTrecho()
        {
            //Arrange
            var jogo = new Jogo("Elden Ring", true, 350, 1, 10000000);

            //Act
            var resultado = jogo.Nome;

            //Assert
            Assert.Contains("Elden", resultado);
        }

        [Fact]
        public void Jogo_ValidarNome_DeveComecarCom()
        {
            //Arrange
            var jogo = new Jogo("Elden Ring", true, 350, 1, 10000000);

            //Act
            var resultado = jogo.Nome;

            //Assert
            Assert.StartsWith("Eld", resultado);
        }

        [Fact]
        public void Jogo_ValidarNome_DeveAcabarCom()
        {
            //Arrange
            var jogo = new Jogo("Elden Ring", true, 350, 1, 10000000);

            //Act
            var resultado = jogo.Nome;

            //Assert
            Assert.EndsWith("ing", resultado);
        }

        [Fact]
        public void Jogo_ValidarNome_ValidarExpressaoRegular()
        {
            //Arrange
            var jogo = new Jogo("Elden Ring", true, 350, 1, 10000000);

            //Act
            var resultado = jogo.Nome;

            //Assert
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", resultado);
        }

        [Fact]
        public void Jogo_ValidarNome_NaoDeveSerNull()
        {
            //Arrange
            var jogo = new Jogo("Elden Ring", true, 350, 1, 10000000);

            //Act
            var resultado = jogo.Nome;

            //Assert
            Assert.False(string.IsNullOrEmpty(resultado));
        }

        [Fact]
        public void Jogo_ValidarNome_DeveSerNull()
        {
            //Arrange
            var jogo = new Jogo(null, true, 350, 1, 10000000);

            //Act
            var resultado = jogo.Nome ?? "";

            //Assert
            Assert.True(string.IsNullOrEmpty(resultado));
            Assert.False(resultado.Length > 0);
        }

        [Theory]
        [InlineData(9998)]
        [InlineData(100000)]
        [InlineData(10000000)]
        public void Jogo_ValidarNivelProducao_ValorTotalProducaoDeveRespeitarNivelProducao(decimal valorTotalProducao)
        {
            //Arrange & Act
            var jogo = new Jogo(null, true, 350, 1, valorTotalProducao);

            //Assert

            if (jogo.NivelProducao == NivelProducao.Indie)
                Assert.InRange(jogo.GastoTotalProducao, 0, 9999);

            if (jogo.NivelProducao == NivelProducao.Medio)
                Assert.InRange(jogo.GastoTotalProducao, 10000, 1000000);

            if (jogo.NivelProducao == NivelProducao.Alto)
                Assert.InRange(jogo.GastoTotalProducao, 1000000, decimal.MaxValue);
        }

        [Fact]
        public void Jogo_ValidarClasse_DeveRetornarTipoJogo()
        {
            //Arrange & Act
            var jogo = new Jogo();

            //Assert
            Assert.IsType<Jogo>(jogo);
        }

        [Fact]
        public void Jogo_Plataformas_DevePossuirPlataformasVazias()
        {
            //Arrange & Act
            var jogo = new Jogo();

            //Assert
            Assert.All(jogo.Plataformas, plataforma => Assert.False(string.IsNullOrWhiteSpace(plataforma)));
        }

        [Fact]
        public void Jogo_Plataformas_NaoDevePossuirPlataformasVazias()
        {
            //Arrange & Act
            var jogo = new Jogo("Elden Ring", true, 350, 1, 1000000);

            //Assert
            Assert.All(jogo.Plataformas, plataforma => Assert.False(string.IsNullOrEmpty(plataforma)));
        }

        [Fact]
        public void Jogo_Plataformas_DevePossuirPlataformaPC()
        {
            //Arrange & Act
            var jogo = new Jogo("Elden Ring", true, 350, 1, 1000000);

            //Assert
            Assert.Contains("PC", jogo.Plataformas);
        }

        [Fact]
        public void Jogo_Plataformas_NãoDevePossuirPlataformaAndroid()
        {
            //Arrange & Act
            var jogo = new Jogo("Elden Ring", true, 350, 1, 1000000);

            //Assert
            Assert.DoesNotContain("Android", jogo.Plataformas);
        }


        [Fact]
        public void Jogo_Exceptions_DeveRetornarErroDeValorGastoProducao()
        {
            //Arrange & Act & Assert
            var exception = Assert.Throws<Exception>(() => new Jogo("Elden Ring", true, 350, 1, -1));
            Assert.Equal("Valor do jogo não pode ser menor que 0", exception.Message);
        }
    }
}
