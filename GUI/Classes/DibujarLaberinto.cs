namespace GUI.Classes
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Cuadrilla;
    using static Cuadrilla.Enums;

    public class DibujarLaberinto
    {
        private readonly PictureBox _pb;
        public Cuadrilla Cuadrilla { get; }
        public int Seed { get; }

        public int Horizontales = 20;
        public int Verticales = 12;

        private int _cellWidth;
        private int _cellHeight;

        public DibujarLaberinto(PictureBox pb, int seed = 0)
        {
            _pb = pb;
            Cuadrilla = new Cuadrilla(Horizontales, Verticales);
            if (seed == 0) seed = (int)DateTime.Now.Ticks;
            Seed = seed;
            Cuadrilla.Randomize(seed);
        }

        public void Dibujar()
        {
            _cellWidth = _pb.Width / Horizontales;
            _cellHeight = _pb.Height / Verticales;

            var image = new Bitmap(_pb.Width, _pb.Height);

            using (var g = Graphics.FromImage(image))
            {
                var background = new Rectangle(0, 0, image.Width, image.Height);
                g.FillRectangle(new SolidBrush(Color.White), background);

                for (var x = 0; x < Horizontales; x++)
                {
                    for (var y = 0; y < Verticales; y++)
                    {
                        var cell = Cuadrilla.GetCell(x, y);
                        switch (cell.Tipo)
                        {
                            case CellType.Vacio:
                                switch (cell.Valor)
                                {
                                    case 2: g.FillRectangle(Brushes.White, GetRectangle(x, y)); break;
                                    case 3: g.FillRectangle(Brushes.White, GetRectangle(x, y)); break;
                                }
                                break;
                            case CellType.Solido:
                                g.FillRectangle(Brushes.Black, GetRectangle(x, y));
                                break;
                            case CellType.Camino:
                                g.FillRectangle(Brushes.Red, GetRectangle(x, y));
                                break;
                            case CellType.Abierto:
                                g.FillRectangle(Brushes.LightSkyBlue, GetRectangle(x, y));
                                break;
                            case CellType.Cerrado:
                                g.FillRectangle(Brushes.LightSeaGreen, GetRectangle(x, y));
                                break;
                            case CellType.Actual:
                                g.FillRectangle(Brushes.Red, GetRectangle(x, y));
                                break;
                            case CellType.A:
                                g.DrawString("->", GetFont(), Brushes.Red, GetPoint(x, y));
                                break;
                            case CellType.B:
                                g.DrawString("->", GetFont(), Brushes.Red, GetPoint(x, y));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("Unknown cell type: " + cell);
                        }

                        g.DrawRectangle(Pens.Black, GetRectangle(x, y));
                    }
                }

                _pb.Image = image;
            }
        }

        public void Reset()
        {
            Cuadrilla.Randomize(Seed);
        }

        private Rectangle GetRectangle(int x, int y)
        {
            return new Rectangle(x * _cellWidth, y * _cellHeight, _cellWidth, _cellHeight);
        }

        private PointF GetPoint(int x, int y)
        {
            return new PointF(x * _cellWidth, y * _cellHeight);
        }

        private Font GetFont()
        {
            return new Font(FontFamily.GenericMonospace, Math.Min(_cellWidth, _cellHeight) / 1.3f, FontStyle.Bold);
        }
    }
}
