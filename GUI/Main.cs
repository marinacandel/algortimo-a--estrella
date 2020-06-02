namespace GUI
{
    using Classes;
    using System;
    using System.Windows.Forms;
    using Algorithms;
    using Cuadrilla;
    using System.Linq;

    public partial class Main : Form
    {
        public DibujarLaberinto _LaberintoNuevo;
        private AlgoritmoBase[] _algoritmo;
        private int AlgoritmoActual;
        private readonly System.Timers.Timer _CronoRecorrer;
       public const int Delay = 1;

        public Main()
        {
            InitializeComponent();
            _CronoRecorrer = new System.Timers.Timer(Delay);
            _CronoRecorrer.Elapsed += tiempoRecorrerRuta;
            IniciarLaberinto();
            X.Text = Convert.ToString(_LaberintoNuevo.Horizontales);
            Y.Text = Convert.ToString(_LaberintoNuevo.Verticales);
            tiempo1.Text = Convert.ToString(Main.Delay);
        }

        private void IniciarLaberinto() 
        {
            _CronoRecorrer.Stop();

            var workingSeed = BuscarNodoFuncional();
            while (workingSeed == 0)
                workingSeed = BuscarNodoFuncional();

            AlgoritmoActual = -1;
            _LaberintoNuevo = new DibujarLaberinto(pbMaze, workingSeed);
            _algoritmo = new AlgoritmoBase[] { new AEstrella(_LaberintoNuevo.Cuadrilla) };
            Text = @"A (Estrella) ";
            _LaberintoNuevo.Dibujar();
        }

        //genera el laberinto y encuentra una ruta entre inicio y fin
        private int BuscarNodoFuncional()
        {
            var ProbarLaberinto = new DibujarLaberinto(pbMaze);
            var BuscarCamino = new DepthFirst(ProbarLaberinto.Cuadrilla);
            var progress = BuscarCamino.MarcarRuta();
            while (progress.RutaPosible && !progress.RutaEncontrada)
            {
                progress = BuscarCamino.MarcarRuta();
            }
            return progress.RutaEncontrada ? ProbarLaberinto.Seed : 0;

        }

       
        private void tiempoRecorrerRuta(object sender, System.Timers.ElapsedEventArgs e)
        {
            _CronoRecorrer.Stop();
            var resetTimer = false;

            var searchStatus = _algoritmo[AlgoritmoActual].MarcarRuta();
           

            if (searchStatus.RutaEncontrada)
            {
                ConstruirRuta(searchStatus);
            }
            else
            {
               _LaberintoNuevo.Dibujar();
                
                resetTimer = true;
            }

            if (resetTimer) _CronoRecorrer.Start();
        }

       
        private void ConstruirRuta(DetallesBusqueda detalles)
        {
            for (var i = 1; i < detalles.Ruta.Length - 1; i++)
            {
                var step = detalles.Ruta[i];
                _LaberintoNuevo.Cuadrilla.SetCell(step.X, step.Y, Enums.CellType.Camino);
                
                _LaberintoNuevo.Dibujar();
               System.Threading.Thread.Sleep(1);
            }

        }

      
        
        private void BtnRecorrer_Click(object sender, EventArgs e)
        {
            AlgoritmoActual++;
            if (AlgoritmoActual == _algoritmo.Length) return;
            _LaberintoNuevo.Reset();

            _CronoRecorrer.Start();
            btnGo.Enabled = false;
        }

       
        private void Btnlaberinto_Click(object sender, EventArgs e)
        {
           
            IniciarLaberinto();
            btnGo.Enabled=true;
        }
    }
}
