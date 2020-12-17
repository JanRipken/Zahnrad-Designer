using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Zahnrad_Designer.Stirnräder.Geradverzahnung_Aussen
{
    class Geradverzahnung_Aussen_Berechnung 
    {
        //Eigenschaften
        public double Zähnezahl { get; set; }
        public double Teilkreisdurchmesser { get; set; }
        public double Breite { get; set; }
        public double Modul { get; set; }
        public double Kopfspiel { get; set; }
        public double Eingriffswinkel { get; set; }
        public double Kopfkreisdurchmesser { get; set; }
        public double Fußkreisdurchmesser { get; set; }
        public double Zahnhöhe { get; set; }
        public double Gewicht { get; set; }
        public double Volumencm { get; set; }
        public double Zahnfußhöhe { get; set; }
        public double Zahnkopfhöhe { get; set; }
        public double Preis { get; set; }
        public string auftraggeber { get; set; }
        public double TeilkreisRadius { get; set; }
        public double Beta { get; set; }
        public double Teilung { get; set; }
        public double Hilfskreisradius { get; set; }
        public double Kopfkreisradius { get; set; }
        public double FußkreisRadius1 { get; set; }
        public double VerrundungsRadius1 { get; set; }
        public double BetaRad { get; set; }
        public double gammaRad { get; set; }
        public double TotalAngle1 { get; set; }
        public double Nullpunkt_x { get; set; }
        public double Nullpunkt_y { get; set; }
        public double gamma { get; set; }
        public double TotalAngletest { get; set; }


        // methoden zur überprüfung ob die eingabeparameter machbar sind 
        internal int MachbarkeitderEingabewerte()
        {
            int n;
            double m, d, z;
            n = 0;

            m = Teilkreisdurchmesser / Zähnezahl;
            d = Modul * Zähnezahl;
            z = Teilkreisdurchmesser / Modul;

            if (m == Modul && d == Teilkreisdurchmesser && z == Zähnezahl)
            {
                n++;
                return 1;
            }
            else
            {
                MessageBox.Show("Der zusammenhang zwischen Modul, Zähnezahl und\nTeilkreisdurchmesser ist so nicht möglich!\nBitte erneut eingeben! ", "ZarCAD", MessageBoxButton.OK);
                return 0;
            }

        }

        internal int KontrolleWertebereichEingriffswinkel()
        {


            if (Eingriffswinkel < 90 && Eingriffswinkel > 0)
            {

                return 1;
            }
            else
            {


                MessageBox.Show("Bitte geben sie für den Eingriffswinkel Zahlen im bereich von 0 bis 90 grad ein!", "ZarCAD", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }

        }

        internal int KontrolleWertebereichKopfspiel()
        {

            if (Kopfspiel <= 0.3 * Modul && Kopfspiel >= 0.1 * Modul)
            {

                return 1;
            }
            else
            {
                MessageBox.Show("ihr Kopfspiel wurde auf 0,167 gesetzt da keine zahl eingegeben wurde", "ZarCAD");
                Kopfspiel = 0.167 * Modul;
                return 1;
            }

        }




        //Methoden zur berechnung der ergebnisse

        internal double ZahnhöheBerechen()
        {


            Zahnhöhe = (2 * Modul) + Kopfspiel;

            return Zahnhöhe;
        }

        internal double ZahnfußhöheBerechnen()
        {


            Zahnfußhöhe = Modul + Kopfspiel;

            return Zahnfußhöhe;
        }

        internal double ZahnkopfhöheBerechnen()
        {


            Zahnkopfhöhe = Modul;

            return Zahnkopfhöhe;
        }

        internal double TeilungBerechnen()
        {


            Teilung = Math.PI * Modul;

            return Math.Round(Teilung, 3);
        }

        internal double FußkreisdurchmesserBerechnen()
        {


            Fußkreisdurchmesser = Teilkreisdurchmesser - (2 * (Modul + Kopfspiel));

            return Fußkreisdurchmesser;
        }

        internal double GrundkreisdurchmesserBerechnen()
        {
            double deg, GrundkreisdurchmesserAusgabe;
            deg = (Eingriffswinkel * (Math.PI)) / 180;
            GrundkreisdurchmesserAusgabe = Modul * Zähnezahl * (Math.Cos(deg));
            return Math.Round(GrundkreisdurchmesserAusgabe, 3);
        }

        internal double KopfkreisdurchmesserBerechnen()
        {
            Kopfkreisdurchmesser = Teilkreisdurchmesser + 2 * Modul;
            return Kopfkreisdurchmesser;
        }

        internal double ZahnfußfestigkeitBerechnen()
        {
            return 0;
        }

        internal double ZahnflankenfestigkeitBerechnen()
        {
            return 0;
        }

        internal void GewichtBerechnen(double dichte)
        {

            Gewicht = Volumencm * dichte;

        }

        internal double VolumenBerechnen()
        {
            double Flächeninhalt, r, Volumenmm;

            r = Kopfkreisdurchmesser / 2;
            Flächeninhalt = Math.PI * (r * r);
            Volumenmm = Flächeninhalt * Breite;
            Volumencm = Volumenmm / 1000;
            return Math.Round(Volumencm, 2);
        }


        //berechnung des preises je nach materialauswahl
        internal double PreisGeradverzahnungBerechnen34Cr4()
        {
            double dichte;
            dichte = 7.72;
            GewichtBerechnen(dichte);
            Preis = Gewicht * 1.5;

            return Math.Round(Preis, 2);
        }

        internal double PreisGeradverzahnungBerechnen20MnCr5()
        {
            double dichte;
            dichte = 7.75;
            GewichtBerechnen(dichte);
            Preis = Gewicht * 2;
            return Math.Round(Preis, 2);
        }

        internal double PreisGeradverzahnungBerechnen46Cr2()
        {
            double dichte;
            dichte = 7.85;
            GewichtBerechnen(dichte);
            Preis = Gewicht * 2.5;
            return Math.Round(Preis, 2);
        }

        internal double PreisGeradverzahnungBerechnen34CrMo4()
        {
            double dichte;
            dichte = 7.9;
            GewichtBerechnen(dichte);
            Preis = Gewicht * 3;
            return Math.Round(Preis, 2);
        }

        internal void TeilkreisRadius1()
        {

            TeilkreisRadius = (Modul * Zähnezahl) / 2;
        }

        internal void HilfskreisRadius()
        {

            Hilfskreisradius = 0.94 * TeilkreisRadius;
        }

        internal void FußkreisRadius()
        {

            FußkreisRadius1 = TeilkreisRadius - 1.25 * Modul;
        }

        internal void KopfkreisRadius()
        {

            Kopfkreisradius = TeilkreisRadius + Modul;
        }

        internal void VerrundungsRadius()
        {

            VerrundungsRadius1 = 0.35 * Modul;
        }

        internal void beta()
        {

            Beta = 90 / Zähnezahl;
            BetaRad = (Math.PI * Beta) / 180;
        }

        internal void Gamma()
        {

            gamma = 90 - (Eingriffswinkel - Beta);
            gammaRad = (Math.PI * gamma) / 180;
        }

        internal void Totalangle1()
        {
            TotalAngletest = (360 / Zähnezahl) / 2;
        }

        internal void TotalAngle()
        {
            double TotalAngle2;
            TotalAngle2 = 360 / Zähnezahl;
            TotalAngle1 = (Math.PI * TotalAngle2) / 180;
        }


        internal void Nullpunkt()
        {

            Nullpunkt_x = 0;
            Nullpunkt_y = 0;
        }


    }
}
