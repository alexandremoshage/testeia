using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class Mesa
    {
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

                var (isFlush, nipe) = VerificarFlush(cartasJogador);
                var (isSequencia, sequencia) = VerificarSequencia(cartasJogador);

                if (isSequencia && isFlush && VerificarStragithFlush(cartasJogador, nipe.Value))
                {
                    jogadorNaMao.Mao = Mao.straigthFush;
                    continue;
                }

                if (VerificarQuadra(cartasJogador))
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


                if (VerificarTrinca(cartasJogador))
                {
                    jogadorNaMao.Mao = Mao.trinca;
                    continue;
                }

                if (VerificarDoisPares(cartasJogador))
                {
                    jogadorNaMao.Mao = Mao.doisPares;
                    continue;
                }

                if (VerificarPar(cartasJogador))
                {
                    jogadorNaMao.Mao = Mao.par;
                    continue;
                }
            }

        }

        #region resultadoMaos

        private (bool, int[]) VerificarSequencia(List<Carta> cartas)
        {
            List<int> cartasOrdenadas = cartas.Select(x => (int)x.Numero).OrderBy(x => x).Distinct().ToList();
            List<int> cartasOrdenadasComAsNoFim = new List<int>();
            if (cartasOrdenadas.Count() < 5)
                return (false, null);

            var temAs = cartasOrdenadas.Where(x => x == 0).Any();
            if (temAs)
            {
                cartasOrdenadasComAsNoFim = cartas.Select(x => (int)x.Numero).OrderBy(x => x).Distinct().ToList();
                for (int i = 0; i < cartasOrdenadas.Count() - 4; i++)
                {
                    if (cartasOrdenadas[i] == 0)
                    {
                        cartasOrdenadas[i] = 1;
                        cartasOrdenadasComAsNoFim[i] = 14;
                    }
                }
            }

            for (int i = 0; i < cartasOrdenadas.Count() - 4; i++)
            {
                if (IsSequential(new int[] { cartasOrdenadas[i], cartasOrdenadas[i + 1], cartasOrdenadas[i + 2], cartasOrdenadas[i + 3], cartasOrdenadas[i + 4] }))
                    return (true, new int[] { cartasOrdenadas[i], cartasOrdenadas[i + 1], cartasOrdenadas[i + 2], cartasOrdenadas[i + 3], cartasOrdenadas[i + 4] });

                if (temAs)
                {
                    cartasOrdenadasComAsNoFim = cartasOrdenadasComAsNoFim.OrderBy(x => x).ToList();
                    if (IsSequential(new int[] { cartasOrdenadasComAsNoFim[i], cartasOrdenadasComAsNoFim[i + 1], cartasOrdenadasComAsNoFim[i + 2], cartasOrdenadasComAsNoFim[i + 3], cartasOrdenadasComAsNoFim[i + 4] }))
                        return (true, new int[] { cartasOrdenadasComAsNoFim[i], cartasOrdenadasComAsNoFim[i + 1], cartasOrdenadasComAsNoFim[i + 2], cartasOrdenadasComAsNoFim[i + 3], cartasOrdenadasComAsNoFim[i + 4] });
                }
            }

            return (false, null);
        }

        private (bool, ENipe?) VerificarFlush(List<Carta> cartas)
        {
            if (cartas.Count() < 5)
                return (false, null);

            List<Carta> cartasOrd = cartas.OrderBy(x => x.Nipe).ToList();

            for (int i = 0; i < cartas.Count() - 4; i++)
            {
                if (cartasOrd[i].Nipe == cartasOrd[i + 1].Nipe && cartasOrd[i].Nipe == cartasOrd[i + 2].Nipe && cartasOrd[i].Nipe == cartasOrd[i + 3].Nipe && cartasOrd[i].Nipe == cartasOrd[i + 4].Nipe)
                    return (true, cartasOrd[i].Nipe);
            }


            return (false, null);
        }

        private bool VerificarTrinca(List<Carta> cartas)
        {
            if (cartas.Count() < 3)
                return false;

            List<Carta> cartasOrd = cartas.OrderBy(x => x.Numero).ToList();

            for (int i = 0; i < cartas.Count() - 2; i++)
            {
                if (cartasOrd[i].Numero == cartasOrd[i + 1].Numero && cartasOrd[i].Numero == cartasOrd[i + 2].Numero)
                    return true;
            }


            return false;
        }

        private bool VerificarDoisPares(List<Carta> cartas)
        {
            if (cartas.Count() < 2)
                return false;

            List<Carta> cartasOrd = cartas.OrderBy(x => x.Numero).ToList();

            for (int i = 0; i < cartas.Count() - 1; i++)
            {
                if (cartasOrd[i].Numero == cartasOrd[i + 1].Numero)
                {
                    for (int j = i + 2; j < cartas.Count() - 1; j++)
                    {
                        if (cartasOrd[j].Numero == cartasOrd[j + 1].Numero)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }


            return false;
        }

        private bool VerificarPar(List<Carta> cartas)
        {
            if (cartas.Count() < 2)
                return false;

            List<Carta> cartasOrd = cartas.OrderBy(x => x.Numero).ToList();

            for (int i = 0; i < cartas.Count() - 1; i++)
            {
                if (cartasOrd[i].Numero == cartasOrd[i + 1].Numero)
                    return true;
            }


            return false;
        }

        private bool VerificarQuadra(List<Carta> cartas)
        {
            if (cartas.Count() < 4)
                return false;

            List<Carta> cartasOrd = cartas.OrderBy(x => x.Numero).ToList();

            for (int i = 0; i < cartas.Count() - 3; i++)
            {
                if (cartasOrd[i].Numero == cartasOrd[i + 1].Numero && cartasOrd[i].Numero == cartasOrd[i + 2].Numero && cartasOrd[i].Numero == cartasOrd[i + 3].Numero)
                    return true;
            }


            return false;
        }

        private bool VerificarStragithFlush(List<Carta> cartas, ENipe nipe)
        {

            List<Carta> cartasNipeFlush = cartas.Where(x => x.Nipe == nipe).OrderBy(x => x.Numero).ToList();
            if (cartasNipeFlush.Count < 5)
                return false;

            List<int> cartasOrdenadas = cartasNipeFlush.Select(x => (int)x.Numero).OrderBy(x => x).Distinct().ToList();
            if (cartasOrdenadas.Count < 5)
                return false;

            List<int> cartasOrdenadasComAsNoFim = new List<int>();
            if (cartasOrdenadas.Count() < 5)
                return false;

            var temAs = cartasOrdenadas.Where(x => x == 0).Any();
            if (temAs)
            {
                cartasOrdenadasComAsNoFim = cartasNipeFlush.Select(x => (int)x.Numero).OrderBy(x => x).Distinct().ToList();
                for (int i = 0; i < cartasOrdenadas.Count() - 4; i++)
                {
                    if (cartasOrdenadas[i] == 0)
                    {
                        cartasOrdenadas[i] = 1;
                        cartasOrdenadasComAsNoFim[i] = 14;
                    }
                }
            }

            for (int i = 0; i < cartasOrdenadas.Count() - 4; i++)
            {
                if (IsSequential(new int[] { cartasOrdenadas[i], cartasOrdenadas[i + 1], cartasOrdenadas[i + 2], cartasOrdenadas[i + 3], cartasOrdenadas[i + 4] }))
                    return true;

                if (temAs)
                {
                    cartasOrdenadasComAsNoFim = cartasOrdenadasComAsNoFim.OrderBy(x => x).ToList();
                    if (IsSequential(new int[] { cartasOrdenadasComAsNoFim[i], cartasOrdenadasComAsNoFim[i + 1], cartasOrdenadasComAsNoFim[i + 2], cartasOrdenadasComAsNoFim[i + 3], cartasOrdenadasComAsNoFim[i + 4] }))
                        return true;
                }
            }

            return false;
        }

        private bool IsSequential(int[] a)
        {
            return Enumerable.Range(1, a.Length - 1).All(i => a[i] - 1 == a[i - 1]);
        }
        #endregion
    }
}
