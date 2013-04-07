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
    /// Interaction logic for WinnerMessage.xaml
    /// </summary>
    public partial class WinnerMessage : UserControl
    {
        public WinnerMessage()
        {
            InitializeComponent();
        }

        //Show the win message of a certain player color. 
        public void showWinner(int playerTurn)
        {
            switch (playerTurn)
            {
                //Red
                case 0:
                    labelWinner.Content += "Rood";
                    labelWinner.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                //Green
                case 1:
                    labelWinner.Content += "Groen";
                    labelWinner.Foreground = new SolidColorBrush(Colors.Green);
                    break;
                //Yellow
                case 2:
                    labelWinner.Content += "Geel";
                    labelWinner.Foreground = new SolidColorBrush(Colors.Yellow);
                    break;
                //Blue
                case 3:
                    labelWinner.Content += "Blauw";
                    labelWinner.Foreground = new SolidColorBrush(Colors.Blue);
                    break;
            }
        }
    }
}
