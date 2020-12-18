using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        Geradverzahnung_Aussen_Berechnung geradverzahnungBerechnungAussen;

        public void btn_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            geradverzahnungBerechnungAussen = new Geradverzahnung_Aussen_Berechnung();



            //test ob die eingaben zahlen sind
            double ModulEingabe;
            if (double.TryParse(txt_Modul.Text, out ModulEingabe))
            {
                geradverzahnungBerechnungAussen.Modul = ModulEingabe;

            }
            else
            {
                MessageBox.Show("Falsche Modul eingabe bitte natürliche Zahlen eingeben", "ZarCAD");
                return;
            }

            double ZähnezahlEingabe;
            if (double.TryParse(txt_Zähnezahl.Text, out ZähnezahlEingabe))
            {
                geradverzahnungBerechnungAussen.Zähnezahl = ZähnezahlEingabe;
            }
            else
            {
                MessageBox.Show("Falsche Zähnezahl eingabe bitte natürliche Zahlen eingeben", "ZarCAD");
                return;
            }


            double TeilkreisdurchmesserEingabe;
            if (double.TryParse(txt_Teilkreisdurchmesser.Text, out TeilkreisdurchmesserEingabe))
            {

                geradverzahnungBerechnungAussen.Teilkreisdurchmesser = TeilkreisdurchmesserEingabe;
            }
            else
            {
                MessageBox.Show("Falsche Teilkreidurchmesser eingabe bitte natürliche Zahlen eingeben", "ZarCAD");
                return;
            }

            double BreiteEingabe;
            if (double.TryParse(txt_Breite.Text, out BreiteEingabe))
            {
                geradverzahnungBerechnungAussen.Breite = BreiteEingabe;
            }
            else
            {
                MessageBox.Show("Falsche Breite eingabe bitte natürliche Zahlen eingeben", "ZarCAD");
                return;
            }

            double EingriffswinkelEingabe;
            if (double.TryParse(txt_Eingriffswinkel.Text, out EingriffswinkelEingabe))
            {
                geradverzahnungBerechnungAussen.Eingriffswinkel = EingriffswinkelEingabe;
            }
            else
            {
                MessageBox.Show("Falsche Eingriffswinkel eingabe bitte natürliche Zahlen eingeben", "ZarCAD");
                return;
            }

            double KopfspielEingabe;
            if (double.TryParse(txt_Kopfspiel.Text, out KopfspielEingabe))
            {
                geradverzahnungBerechnungAussen.Kopfspiel = KopfspielEingabe * geradverzahnungBerechnungAussen.Modul;
            }
            else
            {
                MessageBox.Show("Falsches Kopfspiel eingabe bitte natürliche Zahlen eingeben", "ZarCAD");
                return;
            }



            if (geradverzahnungBerechnungAussen.MachbarkeitderEingabewerte() == 1 && geradverzahnungBerechnungAussen.KontrolleWertebereichEingriffswinkel() == 1 &&
                geradverzahnungBerechnungAussen.KontrolleWertebereichKopfspiel() == 1)
            {

                lbl_Zahnhöhe.Content = geradverzahnungBerechnungAussen.ZahnhöheBerechen();
                lbl_Zahnfußhöhe.Content = geradverzahnungBerechnungAussen.ZahnfußhöheBerechnen();
                lbl_Zahnkopfhöhe.Content = geradverzahnungBerechnungAussen.ZahnkopfhöheBerechnen();
                lbl_Teilung.Content = geradverzahnungBerechnungAussen.TeilungBerechnen();
                lbl_Fußkreisdurchmesser.Content = geradverzahnungBerechnungAussen.FußkreisdurchmesserBerechnen();
                lbl_Grundkreisdurchmesser.Content = geradverzahnungBerechnungAussen.GrundkreisdurchmesserBerechnen();
                lbl_Kopfkreisdurchmesser.Content = geradverzahnungBerechnungAussen.KopfkreisdurchmesserBerechnen();
                lbl_Zahnfußfestigkeit.Content = geradverzahnungBerechnungAussen.ZahnfußfestigkeitBerechnen();
                lbl_Zahnflankenfestigkeit.Content = geradverzahnungBerechnungAussen.ZahnflankenfestigkeitBerechnen();
                lbl_Volumen.Content = geradverzahnungBerechnungAussen.VolumenBerechnen();



                switch (cb_Berechnen.SelectedIndex)
                {
                    case 1:
                        lbl_Preis.Content = geradverzahnungBerechnungAussen.PreisGeradverzahnungBerechnen34Cr4();
                        lbl_Gewicht.Content = geradverzahnungBerechnungAussen.Gewicht.ToString("0.000");
                        break;

                    case 2:
                        lbl_Preis.Content = geradverzahnungBerechnungAussen.PreisGeradverzahnungBerechnen20MnCr5();
                        lbl_Gewicht.Content = geradverzahnungBerechnungAussen.Gewicht.ToString("0.000");
                        break;

                    case 3:
                        lbl_Preis.Content = geradverzahnungBerechnungAussen.PreisGeradverzahnungBerechnen46Cr2();
                        lbl_Gewicht.Content = geradverzahnungBerechnungAussen.Gewicht.ToString("0.000");
                        break;

                    case 4:
                        lbl_Preis.Content = geradverzahnungBerechnungAussen.PreisGeradverzahnungBerechnen34CrMo4();
                        lbl_Gewicht.Content = geradverzahnungBerechnungAussen.Gewicht.ToString("0.000");
                        break;
                }



                geradverzahnungBerechnungAussen.auftraggeber = tb_Auftraggeber.Text;

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
            Catia_API catia_API = new Catia_API();

            if (catia_API.CatiaLaeuft())
            {



                catia_API.ErzeugePart();

                catia_API.ErzeugeSkizze();

                catia_API.StirnradGeradverzahnungZahn(geradverzahnungBerechnungAussen);

                catia_API.spiegeln();

                catia_API.StirnradGeradverzahnungKreismuster1(geradverzahnungBerechnungAussen);

                catia_API.VerbindungKreismuster();

                catia_API.BlockErzeugen(geradverzahnungBerechnungAussen);






            }


            else
            {
                MessageBox.Show("Catia läuft zum jetzigen Zeitpunkt nicht !");

                //zeichnen.StirnradGeradverzahnungKreismuster(geradverzahnungBerechnung);



                if (MessageBox.Show("Soll Catia gestartet werden ?", "Catia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //Starten der Catia App 
                    string Programmname = "CNEXT.exe";
                    Process.Start(Programmname);
                }



            }
        }
    }
}
