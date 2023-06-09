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
        private EAcao AcaoSetada = 0;
        private decimal ValorAcaoSetada = 0;

        public int Id { get => id; set => id = value; }
        public List<Carta> CartasMao { get => cartasMao; set => cartasMao = value; }
        public Mao Mao { get => mao; set => mao = value; }
        public List<Carta> CartasMelhorMao { get => cartasMelhorMao; set => cartasMelhorMao = value; }
        public bool IsNaMao { get => isNaMao; set => isNaMao = value; }
        public decimal Fichas { get => fichas; set => fichas = value; }
        public EPosicao Posicao { get => posicao; set => posicao = value; }

        public Jogador(List<Carta> _cartasMao, int id)
        {
            CartasMao = _cartasMao.OrderBy(x => x.Numero).ToList();
            this.Id = id;
        }

 

        public (EAcao, decimal) AcaoJogador()
        {
            if ( AcaoSetada != null)
            {
                return (AcaoSetada, ValorAcaoSetada);
            }
            else
            {
                throw new NotImplementedException("falta implementar ia");    
            }
        }

        public void SetarAcaoJogador(EAcao EAcao, decimal valor)
        {
            AcaoSetada = EAcao;
            ValorAcaoSetada = valor;
        }
    }
}
