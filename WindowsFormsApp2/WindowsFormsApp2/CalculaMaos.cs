using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class CalculaMaos
    {
        #region resultadoMaos

        public (bool, List<ECarta>) VerificarSequencia(List<Carta> cartas)
        {

            List<ECarta> cartasOrdenadas = cartas.OrderBy(x => x.Numero).Select(x => x.Numero).Distinct().ToList();
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

        public (bool, ENipe?) VerificarFlush(List<Carta> cartas)
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

        public bool VerificarTrinca(List<Carta> cartas)
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

        public bool VerificarDoisPares(List<Carta> cartas)
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

        public bool VerificarPar(List<Carta> cartas)
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

        public bool VerificarQuadra(List<Carta> cartas)
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

        #endregion
    }
}
