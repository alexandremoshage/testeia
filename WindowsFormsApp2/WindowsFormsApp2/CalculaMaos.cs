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

        public (bool, List<ECarta>) VerificarSequencia(IOrderedEnumerable<Carta> cartas)
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

                if (cartasOrdenadas.Last() == ECarta.As &&
                    cartasOrdenadas[i] == ECarta.Dois &&
                    cartasOrdenadas[i + 1] == ECarta.Tres &&
                    cartasOrdenadas[i + 2] == ECarta.Quatro &&
                    cartasOrdenadas[i + 3] == ECarta.Cinco)
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

        public (bool, ENipe?) VerificarFlush(IOrderedEnumerable<Carta> cartas)
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

        public (bool, List<Carta>) VerificarTrinca(IOrderedEnumerable<Carta> cartas)
        {
            var grupos = cartas.GroupBy(carta => carta.Numero).ToList();
            var trinca = grupos.FirstOrDefault(g => g.Count() == 3);

            if (trinca != null)
            {
                var kicker = cartas.Where(x => !trinca.Contains(x)).Reverse().Take(2);
                var cartasMao = trinca.ToList();
                cartasMao.AddRange(kicker);
                return (true, cartasMao);
            }
            else
            {
                return (false, null);
            }
        }

        public (bool, List<Carta>) VerificarDoisPares(IOrderedEnumerable<Carta> cartas)
        {
            if (cartas.Count() < 5)
                return (false, null);

            var grupos = cartas.Reverse().GroupBy(carta => carta.Numero).ToList();
            var pares = grupos.Where(g => g.Count() == 2);

            if (pares.Count() > 1)
            {

                List<Carta> cartasMao = new List<Carta>();
                cartasMao.AddRange(pares.ToList()[0]);
                cartasMao.AddRange(pares.ToList()[1]);
                var kicker = cartas.Where(x => !cartasMao.Contains(x)).Reverse().FirstOrDefault();

                return (true, cartasMao);
            }
            else
            {
                return (false, null);
            }
        }

        public (bool, List<Carta>) VerificarPar(IOrderedEnumerable<Carta> cartas)
        {
            var grupos = cartas.GroupBy(carta => carta.Numero).ToList();
            var par = grupos.FirstOrDefault(g => g.Count() == 2);

            if (par != null)
            {
                var kicker = cartas.Where(x => !par.Contains(x)).Reverse().Take(3);
                var cartasMao = par.ToList();
                cartasMao.AddRange(kicker);
                return (true, cartasMao);
            }
            else
            {
                return (false, null);
            }
        }

        public (bool, List<Carta>) VerificarStraightFlush(IOrderedEnumerable<Carta> cartas, ENipe nipe)
        {
            List<Carta> cartasMesmoNipe = cartas.Where(x => x.Nipe == nipe).ToList();
            var temSequencia = false;
            var sequencia = new List<Carta>();
            if (cartasMesmoNipe.Count() < 5)
            {
                return (false, null);

            }


            for (int i = 0; i < cartasMesmoNipe.Count() - 4; i++)
            {

                if (cartasMesmoNipe.Last().Numero == ECarta.As &&
                    cartasMesmoNipe[i].Numero == ECarta.Dois &&
                    cartasMesmoNipe[i + 1].Numero == ECarta.Tres &&
                    cartasMesmoNipe[i + 2].Numero == ECarta.Quatro &&
                    cartasMesmoNipe[i + 3].Numero == ECarta.Cinco)
                {
                    temSequencia = true;
                    sequencia = new List<Carta>() { cartasMesmoNipe[0], cartasMesmoNipe[i + 1], cartasMesmoNipe[i + 2], cartasMesmoNipe[i + 3], cartasMesmoNipe[i + 4] };
                }



                if (IsSequential(new int[] {(int) cartasMesmoNipe[i].Numero,
                                            (int)cartasMesmoNipe[i + 1].Numero,
                                            (int)cartasMesmoNipe[i + 2].Numero,
                                            (int)cartasMesmoNipe[i + 3].Numero,
                                            (int)cartasMesmoNipe[i + 4].Numero }))
                {

                    temSequencia = true;
                    sequencia = new List<Carta>() { cartasMesmoNipe[i], cartasMesmoNipe[i + 1], cartasMesmoNipe[i + 2], cartasMesmoNipe[i + 3], cartasMesmoNipe[i + 4] };
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



        public (bool, List<Carta>, ENipe?) VerificarFlushOpenIA(IOrderedEnumerable<Carta> cartas)
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


        public (bool, List<Carta>) VerificarQuadraOpenIa(IOrderedEnumerable<Carta> cartas)
        {
            var grupos = cartas.GroupBy(carta => carta.Numero).ToList();
            var quadra = grupos.FirstOrDefault(g => g.Count() == 4);

            if (quadra != null)
            {
                var kicker = cartas.Where(x => !quadra.Contains(x)).Last();
                var cartasMao = quadra.ToList();
                cartasMao.Add(kicker);
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
