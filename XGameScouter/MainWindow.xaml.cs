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
using MahApps.Metro.Controls;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace XGameScouter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    /*
    LEAGUE API AND ETC OVERALL INFORMATION
    https://ddragon.leagueoflegends.com/realms/euw.json ------->Check ICONS API VERSION
    http://ddragon.leagueoflegends.com/cdn/ICONS_API_VERSION/img/profileicon/ICONID.png ----->Get icons by ID
    https://euw.api.pvp.net/api/lol/euw/v1.4/summoner/by-name/NAME?api_key=067c7765-e085-4a55-83f1-be65b5869416" ----->Get summoner's ID
    https://euw.api.pvp.net/observer-mode/rest/consumer/getSpectatorGameInfo/EUW1/SUMMONERID?api_key=067c7765-e085-4a55-83f1-be65b5869416 ----->Get match info
    http://ddragon.leagueoflegends.com/cdn/6.3.1/img/spell/SummonerFlash.png --------> Get spell images by name
    http://ddragon.leagueoflegends.com/cdn/6.3.1/img/map/map11.png ------------> Get map image by map ID
    http://ddragon.leagueoflegends.com/cdn/6.3.1/img/champion/Aatrox.png ------> Get champion square image by name
    SummonerSpells: prefix Summoner:
    -Dot (Ignite)
    -Heal (Heal)
    -Teleport (Teleport)
    -Boost (Cleanse)
    -Haste (Ghost)
    -Exhaust (Exhaust)

*/

    public partial class MainWindow : MetroWindow
    {

        //CREATE PLAYERS
        Summoner sum01 = new Summoner();
        Summoner sum02 = new Summoner();
        Summoner sum03 = new Summoner();
        Summoner sum04 = new Summoner();
        Summoner sum05 = new Summoner();
        Summoner sum06 = new Summoner();
        Summoner sum07 = new Summoner();
        Summoner sum08 = new Summoner();
        Summoner sum09 = new Summoner();
        Summoner sum10 = new Summoner();
        //
        public string version;

        public MainWindow()
        {
            InitializeComponent();
            GetVersion();
        }

        
        //Search
        private void button_Click(object sender, RoutedEventArgs e)
        {
            inicio.Visibility = Visibility.Hidden; //Hide search
            results.Visibility = Visibility.Visible; //show results
            string SummonerID = GetSummonerID(textBox.Text);
            GetInGameSummonersNamesAndInfo(SummonerID);
            /*DEBUG PURPOSES
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\DATA.txt", sum01.TeamID + Environment.NewLine + sum01.Spell2ID + Environment.NewLine + sum01.Spell1ID + Environment.NewLine + sum01.Name + Environment.NewLine + sum01.ID + Environment.NewLine + sum01.IconID + Environment.NewLine + sum01.ChampionID + Environment.NewLine + sum01.bot);
            100 team
            14 spell2
            4 spell1
            lolgendaire72 name
            57158970 summoner id
            839 icon id
            127 championid
            False isBot
            
            */
            //Presenting data visually

            //draw spell images
            sum01spell1.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum01.Spell1ID + ".png"));
            sum01spell2.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum01.Spell2ID + ".png"));
            sum02spell1.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum02.Spell1ID + ".png"));
            sum02spell2.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum02.Spell2ID + ".png"));
            sum03spell1.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum03.Spell1ID + ".png"));
            sum03spell2.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum03.Spell2ID + ".png"));
            sum04spell1.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum04.Spell1ID + ".png"));
            sum04spell2.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum04.Spell2ID + ".png"));
            sum05spell1.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum05.Spell1ID + ".png"));
            sum05spell2.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum05.Spell2ID + ".png"));
            sum06spell1.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum06.Spell1ID + ".png"));
            sum06spell2.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum06.Spell2ID + ".png"));
            sum07spell1.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum07.Spell1ID + ".png"));
            sum07spell2.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum07.Spell2ID + ".png"));
            sum08spell1.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum08.Spell1ID + ".png"));
            sum08spell2.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum08.Spell2ID + ".png"));
            sum09spell1.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum09.Spell1ID + ".png"));
            sum09spell2.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum09.Spell2ID + ".png"));
            sum10spell1.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum10.Spell1ID + ".png"));
            sum10spell2.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Spells\\" + sum10.Spell2ID + ".png"));
            //end draw spell images
            //Write players names
            name1.Content = sum01.Name;
            name2.Content = sum02.Name;
            name3.Content = sum03.Name;
            name4.Content = sum04.Name;
            name5.Content = sum05.Name;
            name6.Content = sum06.Name;
            name7.Content = sum07.Name;
            name8.Content = sum08.Name;
            name9.Content = sum09.Name;
            name10.Content = sum10.Name;
            //end write players names
            //Draw icons
            sum01icon.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Icons\\" + sum01.IconID + ".png"));
            sum02icon.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Icons\\" + sum02.IconID + ".png"));
            sum03icon.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Icons\\" + sum03.IconID + ".png"));
            sum04icon.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Icons\\" + sum04.IconID + ".png"));
            sum05icon.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Icons\\" + sum05.IconID + ".png"));
            sum06icon.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Icons\\" + sum06.IconID + ".png"));
            sum07icon.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Icons\\" + sum07.IconID + ".png"));
            sum08icon.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Icons\\" + sum08.IconID + ".png"));
            sum09icon.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Icons\\" + sum09.IconID + ".png"));
            sum10icon.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Icons\\" + sum10.IconID + ".png"));
            //end draw icons
            //Draw champions
            GetChampion(sum01.ChampionID, "");
            GetChampion(sum02.ChampionID, "");
            GetChampion(sum03.ChampionID, "");
            GetChampion(sum04.ChampionID, "");
            GetChampion(sum05.ChampionID, "");
            GetChampion(sum06.ChampionID, "");
            GetChampion(sum07.ChampionID, "");
            GetChampion(sum08.ChampionID, "");
            GetChampion(sum09.ChampionID, "");
            GetChampion(sum10.ChampionID, "");
            sum01champion.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\temp\\" + sum01.ChampionID + ".png"));
            sum02champion.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\temp\\" + sum02.ChampionID + ".png"));
            sum03champion.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\temp\\" + sum03.ChampionID + ".png"));
            sum04champion.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\temp\\" + sum04.ChampionID + ".png"));
            sum05champion.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\temp\\" + sum05.ChampionID + ".png"));
            sum06champion.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\temp\\" + sum06.ChampionID + ".png"));
            sum07champion.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\temp\\" + sum07.ChampionID + ".png"));
            sum08champion.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\temp\\" + sum08.ChampionID + ".png"));
            sum09champion.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\temp\\" + sum09.ChampionID + ".png"));
            sum10champion.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\temp\\" + sum10.ChampionID + ".png"));
            //end draw champions
        }

        private void GetChampion(string id, string sum)
        {
            string DATA = null;
            WebClient NETClient = new WebClient();
            try
            {
                DATA = NETClient.DownloadString("https://global.api.pvp.net/api/lol/static-data/euw/v1.2/champion/" + id + "?champData=image&api_key=067c7765-e085-4a55-83f1-be65b5869416");
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            JObject DATA2 = JObject.Parse(DATA);
            try
            {
               NETClient.DownloadFile("http://ddragon.leagueoflegends.com/cdn/" + version + "/img/champion/" + DATA2["image"]["full"], Directory.GetCurrentDirectory() + "\\temp\\" + id + ".png");
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void GetInGameSummonersNamesAndInfo(string sumID)
        {
            string DATA = null;
            WebClient NETClient = new WebClient();
            try
            {
              DATA = NETClient.DownloadString("https://euw.api.pvp.net/observer-mode/rest/consumer/getSpectatorGameInfo/EUW1/" + sumID + "?api_key=067c7765-e085-4a55-83f1-be65b5869416");
            }catch(WebException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            JObject DATA2 = JObject.Parse(DATA);
            int i = 0;
            foreach(JToken participant in DATA2["participants"])
            {
                i++;
                if (i==1)
                {
                    sum01.TeamID = participant["teamId"].ToString();
                    sum01.Spell1ID = participant["spell1Id"].ToString();
                    sum01.Spell2ID = participant["spell2Id"].ToString();
                    sum01.ChampionID = participant["championId"].ToString();
                    sum01.IconID = participant["profileIconId"].ToString();
                    sum01.Name = participant["summonerName"].ToString();
                    sum01.bot = participant["bot"].ToString();
                    sum01.ID = participant["summonerId"].ToString();

                }
                else if(i==2)
                {
                    sum02.TeamID = participant["teamId"].ToString();
                    sum02.Spell1ID = participant["spell1Id"].ToString();
                    sum02.Spell2ID = participant["spell2Id"].ToString();
                    sum02.ChampionID = participant["championId"].ToString();
                    sum02.IconID = participant["profileIconId"].ToString();
                    sum02.Name = participant["summonerName"].ToString();
                    sum02.bot = participant["bot"].ToString();
                    sum02.ID = participant["summonerId"].ToString();
                }
                else if (i == 3)
                {
                    sum03.TeamID = participant["teamId"].ToString();
                    sum03.Spell1ID = participant["spell1Id"].ToString();
                    sum03.Spell2ID = participant["spell2Id"].ToString();
                    sum03.ChampionID = participant["championId"].ToString();
                    sum03.IconID = participant["profileIconId"].ToString();
                    sum03.Name = participant["summonerName"].ToString();
                    sum03.bot = participant["bot"].ToString();
                    sum03.ID = participant["summonerId"].ToString();
                }
                else if (i == 4)
                {
                    sum04.TeamID = participant["teamId"].ToString();
                    sum04.Spell1ID = participant["spell1Id"].ToString();
                    sum04.Spell2ID = participant["spell2Id"].ToString();
                    sum04.ChampionID = participant["championId"].ToString();
                    sum04.IconID = participant["profileIconId"].ToString();
                    sum04.Name = participant["summonerName"].ToString();
                    sum04.bot = participant["bot"].ToString();
                    sum04.ID = participant["summonerId"].ToString();
                }
                else if (i == 5)
                {
                    sum05.TeamID = participant["teamId"].ToString();
                    sum05.Spell1ID = participant["spell1Id"].ToString();
                    sum05.Spell2ID = participant["spell2Id"].ToString();
                    sum05.ChampionID = participant["championId"].ToString();
                    sum05.IconID = participant["profileIconId"].ToString();
                    sum05.Name = participant["summonerName"].ToString();
                    sum05.bot = participant["bot"].ToString();
                    sum05.ID = participant["summonerId"].ToString();
                }
                else if (i == 6)
                {
                    sum06.TeamID = participant["teamId"].ToString();
                    sum06.Spell1ID = participant["spell1Id"].ToString();
                    sum06.Spell2ID = participant["spell2Id"].ToString();
                    sum06.ChampionID = participant["championId"].ToString();
                    sum06.IconID = participant["profileIconId"].ToString();
                    sum06.Name = participant["summonerName"].ToString();
                    sum06.bot = participant["bot"].ToString();
                    sum06.ID = participant["summonerId"].ToString();
                }
                else if (i == 7)
                {
                    sum07.TeamID = participant["teamId"].ToString();
                    sum07.Spell1ID = participant["spell1Id"].ToString();
                    sum07.Spell2ID = participant["spell2Id"].ToString();
                    sum07.ChampionID = participant["championId"].ToString();
                    sum07.IconID = participant["profileIconId"].ToString();
                    sum07.Name = participant["summonerName"].ToString();
                    sum07.bot = participant["bot"].ToString();
                    sum07.ID = participant["summonerId"].ToString();
                }
                else if (i == 8)
                {
                    sum08.TeamID = participant["teamId"].ToString();
                    sum08.Spell1ID = participant["spell1Id"].ToString();
                    sum08.Spell2ID = participant["spell2Id"].ToString();
                    sum08.ChampionID = participant["championId"].ToString();
                    sum08.IconID = participant["profileIconId"].ToString();
                    sum08.Name = participant["summonerName"].ToString();
                    sum08.bot = participant["bot"].ToString();
                    sum08.ID = participant["summonerId"].ToString();
                }
                else if (i == 9)
                {
                    sum09.TeamID = participant["teamId"].ToString();
                    sum09.Spell1ID = participant["spell1Id"].ToString();
                    sum09.Spell2ID = participant["spell2Id"].ToString();
                    sum09.ChampionID = participant["championId"].ToString();
                    sum09.IconID = participant["profileIconId"].ToString();
                    sum09.Name = participant["summonerName"].ToString();
                    sum09.bot = participant["bot"].ToString();
                    sum09.ID = participant["summonerId"].ToString();
                }
                else if (i == 10)
                {
                    sum10.TeamID = participant["teamId"].ToString();
                    sum10.Spell1ID = participant["spell1Id"].ToString();
                    sum10.Spell2ID = participant["spell2Id"].ToString();
                    sum10.ChampionID = participant["championId"].ToString();
                    sum10.IconID = participant["profileIconId"].ToString();
                    sum10.Name = participant["summonerName"].ToString();
                    sum10.bot = participant["bot"].ToString();
                    sum10.ID = participant["summonerId"].ToString();
                }
            }
        }
        
        //Custom function for getting summoner ID by TheWebs (aka: kiko298 xD) 
        public string GetSummonerID(string name) 
        {
            bool success = true;
            string ID = null;
            string DATA = null;
            WebClient NETClient = new WebClient();
            try {
                DATA = NETClient.DownloadString("https://euw.api.pvp.net/api/lol/euw/v1.4/summoner/by-name/" + name + "?api_key=067c7765-e085-4a55-83f1-be65b5869416");
            }catch(WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Summoner " + textBox.Text + " does not exist!");
                    success = false;
                }
                else
                {
                    MessageBox.Show(ex.Message);
                    success = false;
                }
            }
            if (success == true)
            {
                JObject summoner = JObject.Parse(DATA);
                summoner.GetValue(name);
                ID = (string)summoner[name.ToLower()]["id"];
                return ID;
            }
            else
            {
                return "error";
            }
        }
        //-----------------------------------------------------------------------------------

            private void GetVersion()
        {
            string DATA = null;
            WebClient NETClient = new WebClient();
            try
            {
               DATA = NETClient.DownloadString("https://ddragon.leagueoflegends.com/realms/euw.json");
            }catch(WebException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            JObject DATA2 = JObject.Parse(DATA);
            version = DATA2["n"]["profileicon"].ToString();

        }

        private void GetSummonerSpell(string id, string summoner)
        {
            string DATA = null;
            WebClient NETClient = new WebClient();
            try
            {
                DATA = NETClient.DownloadString("https://global.api.pvp.net/api/lol/static-data/euw/v1.2/summoner-spell/" + id + "?spellData=image&api_key=067c7765-e085-4a55-83f1-be65b5869416");
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            JObject DATA2 = JObject.Parse(DATA);

            try
            {
                NETClient.DownloadFile("http://ddragon.leagueoflegends.com/cdn/" + version + "/img/spell/" + DATA2["image"]["full"].ToString(), Directory.GetCurrentDirectory() + "\\temp\\" + summoner + ".png");
                
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void GetAllIcons() //max: 1106
        {
            WebClient NETClient = new WebClient();
            int i = 0;
            while (i < 1130)
            {
                try
                {
                    NETClient.DownloadFile("http://ddragon.leagueoflegends.com/cdn/" + version + "/img/profileicon/" + i + ".png", Directory.GetCurrentDirectory() + "\\Icons\\" + i + ".png");
                    Console.WriteLine("Icon {0}.png exists", i);
                    i++;
                }catch(WebException ex)
                {
                    Console.WriteLine("Icon {0}.png does not exist", i);
                    i++;
                }
                }
        }

            //Custom class to handle players info
        public class Summoner
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string TeamID { get; set; }
            public string ChampionID { get; set; }
            public string Spell1ID { get; set; }
            public string Spell2ID { get; set; }
            public string IconID { get; set; }
            public string bot { get; set; }

        }
    }
}

/*
{"amatarasu001": {
   "id": 19358540,
   "name": "Amatarasu001",
   "profileIconId": 783,
   "revisionDate": 1455408496000,
   "summonerLevel": 30
}}

    */
