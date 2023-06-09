using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WindowsFormsApp2;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class testeOpenIa
    {

        [TestMethod]
        public void TestFlushValido()
        {
            var cartas = new List<Carta>
            {
                new Carta(ECarta.Oito, ENipe.Copas),
                new Carta(ECarta.Seis, ENipe.Copas),
                new Carta(ECarta.Quatro, ENipe.Copas),
                new Carta(ECarta.As, ENipe.Copas),
                new Carta(ECarta.Valete, ENipe.Copas),
                new Carta(ECarta.Tres, ENipe.Ouro),
                new Carta(ECarta.Sete, ENipe.Copas)
            };

            var (ehFlush, cartasDaMao, nipe) = new CalculaMaos().VerificarFlushOpenIA(cartas.OrderBy(x => x.Numero));

            Assert.IsTrue(ehFlush);
            Assert.AreEqual(cartasDaMao.Count, 5);
            Assert.AreEqual(cartasDaMao[0].Numero, ECarta.As);
            Assert.AreEqual(cartasDaMao[1].Numero, ECarta.Valete);
            Assert.AreEqual(cartasDaMao[2].Numero, ECarta.Oito);
            Assert.AreEqual(cartasDaMao[3].Numero, ECarta.Sete);
            Assert.AreEqual(cartasDaMao[4].Numero, ECarta.Seis);
        }

        [TestMethod]
        public void TestFlushInvalido()
        {
            var cartas = new List<Carta>
            {
                new Carta(ECarta.Oito, ENipe.Copas),
                new Carta(ECarta.Seis, ENipe.Copas),
                new Carta(ECarta.Quatro, ENipe.Copas),
                new Carta(ECarta.As, ENipe.Ouro),
                new Carta(ECarta.Valete, ENipe.Ouro),
                new Carta(ECarta.Tres, ENipe.Ouro),
                new Carta(ECarta.Sete, ENipe.Copas)
            };

            var (ehFlush, cartasDaMao, nipe) = new CalculaMaos().VerificarFlushOpenIA(cartas.OrderBy(x => x.Numero));

            Assert.AreEqual(ehFlush, false);
        }

        [TestMethod]
        public void TestVerificarQuadra_True()
        {
            // Arrange
            var cartas = new List<Carta>
            {
                new Carta(ECarta.As, ENipe.Copas),
                new Carta(ECarta.As, ENipe.Ouro),
                new Carta(ECarta.As, ENipe.Paus),
                new Carta(ECarta.As, ENipe.Espadas),
                new Carta(ECarta.Cinco, ENipe.Copas),
                new Carta(ECarta.Seis, ENipe.Copas),
                new Carta(ECarta.Sete, ENipe.Copas)
            };


            // Act
            var resultado = new CalculaMaos().VerificarQuadraOpenIa(cartas.OrderBy(x => x.Numero)).Item1;

            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void TestVerificarQuadra_False()
        {
            // Arrange
            var cartas = new List<Carta>
            {
                new Carta(ECarta.As, ENipe.Copas),
                new Carta(ECarta.Dois, ENipe.Copas),
                new Carta(ECarta.Tres, ENipe.Copas),
                new Carta(ECarta.Quatro, ENipe.Copas),
                new Carta(ECarta.Cinco, ENipe.Copas),
                new Carta(ECarta.Seis, ENipe.Copas),
                new Carta(ECarta.Sete, ENipe.Copas)
            };

            var resultado = new CalculaMaos().VerificarQuadraOpenIa(cartas.OrderBy(x => x.Numero)).Item1;

            Assert.AreEqual(false, resultado);
        }

        [TestMethod]
        public void TestVerificarKicker()
        {
            // Arrange
            var cartas = new List<Carta>
            {
                new Carta(ECarta.As, ENipe.Copas),
                new Carta(ECarta.As, ENipe.Ouro),
                new Carta(ECarta.As, ENipe.Paus),
                new Carta(ECarta.As, ENipe.Espadas),
                new Carta(ECarta.Cinco, ENipe.Copas),
                new Carta(ECarta.Seis, ENipe.Copas),
                new Carta(ECarta.Sete, ENipe.Copas)
            };

            var mao = new CalculaMaos().VerificarQuadraOpenIa(cartas.OrderBy(x => x.Numero)).Item2;

            Assert.AreEqual(ECarta.Sete, mao[4].Numero);
        }

        [TestMethod]
        public void TestVerificarKickerAs()
        {
            // Arrange
            var cartas = new List<Carta>
            {
                new Carta(ECarta.Valete, ENipe.Copas),
                new Carta(ECarta.Valete, ENipe.Ouro),
                new Carta(ECarta.Valete, ENipe.Paus),
                new Carta(ECarta.Valete, ENipe.Espadas),
                new Carta(ECarta.As, ENipe.Copas),
                new Carta(ECarta.Seis, ENipe.Copas),
                new Carta(ECarta.Sete, ENipe.Copas)
            };

            var mao = new CalculaMaos().VerificarQuadraOpenIa(cartas.OrderBy(x => x.Numero)).Item2;

            Assert.AreEqual(ECarta.As, mao[4].Numero);
        }

        [TestMethod]
        public void VerificarStraightFlush_DeveRetornarTrueParaStraightFlush()
        {
            var baralho = new Baralho();
            var cartas = new List<Carta>()
            {
                new Carta(ECarta.As, ENipe.Ouro),
                new Carta(ECarta.Dois, ENipe.Ouro),
                new Carta(ECarta.Tres, ENipe.Ouro),
                new Carta(ECarta.Quatro, ENipe.Ouro),
                new Carta(ECarta.Cinco, ENipe.Ouro),
                new Carta(ECarta.Seis, ENipe.Ouro),
                new Carta(ECarta.Sete, ENipe.Ouro)
            };

            var (resultado, cartasStraightFlush) = new CalculaMaos().VerificarStraightFlush(cartas.OrderBy(x => x.Numero), ENipe.Ouro);

            Assert.IsTrue(resultado);
            Assert.AreEqual(5, cartasStraightFlush.Count);
            Assert.IsTrue(cartasStraightFlush[0].Numero == ECarta.Tres && cartasStraightFlush[0].Nipe == ENipe.Ouro);
            Assert.IsTrue(cartasStraightFlush[1].Numero == ECarta.Quatro && cartasStraightFlush[1].Nipe == ENipe.Ouro);
            Assert.IsTrue(cartasStraightFlush[2].Numero == ECarta.Cinco && cartasStraightFlush[2].Nipe == ENipe.Ouro);
            Assert.IsTrue(cartasStraightFlush[3].Numero == ECarta.Seis && cartasStraightFlush[3].Nipe == ENipe.Ouro);
            Assert.IsTrue(cartasStraightFlush[4].Numero == ECarta.Sete && cartasStraightFlush[4].Nipe == ENipe.Ouro);

        }

        [TestMethod]
        public void VerificarStraightFlush_DeveRetornarFalseParaSequenciaDeCincoCartas()
        {
            var baralho = new Baralho();
            var cartas = new List<Carta>()
        {
            new Carta(ECarta.As, ENipe.Ouro),
            new Carta(ECarta.Dois, ENipe.Ouro),
            new Carta(ECarta.Tres, ENipe.Ouro),
            new Carta(ECarta.Quatro, ENipe.Paus),
            new Carta(ECarta.Cinco, ENipe.Paus),
            new Carta(ECarta.Seis, ENipe.Ouro),
            new Carta(ECarta.Sete, ENipe.Ouro)
        };

            bool resultado = new CalculaMaos().VerificarStraightFlush(cartas.OrderBy(x => x.Numero), ENipe.Ouro).Item1;

            Assert.IsFalse(resultado);
        }



        [TestMethod]
        public void TestVerificarTrica_True()
        {
            // Arrange
            var cartas = new List<Carta>
            {
                new Carta(ECarta.As, ENipe.Copas),
                new Carta(ECarta.As, ENipe.Ouro),
                new Carta(ECarta.As, ENipe.Paus),
                new Carta(ECarta.Seis, ENipe.Copas),
                new Carta(ECarta.Sete, ENipe.Ouro),
                new Carta(ECarta.Oito, ENipe.Paus),
                new Carta(ECarta.Rei, ENipe.Paus),
            };

            // Act
            var resultado = new CalculaMaos().VerificarTrinca(cartas.OrderBy(x => x.Numero));

            // Assert
            Assert.IsTrue(resultado.Item1);
        }

    }
}
