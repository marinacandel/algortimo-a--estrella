namespace Algorithms
{
    using Cuadrilla;

    public class DetallesBusqueda
    {
        public bool RutaPosible => RutaEncontrada || TamañoListaAbierta > 0;
        public bool RutaEncontrada => Ruta != null;
        public Coord[] Ruta { get; set; }
        public int CostoRuta { get; set; }
        public Node UltimoNodo { get; set; }
        public int DistanciaDesdeNodoActual { get; set; }
        public int TamañoListaAbierta { get; set; }
        public int TamañoListaCerrada { get; set; }
        public int TamañoListaSinExplorar { get; set; }
        public int Operaciones { get; set; }
    }
}
