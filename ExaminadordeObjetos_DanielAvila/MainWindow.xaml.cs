using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExaminadordeObjetos_DanielAvila
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Boolean modo = true;
        private DirectoryInfo _directory;
        Image image;
        TextBlock nomb_cancion;
        TextBlock nomb_image;
        TextBlock nomb_texto;
        MediaElement media;
        TextBox word;
        private string modo_filtrado = "cancion";
        private List<Cancion> canciones = new List<Cancion>();
        private Cancion song = null;
        private List<Foto> imagenes = new List<Foto>();
        private Foto imagen = null;
        private List<Texto> textos = new List<Texto>();
        private Texto texto = null; 
        string ruta = null;
        
        string nomb = "";

        public MainWindow()
        {

            InitializeComponent();
            Theme();
            ruta = "C:/WINDOWS/MEDIA";
            modo_app.Text = modo_filtrado;
            directorio_file.Text = ruta;
            taman.Text = "";
            Coger_datos(ruta);
        }

        private void nombre(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Daniel Ávila Contento");
        }
        private void lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lista.SelectedItem != null)
            {
                Seleccionar();
            }
        }

        private void cerrar(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void claro(object sender, RoutedEventArgs e)
        {
            CambiarTema();
        }

        private void oscuro(object sender, RoutedEventArgs e)
        {
            CambiarTema();          
        }

        private void ACon(object sender, RoutedEventArgs e)
        {
            string nombFile = nombreFiltrado();
            string filePath = ruta + "\\" + nombFile;
            System.Diagnostics.Process.Start("cmd.exe", "/c start \"\" \"" + filePath + "\"");
        }

        private void copySeguro(object sender, RoutedEventArgs e)
        {

            nomb = nombreFiltrado();
            Copia copiado = new Copia(modo, ruta, nomb, this);
            copiado.Show();

        }

        public void renombre(object sender, RoutedEventArgs e)
        {
            nomb = nombreFiltrado();
            
            Renombrar renombrado = new Renombrar(modo, ruta, nomb, this);
            renombrado.Show();
            
        }

        public void direccion(object sender, RoutedEventArgs e)
        {
            Directorio direct = new Directorio(modo, modo_filtrado, this);
            direct.Show();

        }

        public void filtrar(object sender, RoutedEventArgs e)
        {
            Filtro filtro = new Filtro(modo, modo_filtrado, this);
            filtro.Show();
        }

        
        public void Coger_datos(string ruta_ex)
        {
            _directory = new DirectoryInfo(ruta_ex);
            ruta = ruta_ex;
            Buscar(modo_filtrado);
            directorio_file.Text = ruta;
        }

        public void Buscar(string filtro)
        {
            modo_filtrado = filtro;
            if (modo_filtrado.Equals("cancion"))
            {
                Canciones();
            }
            if (modo_filtrado.Equals("imagen"))
            {
                Imagenes();
            }
            if (modo_filtrado.Equals("texto"))
            {
                Textos();
            }
        }
        public void Seleccionar()
        {
            if (modo_filtrado.Equals("cancion"))
            {
                if (lista.SelectedIndex != 1)
                {
                    song = lista.SelectedItem as Cancion;
                    nomb_cancion.Text = song.name;
                    taman.Text = song.tamanio.ToString();

                }
            }
            if (modo_filtrado.Equals("imagen"))
            {
                imagen = lista.SelectedItem as Foto;
                image.Source = new BitmapImage(new Uri(imagen.ruta, UriKind.Absolute));
                nomb_image.Text = imagen.name;
                taman.Text = imagen.tamanio.ToString();
            }
            if (modo_filtrado.Equals("texto"))
            {
                texto = lista.SelectedItem as Texto;
                Notas();
                nomb_texto.Text = texto.name;
                taman.Text = texto.tamanio.ToString();
            }
        }

        public void Canciones()
        {
            canciones.Clear();
            foreach (var f in _directory.GetFiles("*.wav"))
            {
                canciones.Add(new Cancion()
                {
                    ruta = f.FullName,
                    tamanio = f.Length,
                    name = f.Name
                });
            }

            Reproductor();
            lista.ItemsSource = canciones;
            lista.Items.Refresh();
        }
        public void Imagenes()
        {
            imagenes.Clear();
            foreach (var f in _directory.GetFiles("*.jpg"))
            {
                imagenes.Add(new Foto()
                {
                    ruta = f.FullName,
                    name = f.Name,
                    tamanio = f.Length
                });
            }
            Galeria();
            lista.ItemsSource = imagenes;
            lista.Items.Refresh();
        }

        public void Textos()
        {
            textos.Clear();
            foreach (var f in _directory.GetFiles("*.txt"))
            {
                textos.Add(new Texto()
                {
                    ruta = f.FullName,
                    name = f.Name,
                    tamanio = f.Length
                });
            }
            Nota();
            lista.ItemsSource = textos;
            lista.Items.Refresh();
        }

        public void Notas()
        {
            String docu = "";
            String linea;
            try
            {
                StreamReader sr = new StreamReader(texto.ruta);

                while ((linea = sr.ReadLine()) != null)
                {
                    docu += linea + "\n";
                }
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                word.Text = docu;
            }
        }


        public void Reproductor()
        {
            main.Children.Clear();

            Button btn_reset = new Button();
            Button btn_pause = new Button();
            Button btn_play = new Button();

            DockPanel dock_panel = new DockPanel();

            nomb_cancion = new TextBlock();
            nomb_cancion.Text = "";
            nomb_cancion.FontSize = 18;
            nomb_cancion.Name = "song";
            nomb_cancion.HorizontalAlignment = HorizontalAlignment.Left;

            media = new MediaElement();
            media.UnloadedBehavior = MediaState.Stop;
            media.LoadedBehavior = MediaState.Manual;

            dock_panel.Margin = new Thickness(5);
            dock_panel.Children.Add(media);
            dock_panel.Children.Add(nomb_cancion);

            WrapPanel panel = new WrapPanel();
            panel.Orientation = Orientation.Horizontal;
            panel.VerticalAlignment = VerticalAlignment.Bottom;
            panel.HorizontalAlignment = HorizontalAlignment.Center;
            panel.Margin = new Thickness(15);

            btn_play.Style = modo ? (Style)Resources["light_btn"] : (Style)Resources["dark_btn"];
            btn_pause.Style = modo ? (Style)Resources["light_btn"] : (Style)Resources["dark_btn"];
            btn_reset.Style = modo ? (Style)Resources["light_btn"] : (Style)Resources["dark_btn"];

            btn_play.Click += new RoutedEventHandler(Play);
            btn_play.Content = "Play";

            btn_pause.Click += new RoutedEventHandler(Pausa);
            btn_pause.Content = "Pausa";
            
            btn_reset.Click += new RoutedEventHandler(Reset);
            btn_reset.Content = "Reset";

            panel.Children.Add(btn_play);
            panel.Children.Add(btn_pause);
            panel.Children.Add(btn_reset);
            main.Children.Add(dock_panel);
            main.Children.Add(panel);

            
            
        }


        private void Play(object sender, RoutedEventArgs e)
        {
            if (song != null)
            {
                media.Source = new Uri(song.ruta);
                media.Play();
                
            }
            else
            {
                MessageBox.Show("Seleccione alguna cancion");

            }
        }

        private void Pausa(object sender, RoutedEventArgs e)
        {

            if (song != null )
            {
                
                media.Pause();
            }
            else
            {
                MessageBox.Show("Reproduzca alguna cancion");
            }

        }

        private void Reset(object sender, RoutedEventArgs e)
        {

            media.Pause();
            media.Source = null;
            MessageBox.Show("Reproductor Reseteado");
            lista.SelectedItem = null;
            nomb_cancion.Text = "";
            taman.Text = "";
            song = null;
            
        }

        public void Galeria()
        {
            main.Children.Clear();

            DockPanel dock_panel = new DockPanel();


            nomb_image = new TextBlock();
            nomb_image.Text = "";
            nomb_image.FontSize = 20;
            nomb_image.Name = "image";
            nomb_image.HorizontalAlignment = HorizontalAlignment.Left;

            dock_panel.Margin = new Thickness(5);
            dock_panel.Children.Add(nomb_image);



            image = new Image();

            image.VerticalAlignment = VerticalAlignment.Bottom;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.Margin = new Thickness(15);

            main.Children.Add(dock_panel);
            main.Children.Add(image);
        }


        public void Nota()
        {
            main.Children.Clear();

            DockPanel dock = new DockPanel();


            nomb_texto = new TextBlock();
            nomb_texto.FontSize = 20;
            nomb_texto.HorizontalAlignment = HorizontalAlignment.Left;
            
            dock.Children.Add(nomb_texto);

            word = new TextBox();
            word.Width = 300;
            word.Height = 260;
            word.VerticalAlignment = VerticalAlignment.Center;
            word.HorizontalAlignment = HorizontalAlignment.Center;
            word.MinLines = 15;
            word.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            word.TextWrapping = TextWrapping.Wrap;
            word.AcceptsReturn = true;
            word.AcceptsTab = true;


            main.Children.Add(dock);
            main.Children.Add(word);


        }

        public void Theme()
        {
            Principal.Style = modo ? (Style)Resources["light"] : (Style)Resources["dark"];
            menu.Style = modo ? (Style)Resources["light_txt"] : (Style)Resources["dark_txt"];
            main.Style = modo ? (Style)Resources["light_pnl"] : (Style)Resources["dark_pnl"];
            
            

            light.IsEnabled = modo ? false : true;
            dark.IsEnabled = modo ? true : false;
        }

        public string nombreFiltrado()
        {
            string nombre = "";

            if (modo_filtrado.Equals("cancion"))
            {
                song = lista.SelectedItem as Cancion;
                nombre = song.name;
                
            }
            if (modo_filtrado.Equals("imagen"))
            {
                imagen = lista.SelectedItem as Foto;
                nombre = imagen.name;
                
            }
            if (modo_filtrado.Equals("texto"))
            {
                texto = lista.SelectedItem as Texto;
                nombre = texto.name;
                
            }

            return nombre;
        } 

        public void CambiarTema()
        {
            modo = !modo;
            Theme();
            Buscar(modo_filtrado);
        }


    }
}

