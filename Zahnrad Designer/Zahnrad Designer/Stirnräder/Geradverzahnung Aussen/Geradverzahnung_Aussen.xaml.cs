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

namespace Zahnrad_Designer.Stirnräder.Geradverzahnung_Aussen
{
    /// <summary>
    /// Interaktionslogik für Geradverzahnung_Aussen.xaml
    /// </summary>
    public partial class Geradverzahnung_Aussen : Window
    {
        public Geradverzahnung_Aussen()
        {
            InitializeComponent();
        }

        private void btn_Eingabe_Click(object sender, RoutedEventArgs e)
        {
            grd_Auftraggeber.Visibility = Visibility.Hidden;
            grd_Hauptgiter.Visibility = Visibility.Visible;
        }

        Geradverzahnung_Aussen_Berechnung geradverzahnungBerechnung;

        public void btn_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            geradverzahnungBerechnung = new Geradverzahnung_Aussen_Berechnung();



            //test ob die eingaben zahlen sind
            double ModulEingabe;
            if (double.TryParse(txt_Modul.Text, out ModulEingabe))
            {
                geradverzahnungBerechnung.Modul = ModulEingabe;

            }
            else
            {
                MessageBox.Show("Falsche Modul eingabe bitte natürliche Zahlen eingeben", "ZarCAD");
                return;
            }

            double ZähnezahlEingabe;
            if (double.TryParse(txt_Zähnezahl.Text, out ZähnezahlEingabe))
            {
                geradverzahnungBerechnung.Zähnezahl = ZähnezahlEingabe;
            }
            else
            {
                MessageBox.Show("Falsche Zähnezahl eingabe bitte natürliche Zahlen eingeben", "ZarCAD");
                return;
            }


            double TeilkreisdurchmesserEingabe;
            if (double.TryParse(txt_Teilkreisdurchmesser.Text, out TeilkreisdurchmesserEingabe))
            {

                geradverzahnungBerechnung.Teilkreisdurchmesser = TeilkreisdurchmesserEingabe;
            }
            else
            {
                MessageBox.Show("Falsche Teilkreidurchmesser eingabe bitte natürliche Zahlen eingeben", "ZarCAD");
                return;
            }

            double BreiteEingabe;
            if (double.TryParse(txt_Breite.Text, out BreiteEingabe))
            {
                geradverzahnungBerechnung.Breite = BreiteEingabe;
            }
            else
            {
                MessageBox.Show("Falsche Breite eingabe bitte natürliche Zahlen eingeben", "ZarCAD");
                return;
            }

            double EingriffswinkelEingabe;
            if (double.TryParse(txt_Eingriffswinkel.Text, out EingriffswinkelEingabe))
            {
                geradverzahnungBerechnung.Eingriffswinkel = EingriffswinkelEingabe;
            }
            else
            {
                MessageBox.Show("Falsche Eingriffswinkel eingabe bitte natürliche Zahlen eingeben", "ZarCAD");
                return;
            }

            double KopfspielEingabe;
            if (double.TryParse(txt_Kopfspiel.Text, out KopfspielEingabe))
            {
                geradverzahnungBerechnung.Kopfspiel = KopfspielEingabe * geradverzahnungBerechnung.Modul;
            }
            else
            {
                MessageBox.Show("Falsches Kopfspiel eingabe bitte natürliche Zahlen eingeben", "ZarCAD");
                return;
            }



            if (geradverzahnungBerechnung.MachbarkeitderEingabewerte() == 1 && geradverzahnungBerechnung.KontrolleWertebereichEingriffswinkel() == 1 &&
                geradverzahnungBerechnung.KontrolleWertebereichKopfspiel() == 1)
            {

                lbl_Zahnhöhe.Content = geradverzahnungBerechnung.ZahnhöheBerechen();
                lbl_Zahnfußhöhe.Content = geradverzahnungBerechnung.ZahnfußhöheBerechnen();
                lbl_Zahnkopfhöhe.Content = geradverzahnungBerechnung.ZahnkopfhöheBerechnen();
                lbl_Teilung.Content = geradverzahnungBerechnung.TeilungBerechnen();
                lbl_Fußkreisdurchmesser.Content = geradverzahnungBerechnung.FußkreisdurchmesserBerechnen();
                lbl_Grundkreisdurchmesser.Content = geradverzahnungBerechnung.GrundkreisdurchmesserBerechnen();
                lbl_Kopfkreisdurchmesser.Content = geradverzahnungBerechnung.KopfkreisdurchmesserBerechnen();
                lbl_Zahnfußfestigkeit.Content = geradverzahnungBerechnung.ZahnfußfestigkeitBerechnen();
                lbl_Zahnflankenfestigkeit.Content = geradverzahnungBerechnung.ZahnflankenfestigkeitBerechnen();
                lbl_Volumen.Content = geradverzahnungBerechnung.VolumenBerechnen();



                switch (cb_Berechnen.SelectedIndex)
                {
                    case 1:
                        lbl_Preis.Content = geradverzahnungBerechnung.PreisGeradverzahnungBerechnen34Cr4();
                        lbl_Gewicht.Content = geradverzahnungBerechnung.Gewicht.ToString("0.000");
                        break;

                    case 2:
                        lbl_Preis.Content = geradverzahnungBerechnung.PreisGeradverzahnungBerechnen20MnCr5();
                        lbl_Gewicht.Content = geradverzahnungBerechnung.Gewicht.ToString("0.000");
                        break;

                    case 3:
                        lbl_Preis.Content = geradverzahnungBerechnung.PreisGeradverzahnungBerechnen46Cr2();
                        lbl_Gewicht.Content = geradverzahnungBerechnung.Gewicht.ToString("0.000");
                        break;

                    case 4:
                        lbl_Preis.Content = geradverzahnungBerechnung.PreisGeradverzahnungBerechnen34CrMo4();
                        lbl_Gewicht.Content = geradverzahnungBerechnung.Gewicht.ToString("0.000");
                        break;
                }



                geradverzahnungBerechnung.auftraggeber = tb_Auftraggeber.Text;

                Grd_Ergebnisse.Visibility = Visibility.Visible;

            }
            else
            {
                return;
            }


        }

        private void Btn_Drucken_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Zeichen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
