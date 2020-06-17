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

using System.Collections;

namespace WpfApp2
{
    public struct Vershina
        {
            public Ellipse ellipse;
            public int x;
            public int y;
            public int kolvr;
           
        };
        public struct Rebro
        {
            public Line line;
            public Vershina ver1;
            public Vershina ver2;
        };
    public partial class MainWindow : Window
    {
        int kolvv;
        int kolvrb;
        static int kolv = 0;
        static int kolvr = 0;
        Vershina []ver ;
        Rebro[] reb ;
        int[,] msme;      
        bool SelectVer;
        bool SelectReb;
        bool SelectO;
        static bool selectver;
        static int x1;
        static int y1;
        static int vershid;

        public void ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectReb==true&&kolvr<kolvrb&& sender + "" != "System.Windows.Shapes.Line")
            {
                if (selectver != true)
                {
                     Ellipse ellipse = (Ellipse)sender;
                     int i =(int) ellipse.Tag;
                     selectver = true;
                     x1 = ver[i].x+20;
                     y1 = ver[i].y + 20;
                    vershid = i;
                    if (ver[vershid].kolvr == 3) { selectver = false; MessageBox.Show("Эта вершина имеет 3 ребра"); return; }
                    ver[i].ellipse.Stroke = new SolidColorBrush(Colors.Red);                    
                }
                if(selectver == true)
                {
                    Ellipse ellipse = (Ellipse)sender;
                    int i = (int)ellipse.Tag;
                    if (x1 != ver[i].x && y1  != ver[i].y + 20)
                    {
                        if(ver[i].kolvr==3 || ver[vershid].kolvr==3) { MessageBox.Show("Эта вершина имеет 3 ребра"); return; }
                        foreach (var item in reb)
                        {                           
                            if (item.line != null)
                            if (((item.line.X1==x1|| item.line.X1 == ver[i].x + 20) &&(item.line.X2== ver[i].x + 20 || item.line.X2 ==x1))&&(( item.line.Y1 == y1 || item.line.Y1 == ver[i].y + 20) && (item.line.Y2 == ver[i].y + 20|| item.line.Y2 == y1))) { MessageBox.Show("Между вершинами существует ребро"); return; }
                        }
                        reb[kolvr].line = new Line();
                        reb[kolvr].line.MouseDown += ellipse_MouseDown;
                        reb[kolvr].line.X1 = x1;
                        reb[kolvr].line.Y1 = y1;
                        reb[kolvr].line.X2 = ver[i].x + 20;
                        reb[kolvr].line.Y2 = ver[i].y + 20;
                        reb[kolvr].line.StrokeThickness = 2;
                        reb[kolvr].line.Stroke = Brushes.Black;
                        Canvas.Children.Add(reb[kolvr].line);                        
                        ver[i].kolvr = ver[i].kolvr + 1;
                        ver[vershid].kolvr = ver[vershid].kolvr + 1;
                        reb[kolvr].ver1 = ver[vershid];
                        reb[kolvr].ver2 = ver[i];
                        reb[kolvr].line.Tag = kolvr;
                        selectver = false;
                        ver[vershid].ellipse.Stroke = new SolidColorBrush(Colors.Black);
                        ver[i].ellipse.Stroke = new SolidColorBrush(Colors.Black);                                            
                        kolvr++;
                    }
                }
            }
            if (SelectO == true)
            {
                if(sender+""== "System.Windows.Shapes.Line")
                {
                    Line line = (Line)sender;
                    int i = (int)line.Tag;
                    TI2.Text = "Ребро \n";
                    TI2.Text += "Номер 1 вершины - " + reb[i].ver1.ellipse.Tag + "\n Координаты \n x:y " + reb[i].ver1.x + ":" + reb[i].ver1.y + "\n Номер 2 вершины - " + reb[i].ver2.ellipse.Tag + "\n Координаты \n x:y " + reb[i].ver2.x + ":" + reb[i].ver2.y;
                }                
                if (sender + "" == "System.Windows.Shapes.Ellipse")
                {
                    Ellipse ellipse = (Ellipse)sender;
                    int i = (int)ellipse.Tag;
                    TI2.Text = "Вершина \n";
                    TI2.Text += "Номер вершины - "+i+"\n Количество ребер - "+ ver[i].kolvr+"\n Координаты \n x:y "+ver[i].x+":"+ver[i].y;
                }
            }
        }
        public MainWindow()
        {           
            InitializeComponent();
        }    
            private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);           
            if (selectver==true && ((Math.Abs(ver[vershid].x - (p.X-20)) > 15) || (Math.Abs(ver[vershid].y - (p.Y - 20)) > 15)))
            {             
                selectver = false;
                ver[vershid].ellipse.Stroke = new SolidColorBrush(Colors.Black);
            }           
            if (SelectVer == true)
            {
                if (kolv < kolvv)
                {             
                    int x = (int)p.X - 20;
                    int y = (int)p.Y - 20;
                    foreach (var item in ver)
                    {
                        if ((Math.Abs(x - item.x) < 60) && (Math.Abs(y - item.y) < 60)) { MessageBox.Show("Очень близко"); return; }
                    }
                    ver[kolv].ellipse = new Ellipse();
                    Canvas.Children.Add(ver[kolv].ellipse);
                    Canvas.SetLeft(ver[kolv].ellipse, x);
                    Canvas.SetTop(ver[kolv].ellipse, y);
                    ver[kolv].ellipse.MouseDown += ellipse_MouseDown;
                    ver[kolv].ellipse.Height = 40;
                    ver[kolv].ellipse.Width = 40;
                    ver[kolv].x = x;
                    ver[kolv].y = y;
                    SolidColorBrush blackBrush = new SolidColorBrush();
                    blackBrush.Color = Colors.Black;
                    ver[kolv].ellipse.StrokeThickness = 1;
                    ver[kolv].ellipse.Stroke = new SolidColorBrush(Colors.Black);
                    ver[kolv].ellipse.Fill = new SolidColorBrush(Colors.White);
                    ver[kolv].ellipse.Tag = kolv;
                    ver[kolv].kolvr = 0;
                    kolv++;
                }
                else MessageBox.Show("Все вершины созданы");
            }
        }
        private void V1_Click(object sender, RoutedEventArgs e)
        {
            SelectVer = true;
            SelectReb = false;
            SelectO = false;
            TI1.Text = "Вершины \n";
        }
        private void R1_Click(object sender, RoutedEventArgs e)
        {
            SelectVer = false;
            SelectReb = true;
            SelectO = false;
            TI1.Text = "Ребра \n";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TB1.Text != "") kolvv = int.Parse(TB1.Text);
            else kolvv = 4;
            if (kolvv%2!=0) { MessageBox.Show("Оно нечётное", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            if (kolvv < 3) { MessageBox.Show("Оно < 4", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            ver = new Vershina[kolvv];
            kolvrb = kolvv + kolvv / 2;
            reb = new Rebro[kolvrb];
            Canvas.Visibility = Visibility.Visible;
            V1.Visibility = Visibility.Visible;
            R1.Visibility = Visibility.Visible;
            O1.Visibility = Visibility.Visible;
            TB1.Visibility = Visibility.Hidden;
            TB2.Visibility = Visibility.Hidden;
            TB3.Visibility = Visibility.Hidden;
            TI1.Visibility = Visibility.Visible;
            TI2.Visibility = Visibility.Visible;
            T1.Visibility = Visibility.Visible;
            P1.Visibility = Visibility.Visible;
        }
        public void TextBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsNumber(Convert.ToChar(e.Text))) e.Handled = true;
        }

        private void O1_Click(object sender, RoutedEventArgs e)
        {
            SelectVer = false;
            SelectReb = false;
            SelectO = true;
            TI1.Text = "Осмотр \n";
        }

        private void T1_Click(object sender, RoutedEventArgs e)
        {
            TI2.Text = "";
            if(msme==null)
            msme = new int[kolvv, kolvv];
            for (int i = 0; i < kolvv; i++)
                for (int j = 0; j < kolvv; j++)
                    msme[i, j] = 0;
            foreach (var item in reb)
            {
                if (item.line != null)
                {
                    msme[(int)item.ver1.ellipse.Tag, (int)item.ver2.ellipse.Tag] = 1;
                    msme[(int)item.ver2.ellipse.Tag, (int)item.ver1.ellipse.Tag] = 1;
                }
            }
            for (int i = 0; i < kolvv; i++)
            {
                for (int j = 0; j < kolvv; j++)
                {                    
                    TI2.Text += msme[i, j] + " ";
                }
                TI2.Text += "\n";
            }
            TI1.Text = "Матрица смежности";
        }

     
        private void P1_Click(object sender, RoutedEventArgs e)
        {
            this.T1_Click(sender, e);
            if (kolv != kolvv || kolvr != kolvrb) { MessageBox.Show("Граф не создан до конца");  return; }
            foreach (var item in ver)
            {
                Canvas.Children.Remove(item.ellipse);
            }
            foreach (var item in reb)
            {
                Canvas.Children.Remove(item.line);
            }
            put win2 = new put();
            win2.getgraf(msme, kolvv, kolvrb, ver, reb);
            win2.Show();
            this.Close();
        }
    }
}
