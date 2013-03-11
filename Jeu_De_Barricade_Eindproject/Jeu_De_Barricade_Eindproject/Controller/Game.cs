using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jeu_De_Barricade_Eindproject.Controller
{
    class Game
    {
        private MainWindow main;
        private View.Level level;
        private Player[] aPlayers = new Player[4];
        
        public Game(MainWindow main)
        {

            this.main = main;

            //Show the "hoofdmenu" button, allowing the user to return the the main menu
            main.label_mainmenu.Visibility = Visibility.Visible;
        }

        //Remove the object of the board and remove it as child from the grid so it's gone
        public void destoryMap()
        {
            if (level != null)
            {
                main.mainGrid.Children.Remove(level);
                level = null;
            }
            
            main.ToggleImageOpacityAndButtonGrid();
        }

        //Create a new board according to the type of board selected by the user input
        public void newBoard(int iHumanPlayers, String sBoardType)
        {
            if (sBoardType.Equals("Normaal"))
            {
                level = new View.LevelSlow();
                main.mainGrid.Children.Add(level);
                level.Visibility = Visibility.Visible;
            }
            else
            {
                level = new View.LevelFast();
                main.mainGrid.Children.Add(level);
                level.Visibility = Visibility.Visible;
            }

            createPlayers(iHumanPlayers);
        }

        public void loadBoard()
        {
            //createPlayers(iHumanPlayers, aPawnPositions);
        }

        private void createPlayers(int iHumanPlayers, Model.Field[] aPawnPositions)
        {
            if (level.GetType() == typeof(View.LevelSlow))
            {
                if (aPawnPositions == null)
                {
                    aPlayers[0] = new Player(iHumanPlayers <= 1, 5, level.APlayerRedStartFields);
                    aPlayers[1] = new Player(iHumanPlayers <= 2, 5, level.APlayerRedStartFields);
                    aPlayers[2] = new Player(iHumanPlayers <= 3, 5, level.APlayerRedStartFields);
                    aPlayers[3] = new Player(iHumanPlayers <= 4, 5, level.APlayerRedStartFields);
                }
                else
                {
                    aPlayers[0] = new Player(iHumanPlayers <= 1, 5, level.APlayerRedStartFields, aPawnPositions);
                    aPlayers[1] = new Player(iHumanPlayers <= 2, 5, level.APlayerRedStartFields, aPawnPositions);
                    aPlayers[2] = new Player(iHumanPlayers <= 3, 5, level.APlayerRedStartFields, aPawnPositions);
                    aPlayers[3] = new Player(iHumanPlayers <= 4, 5, level.APlayerRedStartFields, aPawnPositions);
                }
            }
            else
            {
                if (aPawnPositions == null)
                {
                    aPlayers[0] = new Player(iHumanPlayers <= 1, 4, level.APlayerRedStartFields);
                    aPlayers[1] = new Player(iHumanPlayers <= 2, 4, level.APlayerRedStartFields);
                    aPlayers[2] = new Player(iHumanPlayers <= 3, 4, level.APlayerRedStartFields);
                    aPlayers[3] = new Player(iHumanPlayers <= 4, 4, level.APlayerRedStartFields);
                }
                else
                {
                    aPlayers[0] = new Player(iHumanPlayers <= 1, 4, level.APlayerRedStartFields, aPawnPositions);
                    aPlayers[1] = new Player(iHumanPlayers <= 2, 4, level.APlayerRedStartFields, aPawnPositions);
                    aPlayers[2] = new Player(iHumanPlayers <= 3, 4, level.APlayerRedStartFields, aPawnPositions);
                    aPlayers[3] = new Player(iHumanPlayers <= 4, 4, level.APlayerRedStartFields, aPawnPositions);
                }
            }
        }

        private void createPlayers(int iHumanPlayers)
        {
            createPlayers(iHumanPlayers, null);
        }
    }
}
