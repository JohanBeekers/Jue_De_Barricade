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
        private Model.ModelLevel levelModel;
        private Player[] aPlayers;

        private String[,] saveMap;

        private String path;

        private int iHumanPlayers;
        private int iPlayerNumber = 0;

        //Save the current game to a new file.
        public void saveTheGame(int playerTurn, Model.ModelLevel levelModel, Player[] aPlayers)
        {
            this.levelModel = levelModel;
            this.aPlayers = aPlayers;

            iHumanPlayers = 0;

            foreach (Controller.Player player in aPlayers)
            {
                if (player.BIsHuman)
                {
                    iHumanPlayers++;
                }
            }

            createSaveFolder();
            createSaveMap();

            String date = DateTime.Now.ToString();
            date =  date.Replace(":", "-");
            String saveName = date + ".save";

            StreamWriter sw = new StreamWriter(path + "/" +  saveName);

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
            if(levelModel.GetType() == typeof(Model.ModelLevelSlow))
            {
                sw.WriteLine("boardtype:normaal");
            }
            else
            {
                sw.WriteLine("boardtype:snel");
            }

            //Write the board itself
            for (int iRow = 0; iRow < levelModel.IMapHeight; iRow++)
            {
                String sRow = "";

                for (int iColumn = 0; iColumn < levelModel.IMapWidth; iColumn++)
                {
                    sRow += saveMap[iRow, iColumn] + " ";
                }

                sw.WriteLine(sRow);
            }


            //Close the stream
            sw.Close();

            MessageBox.Show("Het spel is opgeslagen.");
        }

        //Create the save folder if it does not exist. 
        private void createSaveFolder()
        {
            path = "Opgeslagen spellen";

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);

            }
            catch (Exception e)
            {
                Console.WriteLine("Kan geen map aanmaken voor de saved games.  " + e);
            } 
        }

        //Method to fill the array saveMap with the right content
        private void createSaveMap()
        {
            //Initialize the saveMap array with the right size
            saveMap = new String[levelModel.IMapHeight, levelModel.IMapWidth];

            //Loop through the orginial map
            for (int iRow = 0; iRow < levelModel.IMapHeight; iRow++)
            {
                for (int iColumn = 0; iColumn < levelModel.IMapWidth; iColumn++)
                {
                    if (levelModel.Map[iRow, iColumn].Equals(" "))
                    {
                        saveMap[iRow, iColumn] = "( )";
                    }
                    else if (!levelModel.Map[iRow, iColumn].Equals(" "))
                    {
                        saveMap[iRow, iColumn] = "(o)";
                    }
                }
            }

            //Loop through all barricades
            foreach (Model.BarricadePawn barricade in levelModel.ABarricades)
            {
                saveMap[barricade.CurrentLocation.Y, barricade.CurrentLocation.X] = "(#)";
            }
            
            //Loop through all player and pawns
            foreach (Controller.Player player in aPlayers)
            {
                String sPlayer = "";

                switch (iPlayerNumber)
                {
                    case 0:
                        sPlayer = "(r)";
                        break;
                    case 1:
                        sPlayer = "(g)";
                        break;
                    case 2:
                        sPlayer = "(y)";
                        break;
                    case 3:
                        sPlayer = "(b)";
                        break;
                }

                foreach (Controller.Pawn pawn in player.APawns)
                {
                    saveMap[pawn.CurrentLocation.Y, pawn.CurrentLocation.X] = sPlayer;
                }

                iPlayerNumber++;
            }

            iPlayerNumber = 0;
        }

    }
}
