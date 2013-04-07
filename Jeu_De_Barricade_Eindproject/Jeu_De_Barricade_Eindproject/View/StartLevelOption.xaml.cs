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

namespace Jeu_De_Barricade_Eindproject.View
{
    /// <summary>
    /// Interaction logic for StartLevelOption.xaml
    /// </summary>
    public partial class StartLevelOption : UserControl
    {
        private MainWindow main;

        public StartLevelOption(MainWindow main)
        {
            InitializeComponent();

            this.main = main;
        }

        //Cancel button has been clicked.
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            main.ToggleImageOpacityAndButtonGrid();
        }

        //Start button has been clicked.
        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            main.startGame(Convert.ToInt32(comboPlayer.Text), comboType.Text);
        }
    }
}
