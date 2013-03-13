using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Jeu_De_Barricade_Eindproject.Controller
{
    public class Game
    {
        private MainWindow main;
        private View.Level level;
        private View.Dice dice;
        private Player[] aPlayers = new Player[4];
        private int playerTurn;
        private Model.BarricadePawn barricade;

        public int PlayerTurn
        {
            get { return playerTurn; }
            set { playerTurn = value; }
        }
        public Model.BarricadePawn Barricade
        {
            get { return barricade; }
        }
        
        public Game(MainWindow main)
        {

            this.main = main;
            playerTurn = 0;

            //Show the "hoofdmenu" button, allowing the user to return the the main menu
            main.label_mainmenu.Visibility = Visibility.Visible;
        }

        //Remove the object of the board and remove it as child from the grid so it's gone
        public void destoryMap()
        {
            if (level != null)
            {
                main.mainGrid.Children.Remove(level);
                main.mainGrid.Children.Remove(dice);
                level = null;
                dice = null;
            }
            
            main.ToggleImageOpacityAndButtonGrid();
        }

        //Create a new board according to the type of board selected by the user input
        public void newBoard(int iHumanPlayers, String sBoardType)
        {
            if (sBoardType.Equals("Normaal"))
            {
                createPlayers(iHumanPlayers, 5);
                level = new View.LevelSlow(this);
                main.mainGrid.Children.Add(level);
                level.Visibility = Visibility.Visible;
            }
            else
            {
                createPlayers(iHumanPlayers, 4);
                level = new View.LevelFast(this);
                main.mainGrid.Children.Add(level);
                level.Visibility = Visibility.Visible;
            }

            dice = new View.Dice();
            dice.HorizontalAlignment = HorizontalAlignment.Right;
            dice.Margin = new Thickness(0,0,10,0);
            main.mainGrid.Children.Add(dice);
        }

        public void loadBoard()
        {
            
        }

        private void createPlayers(int iHumanPlayers, int amountPawns)
        {
            aPlayers[0] = new Player(iHumanPlayers <= 1, amountPawns);
            aPlayers[1] = new Player(iHumanPlayers <= 2, amountPawns);
            aPlayers[2] = new Player(iHumanPlayers <= 3, amountPawns);
            aPlayers[3] = new Player(iHumanPlayers <= 4, amountPawns);
        }

        public void createPawn(Model.Field field, Ellipse image, int i)
        {
            Pawn pawn = new Pawn(image, field, i);
            aPlayers[i].addPawn(pawn);
        }

        public void nextPlayer()
        {
            if (playerTurn == 3)
            {
                playerTurn = 0;
            }
            else
            {
                playerTurn++;
            }

            dice.reset();
        }

        public Boolean getGedobbeld()
        {
            return dice.Gedobbeld;
        }

        public void selectPawn(int column, int row)
        {
            if (level.BoardGrid.Children.Contains(level.Blur) && level.Fields[column, row].Pawn == null)
            {
                level.Fields[Grid.GetColumn(level.Blur), Grid.GetRow(level.Blur)].Pawn.setLocation(level.Fields[column, row]);
                if (level.Fields[column, row].Barricade != null)
                {
                    barricade = level.Fields[column, row].Barricade;
                }
                else
                {
                    nextPlayer();
                }
                level.BoardGrid.Children.Remove(level.Blur);
            }
            else if (level.Fields[column, row] != null &&
                level.Fields[column, row].Pawn != null &&
                getGedobbeld() &&
                level.Fields[column, row].Pawn.PlayerNumber == PlayerTurn)
            {
                level.BoardGrid.Children.Remove(level.Blur);
                level.Blur.SetValue(Grid.ColumnProperty, column);
                level.Blur.SetValue(Grid.RowProperty, row);
                level.BoardGrid.Children.Add(level.Blur);
            }
        }

        public void moveBarricade(int column, int row)
        {
            barricade.setLocation(level.Fields[column, row]);
            barricade = null;
            nextPlayer();
        }

    }
}
