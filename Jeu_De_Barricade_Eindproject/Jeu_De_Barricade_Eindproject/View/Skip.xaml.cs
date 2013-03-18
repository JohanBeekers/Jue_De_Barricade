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
    /// Interaction logic for Skip.xaml
    /// </summary>
    public partial class Skip : UserControl
    {
        Controller.Game game;

        public Skip(Controller.Game game)
        {
            InitializeComponent();

            this.game = game;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            game.nextPlayer();
        }
    }
}
