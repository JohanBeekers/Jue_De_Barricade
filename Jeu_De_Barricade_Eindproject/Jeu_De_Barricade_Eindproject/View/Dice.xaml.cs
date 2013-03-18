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
    /// Interaction logic for Dice.xaml
    /// </summary>
    public partial class Dice : UserControl
    {
        private int worp;
        private Boolean gedobbeld;
        Random random = new Random();

        public int Worp
        {
            get { return worp; }
            set { worp = value; }
        }
        public Boolean Gedobbeld
        {
            get { return gedobbeld; }
            set { gedobbeld = value; }
        }

        public Dice(int playerTurn)
        {
            InitializeComponent();
            changeButtonColor(playerTurn);
        }

        private void Dobbel_Click(object sender, RoutedEventArgs e)
        {
            dobbelen();
        }

        public void dobbelen()
        {
            worp = random.Next(1, 7);
            gedobbeld = true;
            LabelWorp.Content = worp;
            ButtonDobbel.Visibility = Visibility.Collapsed;
        }

        public void reset()
        {
            ButtonDobbel.Visibility = Visibility.Visible;
            gedobbeld = false;
            worp = -1;
        }

        public void changeButtonColor(int playerTurn)
        {
            System.Windows.Media.Brush color = Brushes.Red;

            switch (playerTurn)
            {
                case 0:
                    color = Brushes.Red;
                    break;
                case 1:
                    color = Brushes.Green;
                    break;
                case 2:
                    color = Brushes.Yellow;
                    break;
                case 3:
                    color = Brushes.RoyalBlue;
                    break;
            }
            ButtonDobbel.Background = color;
            Rectangle1.Fill = color;
        }
    }
}
