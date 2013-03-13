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

        public Dice()
        {
            InitializeComponent();
        }

        private void Dobbel_Click(object sender, RoutedEventArgs e)
        {
            worp = random.Next(1,7);
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
    }
}
