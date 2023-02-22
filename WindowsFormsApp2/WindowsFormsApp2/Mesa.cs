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

                var (isFlush, mao, nipe) = VerificarFlushOpenIA(cartasJogador);
                var (isSequencia, sequencia) = VerificarSequencia(cartasJogador);

                if (isSequencia && isFlush && VerificarStraightFlush(cartasJogador, nipe.Value).Item1)
                {
                    jogadorNaMao.Mao = Mao.straigthFush;
                    continue;
                }

                if (VerificarQuadraOpenIa(cartasJogador).Item1)
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

        private (bool, List<ECarta>) VerificarSequencia(List<Carta> cartas)
        {

            List<ECarta> cartasOrdenadas = cartas.OrderBy(x=> x.Numero).Select(x => x.Numero).Distinct().ToList();
            var temSequencia = false;
            var sequencia = new List<ECarta>();
            if (cartasOrdenadas.Count() < 5)
            {
                return (false, null);

            }


            for (int i = 0; i < cartasOrdenadas.Count() - 4; i++)
            {

                if (cartasOrdenadas[0] == ECarta.As &&
                    cartasOrdenadas[i + 1] == ECarta.Dois &&
                    cartasOrdenadas[i + 2] == ECarta.Tres &&
                    cartasOrdenadas[i + 3] == ECarta.Quatro &&
                    cartasOrdenadas[i + 4] == ECarta.Cinco)
                {
                    temSequencia = true;
                    sequencia = new List<ECarta>() { cartasOrdenadas[0], cartasOrdenadas[i + 1], cartasOrdenadas[i + 2], cartasOrdenadas[i + 3], cartasOrdenadas[i + 4] };
                    return (true, sequencia);
                }

                if (IsSequential(new int[] {(int) cartasOrdenadas[i],
                                            (int)cartasOrdenadas[i + 1],
                                            (int)cartasOrdenadas[i + 2],
                                            (int)cartasOrdenadas[i + 3],
                                            (int)cartasOrdenadas[i + 4] }))
                {

                    temSequencia = true;
                    sequencia = new List<ECarta>() { cartasOrdenadas[i], cartasOrdenadas[i + 1], cartasOrdenadas[i + 2], cartasOrdenadas[i + 3], cartasOrdenadas[i + 4] };
                }
            }


            if (temSequencia)
            {
                return (true, sequencia);
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

        public (bool, List<Carta>) VerificarStraightFlush(List<Carta> cartas, ENipe nipe)
        {
            List<Carta> cartasOrdenadas = cartas.Where(x => x.Nipe == nipe).OrderBy(c => c.Numero).ToList();
            var temSequencia = false;
            var sequencia = new List<Carta>();
            if (cartasOrdenadas.Count() < 5)
            {
                return (false, null);

            }


            for (int i = 0; i < cartasOrdenadas.Count() - 4; i++)
            {

                if (cartasOrdenadas[0].Numero == ECarta.As &&
                    cartasOrdenadas[i + 1].Numero == ECarta.Dois &&
                    cartasOrdenadas[i + 2].Numero == ECarta.Tres &&
                    cartasOrdenadas[i + 3].Numero == ECarta.Quatro &&
                    cartasOrdenadas[i + 4].Numero == ECarta.Cinco)
                {
                    temSequencia = true;
                    sequencia = new List<Carta>() { cartasOrdenadas[0], cartasOrdenadas[i + 1], cartasOrdenadas[i + 2], cartasOrdenadas[i + 3], cartasOrdenadas[i + 4] };
                }



                if (IsSequential(new int[] {(int) cartasOrdenadas[i].Numero,
                                            (int)cartasOrdenadas[i + 1].Numero,
                                            (int)cartasOrdenadas[i + 2].Numero,
                                            (int)cartasOrdenadas[i + 3].Numero,
                                            (int)cartasOrdenadas[i + 4].Numero }))
                {

                    temSequencia = true;
                    sequencia = new List<Carta>() { cartasOrdenadas[i], cartasOrdenadas[i + 1], cartasOrdenadas[i + 2], cartasOrdenadas[i + 3], cartasOrdenadas[i + 4] };
                }

            }


            if (temSequencia)
            {
                return (true, sequencia);
            }

            return (false, null);

        }

        private bool IsSequential(int[] a)
        {
            return Enumerable.Range(1, a.Length - 1).All(i => a[i] - 1 == a[i - 1]);
        }
        #endregion


        public (bool, List<Carta>, ENipe?) VerificarFlushOpenIA(List<Carta> cartas)
        {

            // verifica se existem 5 cartas do mesmo nipe
            var flush = cartas.GroupBy(c => c.Nipe)
                              .Where(g => g.Count() >= 5)
                              .FirstOrDefault();

            if (flush != null)
            {
                // ordena as cartas em ordem decrescente e filtra as 5 melhores
                var melhoresCartas = flush.OrderByDescending(c => (int)c.Numero)
                                          .Take(5)
                                          .ToList();

                return (true, melhoresCartas, melhoresCartas[0].Nipe);
            }

            return (false, new List<Carta>(), null);
        }


        public (bool, List<Carta>) VerificarQuadraOpenIa(List<Carta> cartas)
        {
            var grupos = cartas.GroupBy(carta => carta.Numero).ToList();
            var quadra = grupos.FirstOrDefault(g => g.Count() == 4);
            var kicker = grupos.Where(g => g.Count() == 1)
                              .OrderByDescending(g => (int)g.Key)
                              .FirstOrDefault()?.ToList();

            if (quadra != null)
            {
                var cartasMao = quadra.ToList();
                cartasMao.AddRange(kicker);
                return (true, cartasMao);
            }
            else
            {
                return (false, null);
            }
        }



    }
}
