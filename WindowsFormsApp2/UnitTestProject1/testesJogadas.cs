using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WindowsFormsApp2;
using System.Collections.Generic;
using Moq;

namespace UnitTestProject1
{
    [TestClass]
    public class testesJogadas
    {
        [TestMethod]
        public void SequenciaBordo()
        {
            var mesa = new Mesa();
            Mock<IJogador> mockMock = new Mock<IJogador>();
            mockMock.SetupAllProperties() ;
            mockMock.Object.Posicao = EPosicao.botao;
            mockMock.Object.CartasMao = new List<Carta>() { new Carta(ECarta.Dama, ENipe.Paus), new Carta(ECarta.Dama, ENipe.Ouro) };
            mockMock.Object.Id = 1;
            mockMock.Setup(x => x.Acao).Returns(EAcao.call);

            IJogador jogador2 = new Jogador(new List<Carta>() { new Carta(ECarta.Dama, ENipe.Paus), new Carta(ECarta.Dama, ENipe.Ouro) }, mesa.jogadores.Count + 1) {  Posicao = EPosicao.small};
            IJogador jogador3 = new Jogador(new List<Carta>() { new Carta(ECarta.Dama, ENipe.Paus), new Carta(ECarta.Dama, ENipe.Ouro) }, mesa.jogadores.Count + 1) { Posicao = EPosicao.big };


            mesa.jogadores.Add(mockMock.Object);
            mesa.jogadores.Add(jogador2);
            mesa.jogadores.Add(jogador3);
            mesa.ProximaAcao();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.sequencia && mesa.jogadores[1].Mao == Mao.sequencia && mesa.jogadores[2].Mao == Mao.sequencia);
        }
    }
}
