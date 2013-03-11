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
    /// Interaction logic for LevelFast.xaml
    /// </summary>
    public partial class LevelFast : Level
    {
        
        public LevelFast() 
            : base()
        {
        }

        protected override void initVariables()
        {
            //Level should initialize before any other superclass methods are executed. 
            InitializeComponent();

            /* Map legenda:
             * f = finish
             * # = barricade
             * s = safespot
             * o = normal tile
             * | = link line
             * r = red_player home base
             * g = green_player home base
             * y = yellow_player home base
             * b = blue player_home base
             */
            map = new String[,]
            {
                // 1    2    3    4    5    6    7    8    9   10   11  
                {" "," ", " ", " ", " ", " ", "f", " ", " ", " ", " ", " ", " "},
                {" "," ", "o", "o", "o", "o", "#", "o", "o", "o", "o", " ", " "},
                {" "," ", "|", " ", " ", " ", " ", " ", " ", " ", "|", " ", " "},
                {" "," ", "o", "o", "o", "s", "#", "s", "o", "o", "o", " ", " "},
                {" "," ", " ", " ", " ", " ", "|", " ", " ", " ", " ", " ", " "},
                {" "," ", " ", "o", "o", "#", "o", "#", "o", "o", " ", " ", " "},
                {" "," ", " ", "|", " ", " ", " ", " ", " ", "|", " ", " ", " "},
                {" "," ", " ", "s", "#", "o", "o", "o", "#", "s", " ", " ", " "},
                {" "," ", " ", " ", " ", " ", "|", " ", " ", " ", " ", " ", " "},
                {" "," ", " ", " ", "o", "o", "s", "o", "o", " ", " ", " ", " "},
                {" "," ", " ", " ", "|", " ", " ", " ", "|", " ", " ", " ", " "},
                {" ","s", "o", "s", "o", "o", "s", "o", "o", "s", "o", "s", " "},
                {" ","|", " ", "|", " ", " ", "|", " ", " ", "|", " ", "|", " "},
                {" ","o", "R", "o", "G", "o", "o", "o", "Y", "o", "B", "o", " "},
                {" "," ", "r", " ", "g", " ", " ", " ", "y", " ", "b", " ", " "},
                {" "," ", "r", " ", "g", " ", " ", " ", "y", " ", "b", " ", " "},
                {" "," ", "r", " ", "g", " ", " ", " ", "y", " ", "b", " ", " "},
                {" "," ", "r", " ", "g", " ", " ", " ", "y", " ", "b", " ", " "}
            };

            pawnAmount = 4;
            iMapWidth = 13;
            iMapHeight = 18;
            this.Width = iMapWidth * 32;
            this.Height = iMapHeight * 32;

            boardGrid = mainGrid;
        }
    }
}
