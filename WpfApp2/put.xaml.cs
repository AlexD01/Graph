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
using System.Collections;

namespace WpfApp2
{

    public partial class put : Window
    {
        int[,]m;
        int kolvv;
        int kolvrb;
        Vershina[] ver;
        Rebro[] reb;
        public put()
        {           
            InitializeComponent();
        }
        public void getgraf(int[,] msme, int kolvv, int kolvrb, Vershina[] ver, Rebro[] reb)
        {
            this.m = msme;
            this.kolvv = kolvv;
            this.kolvrb = kolvrb;
            this.ver = ver;
            this.reb = reb;
            T2.Text = "";
            for (int i = 0; i < kolvv; i++)
            {
                for (int j = 0; j < kolvv; j++)
                {
                    T2.Text += msme[i, j] + " ";
                }
                T2.Text += "\n";
            }
            foreach (var item in ver)
            {
                Canvas.Children.Add(item.ellipse);
                Canvas.SetLeft(item.ellipse,item.x);
                Canvas.SetTop(item.ellipse, item.y);
                TextBlock tb = new TextBlock();
                tb.Text = item.ellipse.Tag+"";
                Canvas.Children.Add(tb);
                Canvas.SetLeft(tb, item.x);
                Canvas.SetTop(tb, item.y-20);
            }
            foreach (var item in reb)
            {
                if(item.line!=null)
                Canvas.Children.Add(item.line);
            }
        }
        private void V1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsNumber(Convert.ToChar(e.Text))) e.Handled = true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in ver)
            {
                item.ellipse.Stroke = new SolidColorBrush(Colors.Black);
            }
            foreach (var item in reb)
            {
                if(item.line!=null)
                item.line.Stroke = new SolidColorBrush(Colors.Black);
            }
            if (V1.Text == "" || V2.Text == "") { MessageBox.Show("Введите вершины", "Error", MessageBoxButton.OK, MessageBoxImage.Error);  return; }
            int n = int.Parse(V1.Text);
            int k = int.Parse(V2.Text);
            if(n>kolvv-1) { MessageBox.Show("Такой вершины нету"); return; }
            if (k > kolvv-1) { MessageBox.Show("Такой вершины нету"); return; }
            if (pr == null) { MessageBox.Show("Выполните обход"); return; }
            if (pr != null) if (pr[k] != true) { MessageBox.Show("вершина не досягаема"); return; }      
            List<string> lists = new List<string>();
            if (k == n) { MessageBox.Show(n+"");return; }
            for(int i=0;i<kolvv;i++)
            {
                if (m[n, i] == 1)
                {
                    string tls = "";
                    tls=""+n + " " + i;
                    lists.Add(tls);
                }
            }
            int kol = lists.Count;
            string tls11 = lists[lists.Count - 1];
            string[] str1 = tls11.Split(' ');
            int kk = 0;
            foreach (var item in pr)
            {
                if (item == false) kk++;
            }
            while (str1.Length < kolvv-kk)
            {
                kol = lists.Count;
                for (int i = 0; i < kol; i++)
                {
                    string tls1 = lists[i];
                    string[] str = tls1.Split(' ');
                    string n1 = str[str.Length - 1];
                    int nn = int.Parse(n1);
                    for (int ii = 0; ii < kolvv; ii++)
                    {
                        if (m[nn, ii] == 1)
                        {
                            int u = tls1.IndexOf(Convert.ToString(ii));
                            if (u != -1) continue;
                            string tls = tls1;
                            tls += " " + ii;
                            if (lists.IndexOf(tls) != -1) continue;
                            lists.Add(tls);
                        }
                    }
                }
                tls11 = lists[lists.Count - 1];
                str1 = tls11.Split(' ');
            }
            kol = lists.Count;
            List<string> listtrue = new List<string>();
            foreach (var item in lists)
            {
                string tls1 = item;
                string[] str = tls1.Split(' ');
                if (str[0] == Convert.ToString(n)&& str[str.Length-1] == Convert.ToString(k)) listtrue.Add(item); 
            }
            string smin = "";
            int kmin = kolvv+1;
            foreach (var item in listtrue)
            {
                string tls1 = item;
                string[] str = tls1.Split(' ');
                if (str.Length < kmin) { kmin = str.Length;smin = item; }
            }
            T2.Text += smin;
            string[] strt = smin.Split(' ');
            foreach(var item in reb)
            {
            if (item.line != null)
                for (int i = 1; i < strt.Length; i++)
                {
                    if ((Convert.ToString(item.ver1.ellipse.Tag) == strt[i] && Convert.ToString(item.ver2.ellipse.Tag) == strt[i - 1]) || (Convert.ToString(item.ver1.ellipse.Tag) == strt[i-1] && Convert.ToString(item.ver2.ellipse.Tag) == strt[i] ))
                    {
                            item.line.Stroke = new SolidColorBrush(Colors.Red);
                    }
                }
            }
        }
        bool[] pr;
        List<int> ls = new List<int>();
        void glub(int st)
        {
            int r;        
            pr[st] = true;          
            ver[st].ellipse.Stroke = new SolidColorBrush(Colors.Red);
            MessageBox.Show("" + st);          
            for (r = 0; r < kolvv; r++)
                if ((m[st,r] != 0) && (!pr[r]))
                    glub(r);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var item in ver)
            {
                item.ellipse.Stroke = new SolidColorBrush(Colors.Black);
            }
            if (VER.Text == "" ) { MessageBox.Show("Введите вершину", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            int v = int.Parse(VER.Text);
            if (v > kolvv-1) { MessageBox.Show("Такой вершины нету"); return; }
            Queue queue = new Queue();
            queue.Enqueue(v);
            pr = new bool[kolvv];
            pr[v] = true;
            ver[v].ellipse.Stroke = new SolidColorBrush(Colors.Red);
            int w = 0;
            while (queue.Count != 0)
            {
                v = (int)queue.Dequeue();  
                int i;
                for (i = 0; i < kolvv; i++)
                {
                    if (m[v, i] == 1)
                    {
                        w = i; if (pr[w]) continue;
                        queue.Enqueue(w);
                        pr[w] = true;
                        MessageBox.Show("" + w);
                        ver[w].ellipse.Stroke = new SolidColorBrush(Colors.Red);
                    }
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            foreach (var item in ver)
            {
                item.ellipse.Stroke = new SolidColorBrush(Colors.Black);
            }

            if (VER.Text == "") { MessageBox.Show("Введите вершину", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            int v = int.Parse(VER.Text);
            if (v > kolvv-1) { MessageBox.Show("Такой вершины нету"); return; }
            pr = new bool[kolvv];
            glub(v);
        }
    }
}
