using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MECMOD;
using INFITF;
using PARTITF;
using HybridShapeTypeLib;
using KnowledgewareTypeLib;
using ProductStructureTypeLib;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Runtime.CompilerServices;
//Zum Starten über Anwendung
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Zahnrad_Designer.Stirnräder.Geradverzahnung_Aussen
{
    class Catia_API
    {

        INFITF.Application ZarCAD_CatiaApp;
        MECMOD.PartDocument ZarCAD_CatiaPart;
        MECMOD.Sketch ZarCAD_catia_Profil;




        //Methode für Feststellung ob Catia läuft
        public bool CatiaLaeuft()
        {


            try
            {
                object catiaObject = System.Runtime.InteropServices.Marshal.GetActiveObject("CATIA.Application");

                ZarCAD_CatiaApp = (INFITF.Application)catiaObject;
                return true;
            }

            catch (Exception)
            {
                return false;
            }

        }

        //Methode zur erstellung eines neuen Parts
        internal void ErzeugePart()
        {
            INFITF.Documents catDocuments1 = ZarCAD_CatiaApp.Documents;
            ZarCAD_CatiaPart = (PartDocument)catDocuments1.Add("Part");

        }

        //Erstellen einer neuen Skizze
        internal void ErzeugeSkizze()
        {


            HybridBodies catHybridBodies1 = ZarCAD_CatiaPart.Part.HybridBodies;
            HybridBody catHybridBody1;

            try
            {
                catHybridBody1 = catHybridBodies1.Item("Geometrisches Set.1");
            }
            catch (Exception)
            {
                MessageBox.Show("Kein geometrisches Set gefunden! " + Environment.NewLine + "Ein PART manuell erzeugen und ein darauf achten, dass 'Geometisches Set' aktiviert ist.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            catHybridBody1.set_Name("Profil");
            Sketches catSketch = catHybridBody1.HybridSketches;
            OriginElements catOriginElements = ZarCAD_CatiaPart.Part.OriginElements;
            Reference catReferenz = (Reference)catOriginElements.PlaneYZ;

            ZarCAD_catia_Profil = catSketch.Add(catReferenz);

            ErzeugeAchsensystem();

            ZarCAD_CatiaPart.Part.Update();

        }

        private void ErzeugeAchsensystem()
        {
            object[] arr = new object[] {0.0, 0.0, 0.0,
                                         0.0, 1.0, 0.0,
                                         0.0, 0.0, 1.0 };
            ZarCAD_catia_Profil.SetAbsoluteAxisData(arr);
        }



        internal void StirnradGeradverzahnungZahn(Stirnräder.Geradverzahnung_Aussen.Geradverzahnung_Aussen_Berechnung geradverzahnung_Aussen_Berechnung )
        {
            //Methoden zum errechnen der werte
            geradverzahnung_Aussen_Berechnung.TeilkreisRadius1();
            geradverzahnung_Aussen_Berechnung.HilfskreisRadius();
            geradverzahnung_Aussen_Berechnung.FußkreisRadius();
            geradverzahnung_Aussen_Berechnung.KopfkreisRadius();
            geradverzahnung_Aussen_Berechnung.VerrundungsRadius();
            geradverzahnung_Aussen_Berechnung.beta();
            geradverzahnung_Aussen_Berechnung.Gamma();
            geradverzahnung_Aussen_Berechnung.TotalAngle();
            geradverzahnung_Aussen_Berechnung.Nullpunkt();

            geradverzahnung_Aussen_Berechnung.Totalangle1();

            //Skizze umbenennen und öffnen
            ZarCAD_catia_Profil.set_Name("Stirnrad Geradverzahnung");
            Factory2D catFactory2D1 = ZarCAD_catia_Profil.OpenEdition();



            //Punkte            
            Point2D catP2D_Ursprung = catFactory2D1.CreatePoint(geradverzahnung_Aussen_Berechnung.Nullpunkt_x, geradverzahnung_Aussen_Berechnung.Nullpunkt_y);

            Point2D catP2D_EndpunktKopfkreis_y = catFactory2D1.CreatePoint(0, geradverzahnung_Aussen_Berechnung.Kopfkreisradius);
            catP2D_EndpunktKopfkreis_y.Construction = true;

            Point2D catP2D_EndpunktTeilkreis_y = catFactory2D1.CreatePoint(0, geradverzahnung_Aussen_Berechnung.TeilkreisRadius);
            catP2D_EndpunktTeilkreis_y.Construction = true;

            Point2D catP2D_EndpunktHilfskreis_y = catFactory2D1.CreatePoint(1, geradverzahnung_Aussen_Berechnung.Hilfskreisradius);
            catP2D_EndpunktTeilkreis_y.Construction = true;

            Point2D catP2D_Verrundungspunkt_unten = catFactory2D1.CreatePoint(0, 0);
            catP2D_Verrundungspunkt_unten.Construction = true;

            Point2D catP2D_Verrundungspunkt_oben = catFactory2D1.CreatePoint(0, 0);
            catP2D_Verrundungspunkt_oben.Construction = true;

            Point2D catP2D_Evolventenpunkt_Oben = catFactory2D1.CreatePoint(0, 0);
            catP2D_Verrundungspunkt_oben.Construction = true;

            Point2D catP2D_Evolventenpunkt_Unten = catFactory2D1.CreatePoint(0, 0);
            catP2D_Verrundungspunkt_oben.Construction = true;

            Point2D catP2D_StartPkt_Fußkreis = catFactory2D1.CreatePoint(-geradverzahnung_Aussen_Berechnung.FußkreisRadius1, geradverzahnung_Aussen_Berechnung.FußkreisRadius1);
            catP2D_StartPkt_Fußkreis.Construction = true;



            //Linien
            //Kopfkreislinie COnstruction
            Line2D catL2D_Kopfkreislinie = catFactory2D1.CreateLine(0, 0, 0, geradverzahnung_Aussen_Berechnung.Kopfkreisradius);
            catL2D_Kopfkreislinie.StartPoint = catP2D_Ursprung;
            catL2D_Kopfkreislinie.EndPoint = catP2D_EndpunktKopfkreis_y;
            catL2D_Kopfkreislinie.Construction = true;

            //TEilkreislinie Construction
            Line2D catL2D_Teilkreislinie = catFactory2D1.CreateLine(0, 0, 0, geradverzahnung_Aussen_Berechnung.TeilkreisRadius);
            catL2D_Teilkreislinie.StartPoint = catP2D_Ursprung;
            catL2D_Teilkreislinie.EndPoint = catP2D_EndpunktTeilkreis_y;
            catL2D_Teilkreislinie.Construction = true;

            //Hilfkreislinie Construction
            Line2D catL2D_Hilfskreislinie = catFactory2D1.CreateLine(0, 0, 1, geradverzahnung_Aussen_Berechnung.Hilfskreisradius);
            catL2D_Hilfskreislinie.StartPoint = catP2D_Ursprung;
            catL2D_Hilfskreislinie.EndPoint = catP2D_EndpunktHilfskreis_y;
            catL2D_Hilfskreislinie.Construction = true;

            //hilfskreislinie Fußkreismesserstart
            Line2D catL2D_Fußkreismesserstart = catFactory2D1.CreateLine(0, 0, -1, geradverzahnung_Aussen_Berechnung.FußkreisRadius1);
            catL2D_Fußkreismesserstart.StartPoint = catP2D_Ursprung;
            catL2D_Fußkreismesserstart.EndPoint = catP2D_StartPkt_Fußkreis;
            catL2D_Fußkreismesserstart.Construction = true;



            //Kreise
            //Fußkreis Construction
            Circle2D catC2D_FußkreisConstruction = catFactory2D1.CreateCircle(0, 0, geradverzahnung_Aussen_Berechnung.FußkreisRadius1, 0, 0);
            catC2D_FußkreisConstruction.Construction = true;

            //Hilfskreis Construction
            Circle2D catC2D_HilfskreisConstruction = catFactory2D1.CreateCircle(0, 0, (geradverzahnung_Aussen_Berechnung.Hilfskreisradius), 0, 0);
            catC2D_HilfskreisConstruction.Construction = true;

            //Teilkreis Construction
            Circle2D catC2D_TeilkreisConstruction = catFactory2D1.CreateCircle(0, 0, (geradverzahnung_Aussen_Berechnung.TeilkreisRadius), 0, 0);
            catC2D_TeilkreisConstruction.Construction = true;

            //Kopfkreis Construction
            Circle2D catC2D_KopfkreisConstruction = catFactory2D1.CreateCircle(0, 0, (geradverzahnung_Aussen_Berechnung.Kopfkreisradius), 0, 0);
            catC2D_KopfkreisConstruction.Construction = true;

            //EvolventenKreisConstruction
            Circle2D catC2D_Evolventenkreis = catFactory2D1.CreateCircle(1, geradverzahnung_Aussen_Berechnung.Hilfskreisradius, 10, 0, 0);
            catC2D_Evolventenkreis.StartPoint = catP2D_Evolventenpunkt_Oben;
            catC2D_Evolventenkreis.EndPoint = catP2D_Evolventenpunkt_Unten;
            catC2D_Evolventenkreis.Construction = true;

            //verrundungskreis Construction
            Circle2D catC2D_Verrundungskreis = catFactory2D1.CreateCircle(-geradverzahnung_Aussen_Berechnung.Kopfkreisdurchmesser, 0, 0, 0, 0);
            catC2D_Verrundungskreis.Construction = true;




            //Referenzen

            //punkte
            Reference ref_Hilfskreislininepunkt = (Reference)catP2D_EndpunktHilfskreis_y;
            Reference ref_Teilkreislininepunkt = (Reference)catP2D_EndpunktTeilkreis_y;
            Reference ref_Kopfkreislininepunkt = (Reference)catP2D_EndpunktKopfkreis_y;
            Reference ref_VErrundungspunkt_unten = (Reference)catP2D_Verrundungspunkt_unten;
            Reference ref_VErrundungspunkt_oben = (Reference)catP2D_Verrundungspunkt_oben;
            Reference ref_Evolventenpunktoben = (Reference)catP2D_Evolventenpunkt_Oben;
            Reference ref_Evolventenpunktunten = (Reference)catP2D_Evolventenpunkt_Unten;
            Reference ref_startpunktFußkreispunkt = (Reference)catP2D_StartPkt_Fußkreis;

            //Linien
            Reference ref_Kopfkreislinie = (Reference)catL2D_Kopfkreislinie;
            Reference ref_Teilkreislinie = (Reference)catL2D_Teilkreislinie;
            Reference ref_Hilfskreislinie = (Reference)catL2D_Hilfskreislinie;
            Reference ref_Fußkreismesserstart = (Reference)catL2D_Fußkreismesserstart;

            //ebenenen
            Reference ref_XY_Plane = ZarCAD_CatiaPart.Part.CreateReferenceFromGeometry(ZarCAD_CatiaPart.Part.OriginElements.PlaneXY);
            Reference ref_YZ_Plane = ZarCAD_CatiaPart.Part.CreateReferenceFromGeometry(ZarCAD_CatiaPart.Part.OriginElements.PlaneYZ);
            Reference ref_ZX_Plane = ZarCAD_CatiaPart.Part.CreateReferenceFromGeometry(ZarCAD_CatiaPart.Part.OriginElements.PlaneZX);

            //Ursprung
            Reference ref_Ursprung = (Reference)catP2D_Ursprung;

            //kreise
            Reference ref_Evolventenkreis = (Reference)catC2D_Evolventenkreis;
            Reference ref_Verrundungskreis = (Reference)catC2D_Verrundungskreis;
            Reference ref_CircleKopfkreisConstruction = (Reference)catC2D_KopfkreisConstruction;
            Reference ref_CircleFußkreisConstruction = (Reference)catC2D_FußkreisConstruction;
            Reference ref_CircleTeilkreisConstruction = (Reference)catC2D_TeilkreisConstruction;
            Reference ref_CircleHilfskreisConstruction = (Reference)catC2D_HilfskreisConstruction;



            //Constrainttyp festlegen
            CatConstraintType cCT_On1 = CatConstraintType.catCstTypeOn;
            CatConstraintType CcT_Radius1 = CatConstraintType.catCstTypeRadius;
            CatConstraintType CcT_Angle1 = CatConstraintType.catCstTypeAngle;
            CatConstraintType CcT_Concentric1 = CatConstraintType.catCstTypeConcentricity;
            CatConstraintType CcT_Tangenten1 = CatConstraintType.catCstTypeTangency;



            //Constraints anlegen
            Constraints myConstraints = ZarCAD_catia_Profil.Constraints;



            //Tangentenstetigkeit
            Constraint ConstraintTangenten2 = myConstraints.AddBiEltCst(CcT_Tangenten1, ref_Verrundungskreis, ref_Evolventenkreis);
            Constraint ConstraintTangenten1 = myConstraints.AddBiEltCst(CcT_Tangenten1, ref_Verrundungskreis, ref_CircleFußkreisConstruction);



            //Winkel
            Constraint ConstraintWinkel1 = myConstraints.AddBiEltCst(CcT_Angle1, ref_Kopfkreislinie, ref_Teilkreislinie);
            Constraint ConstraintWinkel2 = myConstraints.AddBiEltCst(CcT_Angle1, ref_Kopfkreislinie, ref_Hilfskreislinie);
            Constraint ConstraintWinkel3 = myConstraints.AddBiEltCst(CcT_Angle1, ref_Kopfkreislinie, ref_XY_Plane);
            Constraint ConstraintWinkel4 = myConstraints.AddBiEltCst(CcT_Angle1, ref_Kopfkreislinie, ref_Fußkreismesserstart);

            Constraint ConstraintFußkreisstartpunktlinie = myConstraints.AddBiEltCst(cCT_On1, ref_Fußkreismesserstart, ref_startpunktFußkreispunkt);



            //Kongurenzen
            Constraint ConstraintonFixierungaufNullpunkt1 = myConstraints.AddTriEltCst(cCT_On1, ref_Ursprung, ref_XY_Plane, ref_YZ_Plane);
            Constraint ConstraintonFixierungaufNullpunkt2 = myConstraints.AddBiEltCst(cCT_On1, ref_Ursprung, ref_ZX_Plane);
            Constraint ConstraintonHilfskreispunkt = myConstraints.AddBiEltCst(cCT_On1, ref_Hilfskreislininepunkt, ref_CircleHilfskreisConstruction);
            Constraint ConstraintonTeilkreispunkt = myConstraints.AddBiEltCst(cCT_On1, ref_Teilkreislininepunkt, ref_CircleTeilkreisConstruction);
            Constraint Constrainton5 = myConstraints.AddBiEltCst(cCT_On1, ref_Teilkreislininepunkt, ref_Evolventenkreis);
            Constraint Constrainton6 = myConstraints.AddBiEltCst(cCT_On1, ref_Kopfkreislininepunkt, ref_CircleKopfkreisConstruction);




            //Evolventenpunkte
            Constraint Constraitevolventenpunkt_oben1 = myConstraints.AddBiEltCst(cCT_On1, ref_Evolventenpunktoben, ref_CircleKopfkreisConstruction);
            Constraint Constraitevolventenpunkt_oben2 = myConstraints.AddBiEltCst(cCT_On1, ref_Evolventenpunktoben, ref_Evolventenkreis);
            Constraint Constraitevolventenpunkt_unten1 = myConstraints.AddBiEltCst(cCT_On1, ref_Evolventenpunktunten, ref_CircleFußkreisConstruction);
            Constraint Constraitevolventenpunkt_unten2 = myConstraints.AddBiEltCst(cCT_On1, ref_Evolventenpunktunten, ref_Evolventenkreis);


            //verrundungspunkte
            Constraint ConstraitVerrundungspunktunten_1_1 = myConstraints.AddBiEltCst(cCT_On1, ref_VErrundungspunkt_unten, ref_Verrundungskreis);
            Constraint ConstraitVerrundungspunktunten_1 = myConstraints.AddBiEltCst(cCT_On1, ref_VErrundungspunkt_unten, ref_CircleFußkreisConstruction);
            Constraint ConstraitVerrundungspunktunten_2_1 = myConstraints.AddBiEltCst(cCT_On1, ref_VErrundungspunkt_oben, ref_Verrundungskreis);
            Constraint ConstraitVerrundungspunktunten_2 = myConstraints.AddBiEltCst(cCT_On1, ref_VErrundungspunkt_oben, ref_Evolventenkreis);




            Constraint ConstraintRadiusHilfskreis = myConstraints.AddMonoEltCst(CcT_Radius1, ref_CircleHilfskreisConstruction);
            Constraint ConstraintRadiusTeilkreis = myConstraints.AddMonoEltCst(CcT_Radius1, ref_CircleTeilkreisConstruction);
            Constraint ConstraintRadiusFußkreis = myConstraints.AddMonoEltCst(CcT_Radius1, ref_CircleFußkreisConstruction);
            Constraint ConstraintRadiusKopfkreis = myConstraints.AddMonoEltCst(CcT_Radius1, ref_CircleKopfkreisConstruction);
            Constraint COnstraintRadiusVerrundungskreis = myConstraints.AddMonoEltCst(CcT_Radius1, ref_Verrundungskreis);


            //Konzentrisch
            Constraint ConstraintKonzentrischFußkreis = myConstraints.AddBiEltCst(CcT_Concentric1, ref_Ursprung, ref_CircleFußkreisConstruction);
            Constraint ConstraintKonzentrischHilfskreis = myConstraints.AddBiEltCst(CcT_Concentric1, ref_Ursprung, ref_CircleHilfskreisConstruction);
            Constraint ConstraintKonzentrischTeilkreis = myConstraints.AddBiEltCst(CcT_Concentric1, ref_Ursprung, ref_CircleTeilkreisConstruction);
            Constraint ConstraintKonzentrischKopfkreis = myConstraints.AddBiEltCst(CcT_Concentric1, ref_Ursprung, ref_CircleKopfkreisConstruction);
            Constraint ConstraintKonzentrischEvolventenkreis = myConstraints.AddBiEltCst(CcT_Concentric1, ref_Hilfskreislininepunkt, ref_Evolventenkreis);





            //Winkle bemaßung
            Dimension Angle1 = ConstraintWinkel1.Dimension;
            Angle1.Value = geradverzahnung_Aussen_Berechnung.Beta;

            Dimension Angle2 = ConstraintWinkel2.Dimension;
            Angle2.Value = geradverzahnung_Aussen_Berechnung.Eingriffswinkel;

            Dimension Angle3 = ConstraintWinkel3.Dimension;
            Angle3.Value = 90;

            Dimension Angle4 = ConstraintWinkel4.Dimension;
            Angle4.Value = geradverzahnung_Aussen_Berechnung.TotalAngletest;



            //Radien bemaßung
            Dimension radiusHilfskreis = ConstraintRadiusHilfskreis.Dimension;
            radiusHilfskreis.Value = geradverzahnung_Aussen_Berechnung.Hilfskreisradius;

            Dimension radiusTeilkreis = ConstraintRadiusTeilkreis.Dimension;
            radiusTeilkreis.Value = geradverzahnung_Aussen_Berechnung.TeilkreisRadius;

            Dimension radiusFußkreiskreis = ConstraintRadiusFußkreis.Dimension;
            radiusFußkreiskreis.Value = geradverzahnung_Aussen_Berechnung.FußkreisRadius1;

            Dimension radiusKopfkreiskreis = ConstraintRadiusKopfkreis.Dimension;
            radiusKopfkreiskreis.Value = geradverzahnung_Aussen_Berechnung.Kopfkreisradius;

            Dimension radiusVerrundungskreis = COnstraintRadiusVerrundungskreis.Dimension;
            radiusVerrundungskreis.Value = geradverzahnung_Aussen_Berechnung.VerrundungsRadius1;




            //Winkel Definition
            ConstraintWinkel3.AngleSector = (CatConstraintAngleSector)2;



            //Evolventenkreis
            Circle2D catC2D_Evolventenkreis1 = catFactory2D1.CreateCircle(0, 0, 5, 0, 0);
            catC2D_Evolventenkreis1.StartPoint = catP2D_Evolventenpunkt_Oben;
            catC2D_Evolventenkreis1.EndPoint = catP2D_Verrundungspunkt_oben;
            catC2D_Evolventenkreis1.Construction = false;

            Reference ref_CircleEvolventenkreis1 = (Reference)catC2D_Evolventenkreis1;

            Constraint ConstraintEvolventenlinie1 = myConstraints.AddBiEltCst(cCT_On1, ref_CircleEvolventenkreis1, ref_VErrundungspunkt_oben);
            Constraint ConstraintEvolventenlinie2 = myConstraints.AddBiEltCst(cCT_On1, ref_CircleEvolventenkreis1, ref_Evolventenpunktoben);
            Constraint ConstraintEvolventenlinie3 = myConstraints.AddBiEltCst(cCT_On1, ref_CircleEvolventenkreis1, ref_Evolventenkreis);




            //Verrundungskreis
            Circle2D catC2D_Verrundungskreis1 = catFactory2D1.CreateCircle(0, 0, 0, 0, 0);
            catC2D_Verrundungskreis1.StartPoint = catP2D_Verrundungspunkt_unten;
            catC2D_Verrundungskreis1.EndPoint = catP2D_Verrundungspunkt_oben;
            catC2D_Verrundungskreis1.Construction = false;

            Reference ref_CircleVerrundungskreis1 = (Reference)catC2D_Verrundungskreis1;

            Constraint ConstraintVerrundungskreis1 = myConstraints.AddBiEltCst(cCT_On1, ref_CircleVerrundungskreis1, ref_VErrundungspunkt_oben);
            Constraint ConstraintVerrundungskreis2 = myConstraints.AddBiEltCst(cCT_On1, ref_CircleVerrundungskreis1, ref_Verrundungskreis);
            Constraint ConstraintVerrundungskreis3 = myConstraints.AddBiEltCst(cCT_On1, ref_CircleVerrundungskreis1, ref_VErrundungspunkt_unten);




            //Fußkreisverbidung
            Circle2D catC2D_Fußkreisverbindung = catFactory2D1.CreateCircle(0, 0, 0, 0, 0);
            catC2D_Fußkreisverbindung.StartPoint = catP2D_Verrundungspunkt_unten;
            catC2D_Fußkreisverbindung.EndPoint = catP2D_StartPkt_Fußkreis;
            catC2D_Fußkreisverbindung.Construction = false;

            Reference ref_CircleFußkreisverbindung = (Reference)catC2D_Fußkreisverbindung;

            Constraint ConstraintFußkreisstartpunkt = myConstraints.AddBiEltCst(cCT_On1, ref_CircleFußkreisConstruction, ref_startpunktFußkreispunkt);
            Constraint ConstraintFußkreisverbindung1 = myConstraints.AddBiEltCst(cCT_On1, ref_CircleFußkreisverbindung, ref_VErrundungspunkt_unten);
            Constraint ConstraintFußkreisverbindung2 = myConstraints.AddBiEltCst(cCT_On1, ref_CircleFußkreisverbindung, ref_CircleFußkreisConstruction);
            Constraint ConstraintFußkreisverbindung3 = myConstraints.AddBiEltCst(cCT_On1, ref_CircleFußkreisverbindung, ref_startpunktFußkreispunkt);




            //Kopfkreis
            Circle2D catC2D_Kopfkreisverbindung = catFactory2D1.CreateCircle(0, 0, 0, 0, 0);
            catC2D_Kopfkreisverbindung.StartPoint = catP2D_EndpunktKopfkreis_y;
            catC2D_Kopfkreisverbindung.EndPoint = catP2D_Evolventenpunkt_Oben;
            catC2D_Kopfkreisverbindung.Construction = false;

            Reference ref_CircleKopfkreisverbindung = (Reference)catC2D_Kopfkreisverbindung;
            Reference ref_LineEndpunktkopfkreis = (Reference)catP2D_EndpunktKopfkreis_y;

            Constraint ConstraintKopfkreisverbindung1 = myConstraints.AddBiEltCst(cCT_On1, ref_CircleKopfkreisverbindung, ref_Evolventenpunktoben);
            Constraint ConstraintKopfkreisverbindung2 = myConstraints.AddBiEltCst(cCT_On1, ref_CircleKopfkreisverbindung, ref_CircleKopfkreisConstruction);
            Constraint ConstraintKopfkreisverbindung3 = myConstraints.AddBiEltCst(cCT_On1, ref_CircleKopfkreisverbindung, ref_LineEndpunktkopfkreis);



            ZarCAD_catia_Profil.CloseEdition();

            ZarCAD_CatiaPart.Part.Update();
        }

        internal void spiegeln()
        {

            //Symmetrie

            Part myPart = ZarCAD_CatiaPart.Part;
            HybridShapeFactory HSF = (HybridShapeFactory)ZarCAD_CatiaPart.Part.HybridShapeFactory;
            HybridBodies HBS = myPart.HybridBodies;
            HybridBody HBY = HBS.Item("Profil");
            Sketches sketches = HBY.HybridSketches;
            Sketch sketch = sketches.Item("Stirnrad Geradverzahnung");

            Reference Halbzahn = myPart.CreateReferenceFromObject(sketch);

            OriginElements originElements = myPart.OriginElements;

            HybridShapePlaneExplicit hybridShapePlaneExplicit = (HybridShapePlaneExplicit)originElements.PlaneZX;

            Reference Halbzahnspiegelung = myPart.CreateReferenceFromObject(hybridShapePlaneExplicit);

            HybridShapeSymmetry HSS = HSF.AddNewSymmetry(Halbzahn, Halbzahnspiegelung);

            HSS.VolumeResult = false;

            HBY.AppendHybridShape(HSS);

            myPart.InWorkObject = HSS;

            myPart.Update();




            //Verbinden

            Part myPart1 = ZarCAD_CatiaPart.Part;
            HybridShapeFactory HSF1 = (HybridShapeFactory)myPart1.HybridBodies;
            HybridBodies bodies1 = myPart1.HybridBodies;
            HybridBody body1 = bodies1.Item("Profil");
            HybridShapes HS1 = body1.HybridShapes;
            HybridShapeSymmetry HSS1 = (HybridShapeSymmetry)HS1.Item("Symmetrie.1");

            Reference symm = myPart1.CreateReferenceFromObject(HSS1);
            Sketches sketches1 = body1.HybridSketches;
            Sketch sketch1 = sketches1.Item("Stirnrad Geradverzahnung");
            Reference teil = myPart1.CreateReferenceFromObject(sketch1);
            HybridShapeAssemble HSA1 = HSF1.AddNewJoin(symm, teil);

            HSA1.SetConnex(true);
            HSA1.SetManifold(true);
            HSA1.SetSimplify(false);
            HSA1.SetSuppressMode(false);
            HSA1.SetDeviation(0.001000);
            HSA1.SetAngularToleranceMode(false);
            HSA1.SetAngularTolerance(0.500000);
            HSA1.SetFederationPropagation(0);

            Bodies bodies = myPart1.Bodies;
            Body myBody = bodies.Add();
            myBody.set_Name("Zahnrad");
            myBody.InsertHybridShape(HSA1);


            myPart1.Update();


        }



        internal void StirnradGeradverzahnungKreismuster1(Stirnräder.Geradverzahnung_Aussen.Geradverzahnung_Aussen_Berechnung geradverzahnung_Aussen_Berechnung)
        {

            //Kreismuster
            Part myPart = ZarCAD_CatiaPart.Part;
            ShapeFactory SF = (ShapeFactory)myPart.ShapeFactory;
            Factory2D Factory2D1 = ZarCAD_catia_Profil.Factory2D;

            Reference ref1 = myPart.CreateReferenceFromName("");
            Reference ref2 = myPart.CreateReferenceFromName("");

            CircPattern Kreismuster = SF.AddNewSurfacicCircPattern(Factory2D1, 1, 2, 20, 45, 1, 1, ref1, ref2, true, 0, true, false);
            Kreismuster.CircularPatternParameters = CatCircularPatternParameters.catInstancesandAngularSpacing;
            AngularRepartition Angularepartition1 = Kreismuster.AngularRepartition;
            AngularRepartition Angularepartition2 = Kreismuster.AngularRepartition;
            Angle angle1 = Angularepartition2.AngularSpacing;
            angle1.Value = Convert.ToDouble(360 / Convert.ToDouble(geradverzahnung_Aussen_Berechnung.Zähnezahl));
            IntParam intParam1 = Angularepartition1.InstancesCount;
            intParam1.Value = Convert.ToInt32(geradverzahnung_Aussen_Berechnung.Zähnezahl);

            Bodies bodies1 = myPart.Bodies;
            Body body1 = bodies1.Item("Zahnrad");
            HybridShapes HS = body1.HybridShapes;
            HybridShapeAssemble HSA = (HybridShapeAssemble)HS.Item("Verbindung.1");

            Kreismuster.ItemToCopy = HSA;

            OriginElements originElements = myPart.OriginElements;
            HybridShapePlaneExplicit HSPE = (HybridShapePlaneExplicit)originElements.PlaneYZ;

            Reference ref3 = myPart.CreateReferenceFromObject(HSPE);
            Kreismuster.SetRotationAxis(ref3);

            myPart.UpdateObject(Kreismuster);
        }

        internal void VerbindungKreismuster()
        {
            //verbinden des kreismusters

            Part myPart = ZarCAD_CatiaPart.Part;
            HybridShapeFactory HSF = (HybridShapeFactory)myPart.HybridShapeFactory;
            Bodies bodies = myPart.Bodies;
            Body body = bodies.Item("Zahnrad");
            Shapes shapes = body.Shapes;

            Reference ref1 = (Reference)shapes.Item("Kreismuster.1");

            HybridShapes hybridShapes = body.HybridShapes;
            HybridShapeAssemble HSA = (HybridShapeAssemble)hybridShapes.Item("Verbindung.1");

            Reference ref2 = myPart.CreateReferenceFromObject(HSA);

            HybridShapeAssemble HSA2 = HSF.AddNewJoin(ref1, ref2);

            HSA2.SetConnex(true);
            HSA2.SetManifold(true);
            HSA2.SetSimplify(false);
            HSA2.SetSuppressMode(false);
            HSA2.SetDeviation(0.001000);
            HSA2.SetAngularToleranceMode(false);
            HSA2.SetAngularTolerance(0.500000);
            HSA2.SetFederationPropagation(0);

            body.InsertHybridShape(HSA2);
            myPart.InWorkObject = HSA2;

            myPart.Update();

        }



        internal void BlockErzeugen(Stirnräder.Geradverzahnung_Aussen.Geradverzahnung_Aussen_Berechnung geradverzahnung_Aussen_Berechnung)
        {

            //block erzeugen aus kreismuster und der verbindung

            Part myPart = ZarCAD_CatiaPart.Part;
            ShapeFactory SF = (ShapeFactory)myPart.ShapeFactory;

            Reference ref1 = myPart.CreateReferenceFromName("");

            Pad pad1 = SF.AddNewPadFromRef(ref1, geradverzahnung_Aussen_Berechnung.Breite);
            Bodies bodies1 = myPart.Bodies;
            Body body1 = bodies1.Item("Zahnrad");

            HybridShapes HS = body1.HybridShapes;
            HybridShapeAssemble HSA1 = (HybridShapeAssemble)HS.Item("Verbindung.2");

            Reference ref2 = myPart.CreateReferenceFromObject(HSA1);
            pad1.SetProfileElement(ref2);

            Reference ref3 = myPart.CreateReferenceFromObject(HSA1);
            pad1.SetProfileElement(ref3);

            pad1.SetProfileElement(ref3);

            myPart.UpdateObject(pad1);


        }

    }
}
