namespace Algorithms
{
    using Cuadrilla;
    using System.Collections.Generic;
    using System.Linq;

    public class AEstrella : AlgoritmoBase
    {
        private readonly List<Nodo> _Listaabierta = new List<Nodo>();
        private readonly List<Coord> _vecinos;

        public AEstrella(Cuadsrilla cuadrilla) : base(cuadrilla)
        {
            Algoritmo = "A*";
            _vecinos = new List<Coord>();

            // Put the origin on the open list
            _Listaabierta.Add(new Nodo(Id++, null, Origen, 0, GetH(Origen, Destino)));
        }

        public override SearchDetails GetPathTick()
        {
            if (NodoActual == null)
            {
                if (!_Listaabierta.Any()) return GetSearchDetails();

                // Take the current node off the open list to be examined
                NodoActual = _Listaabierta.OrderBy(x => x.F).ThenBy(x => x.H).First();

                // Move it to the closed list so it doesn't get examined again
                _Listaabierta.Remove(NodoActual);
                Cerrado.Add(NodoActual);
                Cuadricula.SetCell(NodoActual.Coord, Enums.CellType.Closed);

                _vecinos.AddRange(GetVECINO(NodoActual));
            }

            if (_vecinos.Any())
            {
                Cuadricula.SetCell(NodoActual.Coord, Enums.CellType.Current);

                var thisNeighbour = _vecinos.First();
                _vecinos.Remove(thisNeighbour);

                // If the neighbour is the destination
                if (CoordsMatch(thisNeighbour, Destination))
                {
                    // Construct the path by tracing back through the closed list until there are no more parent id references
                    Path = new List<Coord> { thisNeighbour };
                    int? parentId = NodoActual.Id;
                    while (parentId.HasValue)
                    {
                        var nextNode = Closed.First(x => x.Id == parentId);
                        Path.Add(nextNode.Coord);
                        parentId = nextNode.ParentId;
                    }

                    // Reorder the path to be from origin to destination and return
                    Path.Reverse();

                    return GetSearchDetails();
                }

                // Get the cost of the current node plus the extra step weight and heuristic
                var hFromHere = GetH(thisNeighbour, Destination);
                var cellWeight = Grid.GetCell(thisNeighbour.X, thisNeighbour.Y).Weight;
                var neighbourCost = NodoActual.G + cellWeight + hFromHere;

                // Check if the node is on the open list already and if it has a higher cost path
                var openListItem = _Listaabierta.FirstOrDefault(x => x.Id == GetExistingNode(true, thisNeighbour));
                if (openListItem != null && openListItem.F > neighbourCost)
                {
                    // Repoint the openlist node to use this lower cost path
                    openListItem.F = neighbourCost;
                    openListItem.ParentId = NodoActual.Id;
                }

                // Check if the node is on the closed list already and if it has a higher cost path
                var closedListItem = Closed.FirstOrDefault(x => x.Id == GetExistingNode(false, thisNeighbour));
                if (closedListItem != null && closedListItem.F > neighbourCost)
                {
                    // Repoint the closedlist node to use this lower cost path
                    closedListItem.F = neighbourCost;
                    closedListItem.ParentId = NodoActual.Id;
                }

                // If the neighbour node isn't on the open or closed list, add it
                if (openListItem != null || closedListItem != null) return GetSearchDetails();
                _Listaabierta.Add(new Nodo(Id++, NodoActual.Id, thisNeighbour, NodoActual.G + cellWeight, hFromHere));
                Grid.SetCell(thisNeighbour.X, thisNeighbour.Y, Enums.CellType.Open);
            }
            else
            {
                Grid.SetCell(NodoActual.Coord, Enums.CellType.Closed);
                NodoActual = null;
                return GetPathTick();
            }

            return GetSearchDetails();
        }

        private static int GetH(Coord origin, Coord destination)
        {
            return GetManhattenDistance(origin, destination);
        }

        private int? GetExistingNode(bool checkOpenList, Coord coordToCheck)
        {
            return checkOpenList ? _Listaabierta.FirstOrDefault(x => CoordsMatch(x.Coord, coordToCheck))?.Id : Closed.FirstOrDefault(x => CoordsMatch(x.Coord, coordToCheck))?.Id;
        }

        protected override SearchDetails GetSearchDetails()
        {
            return new SearchDetails
            {
                Path = Path?.ToArray(),
                PathCost = GetPathCost(),
                LastNode = NodoActual,
                DistanceOfCurrentNode = NodoActual == null ? 0 : GetH(NodoActual.Coord, Destination),
                OpenListSize = _Listaabierta.Count,
                ClosedListSize = Closed.Count,
                UnexploredListSize = Grid.GetCountOfType(Enums.CellType.Empty),
                Operations = Operations++
            };
        }
    }
}
