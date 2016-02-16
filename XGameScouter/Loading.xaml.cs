using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;

namespace XGameScouter
{
    /// <summary>
    /// Interaction logic for Loading.xaml
    /// </summary>
    public partial class Loading : Window
    {
        public string version;
        public Loading()
        {
            InitializeComponent();
            this.Visibility = Visibility.Visible;
            DoEvents();
            barra.Maximum = 1130;
            GetVersion();
            GetAllIcons();
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
            
        }

        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                                                  new Action(delegate { }));
        }

        private void GetVersion()
        {
            string DATA = null;
            WebClient NETClient = new WebClient();
            try
            {
                DATA = NETClient.DownloadString("https://ddragon.leagueoflegends.com/realms/euw.json");
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            JObject DATA2 = JObject.Parse(DATA);
            version = DATA2["n"]["profileicon"].ToString();

        }


        private void GetAllIcons() //max: 1106
        {
            WebClient NETClient = new WebClient();
            int i = 0;
            while (i < 1130)
            {
                if (File.Exists(Directory.GetCurrentDirectory() + "\\Icons\\" + i + ".png") == true)
                {
                    i++;
                    label.Content = ((i * 100) / 1130).ToString() + "%";
                    barra.Value = i;
                    DoEvents();
                }
                else
                {
                    try
                    {
                        NETClient.DownloadFile("http://ddragon.leagueoflegends.com/cdn/" + version + "/img/profileicon/" + i + ".png", Directory.GetCurrentDirectory() + "\\Icons\\" + i + ".png");
                        Console.WriteLine("Icon {0}.png exists", i);
                        label.Content = ((i * 100) / 1130).ToString() + "%";
                        barra.Value = i;
                        DoEvents();
                        i++;
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("Icon {0}.png does not exist", i);
                        label.Content = ((i * 100) / 1130).ToString() + "%";
                        barra.Value = i;
                        DoEvents();
                        i++;
                    }
                }
            }
        }
    }
}
