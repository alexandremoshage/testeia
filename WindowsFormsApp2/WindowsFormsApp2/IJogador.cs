using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public interface IJogador
    {
        int Id { get; set; }
        List<Carta> CartasMao { get; set; }
        Mao Mao { get; set; }
        List<Carta> CartasMelhorMao { get; set; }
        bool IsNaMao { get; set; }
        decimal Fichas { get; set; }
        EPosicao Posicao { get; set; }

        (EAcao, decimal) AcaoJogador();

        void SetarAcaoJogador(EAcao EAcao,  decimal valor);
    }
}
