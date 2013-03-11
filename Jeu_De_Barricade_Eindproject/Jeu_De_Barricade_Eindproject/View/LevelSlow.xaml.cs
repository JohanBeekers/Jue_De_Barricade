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
    /// Interaction logic for LevelSlow.xaml
    /// </summary>
    public partial class LevelSlow : Level
    {

        public LevelSlow()
            : base()
        {
        }

        protected override void initVariables()
        {
            //Level should initialize before any other superclass methods are executed. 
            InitializeComponent();

            //set map specific variables, then continue with the superclass constructor.
            
            /* Map legenda:
             * f = finish
             * # = barricade
             * o = normal tile
             * r = red_player home base
             * g = green_player home base
             * y = yellow_player home base
             * b = blue player_home base
             */
            map = new String[,]
            {
                // 1    2    3    4    5    6    7    8    9   10   11   12   13   14   15   16   17  18   19
                {" "," ", " ", " ", " ", " ", " ", " ", " ", "f", " ", " ", " ", " ", " ", " ", " ", " ", " "},
                {" ","o", "o", "o", "o", "o", "o", "o", "o", "#", "o", "o", "o", "o", "o", "o", "o", "o", " "},
                {" ","o", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "o", " "},
                {" ","o", "o", "o", "o", "o", "o", "o", "o", "#", "o", "o", "o", "o", "o", "o", "o", "o", " "},
                {" "," ", " ", " ", " ", " ", " ", " ", " ", "#", " ", " ", " ", " ", " ", " ", " ", " ", " "},
                {" "," ", " ", " ", " ", " ", " ", "o", "o", "#", "o", "o", " ", " ", " ", " ", " ", " ", " "},
                {" "," ", " ", " ", " ", " ", " ", "o", " ", " ", " ", "o", " ", " ", " ", " ", " ", " ", " "},
                {" "," ", " ", " ", " ", "o", "o", "#", "o", "o", "o", "#", "o", "o", " ", " ", " ", " ", " "},
                {" "," ", " ", " ", " ", "o", " ", " ", " ", " ", " ", " ", " ", "o", " ", " ", " ", " ", " "},
                {" "," ", " ", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", " ", " ", " "},
                {" "," ", " ", "o", " ", " ", " ", "o", " ", " ", " ", "o", " ", " ", " ", "o", " ", " ", " "},
                {" ","#", "o", "o", "o", "#", "o", "o", "o", "#", "o", "o", "o", "#", "o", "o", "o", "#", " "},
                {" ","o", " ", " ", " ", "o", " ", " ", " ", "o", " ", " ", " ", "o", " ", " ", " ", "o", " "},
                {" ","o", "o", "R", "o", "o", "o", "G", "o", "o", "o", "Y", "o", "o", "o", "B", "o", "o", " "},
                {" "," ", " ", "r", " ", " ", " ", "g", " ", " ", " ", "y", " ", " ", " ", "b", " ", " ", " "},
                {" "," ", "r", "r", "r", " ", "g", "g", "g", " ", "y", "y", "y", " ", "b", "b", "b", " ", " "},
                {" "," ", " ", "r", " ", " ", " ", "g", " ", " ", " ", "y", " ", " ", " ", "b", " ", " ", " "},
            };

            pawnAmount = 5;
            iMapWidth = 19;
            iMapHeight = 17;
            this.Width = iMapWidth * 32;
            this.Height = iMapHeight * 32;

            boardGrid = mainGrid;
        }

    }
}
