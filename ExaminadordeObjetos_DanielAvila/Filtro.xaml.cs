using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ExaminadordeObjetos_DanielAvila
{
    /// <summary>
    /// Lógica de interacción para Filtro.xaml
    /// </summary>
    public partial class Filtro : Window
    {
        Boolean modo;
        string filter;
        MainWindow ventana;

        public Filtro(Boolean modo, string filter, MainWindow ventana)
        {
            InitializeComponent();
            this.modo = modo;
            this.filter = filter;
            this.ventana = ventana;
            Theme();
            Filtro_selec();
        }
        private void Filter_click(object sender, RoutedEventArgs e)
        {
            Filtrad();
        }

        public void Filtro_selec()
        {
            if (filter.Equals("cancion"))
            {
                reproductor.IsChecked = true;
            }
            if (filter.Equals("imagen"))
            {
                visor.IsChecked = true;
            }
            if (filter.Equals("texto"))
            {
                notas.IsChecked = true;
            }
        }

        
        public void Filtrad()
        {
            if (reproductor.IsChecked == true)
            {
                filter = "cancion";
            }
            if (visor.IsChecked == true)
            {
                filter = "imagen";

            }
            if (notas.IsChecked == true)
            {
                filter = "texto";
            }
            ventana.modo_app.Text = filter.ToUpper();
            ventana.Buscar(filter);
            this.Close();
        }

        
        public void Theme()
        {
            Filtracion.Style = !modo ? (Style)Resources["dark"] : (Style)Resources["light"];
            reproductor.Style = modo ? (Style)Resources["dark_rd"] : (Style)Resources["light_rd"];
            visor.Style = modo ? (Style)Resources["dark_rd"] : (Style)Resources["light_rd"];
            notas.Style = modo ? (Style)Resources["dark_rd"] : (Style)Resources["light_rd"];
            titulo.Style = modo ? (Style)Resources["dark_txt"] : (Style)Resources["light_txt"];
            Filter.Style = modo ? (Style)Resources["light_btn"] : (Style)Resources["dark_btn"];

        }
    }
}

