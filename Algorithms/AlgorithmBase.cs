namespace Algorithms
{
    using Cuadrilla;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class AlgoritmoBase
    {
        protected readonly Cuadrilla Cuadrilla;
        protected readonly List<Node> Cerrada;
        protected List<Coord> ruta;
        protected readonly Coord Origen;
        protected readonly Coord Destino;
        protected int Id;
        protected Node NodoActual;
        protected int Operations;
        public string NombreAlgoritmo;


        protected AlgoritmoBase(Cuadrilla grid)
        {
            Cuadrilla = grid;
            Cerrada = new List<Node>();
            Origen = Cuadrilla.GetStart().Coord;
            Destino = Cuadrilla.GetEnd().Coord;
            Operations = 0;
            Id = 1;
        }

        public abstract DetallesBusqueda MarcarRuta();

        /// <summary>
        /// Find the coords that are above, below, left, and right of the current cell, assuming they are valid
        /// </summary>
        /// <param name="current"></param>
        /// <returns>The valid coords around the current cell</returns>
        protected virtual IEnumerable<Coord> GetNeighbours(Node current)
        {
            var neighbours = new List<Cell>
            {
                Cuadrilla.GetCell(current.Coord.X - 1, current.Coord.Y),
                Cuadrilla.GetCell(current.Coord.X + 1, current.Coord.Y),
                Cuadrilla.GetCell(current.Coord.X, current.Coord.Y - 1),
                Cuadrilla.GetCell(current.Coord.X, current.Coord.Y + 1)
            };

            return neighbours.Where(x => x.Tipo != Enums.CellType.Invalido && x.Tipo != Enums.CellType.Solido).Select(x => x.Coord).ToArray();
        }

        protected abstract DetallesBusqueda detallesBusqueda();

        protected static bool CoordsMatch(Coord a, Coord b) => a.X == b.X && a.Y == b.Y;

        /// <summary>
        /// Get the total blocks horizontally and vertically from one coord to another
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns>Distance in blocks</returns>
        protected static int GetManhattenDistance(Coord origin, Coord destination)
        {
            return Math.Abs(origin.X - destination.X) + Math.Abs(origin.Y - destination.Y);
        }

        /// <summary>
        /// Get the cost of the path between A and B
        /// </summary>
        /// <returns>Cost of the path or 0 if no path has been found</returns>
        protected int GetPathCost()
        {
            if (ruta == null) return 0;

            var cost = 0;
            foreach (var step in ruta)
                cost += Cuadrilla.GetCell(step.X, step.Y).Valor;

            return cost;
        }
    }
}
