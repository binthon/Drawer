 
#region [ Użyte biblioteki systemowe ]

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

#endregion

namespace Ezn.lekcje.programowanie.wpf
{

    /// <summary>
    /// Implementacja logiki zachowań dla Okno.xaml
    /// </summary>
    public partial class Okno : Window
    {
        #region [ Stałe i zmienne ]

        private struct WlasciwosciZolwia
        {
            public double x;
            public double y;
            public double azymut;
            public bool widoczny;
            ///// <summary>
            ///// Turtle shape: triangle (default) or image
            ///// </summary>
            //public TurtleShapeMode shape;
            ///// <summary>
            ///// Turtle shape color if turtle shape is triangle
            ///// </summary>
            //public LogoColor kolor;
            ///// <summary>
            ///// Turtle image if turtle shape is image
            ///// </summary>
            public string rysunek;
        }
        private struct WlasciwosciPiora
        {
            public int grubosc;
            public string stan;
        }
        private WlasciwosciZolwia _cechyZolwia = default(WlasciwosciZolwia);
        private WlasciwosciPiora _cechyPiora = default(WlasciwosciPiora);

        #endregion
         Random _generator = new Random();

        public Okno()
        {
            try
            {
                InitializeComponent();
                InicjalizujGrafikeZolwia();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }

        #region [ Turtle ]

        #region [ Definicje biblioteki Turtle ]

        private void InicjalizujKanwe()
        {
            try
            {
                Thickness margin = kanwa.Margin;
                margin.Top = 10;
                margin.Bottom = 10;
                margin.Left = 10;
                margin.Right = 10;

                Color canvaColor = new Color(); //create colour
                canvaColor.R = 192;
                canvaColor.B = 192;
                canvaColor.G = 192;
                canvaColor.A = 255; // kanał Alfa - aby kolor nie był przezroczysty
                kanwa.Background = new SolidColorBrush(canvaColor);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }

        private void InicjalizujGrafikeZolwia()
        {
            try
            {
                InicjalizujKanwe();
                InicjalizujDzialaniePrzyciskow();

                _cechyZolwia.x = GetMaxX() / 2;
                _cechyZolwia.y = GetMaxY() / 2;
                _cechyZolwia.azymut = 90;
                _cechyZolwia.widoczny = true;
                _cechyZolwia.rysunek = @"obrazy\TurtleLOGO.png";

                obrazZolwia.SetValue(Canvas.LeftProperty, _cechyZolwia.x - 9);
                obrazZolwia.SetValue(Canvas.TopProperty, _cechyZolwia.y - 9);
                obrazZolwia.Visibility = Visibility.Visible;

                _cechyPiora.grubosc = 1;
                _cechyPiora.stan = "opuszczone";
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }

        private void InicjalizujDzialaniePrzyciskow()
        {
            try
            {
                przyciskWykonaj.Click += new RoutedEventHandler(przyciskWykonaj_Click);
                przyciskWyczysc.Click += new RoutedEventHandler(przyciskWyczysc_Click);
                przyciskZamknij.Click += new RoutedEventHandler(przyciskZamknij_Click);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }

        void przyciskWyczysc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Line line = default(Line);
                foreach (UIElement element in kanwa.Children)
                {
                    if (element.GetType()==line.GetType()) kanwa.Children.Remove(element);    
                }                
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }

        void przyciskZamknij_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }

        void przyciskWykonaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Rysowanie();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }
        
        public void RysujLinie(Point doPunktu)
        {
            try
            {
                if (_cechyPiora.stan == "opuszczone")
                {
                    Line linia = new Line();
                    linia.Stroke = System.Windows.Media.Brushes.Blue;
                    linia.X1 = _cechyZolwia.x;
                    linia.X2 = doPunktu.X;
                    linia.Y1 = _cechyZolwia.y;
                    linia.Y2 = doPunktu.Y;
                    linia.HorizontalAlignment = HorizontalAlignment.Left;
                    linia.VerticalAlignment = VerticalAlignment.Center;
                    linia.StrokeThickness = _cechyPiora.grubosc;
                    kanwa.Children.Add(linia);
                }
                else if (_cechyPiora.stan == "gumka")
                {
                    Line linia = new Line();
                    linia.Stroke = System.Windows.Media.Brushes.LightGray;
                    linia.X1 = _cechyZolwia.x;
                    linia.X2 = doPunktu.X;
                    linia.Y1 = _cechyZolwia.y;
                    linia.Y2 = doPunktu.Y;
                    linia.HorizontalAlignment = HorizontalAlignment.Left;
                    linia.VerticalAlignment = VerticalAlignment.Center;
                    linia.StrokeThickness = _cechyPiora.grubosc;
                    kanwa.Children.Add(linia);
                }
                UstanowOstatniePolozenie(doPunktu);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }

        public double GetMaxX() { return this.Width; }

        public double GetMaxY() { return this.Height; }

        private void UstanowOstatniePolozenie(Point punkt)
        {
            try
            {
                _cechyZolwia.x = punkt.X;
                _cechyZolwia.y = punkt.Y;
                obrazZolwia.SetValue(Canvas.LeftProperty, punkt.X - 9);
                obrazZolwia.SetValue(Canvas.TopProperty, punkt.Y - 9);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }

        #endregion

        #region [ Rozkazy LOGO ]

        private void Home()
        {
            try
            {
                Point punkt = new Point();
                punkt.X = this.Width / 2;
                punkt.Y = this.Height / 2;
                UstanowOstatniePolozenie(punkt);
                SetH(0);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }
        private void home() { Home(); }

        private void Cs()
        {
            try
            {
                Home();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }
        private void cs() { Cs(); }

        private void Fd(double ile)
        {
            try
            {
                Point punktDocelowy = new Point();
                punktDocelowy.X = _cechyZolwia.x + ile * Math.Cos(_cechyZolwia.azymut / 180 * Math.PI);
                punktDocelowy.Y = _cechyZolwia.y - ile * Math.Sin(_cechyZolwia.azymut / 180 * Math.PI);
                RysujLinie(punktDocelowy);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }
        private void fd(double ile) { Fd(ile); }

        private void Rt(double kat)
        {
            try
            {
                _cechyZolwia.azymut -= kat;
                Transform obrot = new RotateTransform(90-_cechyZolwia.azymut);
                obrazZolwia.LayoutTransform = obrot;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }
        private void rt(double kat) { Rt(kat); }

        private void Lt(double kat)
        {
            try
            {
                _cechyZolwia.azymut += kat;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }
        private void lt(double kat) { Lt(kat); }

        private void Pu()
        {
            _cechyPiora.stan = "podniesione";
        }
        private void pu() { Pu(); }

        private void Pd()
        {
            _cechyPiora.stan = "opuszczone";
        }
        private void pd() { Pd(); }

        private void Pe()
        {
            _cechyPiora.stan = "gumka";
        }
        private void pe() { Pe(); }

        private void St()
        {
            _cechyZolwia.widoczny = true;
            obrazZolwia.Visibility = Visibility.Visible;
        }
        private void st() { St(); }

        private void Ht()
        {
            _cechyZolwia.widoczny = false;
            obrazZolwia.Visibility = Visibility.Hidden;
        }
        private void ht() { Ht(); }

        private void SetH(double kat)
        {
            try
            {
                _cechyZolwia.azymut = 90 - kat;
                Transform obrot = new RotateTransform(kat);
                obrazZolwia.LayoutTransform = obrot;
            }
            catch (Exception ex)
            {                
                throw new NotImplementedException(ex.ToString());
            }
        }
        private void seth(double kat) { SetH(kat); }

        #endregion

        #region [ 3D ]

        #region [ Stałe i zmienne 3D ]

        private double _skalaXGrafiki2D = 1.0;
        private double _skalaYGrafiki2D = 1.0;

        private double _katOsiX = 0;
        private double _katOsiY = 0;
        private double _katOsiZ = 0;

        private double _podzialkaX = 0;
        private double _podzialkaY = 0;
        private double _podzialkaZ = 0;

        #endregion

        public void Gora (double ile) { SetH(_katOsiY); Fd(ile * _podzialkaY); }
        public void Prawo(double ile) { SetH(_katOsiX); Fd(ile * _podzialkaX); }
        public void Przod(double ile) { SetH(_katOsiZ); Fd(ile * _podzialkaZ); }

        public void KatyOsi(double katOsiX, double katOsiY, double katOsiZ)
        {
            _katOsiX = katOsiX; _katOsiY = katOsiY; _katOsiZ = katOsiZ;
        }

        public void Podzialki(double podzialkaX, double podzialkaY, double podzialkaZ)
        {
            _podzialkaX = podzialkaX; _podzialkaY = podzialkaY; _podzialkaZ = podzialkaZ;
        }

        public void Skaluj(double skala)
        {
            _podzialkaX *= skala;
            _podzialkaY *= skala;
            _podzialkaZ *= skala;
        }

        public void Uklad()
        {
            Gora(500); Gora(-500); Prawo(500); Prawo(-500); Przod(500); Przod(-500);
        }

        public void Wektor(double x, double y, double z)
        {
            double deltaX = _skalaXGrafiki2D * (x * _podzialkaX * Math.Sin(_katOsiX * Math.PI / 180) +
                                                y * _podzialkaY * Math.Sin(_katOsiY * Math.PI / 180) +
                                                z * _podzialkaZ * Math.Sin(_katOsiZ * Math.PI / 180));
            double deltaY = _skalaYGrafiki2D * (x * _podzialkaX * Math.Cos(_katOsiX * Math.PI / 180) +
                                                y * _podzialkaY * Math.Cos(_katOsiY * Math.PI / 180) +
                                                z * _podzialkaZ * Math.Cos(_katOsiZ * Math.PI / 180));

            Point punktDocelowy = new Point();
            punktDocelowy.X = _cechyZolwia.x + deltaX;
            punktDocelowy.Y = _cechyZolwia.y - deltaY;
            RysujLinie(punktDocelowy);
        }

        /// <summary>
        /// Procedura nadaje wartości kątom osi oraz ustala podziałki.
        /// Zaimplementowane rzuty: P - Prostokatny, J - Jednomiarowy, W - Wojskowy, T - Trzy_Czwarte, G - Z_Gory, B - Z_Boku, Z - Z_Przodu.
        /// </summary>
        /// <param name="typRzutu">P J W T G B Z - wielką literą</param>
        public void Rzut(char typRzutu)
        {
            switch (typRzutu)
            {
                case 'p':
                case 'P': KatyOsi( 90, 0, 225); Podzialki(1, 1, 0.5 ); break;
                case 'j':
                case 'J': KatyOsi(120, 0, 240); Podzialki(1, 1, 1   ); break;
                case 'w':
                case 'W': KatyOsi(135, 0, 225); Podzialki(1, 1, 1   ); break;
                case 't':
                case 'T': KatyOsi(100, 0, 235); Podzialki(1, 1, 0.75); break;
                case 'g':
                case 'G': KatyOsi(270, 0, 180); Podzialki(1, 0, 1   ); break;
                case 'b':
                case 'B': KatyOsi(  0, 0,  90); Podzialki(0, 1, 1   ); break;
                case 'z':
                case 'Z': KatyOsi(270, 0,   0); Podzialki(1, 1, 0   ); break;
                default:
                    System.Windows.MessageBox.Show("Nie znam takiegu rzutu [" + typRzutu.ToString() + "].", "Błąd. Nieprawidłowy typ rzutu", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    KatyOsi(0, 0, 0); Podzialki(0, 0, 0); break;
            }
        }

        /// <summary>
        /// Procedura rysuje obiekt w trzech rzutach: Z boku, Z przodu i Z góry
        /// </summary>
        /// <param name="wspolczynnikSkali">Współczynnik skali pozwala powiększać obiekty bez zmiany ich definicji</param>
        private void Rzuty_Mongea(double wspolczynnikSkali)
        {
            Cs(); Fd(500); Fd(-1000); Fd(500); Rt(90); Fd(500); Fd(-1000); Fd(-500);

            Rzut('Z'); Skaluj(wspolczynnikSkali); Pu(); Home(); Fd( 30); Rt(-90); Fd(30); Pd(); //Dom();//Wieza();//Kostka(100,200,50);
            Rzut('B'); Skaluj(wspolczynnikSkali); Pu(); Home(); Fd( 30); Rt( 90); Fd(30); Pd(); //Dom();//Wieza();//Kostka(100,200,50);
            Rzut('G'); Skaluj(wspolczynnikSkali); Pu(); Home(); Fd(-30); Rt(-90); Fd(30); Pd(); //Dom();//Wieza();//Kostka(100,200,50);
        }

        #endregion

        #endregion      

        #region [ Definicje ucznia ]

        public void Rysowanie()
        {
            try
            {
                //Fraktale();
                //Łąka(2);
                UstawienieZolwia();
                Wieża(50, 80, 30);
                Miasto(10);



            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());
            }
        }

        private void UstawienieZolwia()
        {
            rt(90);
            pu();
            fd(500);
            rt(90);
            fd(300);
            rt(180);
            pd();
        }

        private void Miasto(int ile)
        {
            for (int i = 0; i < ile; i++)
            {
                double losowaX = _generator.Next(30, 100);
                double losowaY = _generator.Next(30, 100);
                double losowaZ = _generator.Next(30, 100);
                double losowaOkap = 10;
                double losowaPoziom = _generator.Next(-200, 200);
                double losowaPion = _generator.Next(-200,200);
                Rzut('T');
                Skok(losowaPoziom, losowaPion);
                Dom(losowaX, losowaY, losowaZ, losowaOkap);
                Skok(-losowaPoziom, -losowaPion);
            }
            
        }

        private void Skok(double losowaPoziom, double losowaPion)
        {
            pu();
            fd(losowaPion);
            rt(90);
            fd(losowaPoziom);
            rt(-90);
            pd();
        }
        private void Dom(double skokX, double skokY, double skokZ, double okap)
        {
            Kostka(skokX, skokY, skokZ);
            Gora(skokY);
            ProstokątXZ(skokX,skokZ,okap);
            Dach(skokX+2*okap, skokY+2*okap, skokZ+2*okap);
        }

        private void Dach(double skokX, double skokY, double skokZ)
        {
            Wektor(-skokX / 4, skokY, -skokZ / 2);
            Wektor(skokX / 4, -skokY, -skokZ / 2);
            Prawo(-skokX);
            Wektor(skokX / 4, skokY, skokZ / 2);
            Prawo(skokX / 2);
            Prawo(-skokX / 2);
            Wektor(-skokX / 4, -skokY, skokZ / 2);
        }

        private void ProstokątXZ(double skokX, double skokZ, double okap)
        {

            
            pu();
            Prawo(okap);
            pd(); 
            Przod(-skokZ - okap);
            Prawo(-skokX - 2 * okap);
            Przod(skokZ + 2 * okap);
            Prawo(skokX + 2 * okap);
            Przod(-okap);
            Przod(okap);
        }

        private void Wieża(double skokX, double skokY, double skokZ)
        {
            Rzut('T');
            Kostka2(skokX, skokY, skokZ);
            Ostrosłup(skokX, skokY, skokZ);
            Gora(-skokY);
            Przod(-skokZ);
            Gora(0);
        }

        private void Ostrosłup(double skokX, double skokY, double skokZ)
        {
            Gora(skokY);
            Wektor(-skokX / 2, skokY, -skokZ / 2);
            Wektor(-skokX / 2, -skokY, -skokZ / 2);
            Prawo(skokX);
            Wektor(-skokX / 2, skokY, skokZ / 2);
            Wektor(-skokX / 2, -skokY, skokZ / 2);
        }

        private void Kostka(double skokX, double skokY, double skokZ)
        {
            Tył(skokX, skokY);
            Prawa(skokY, skokZ);
            Front(skokX, skokY);
            Lewa(skokY, skokZ);
        }
        private void Kostka2(double skokX, double skokY, double skokZ)
        {
            Tył(skokX, skokY);
            Prawa(skokY, skokZ);
            Front(skokX, skokY);
            Lewa(skokY, skokZ);
        }
        private void Lewa(double skokY, double skokZ)
        {
            Gora(skokY);
            Przod(-skokZ);
            Gora(-skokY);
            Przod(skokZ);
        }

        private void Front(double skokX, double skokY)
        {
            Prawo(skokX);
            Gora(skokY);
            Prawo(-skokX);
            Gora(-skokY);
            Prawo(skokX);
        }

        private void Prawa(double skokY, double skokZ)
        {
            Gora(skokY);
            Przod(skokZ);
            Gora(-skokY);
            Przod(-skokZ);
            Przod(skokZ);

        }

        private void Tył(double skokX, double skokY)
        {
            Prawo(skokX);
            Gora(skokY);
            Prawo(-skokX);
            Gora(-skokY);
        }

        private void Fraktale()
        {
            pu();
            rt(-180);
            fd(150);
            rt(180);
            pd();
            Płatek(4, 100);
            rt(-150);
            pu();
            fd(150);
            rt(-90);
            pd();
            DrzewoB(4, 150);
            rt(90);
            pu();
            fd(150);
            rt(90);
            pd();
            Pitagoras(4, 100);
        }

        private void Płatek(int stopien, double dlugosc)
        {
            Fraktal(stopien, dlugosc);
            rt(120);
            Fraktal(stopien, dlugosc);
            rt(120);
            Fraktal(stopien, dlugosc);
        }
        public void Fraktal(int stopien, double dlugosc)
        {
            if (stopien == 1)
            {

                fd(dlugosc);
            }
            else
            {
                Fraktal(stopien - 1, dlugosc / 3); rt(-60);
                Fraktal(stopien - 1, dlugosc / 3); rt(120);
                Fraktal(stopien - 1, dlugosc / 3); rt(-60);
                Fraktal(stopien - 1, dlugosc / 3);

            }
        }
        public void DrzewoB(int stopien, double dlugosc)
        {
            if (stopien > 0)
            {
                fd(dlugosc);
                rt(-30);
                DrzewoB(stopien - 1, dlugosc / 3);
                rt(60);
                DrzewoB(stopien - 1, dlugosc / 3);
                rt(-30);
                fd(-dlugosc);
            }
        }

        public void Pitagoras(int stopien, double dl)
        {
            if (stopien == 1)
            {
                fd(dl);
                rt(90);
                fd(dl);
                rt(90);
                fd(dl);
            }
            else
            {
                fd(dl);
                rt(-30);
                Pitagoras(stopien - 1, dl * Math.Sqrt(3) / 2);
                rt(-90);
                Pitagoras(stopien - 1, dl / 2);
                rt(-60);
                fd(dl);
            }
        }
        private void Łąka(int ile)
        {
            
            for (int i = 0; i < ile; i++)
            {

                int losowa = _generator.Next(30, 150);
                int losowaX = _generator.Next(-150, 150);
                int losowaY = _generator.Next(-150, 150);
                Przeskok(losowaX, losowaY);
                Kwiat(losowa);
                Przeskok(-losowaX, -losowaY);
                
            }

        }

        private void Przeskok(int poziom, int pion)
        {
            pu();
            fd(pion);
            rt(-90);       
            fd(poziom);
            rt(90);
            pd();
        }

        private void Kwiat(int dlugosc)
        {
            fd(dlugosc*0.5);
            Liść(dlugosc*0.2);
            fd(dlugosc*0.5);
            RozetaP(15, dlugosc, 4);
            rt(-180);
            fd(dlugosc);
            rt(180);
        }


        private void Liść(double dlugosc)
        {
            rt(60);
            fd(dlugosc);
            rt(60);
            fd(dlugosc);
            rt(120);
            fd(dlugosc);
            rt(60);
            fd(dlugosc);
            rt(60);
        }

        

        private void RozetaP(int figury,int dlugosc, int katy)
        {
            for (int i = 0; i < figury; i++)
            {
                Wielokąt(katy,dlugosc*0.2);
                rt(360 / figury);
            }

        }



        private void Wielokąt(int ile, double bok)
        {
            for (int i = 0; i < ile; i++)
            {
                fd(bok);
                rt(360.0 / ile);
            }
        }























        #endregion
    }

}
