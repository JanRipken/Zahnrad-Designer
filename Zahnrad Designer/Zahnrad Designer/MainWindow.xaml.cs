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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics; //Nötig für das ausführen des Catia Programm

namespace Zahnrad_Designer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Schlissen_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn__Catia_Click(object sender, RoutedEventArgs e)
        {
            Zahnrad_Auswahl Auswahl = new Zahnrad_Auswahl();
            Auswahl.Show();


            string Programmname = "CNEXT.exe";
            Process.Start(Programmname);

            
            this.Close();
        }
         
        //Button Start
        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            Zahnrad_Auswahl Auswahl = new Zahnrad_Auswahl();
            Auswahl.Show();


            this.Close();
        }
    }
}
