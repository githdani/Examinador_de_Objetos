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
using System.Windows.Shapes;

namespace ExaminadordeObjetos_DanielAvila
{
    /// <summary>
    /// Lógica de interacción para Copia.xaml
    /// </summary>
    public partial class Copia : Window
    {

        Boolean modo = true;
        public string ruta;
        string nombre;
        MainWindow ventana;

        public Copia(Boolean modo, string ruta, string nombre, MainWindow ventana)
        {
            InitializeComponent();
            
            this.modo = modo;
            this.ruta = ruta;
            this.nombre = nombre;
            Theme();
            this.ventana = ventana;
        }

        private void security_copy(object sender, RoutedEventArgs e)
        {
            copia_segu();
        }

        public void copia_segu()
        {

            if (Copiia.Text.Length != 0)
            {
                string nomb = Copiia.Text;
                string path = ruta + "\\" + nombre;

                
                string newPath = System.IO.Path.Combine(nomb, nombre);
                MessageBox.Show(newPath);


                File.Copy(path, newPath);
                

                Close();
            }
        }

        public void Theme()
        {
            Security.Style = !modo ? (Style)Resources["dark"] : (Style)Resources["light"];
            Copiia.Style = modo ? (Style)Resources["dark_box"] : (Style)Resources["light_box"];
            Guardar_Copia.Style = modo ? (Style)Resources["light_btn"] : (Style)Resources["dark_btn"];
        }
    }
}
