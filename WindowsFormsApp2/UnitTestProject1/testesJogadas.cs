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
    }
}
