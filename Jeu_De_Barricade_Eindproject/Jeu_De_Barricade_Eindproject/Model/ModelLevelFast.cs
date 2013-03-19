using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_De_Barricade_Eindproject.Model
{
    class ModelLevelFast : ModelLevel
    {
        public ModelLevelFast()
        {
            //The array that contains the map

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
                //1   2    3    4    5    6    7    8    9   10   11   12   13
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
                {" ","o", "R", "o", "G", "o", "o", "o", "Y", "o", "B", "o", " "}, /* No barricades - line 13 */
                {" "," ", "r", " ", "g", " ", " ", " ", "y", " ", "b", " ", " "},
                {" "," ", "r", " ", "g", " ", " ", " ", "y", " ", "b", " ", " "},
                {" "," ", "r", " ", "g", " ", " ", " ", "y", " ", "b", " ", " "},
                {" "," ", "r", " ", "g", " ", " ", " ", "y", " ", "b", " ", " "}
            };

            //Amount of pawns per player
            pawnAmount = 4;

            //Width of the map in cells
            iMapWidth = 13;

            //Height of the map in cells
            iMapHeight = 18;

            //Width of the map in px
            iWidth = iMapWidth * 32;

            //Height of the map in px
            iHeight = iMapHeight * 32;

            //Set the array of barricades and give it the right size
            ABarricades = new Model.BarricadePawn[6];

            //Initialize the array witht he size of 1, there is just 1 row where barricades can not be added to
            aNoBarricades = new int[1];
            //Set the value '13' (row no barricades) in the array
            aNoBarricades[0] = 13;

            fillViewArray();
        }
    }
}
