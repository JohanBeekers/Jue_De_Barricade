using System;
using System.Collections.Generic;
using System.IO;
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

namespace Jeu_De_Barricade_Eindproject.View
{
    /// <summary>
    /// Interaction logic for LoadLevelOption.xaml
    /// </summary>
    public partial class LoadLevelOption : UserControl
    {
        private MainWindow main;

        public LoadLevelOption(MainWindow main)
        {
            InitializeComponent();

            this.main = main;
        }

        public void updateAllSavedGames()
        {
            List<String> savedGamesArray = new List<String>();

            foreach (String saved in Directory.GetFiles("Opgeslagen spellen"))
            {
                if (saved.EndsWith(".save"))
                {
                    savedGamesArray.Add(saved.Substring(19, saved.Length -24));
                }
            }

            listSavedGames.ItemsSource = savedGamesArray;
        }

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            main.loadGame();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            main.ToggleImageOpacityAndButtonGrid();
        }
    }
}
