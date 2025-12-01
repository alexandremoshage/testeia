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
            mesa.jogadores = new List<IJogador>();
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Dama, ENipe.Paus), new Carta(ECarta.Dama, ENipe.Ouro) }, mesa.jogadores.Count + 1) { Posicao = EPosicao.small });
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Dama, ENipe.Paus), new Carta(ECarta.Dama, ENipe.Ouro) }, mesa.jogadores.Count + 1) { Posicao = EPosicao.big });
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Dama, ENipe.Paus), new Carta(ECarta.Dama, ENipe.Ouro) }, mesa.jogadores.Count + 1) { Posicao = EPosicao.botao });
            mesa.jogadores[2].SetarAcaoJogador(EAcao.raise, 2);
            mesa.jogadores[1].SetarAcaoJogador(EAcao.fold, 0);
            mesa.jogadores[0].SetarAcaoJogador(EAcao.fold, 0);

        }

        [TestMethod]
        public void ExecutarAcoesJogadores_DeveRecolocarJogadoresNaFilaAposRaise()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.As, ENipe.Paus), new Carta(ECarta.Dois, ENipe.Ouro) }, mesa.jogadores.Count + 1) { Posicao = EPosicao.small });
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Tres, ENipe.Paus), new Carta(ECarta.Quatro, ENipe.Ouro) }, mesa.jogadores.Count + 1) { Posicao = EPosicao.big });
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Cinco, ENipe.Paus), new Carta(ECarta.Seis, ENipe.Ouro) }, mesa.jogadores.Count + 1) { Posicao = EPosicao.botao });

            mesa.jogadores[0].SetarAcaoJogador(EAcao.call, 1);
            mesa.jogadores[1].SetarAcaoJogador(EAcao.call, 1);
            mesa.jogadores[2].SetarAcaoJogador(EAcao.raise, 3);

            mesa.ExecutarAcoesJogadores(ERodada.PreFlop);

            Assert.AreEqual(mesa.jogadorUltimaAcao, mesa.jogadores[1]);
            Assert.AreEqual(mesa.JogadorUltimoRaise, mesa.jogadores[2]);
        }

        [TestMethod]
        public void ExecutarAcoesJogadores_PreFlop_DeveComecarPeloUtgETerminarNoBigBlind()
        {
            var ordemAcoes = new List<string>();

            var jogadorSmall = new Mock<IJogador>();
            jogadorSmall.SetupAllProperties();
            jogadorSmall.Object.Posicao = EPosicao.small;
            jogadorSmall.Object.IsNaMao = true;
            jogadorSmall.Object.Fichas = 10;
            jogadorSmall.Setup(j => j.AcaoJogador()).Returns(() =>
            {
                ordemAcoes.Add("small");
                return (EAcao.check, 0);
            });

            var jogadorBig = new Mock<IJogador>();
            jogadorBig.SetupAllProperties();
            jogadorBig.Object.Posicao = EPosicao.big;
            jogadorBig.Object.IsNaMao = true;
            jogadorBig.Object.Fichas = 10;
            jogadorBig.Setup(j => j.AcaoJogador()).Returns(() =>
            {
                ordemAcoes.Add("big");
                return (EAcao.check, 0);
            });

            var jogadorBotao = new Mock<IJogador>();
            jogadorBotao.SetupAllProperties();
            jogadorBotao.Object.Posicao = EPosicao.botao;
            jogadorBotao.Object.IsNaMao = true;
            jogadorBotao.Object.Fichas = 10;
            jogadorBotao.Setup(j => j.AcaoJogador()).Returns(() =>
            {
                ordemAcoes.Add("botao");
                return (EAcao.check, 0);
            });

            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador> { jogadorSmall.Object, jogadorBig.Object, jogadorBotao.Object };

            mesa.ExecutarAcoesJogadores(ERodada.PreFlop);

            CollectionAssert.AreEqual(new List<string> { "botao", "small", "big" }, ordemAcoes);
        }

        [TestMethod]
        public void ExecutarAcoesJogadores_PosFlop_DeveComecarNoBigBlind()
        {
            var ordemAcoes = new List<string>();

            var jogadorSmall = new Mock<IJogador>();
            jogadorSmall.SetupAllProperties();
            jogadorSmall.Object.Posicao = EPosicao.small;
            jogadorSmall.Object.IsNaMao = true;
            jogadorSmall.Object.Fichas = 10;
            jogadorSmall.Setup(j => j.AcaoJogador()).Returns(() =>
            {
                ordemAcoes.Add("small");
                return (EAcao.check, 0);
            });

            var jogadorBig = new Mock<IJogador>();
            jogadorBig.SetupAllProperties();
            jogadorBig.Object.Posicao = EPosicao.big;
            jogadorBig.Object.IsNaMao = true;
            jogadorBig.Object.Fichas = 10;
            jogadorBig.Setup(j => j.AcaoJogador()).Returns(() =>
            {
                ordemAcoes.Add("big");
                return (EAcao.check, 0);
            });

            var jogadorBotao = new Mock<IJogador>();
            jogadorBotao.SetupAllProperties();
            jogadorBotao.Object.Posicao = EPosicao.botao;
            jogadorBotao.Object.IsNaMao = true;
            jogadorBotao.Object.Fichas = 10;
            jogadorBotao.Setup(j => j.AcaoJogador()).Returns(() =>
            {
                ordemAcoes.Add("botao");
                return (EAcao.check, 0);
            });

            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador> { jogadorSmall.Object, jogadorBig.Object, jogadorBotao.Object };

            mesa.ExecutarAcoesJogadores(ERodada.Flop);

            CollectionAssert.AreEqual(new List<string> { "big", "botao", "small" }, ordemAcoes);
        }
    }
}
