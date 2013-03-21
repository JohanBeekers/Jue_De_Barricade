using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Jeu_De_Barricade_Eindproject.Controller
{
    public class Game
    {
        private MainWindow main;
        private Model.ModelLevel levelModel;
        private View.ViewLevel level;
        private View.Dice dice;
        private View.Skip skip;
        private View.WinnerMessage winner;
        private Player[] aPlayers = new Player[4];
        private int playerTurn;
        private Model.BarricadePawn barricade;
        private Model.Field[] aPawnMovementOptions;
        private Pawn pawnSelected;
        private Random random;

        private Controller.SaveGame save;

        public int PlayerTurn
        {
            get { return playerTurn; }
            set { playerTurn = value; }
        }
        public Model.BarricadePawn Barricade
        {
            get { return barricade; }
        }
        public Model.Field[] APawnMovementOptions
        {
            get { return aPawnMovementOptions; }
            set { aPawnMovementOptions = value; }
        }
        
        public Game(MainWindow main)
        {
            this.main = main;
            random = new Random();
            playerTurn = 0;

            //Show the "hoofdmenu" button, allowing the user to return the the main menu
            main.label_mainmenu.Visibility = Visibility.Visible;
            main.label_save.Visibility = Visibility.Visible;

            main.KeyUp += keyPress;
        }

        //Remove the object of the board and remove it as child from the grid so it's gone
        public void destoryMap()
        {
            if (level != null)
            {
                main.mainGrid.Children.Remove(level);
                main.mainGrid.Children.Remove(dice);
                main.mainGrid.Children.Remove(skip);
                main.mainGrid.Children.Remove(winner);
                level = null;
                dice = null;
                skip = null;
                winner = null;
            }
            
            main.ToggleImageOpacityAndButtonGrid();
        }

        //Create a new board according to the type of board selected by the user input
        public void newBoard(int iHumanPlayers, String sBoardType)
        {
            if (sBoardType.Equals("Normaal"))
            {
                createPlayers(iHumanPlayers, 5);
                levelModel = new Model.ModelLevelSlow();
                level = new View.ViewLevel(this, levelModel);
            }
            else
            {
                createPlayers(iHumanPlayers, 4);
                levelModel = new Model.ModelLevelFast();
                level = new View.ViewLevel(this, levelModel);
            }
            main.mainGrid.Children.Add(level);
            level.Visibility = Visibility.Visible;


            dice = new View.Dice(playerTurn);
            dice.HorizontalAlignment = HorizontalAlignment.Right;
            dice.Margin = new Thickness(0,0,10,0);
            main.mainGrid.Children.Add(dice);

            skip = new View.Skip(this);
            skip.HorizontalAlignment = HorizontalAlignment.Right;
            skip.Margin = new Thickness(0, 160, 10, 0);
            main.mainGrid.Children.Add(skip);

            winner = new View.WinnerMessage();
            winner.Visibility = Visibility.Collapsed;
            winner.HorizontalAlignment = HorizontalAlignment.Center;
            winner.VerticalAlignment = VerticalAlignment.Center;
            main.mainGrid.Children.Add(winner);

            //Create the save game controller and give it the model
            save = new Controller.SaveGame(levelModel.ABarricades, aPlayers, levelModel.GetType().ToString());
        }

        public void loadBoard()
        {
            
        }

        private void createPlayers(int iHumanPlayers, int amountPawns)
        {
            aPlayers[0] = new Player(iHumanPlayers >= 1, amountPawns, this);
            aPlayers[1] = new Player(iHumanPlayers >= 2, amountPawns, this);
            aPlayers[2] = new Player(iHumanPlayers >= 3, amountPawns, this);
            aPlayers[3] = new Player(iHumanPlayers >= 4, amountPawns, this);
        }

        public void createPawn(Model.Field field, Ellipse image, int i)
        {
            Pawn pawn = new Pawn(image, field, i);
            aPlayers[i].addPawn(pawn);
        }

        //Method called to give the turn to the next player
        public void nextPlayer()
        {
            //If the last player is done, give the turn to the first player
            if (playerTurn == 3)
            {
                playerTurn = 0;
            }
            //Else give the turn to the next player
            else
            {
                playerTurn++;
            }

            //Change dice button background color
            dice.changeButtonColor(playerTurn);

            //Empty the dice value
            dice.reset();

            if (!aPlayers[playerTurn].BIsHuman)
            {
                dice.dobbelen();
                aPlayers[playerTurn].automateTurn(dice.Worp);
            }
        }

        //Method called when a player clicks on a field
        public void fieldClick(Model.Field field)
        {
            //If the dice is rolled
            if (dice.Gedobbeld)
            {
                //If the field does contain a Pawn and the Pawn belongs to the player
                if (field.Pawn != null &&
                    field.Pawn.PlayerNumber == PlayerTurn)
                {
                    //Select the Pawn and get it's possible moves
                    aPawnMovementOptions = field.Pawn.getPossibleMoves(dice.Worp).ToArray();
                    level.createBlurs(aPawnMovementOptions, field);
                    //Set the selected pawn
                    pawnSelected = field.Pawn;

                    level.setCursor(PlayerTurn.ToString(), field);
                }
                //Else if there is an Pawn selected and the field you click on is added to the possible moves
                else if (pawnSelected != null &&
                        aPawnMovementOptions.Contains(field))
                {
                    //If the possible move contains an enemy Pawn
                    if (field.Pawn != null &&
                        field.Pawn.PlayerNumber != playerTurn)
                    {
                        //Send enemy Pawn back to it's start location
                        field.Pawn.toStartLocation();
                    }
                    //Move the Pawn to it's new location
                    levelModel.Fields[pawnSelected.CurrentLocation.X, pawnSelected.CurrentLocation.Y].Pawn.setLocation(field);
                    //Change back the cursor to an arrow
                    level.setCursor("arrow", field);

                    //If the target field contains a barricade
                    if (field.Barricade != null)
                    {
                        //Remember the barricade and do not end the players turn
                        barricade = field.Barricade;

                        level.setCursor("barricade", field);
                    }
                    //Else if the target field is a finish
                    else if (field is Model.Finish)
                    {
                        //The game has been won. 
                        winGame();
                    }
                    //Else the turn is over
                    else
                    {
                        //Give the turn to the next player
                        nextPlayer();
                    }
                    //Remove the possible moves- and selected pawn blurs
                    level.removeBlurs();
                    //Empty the selected pawn
                    pawnSelected = null;
                }
            }
        }

        public Boolean moveBarricade(Model.Field field)
        {
            if (field.Pawn == null && 
                field.Barricade == null &&
                (field.GetType() == typeof(Model.Field) || 
                field.GetType() == typeof(Model.Barricade)))
            {
                //If the row is in the array it can not contain a barricade
                if (!level.ANoBarricades.Contains(field.Y))
                {
                    barricade.setLocation(field);
                    barricade = null;
                    level.setCursor("arrow", field);
                    nextPlayer();
                    return true;
                }
            }
            return false;
        }

        public void winGame()
        {
            main.label_save.Visibility = Visibility.Collapsed;

            level.showWinAnimation(playerTurn);
            skip.Visibility = Visibility.Collapsed;
            dice.Visibility = Visibility.Collapsed;

            winner.showWinner(playerTurn);
            winner.Visibility = Visibility.Visible;
        }

        public void moveBarricadeRandom()
        {
            Model.Field field = levelModel.Fields[random.Next(0, level.LevelModel.IMapWidth), random.Next(0, level.LevelModel.IMapHeight)];
            while(field == null ||
                !moveBarricade(field))
            {
                field = levelModel.Fields[random.Next(0, level.LevelModel.IMapWidth), random.Next(0, level.LevelModel.IMapHeight)];
            }
        }

        public void saveCurrentGame()
        {
            save.saveTheGame(playerTurn);
        }

        private void keyPress(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                dice.dobbelen((int)e.Key - 34);
            }
        }

    }
}
