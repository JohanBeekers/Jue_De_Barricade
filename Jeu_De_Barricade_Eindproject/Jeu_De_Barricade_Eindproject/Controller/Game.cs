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
        private Model.Field[] aPawnMovementOptions;
        private Pawn pawnSelected;

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

        public void fieldClick(Model.Field field)
        {
            if (dice.Gedobbeld)
            {
                if (field.Pawn != null &&
                    field.Pawn.PlayerNumber == PlayerTurn)
                {
                    //Get the pawn that is selected and get the possible moves
                    aPawnMovementOptions = field.Pawn.getPossibleMoves(dice.Worp).ToArray();
                    level.createBlurs(aPawnMovementOptions, field);
                    pawnSelected = field.Pawn;
                }
                else if (pawnSelected != null &&
                        aPawnMovementOptions.Contains(field))
                {
                    if (field.Pawn != null &&
                        field.Pawn.PlayerNumber != playerTurn)
                    {
                        field.Pawn.toStartLocation();
                    }

                    level.Fields[pawnSelected.CurrentLocation.X, pawnSelected.CurrentLocation.Y].Pawn.setLocation(field);
                    if (field.Barricade != null)
                    {
                        barricade = field.Barricade;
                    }
                    else if (field is Model.Finish)
                    {
                        level.showWinAnimation(playerTurn);
                    }
                    else
                    {
                        nextPlayer();
                    }
                    level.removeBlurs();
                    pawnSelected = null;
                }
            }
        }

        public void moveBarricade(Model.Field field)
        {
            if (field.Pawn == null && 
                field.Barricade == null &&
                (field.GetType() == typeof(Model.Field) || 
                field.GetType() == typeof(Model.Barricade)))
            {
                if((level is View.LevelFast && field.Y != 13) ||
                    (level is View.LevelSlow && field.Y != 13))
                {
                    barricade.setLocation(field);
                    barricade = null;
                    nextPlayer();
                }
            }
        }

    }
}
