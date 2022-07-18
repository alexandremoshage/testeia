using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class Baralho
    {
        List<Carta> Cartas = new List<Carta>();
        public Baralho()
        {
            for (int i = 0; i < 14; i++)
            {
                if (i == 1)
                    continue;
                for (int j = 0; j < 4; j++)
                {
                    Cartas.Add(new Carta((ECarta)i, (ENipe)j));
                }
            }

            Embaralhar();

        }

        private void Embaralhar()
        {
            var rng = new Random();
            int n = Cartas.Count();
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Carta value = Cartas[k];
                Cartas[k] = Cartas[n];
                Cartas[n] = value;
            }
        }

        public Carta EntregarCarta()
        {
            var primeriaCarta = Cartas.First();
            Cartas.RemoveAt(0);
            return primeriaCarta;
        }
    }

    

    public class Carta
    {
        public ECarta Numero { get; set; }
        public ENipe Nipe { get; set; }
        public Carta(ECarta carta, ENipe nipe)
        {
            Numero = carta;
            Nipe = nipe;
        }

    }




}
