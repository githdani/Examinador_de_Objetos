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
using System.IO;
using Path = System.IO.Path;

namespace ExaminadordeObjetos_DanielAvila
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Renombrar : Window
    {
        Boolean modo = true;
        public string ruta;
        string nombreOld;
        MainWindow ventana;


        public Renombrar(Boolean modo, string ruta, string nombreOld, MainWindow ventana)
        {
            InitializeComponent();
            
            this.modo = modo;
            this.ruta = ruta;
            this.nombreOld = nombreOld;
            Theme();
            this.ventana = ventana;
        }

        private void rename_file(object sender, RoutedEventArgs e)
        {
            Renomb();
        }

        public void Renomb()
        {

            if (Nombre.Text.Length != 0)
            {
                string nomb = Nombre.Text;
                string path = ruta +"\\"+ nombreOld;
                FileInfo f = new FileInfo(path);

                string extension = System.IO.Path.GetExtension(nombreOld);
                MessageBox.Show(extension);

                string newPath = System.IO.Path.Combine(ruta,  nomb + extension);
                
                MessageBox.Show(newPath);

                f.MoveTo(newPath);

                //File.Move(path, newPath);

                Close();
            }
        }
    
        

        public void Theme()
        {
            Renamee.Style = !modo ? (Style)Resources["dark"] : (Style)Resources["light"];
            Nombre.Style = modo ? (Style)Resources["dark_box"] : (Style)Resources["light_box"];
            Renombre.Style = modo ? (Style)Resources["light_btn"] : (Style)Resources["dark_btn"];
        }
    }
}
