using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jeu_De_Barricade_Eindproject.Controller
{
    class LoadGame
    {
        private Controller.Game game;
        private StreamReader sr;

        private String fileName;


        public LoadGame(Controller.Game game)
        {
            this.game = game;
        }

        public void loadSavedGame(String file)
        {
            fileName = file;

            int humanPlayers = 0;
            int turn = 0;
            String boardType = "";

            int index = 0;

            sr = new StreamReader("Opgeslagen spellen/" + fileName + ".save");

            String line;
            int lineNumber = 0;

            while (((line = sr.ReadLine()) != null))
            {
                switch(lineNumber)
                {
                    //Amount of human players
                    case 0:

                        index = (line.IndexOf(":")) +1;
                        line = line.Substring(index, (line.Count() - index));

                        humanPlayers = Int32.Parse(line);

                        break;
                    //Who's turn is it
                    case 1:
                        
                        index = (line.IndexOf(":")) +1;
                        line = line.Substring(index, (line.Count() - index));

                        switch (line) 
                        { 
                            case("red"):
                                turn = 0;
                                break;
                            case ("green"):
                                turn = 1;
                                break;
                            case ("yellow"):
                                turn = 2;
                                break;
                            case ("blue"):
                                turn = 3;
                                break;
                        }

                        break;
                    //What boardtype were they playing
                    case 2:

                        index = (line.IndexOf(":")) +1;
                        boardType = line.Substring(index, (line.Count() - index));

                        //Create the right board with the right amount of human players
                        game.newBoard(humanPlayers, boardType, turn);

                        break;

                }
                lineNumber++;
            }
            sr.Close();
        }

        public void placePawns(Model.ModelLevel levelModel)
        {
            int lineNumber = 0;

            String line;

            int row = 0;
            int column = 0;
            int barricadeNumber = 0;
            int rPlayer = 0, bPlayer = 0, yPlayer = 0, gPlayer = 0;

            sr = new StreamReader("Opgeslagen spellen/" + fileName + ".save");

            while (((line = sr.ReadLine()) != null))
            {
                if (lineNumber >= 3)
                {
                    column = 0;

                    String[] separator = new String[] { ") (" };
                    String[] fields = line.Split(separator, StringSplitOptions.None);
                    foreach (String field in fields)
                    {

                        if(field.Contains("#"))
                        {
                            levelModel.ABarricades[barricadeNumber].setLocation(levelModel.Fields[column,row]);                            
                            barricadeNumber++;
                        }
                        else if(field.Contains("r"))
                        {
                            game.APlayers[0].APawns[rPlayer].setLocation(levelModel.Fields[column, row]); 
                            rPlayer++;
                        }
                        else if (field.Contains("g"))
                        {
                            game.APlayers[1].APawns[gPlayer].setLocation(levelModel.Fields[column, row]); 
                            gPlayer++;
                        }
                        else if (field.Contains("y"))
                        {
                            game.APlayers[2].APawns[yPlayer].setLocation(levelModel.Fields[column, row]); 
                            yPlayer++;
                        }
                        else if (field.Contains("b"))
                        {
                            game.APlayers[3].APawns[bPlayer].setLocation(levelModel.Fields[column, row]); 
                            bPlayer++;
                        }

                        column++;
                    }
                    row++;
                }
                lineNumber++;
            }
        }
    }
}
