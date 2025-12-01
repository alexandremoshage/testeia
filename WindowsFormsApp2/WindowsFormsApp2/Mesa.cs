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
                IOrderedEnumerable<Carta> cartasOrdenadas = cartasJogador.OrderBy(x => x.Numero);

                var (isFlush, mao, nipe) = calculaMaos.VerificarFlushOpenIA(cartasOrdenadas);
                var (isSequencia, sequencia) = calculaMaos.VerificarSequencia(cartasOrdenadas);

                if (isSequencia && isFlush && calculaMaos.VerificarStraightFlush(cartasOrdenadas, nipe.Value).Item1)
                {
                    jogadorNaMao.Mao = Mao.straigthFush;
                    continue;
                }

                if (calculaMaos.VerificarQuadraOpenIa(cartasOrdenadas).Item1)
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


                if (calculaMaos.VerificarTrinca(cartasOrdenadas).Item1)
                {
                    jogadorNaMao.Mao = Mao.trinca;
                    continue;
                }

                if (calculaMaos.VerificarDoisPares(cartasOrdenadas).Item1)
                {
                    jogadorNaMao.Mao = Mao.doisPares;
                    continue;
                }

                if (calculaMaos.VerificarPar(cartasOrdenadas).Item1)
                {
                    jogadorNaMao.Mao = Mao.par;
                    continue;
                }
            }

        }

        public void ExecutarAcoesJogadores(ERodada rodada)
        {
            filaAcaoJogadores = ObterOrdemAcoes(rodada).ToList();

            JogadorUltimoRaise = null;

            while (filaAcaoJogadores.Any())
            {
                var jogador = filaAcaoJogadores.First();
                filaAcaoJogadores.RemoveAt(0);

                var (acao, valor) = jogador.AcaoJogador();
                jogadorUltimaAcao = jogador;

                switch (acao)
                {
                    case EAcao.fold:
                        jogador.IsNaMao = false;
                        break;
                    case EAcao.check:
                        break;
                    case EAcao.call:
                        pote += valor;
                        jogador.Fichas -= valor;
                        break;
                    case EAcao.raise:
                        apostaCorrente = valor;
                        JogadorUltimoRaise = jogador;
                        pote += valor;
                        jogador.Fichas -= valor;

                        foreach (var outroJogador in jogadores.Where(x => x.IsNaMao && x != jogador))
                        {
                            if (!filaAcaoJogadores.Contains(outroJogador))
                            {
                                filaAcaoJogadores.Add(outroJogador);
                            }
                        }

                        break;
                }
            }
        }

        private IEnumerable<IJogador> ObterOrdemAcoes(ERodada rodada)
        {
            var jogadoresNaMao = jogadores
                .Where(x => x.IsNaMao)
                .ToList();

            if (!jogadoresNaMao.Any())
            {
                return jogadoresNaMao;
            }

            int indiceBigBlind = jogadoresNaMao.FindIndex(x => x.Posicao == EPosicao.big);

            if (indiceBigBlind < 0)
            {
                indiceBigBlind = 0;
            }

            int indiceInicial = rodada == ERodada.PreFlop
                ? (indiceBigBlind + 1) % jogadoresNaMao.Count
                : indiceBigBlind;

            return Enumerable
                .Range(0, jogadoresNaMao.Count)
                .Select(i => jogadoresNaMao[(indiceInicial + i) % jogadoresNaMao.Count]);
        }

    }
}
