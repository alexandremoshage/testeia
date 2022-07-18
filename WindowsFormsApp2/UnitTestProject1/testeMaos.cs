using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WindowsFormsApp2;
using System.Collections.Generic;
namespace UnitTestProject1
{
    [TestClass]
    public class TesteMaos
    {
        [TestMethod]
        public void SequenciaBordo()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();
            mesa.bordo.Add(new Carta(ECarta.Dois, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Tres, ENipe.Ouro));
            mesa.bordo.Add(new Carta(ECarta.Quatro, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Cinco, ENipe.Paus));
            mesa.bordo.Add(new Carta(ECarta.Seis, ENipe.Copas));

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Dama, ENipe.Paus), new Carta(ECarta.Dama, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Rei, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Valete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.sequencia && mesa.jogadores[1].Mao == Mao.sequencia && mesa.jogadores[2].Mao == Mao.sequencia);
        }

        [TestMethod]
        public void SequenciaBordoComAsInicio()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();
            mesa.bordo.Add(new Carta(ECarta.Dois, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Tres, ENipe.Ouro));
            mesa.bordo.Add(new Carta(ECarta.Quatro, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Cinco, ENipe.Paus));
            mesa.bordo.Add(new Carta(ECarta.As, ENipe.Copas));

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Dama, ENipe.Paus), new Carta(ECarta.Dama, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Rei, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Valete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.sequencia && mesa.jogadores[1].Mao == Mao.sequencia && mesa.jogadores[2].Mao == Mao.sequencia);
        }

        [TestMethod]
        public void SequenciaBordoComAsFim()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();
            mesa.bordo.Add(new Carta(ECarta.Dez, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Valete, ENipe.Ouro));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Rei, ENipe.Paus));
            mesa.bordo.Add(new Carta(ECarta.As, ENipe.Copas));

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Dama, ENipe.Paus), new Carta(ECarta.Dama, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Rei, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Valete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.sequencia && mesa.jogadores[1].Mao == Mao.sequencia && mesa.jogadores[2].Mao == Mao.sequencia);
        }

        [TestMethod]
        public void SequenciaJogador1()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();
            mesa.bordo.Add(new Carta(ECarta.Dez, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Valete, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Paus));
            mesa.bordo.Add(new Carta(ECarta.Rei, ENipe.Ouro));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Espadas));

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.As, ENipe.Paus), new Carta(ECarta.Dama, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Rei, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Valete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.sequencia && mesa.jogadores[1].Mao != Mao.sequencia && mesa.jogadores[2].Mao != Mao.sequencia);
        }

        [TestMethod]
        public void SequenciaJogadorFloop1()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();
            mesa.bordo.Add(new Carta(ECarta.Dez, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Valete, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Copas));

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.As, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Espadas) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Rei, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Valete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.sequencia && mesa.jogadores[1].Mao != Mao.sequencia && mesa.jogadores[2].Mao != Mao.sequencia);
        }

        [TestMethod]
        public void FushBordo()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();
            mesa.bordo.Add(new Carta(ECarta.Dois, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Quatro, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Cinco, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Seis, ENipe.Copas));

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Dama, ENipe.Paus), new Carta(ECarta.Dama, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Rei, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Valete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.flush && mesa.jogadores[1].Mao == Mao.flush && mesa.jogadores[2].Mao == Mao.flush);
        }

        [TestMethod]
        public void FlushJogador1()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();
            mesa.bordo.Add(new Carta(ECarta.Dez, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Valete, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Paus));
            mesa.bordo.Add(new Carta(ECarta.Rei, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Espadas));

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.As, ENipe.Espadas), new Carta(ECarta.Nove, ENipe.Espadas) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Rei, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Valete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.flush && mesa.jogadores[1].Mao != Mao.flush && mesa.jogadores[2].Mao != Mao.flush);
        }

        [TestMethod]
        public void TrincaJogador1()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();
            mesa.bordo.Add(new Carta(ECarta.Dez, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Valete, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Paus));
            mesa.bordo.Add(new Carta(ECarta.Rei, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Espadas));

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Dama, ENipe.Ouro), new Carta(ECarta.Oito, ENipe.Espadas) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Seis, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Sete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.trinca && mesa.jogadores[1].Mao != Mao.trinca && mesa.jogadores[2].Mao != Mao.trinca);
        }

        [TestMethod]
        public void QuadraJogador1()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();
            mesa.bordo.Add(new Carta(ECarta.Dez, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Valete, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Paus));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Espadas));

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Dama, ENipe.Ouro), new Carta(ECarta.Oito, ENipe.Espadas) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Seis, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Sete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.quadra && mesa.jogadores[1].Mao != Mao.quadra && mesa.jogadores[2].Mao != Mao.quadra);
        }



        [TestMethod]
        public void StragithflushJogador1()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();
            mesa.jogadores = new List<IJogador>();
            mesa.bordo.Add(new Carta(ECarta.Dez, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Valete, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Paus));
            mesa.bordo.Add(new Carta(ECarta.Rei, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Espadas));

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.As, ENipe.Espadas), new Carta(ECarta.Dez, ENipe.Espadas) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Rei, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Valete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.straigthFush && mesa.jogadores[1].Mao != Mao.straigthFush && mesa.jogadores[2].Mao != Mao.straigthFush);
        }


        [TestMethod]
        public void ParJogador1()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();
            mesa.bordo.Add(new Carta(ECarta.Dez, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Valete, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Paus));
            mesa.bordo.Add(new Carta(ECarta.Rei, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dois, ENipe.Espadas));

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Dama, ENipe.Ouro), new Carta(ECarta.Oito, ENipe.Espadas) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Seis, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Sete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.par);
        }

        [TestMethod]
        public void ParNaMaoJogador1()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();
            mesa.bordo.Add(new Carta(ECarta.Dez, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Valete, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Paus));
            mesa.bordo.Add(new Carta(ECarta.Rei, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dois, ENipe.Espadas));

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Oito, ENipe.Ouro), new Carta(ECarta.Oito, ENipe.Espadas) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Seis, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Sete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.par);
        }


        [TestMethod]
        public void ParNaMaoSemBordoJogador1()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Oito, ENipe.Ouro), new Carta(ECarta.Oito, ENipe.Espadas) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Seis, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Sete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.par);
        }


        [TestMethod]
        public void TrincaFloop()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();


            mesa.bordo.Add(new Carta(ECarta.Oito, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Valete, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Paus));

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Oito, ENipe.Ouro), new Carta(ECarta.Oito, ENipe.Espadas) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Seis, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Sete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.trinca);
        }


        [TestMethod]
        public void DoisParesJogador1()
        {
            var mesa = new Mesa();
            mesa.jogadores = new List<IJogador>();
            mesa.bordo.Add(new Carta(ECarta.Dez, ENipe.Copas));
            mesa.bordo.Add(new Carta(ECarta.Dez, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dama, ENipe.Paus));
            mesa.bordo.Add(new Carta(ECarta.Rei, ENipe.Espadas));
            mesa.bordo.Add(new Carta(ECarta.Dois, ENipe.Espadas));

            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Oito, ENipe.Ouro), new Carta(ECarta.Oito, ENipe.Espadas) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Seis, ENipe.Paus), new Carta(ECarta.Rei, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.jogadores.Add(new Jogador(new List<Carta>() { new Carta(ECarta.Sete, ENipe.Paus), new Carta(ECarta.Valete, ENipe.Ouro) }, mesa.jogadores.Count + 1));
            mesa.CalcularGanhador();

            Assert.AreEqual(true, mesa.jogadores[0].Mao == Mao.doisPares);
        }
    }
}
