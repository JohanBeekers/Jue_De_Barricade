using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jeu_De_Barricade_Eindproject.Controller
{
    class SaveGame
    {
        private Model.BarricadePawn[] aBarricades;
        private Player[] aPlayers;

        private String boardType;
        private int iHumanPlayers = 0;


        public SaveGame(Model.BarricadePawn[] aBarricades, Player[] aPlayers, String boardType)
        {
            this.aBarricades = aBarricades;
            this.aPlayers = aPlayers;
            this.boardType = boardType;

            foreach (Controller.Player player in aPlayers)
            {
                iHumanPlayers++;
            }
        }

        public void saveTheGame(int playerTurn)
        {
            String date = DateTime.Now.ToString();
            date =  date.Replace(":", "-");
            String saveName = date + ".save";

            StreamWriter sw = new StreamWriter(saveName);

            //Write amount of human players             LINE 1
            sw.WriteLine("humanplayers:" + iHumanPlayers);

            //Write who's turn it is                    LINE 2
            switch (playerTurn)
            {
                case 0:
                    sw.WriteLine("turn:red");
                    break;
                case 1:
                    sw.WriteLine("turn:green");
                    break;
                case 2:
                    sw.WriteLine("turn:yellow");
                    break;
                case 3:
                    sw.WriteLine("turn:blue");
                    break;                    
            }

            //Write what board we are playing           LINE 3
            if(boardType.Contains("Slow"))
            {
                sw.WriteLine("boardtype:normaal");
            }
            else
            {
                sw.WriteLine("boardtype:snel");
            }

            //Write the board itself - pawn and barricade location
            sw.WriteLine("test johan");
            sw.WriteLine("test bas");
            sw.Close();
        }
    }
}
