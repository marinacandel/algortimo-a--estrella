namespace Algoritmo
{
    using Cuadrilla;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class AlgoritmoBase
    {
        protected readonly Cuadrilla Cuadrilla;
        protected readonly List<Nodo> Cerrado;
        protected List<Coord> Ruta;
        protected readonly Coord Origen;
        protected readonly Coord Destino;
        protected int Id;
        protected Nodo NodoActual;
        protected int Operacion;
        public string Algoritmo;


        protected AlgoritmoBase(Cuadrilla cuadrilla)
        {
            Cuadrilla = cuadrilla;
            Cerrado = new List<Nodo>();
            Origen = Grid.GetStart().Coord;
            Destino = Grid.GetEnd().Coord;
            Operacion = 0;
            Id = 1;
        }

        public abstract SearchDetails GetPathTick();

        
        protected virtual IEnumerable<Coord> GetVECINO(Nodo actual)
        {
            var VECINO = new List<Cell>
            {
                Grid.GetCell(actual.Coord.X - 1, actual.Coord.Y),
                Grid.GetCell(actual.Coord.X + 1, actual.Coord.Y),
                Grid.GetCell(actual.Coord.X, actual.Coord.Y - 1),
                Grid.GetCell(actual.Coord.X, actual.Coord.Y + 1)
            };

            return VECINO.Where(x => x.Type != Enums.CellType.Invalid && x.Type != Enums.CellType.Solid).Select(x => x.Coord).ToArray();
        }

        protected abstract SearchDetails GetSearchDetails();

        protected static bool CoordsMatch(Coord a, Coord b) => a.X == b.X && a.Y == b.Y;

        /// <summary>
        /// Get the total blocks horizontally and vertically from one coord to another
        /// </summary>
        /// <param name="origen"></param>
        /// <param name="destino"></param>
        /// <returns>Distance in blocks</returns>
        protected static int GetManhattenDistance(Coord origen, Coord destino)
        {
            return Math.Abs(origen.X - destino.X) + Math.Abs(origen.Y - destino.Y);
        }

        /// <summary>
        /// Get the cost of the path between A and B
        /// </summary>
        /// <returns>Cost of the path or 0 if no path has been found</returns>
        protected int GetPathCost()
        {
            if (Ruta == null) return 0;

            var cost = 0;
            foreach (var step in Ruta)
                cost += Cuadrilla.GetCell(step.X, step.Y).Weight;

            return cost;
        }
    }
}
