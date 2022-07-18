using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class Jogador : IJogador
    {
        private int id = 0;
        private List<Carta> cartasMao;
        private Mao mao;
        private List<Carta> cartasMelhorMao;
        private Boolean isNaMao = true;
        private decimal fichas = 25;
        private EPosicao posicao = 0;
        private EAcao acao = 0;

        public int Id { get => id; set => id = value; }
        public List<Carta> CartasMao { get => cartasMao; set => cartasMao = value; }
        public Mao Mao { get => mao; set => mao = value; }
        public List<Carta> CartasMelhorMao { get => cartasMelhorMao; set => cartasMelhorMao = value; }
        public bool IsNaMao { get => isNaMao; set => isNaMao = value; }
        public decimal Fichas { get => fichas; set => fichas = value; }
        public EPosicao Posicao { get => posicao; set => posicao = value; }
        public EAcao Acao { get => acao; set => acao = value; }

        public Jogador(List<Carta> _cartasMao, int id)
        {
            CartasMao = _cartasMao.OrderBy(x => x.Numero).ToList();
            this.Id = id;
        }

        public (EAcao, decimal) AcaoJogador()
        {

            return (EAcao.check, 0);
        }

        public (EAcao, decimal) AcaoJogador(EAcao acao, decimal valor)
        {
            if (acao == EAcao.fold)
            {
                return (EAcao.fold, 0);
            }

            if (acao == EAcao.check)
            {
                return (EAcao.check, 0);
            }

            if (acao == EAcao.call)
            {
                Fichas = Fichas - valor;
                return (EAcao.call, valor);
            }

            if (acao == EAcao.raise)
            {
                Fichas = Fichas - valor;
                return (EAcao.raise, valor);
            }

            return (EAcao.check, 0);
        }


    }

    public enum Mao
    {
        nada = 0,
        par = 1,
        doisPares = 2,
        trinca = 3,
        sequencia = 4,
        flush = 5,
        fullHouse = 6,
        quadra = 7,
        straigthFush = 8
    }

    public enum EPosicao
    {
        botao = 0,
        small = 1,
        big = 2
    }

    public enum EAcao
    {
        fold = 0,
        check = 1,
        call = 2,
        raise = 3
    }
}
