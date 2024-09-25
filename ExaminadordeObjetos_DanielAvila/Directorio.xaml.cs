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
    /// Lógica de interacción para Directorio.xaml
    /// </summary>
    public partial class Directorio : Window
    {
        Boolean modo = true;
        string ruta;
        MainWindow ventana;
        public Directorio(Boolean modo, string ruta, MainWindow ventana)
        {
            InitializeComponent();

            this.modo = modo;
            this.ruta = ruta;
            Theme();
            this.ventana = ventana;
        }
        
        private void Guardar_ruta(object sender, RoutedEventArgs e)
        {
            CambiarRuta();
        }



        public void CambiarRuta()
        {
            try
            {
                if (Ruta.Text.Length != 0)
                {
                    ruta = Ruta.Text;
                    ventana.Coger_datos(ruta);
                    Close();
                }
            }
            catch (ArgumentException e)
            {
                ventana.Coger_datos("C:/WINDOWS/MEDIA/");
                Close();
            }

        }

        

        public void Theme()
        {
            Direct.Style = !modo ? (Style)Resources["dark"] : (Style)Resources["light"];
            Ruta.Style = modo ? (Style)Resources["dark_box"] : (Style)Resources["light_box"];
            Guardar.Style = modo ? (Style)Resources["light_btn"] : (Style)Resources["dark_btn"];
        }
    }
}
