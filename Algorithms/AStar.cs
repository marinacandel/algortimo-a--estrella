namespace Algorithms
{
    using Cuadrilla;
    using System.Collections.Generic;
    using System.Linq;

    public class AEstrella : AlgoritmoBase
    {
        private readonly List<Node> _ListaAbierta = new List<Node>();
        private readonly List<Coord> _Vecinos;

        public AEstrella(Cuadrilla grid) : base(grid)
        {
            NombreAlgoritmo = "A*";
            _Vecinos = new List<Coord>();

            // origen a la lista abierta
            _ListaAbierta.Add(new Node(Id++, null, Origen, 0, GetH(Origen, Destino)));
        }

        public override DetallesBusqueda MarcarRuta()
        {
            if (NodoActual == null)
            {
                if (!_ListaAbierta.Any()) return detallesBusqueda();

                // utilizar el nodo actual de la lista abierta para examinarlo
                NodoActual = _ListaAbierta.OrderBy(x => x.F).ThenBy(x => x.H).First();

                // mover a la lista cerrada para no ser examinado de nuevo
                _ListaAbierta.Remove(NodoActual);
                Cerrada.Add(NodoActual);
                Cuadrilla.SetCell(NodoActual.Coord, Enums.CellType.Cerrado);

                _Vecinos.AddRange(GetNeighbours(NodoActual));
            }

            if (_Vecinos.Any())
            {
                Cuadrilla.SetCell(NodoActual.Coord, Enums.CellType.Actual);

                var vecinoDestino = _Vecinos.First();
                _Vecinos.Remove(vecinoDestino);

                // si el vecino es el destino
                if (CoordsMatch(vecinoDestino, Destino))
                {
                    // construir ruta en base a lista cerrada si se llego al destino
                    ruta = new List<Coord> { vecinoDestino };
                    int? Idpariente = NodoActual.Id;
                    while (Idpariente.HasValue)
                    {
                        var NodoSiguiente = Cerrada.First(x => x.Id == Idpariente);
                        ruta.Add(NodoSiguiente.Coord);
                        Idpariente = NodoSiguiente.ParentId;
                    }

                    // reordenar la ruta desde el origen al destino 
                    ruta.Reverse();

                    return detallesBusqueda();
                }

                // costo del nodo actual + el valor del paso anterior y heuristica
                var Hn = GetH(vecinoDestino, Destino);
                var CostoCelda = Cuadrilla.GetCell(vecinoDestino.X, vecinoDestino.Y).Valor;
                var CostoVecino = NodoActual.G + CostoCelda + Hn;

                // el nodo con menor valor en la lista abierta
                var ItemListaAbierta = _ListaAbierta.FirstOrDefault(x => x.Id == GetExistingNode(true, vecinoDestino));
                if (ItemListaAbierta != null && ItemListaAbierta.F > CostoVecino)
                {
                    // utilizo el nodo con menor costo en la lista para crear ruta
                    ItemListaAbierta.F = CostoVecino;
                    ItemListaAbierta.ParentId = NodoActual.Id;
                }

               
                var itemListaCerrada = Cerrada.FirstOrDefault(x => x.Id == GetExistingNode(false, vecinoDestino));
                if (itemListaCerrada != null && itemListaCerrada.F > CostoVecino)
                {
                    //menor costo en lista cerrada
                    itemListaCerrada.F = CostoVecino;
                    itemListaCerrada.ParentId = NodoActual.Id;
                }

               
                if (ItemListaAbierta != null || itemListaCerrada != null) return detallesBusqueda();
                _ListaAbierta.Add(new Node(Id++, NodoActual.Id, vecinoDestino, NodoActual.G + CostoCelda, Hn));
                Cuadrilla.SetCell(vecinoDestino.X, vecinoDestino.Y, Enums.CellType.Abierto);
            }
            else
            {
                Cuadrilla.SetCell(NodoActual.Coord, Enums.CellType.Cerrado);
                NodoActual = null;
                return MarcarRuta();
            }

            return detallesBusqueda();
        }

        private static int GetH(Coord origin, Coord destination)
        {
            return GetManhattenDistance(origin, destination);
        }

        private int? GetExistingNode(bool checkOpenList, Coord coordToCheck)
        {
            return checkOpenList ? _ListaAbierta.FirstOrDefault(x => CoordsMatch(x.Coord, coordToCheck))?.Id : Cerrada.FirstOrDefault(x => CoordsMatch(x.Coord, coordToCheck))?.Id;
        }

        protected override DetallesBusqueda detallesBusqueda()
        {
            return new DetallesBusqueda
            {
                Ruta = ruta?.ToArray(),
                CostoRuta = GetPathCost(),
                UltimoNodo = NodoActual,
                DistanciaDesdeNodoActual = NodoActual == null ? 0 : GetH(NodoActual.Coord, Destino),
                TamañoListaAbierta = _ListaAbierta.Count,
                TamañoListaCerrada = Cerrada.Count,
                TamañoListaSinExplorar = Cuadrilla.GetCountOfType(Enums.CellType.Vacio),
                Operaciones = Operations++
            };
        }
    }
}
