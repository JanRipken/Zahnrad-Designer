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

namespace Zahnrad_Designer
{
    /// <summary>
    /// Interaktionslogik für Zahnrad_Auswahl.xaml
    /// </summary>
    public partial class Zahnrad_Auswahl : Window
    {
        public Zahnrad_Auswahl()
        {
            InitializeComponent();
        }

        private void trv_Geradverzahnung_Selected(object sender, RoutedEventArgs e)
        {
            //objekterzeugung
            Stirnräder.Geradverzahnung_Aussen.Geradverzahnung_Aussen geradverzahnung_Aussen = new Stirnräder.Geradverzahnung_Aussen.Geradverzahnung_Aussen();
            geradverzahnung_Aussen.Show();

        }
    }
}
