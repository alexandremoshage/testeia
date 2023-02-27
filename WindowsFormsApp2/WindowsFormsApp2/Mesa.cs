using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public class Mesa
    {
        private CalculaMaos calculaMaos = new CalculaMaos();
        private Baralho baralho = new Baralho();
        public List<IJogador> jogadores = new List<IJogador>();
        public List<Carta> bordo = new List<Carta>();

        public decimal pote = 0;
        public IJogador JogadorUltimoRaise;
        public decimal apostaCorrente = 1;
        public IJogador jogadorUltimaAcao;
        private List<IJogador> filaAcaoJogadores;

        public Mesa()
        {
            jogadores.Add(new Jogador(new List<Carta> { baralho.EntregarCarta(), baralho.EntregarCarta() }, 1));
            jogadores.Add(new Jogador(new List<Carta> { baralho.EntregarCarta(), baralho.EntregarCarta() }, 2));
            jogadores.Add(new Jogador(new List<Carta> { baralho.EntregarCarta(), baralho.EntregarCarta() }, 3));
        }

        public void AcionarJogadores()
        {
            while (filaAcaoJogadores.Count > 0)
            {
                ProximaAcao();
            }
        }
        public void ProximaAcao()
        {
            var jogadoresNaMao = jogadores.Where(x => x.IsNaMao).ToList();
            var smal = jogadoresNaMao.Where(x => x.Posicao == EPosicao.small).FirstOrDefault();
            var big = jogadoresNaMao.Where(x => x.Posicao == EPosicao.big).FirstOrDefault();
            var botao = jogadoresNaMao.Where(x => x.Posicao == EPosicao.botao).FirstOrDefault();

            var jogadorNaAcao = ObterJogadorProximaAcao(jogadoresNaMao);
            var (acao, valor) = jogadorNaAcao.AcaoJogador();
            if (acao == EAcao.raise)
            {
                JogadorUltimoRaise = jogadorNaAcao;
                AtualizarFila(smal, big, botao, jogadorNaAcao);
            }

            if (acao == EAcao.fold)
            {
                jogadorNaAcao.IsNaMao = false;
            }

            if (acao == EAcao.check)
            {
                //por equanto não faz nada
            }


            if (acao == EAcao.call)
            {
                //por equanto não faz nada
            }
        }

        private void AtualizarFila(IJogador smal, IJogador big, IJogador botao, IJogador jogadorNaAcao)
        {
            if (jogadorNaAcao.Posicao == EPosicao.botao)
            {
                if (smal != null)
                    filaAcaoJogadores.Add(smal);

                if (big != null)
                    filaAcaoJogadores.Add(big);
            }

            if (jogadorNaAcao.Posicao == EPosicao.small)
            {
                if (big != null)
                    filaAcaoJogadores.Add(big);

                if (botao != null)
                    filaAcaoJogadores.Add(botao);
            }

            if (jogadorNaAcao.Posicao == EPosicao.big)
            {
                if (botao != null)
                    filaAcaoJogadores.Add(botao);

                if (smal != null)
                    filaAcaoJogadores.Add(smal);
            }
        }

        public IJogador ObterJogadorProximaAcao(List<IJogador> p_jogadoresNaMao)
        {

            if (filaAcaoJogadores == null)
            {
                filaAcaoJogadores = new List<IJogador>();
                filaAcaoJogadores.Add(p_jogadoresNaMao.Where(x => x.Posicao == EPosicao.small).FirstOrDefault());
                filaAcaoJogadores.Add(p_jogadoresNaMao.Where(x => x.Posicao == EPosicao.big).FirstOrDefault());
                return p_jogadoresNaMao.Where(x => x.Posicao == EPosicao.botao).FirstOrDefault();
            }
            else
            {
                var proximoJogador = filaAcaoJogadores.FirstOrDefault();
                filaAcaoJogadores.RemoveAt(0);
                return proximoJogador;
            }
        }

        public void SortearBotao()
        {

            int botao = 1;
            if (botao == 1)
            {
                jogadores[0].Posicao = EPosicao.botao;
                jogadores[1].Posicao = EPosicao.small;
                jogadores[2].Posicao = EPosicao.big;
            }

            if (botao == 2)
            {
                jogadores[0].Posicao = EPosicao.big;
                jogadores[1].Posicao = EPosicao.botao;
                jogadores[2].Posicao = EPosicao.small;
            }

            if (botao == 3)
            {
                jogadores[0].Posicao = EPosicao.small;
                jogadores[1].Posicao = EPosicao.big;
                jogadores[2].Posicao = EPosicao.botao;
            }
        }

        public void DistribuirFloop()
        {
            bordo.Add(baralho.EntregarCarta());
            bordo.Add(baralho.EntregarCarta());
            bordo.Add(baralho.EntregarCarta());
        }

        public void DistribuirTurn()
        {
            bordo.Add(baralho.EntregarCarta());
        }

        public void DistribuirRiver()
        {
            bordo.Add(baralho.EntregarCarta());
        }

        public void CalcularGanhador()
        {


            List<IJogador> jogadoresNaMao = jogadores.Where(x => x.IsNaMao).ToList();


            foreach (var jogadorNaMao in jogadoresNaMao)
            {
                List<Carta> cartasJogador = new List<Carta>();
                cartasJogador.AddRange(bordo);
                cartasJogador.AddRange(jogadorNaMao.CartasMao);

                var (isFlush, mao, nipe) = calculaMaos.VerificarFlushOpenIA(cartasJogador);
                var (isSequencia, sequencia) = calculaMaos.VerificarSequencia(cartasJogador);

                if (isSequencia && isFlush && calculaMaos.VerificarStraightFlush(cartasJogador, nipe.Value).Item1)
                {
                    jogadorNaMao.Mao = Mao.straigthFush;
                    continue;
                }

                if (calculaMaos.VerificarQuadraOpenIa(cartasJogador).Item1)
                {
                    jogadorNaMao.Mao = Mao.quadra;
                    continue;
                }

                if (isFlush)
                {
                    jogadorNaMao.Mao = Mao.flush;
                    continue;
                }

                if (isSequencia)
                {
                    jogadorNaMao.Mao = Mao.sequencia;
                    continue;
                }


                if (calculaMaos.VerificarTrinca(cartasJogador))
                {
                    jogadorNaMao.Mao = Mao.trinca;
                    continue;
                }

                if (calculaMaos.VerificarDoisPares(cartasJogador))
                {
                    jogadorNaMao.Mao = Mao.doisPares;
                    continue;
                }

                if (calculaMaos.VerificarPar(cartasJogador))
                {
                    jogadorNaMao.Mao = Mao.par;
                    continue;
                }
            }

        }



    }
}
