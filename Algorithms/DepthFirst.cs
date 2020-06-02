namespace Algorithms
{
    using Cuadrilla;
    using System.Collections.Generic;
    using System.Linq;

    public class DepthFirst : AlgoritmoBase
    {
        readonly Stack<Node> _stack = new Stack<Node>();

        public DepthFirst(Cuadrilla grid) : base (grid)
        {
            NombreAlgoritmo = "Depth-First Search";

            // Add the first node to the stack
            _stack.Push(new Node(Id++, null, Origen, 0, 0));
        }

        public override DetallesBusqueda MarcarRuta()
        {
            // Check the next node on the stack to see if it is the destination
            NodoActual = _stack.Peek();
            if (CoordsMatch(NodoActual.Coord, Destino))
            {
                // All the items on the stack will be the path so add them and reverse the order
                ruta = new List<Coord>();
                foreach (var item in _stack)
                    ruta.Add(item.Coord);

                ruta.Reverse();

                return detallesBusqueda();
            }

            // Get all the neighbours that haven't been visited
            var neighbours = GetNeighbours(NodoActual).Where(x => !AlreadyVisited(new Coord(x.X, x.Y))).ToArray();
            if (neighbours.Any())
            {
                foreach (var neighbour in neighbours)
                    Cuadrilla.SetCell(neighbour.X, neighbour.Y, Enums.CellType.Abierto);

                // Take this neighbour and add it the stack
                var next = neighbours.First();
                var newNode = new Node(Id++, null, next.X, next.Y, 0, 0);
                _stack.Push(newNode);
                Cuadrilla.SetCell(newNode.Coord.X, newNode.Coord.Y, Enums.CellType.Actual);
            }
            else
            {
                // Remove this unused node from the stack and add it to the closed list
                var abandonedCell = _stack.Pop();
                Cuadrilla.SetCell(abandonedCell.Coord.X, abandonedCell.Coord.Y, Enums.CellType.Cerrado);
                Cerrada.Add(abandonedCell);
            }

            return detallesBusqueda();
        }

        private bool AlreadyVisited(Coord coord)
        {
            return _stack.Any(x => CoordsMatch(x.Coord, coord)) || Cerrada.Any(x => CoordsMatch(x.Coord, coord));
        }

        protected override DetallesBusqueda detallesBusqueda()
        {
            return new DetallesBusqueda
            {
                Ruta = ruta?.ToArray(),
                CostoRuta = GetPathCost(),
                UltimoNodo = NodoActual,
                DistanciaDesdeNodoActual = NodoActual == null ? 0 : GetManhattenDistance(NodoActual.Coord, Destino),
                TamañoListaAbierta = _stack.Count,
                TamañoListaCerrada = Cerrada.Count,
                TamañoListaSinExplorar = Cuadrilla.GetCountOfType(Enums.CellType.Vacio),
                Operaciones = Operations++
            };
        }
    }
}
