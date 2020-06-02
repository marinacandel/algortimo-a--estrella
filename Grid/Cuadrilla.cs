namespace Cuadrilla
{
    using System;
    using System.Linq;
    using static Enums;

    public class Cuadrilla
    {
        private readonly Cell[,] _grid;
        public Cuadrilla(int horizontalCells, int verticalCells)
        {
            _grid = new Cell[horizontalCells, verticalCells];
            for (var x = 0; x < _grid.GetLength(0); x++)
            {
                for (var y = 0; y < _grid.GetLength(1); y++)
                {
                    SetCell(x, y, CellType.Vacio);
                }
            }

            SetStartAndEnd();
        }

        public void Randomize()
        {
            Randomize((int)DateTime.Now.Ticks);
        }

        public void Randomize(int seed)
        {
            var rand = new Random(seed);

            // Iterate through the whole grid
            for (var x = 0; x < _grid.GetLength(0); x++)
            {
                for (var y = 0; y < _grid.GetLength(1); y++)
                {
                    // Make each cell either solid or empty at random
                    _grid[x, y].Tipo = rand.Next(0, 10) > 5 ? CellType.Solido : CellType.Vacio;
                    if (_grid[x, y].Tipo != CellType.Vacio) continue;

                    // If it's empty, randomly give the path a weight
                    var weightSpread = rand.Next(0, 10);
                    if (weightSpread > 8)
                        _grid[x, y].Valor = 3;
                    else if (weightSpread > 6)
                        _grid[x, y].Valor = 2;
                    else
                        _grid[x, y].Valor = 1;
                }
            }
            SetStartAndEnd();
        }

        public Cell GetCell(int x, int y)
        {
            if (x > _grid.GetLength(0) - 1 || x < 0 || y > _grid.GetLength(1) - 1 || y < 0) return new Cell { Coord = new Coord(-1, -1), Tipo = CellType.Invalido };

            return _grid[x, y];
        }

        public Cell GetStart()
        {
            return _grid.Cast<Cell>().FirstOrDefault(cell => cell.Tipo == CellType.A);
        }

        public Cell GetEnd()
        {
            return _grid.Cast<Cell>().FirstOrDefault(cell => cell.Tipo == CellType.B);
        }

        public void SetCell(int x, int y, CellType type)
        {
            _grid[x, y] = new Cell
            {
                Coord = new Coord(x, y),
                Tipo = type,
                Valor = GetCell(x, y)?.Valor ?? 0
            };

            SetStartAndEnd();
        }

        public void SetCell(Coord coord, CellType type)
        {
            SetCell(coord.X, coord.Y, type);
        }

        public int GetCountOfType(CellType type)
        {
            var total = 0;
            foreach (var cell in _grid)
            {
                total += cell.Tipo == type ? 1 : 0;
            }

            return total;
        }

        public int GetTraversableCells()
        {
            return GetCountOfType(CellType.Abierto) + GetCountOfType(CellType.A) + GetCountOfType(CellType.B);
        }

        private void SetStartAndEnd()
        {
            _grid[0, 0] = new Cell
            {
                Coord = new Coord(0, 0),
                Tipo = CellType.A
            };
            _grid[_grid.GetLength(0) - 1, _grid.GetLength(1) - 1] = new Cell
            {
                Coord = new Coord(_grid.GetLength(0) - 1, _grid.GetLength(1) - 1),
                Tipo = CellType.B
            };
        }
    }
}
