using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jeu_De_Barricade_Eindproject.Controller
{
    class MapConverter
    {
        private Model.Field[,] fields;
        private int iRows, iColumns;
        private Model.Field redStart, greenStart, yellowStart, blueStart;

        public Model.Field[,] convertMap(String[,] stringMap, int width, int height)
        {
            iRows = height;
            iColumns = width;

            fields = new Model.Field[iColumns,iRows];

            for (int iRow = 0; iRow < iRows; iRow++)
            {
                for (int iColumn = 0; iColumn < iColumns; iColumn++)
                {
                    switch (stringMap[iRow,iColumn])
                    {
                        case "f":
                            fields[iColumn, iRow] = new Model.Finish();
                            break;
                        case "#":
                            fields[iColumn, iRow] = new Model.Barricade();
                            break;
                        case "o":
                            fields[iColumn, iRow] = new Model.Field();
                            break;
                        case "|":
                            fields[iColumn, iRow] = new Model.LineField();
                            break;
                        case "s":
                            fields[iColumn, iRow] = new Model.SafeSpot();
                            break;
                        case "r":
                            fields[iColumn, iRow] = new Model.RedBase();
                            break;
                        case "R":
                            redStart = new Model.RedStart();
                            fields[iColumn, iRow] = redStart;
                            break;
                        case "g":
                            fields[iColumn, iRow] = new Model.GreenBase();
                            break;
                        case "G":
                            greenStart = new Model.GreenStart();
                            fields[iColumn, iRow] = greenStart;
                            break;
                        case "y":
                            fields[iColumn, iRow] = new Model.YellowBase();
                            break;
                        case "Y":
                            yellowStart = new Model.YellowStart();
                            fields[iColumn, iRow] = yellowStart;
                            break;
                        case "b":
                            fields[iColumn, iRow] = new Model.BlueBase();
                            break;
                        case "B":
                            blueStart = new Model.BlueStart();
                            fields[iColumn, iRow] = blueStart;
                            break;
                        default:
                            break;
                    }

                    if(fields[iColumn, iRow] is Model.Field)
                    {
                        fields[iColumn, iRow].X = iColumn;
                        fields[iColumn, iRow].Y = iRow;
                    }
                    
                }
            }

            createLinks();

            return fields;
        }

        //Set all the links of the fields.
        private void createLinks()
        {
            for (int iRow = 0; iRow < iRows; iRow++)
            {
                for (int iColumn = 0; iColumn < iColumns; iColumn++)
                {
                    if (fields[iColumn, iRow] != null)
                    {
                        //Set the links of every field
                        if (fields[iColumn, iRow].GetType() == typeof(Model.Field) ||
                            fields[iColumn, iRow].GetType() == typeof(Model.Barricade) ||
                            fields[iColumn, iRow].GetType() == typeof(Model.SafeSpot) ||
                            fields[iColumn, iRow].GetType() == typeof(Model.Finish) ||
                            fields[iColumn, iRow].GetType() == typeof(Model.RedStart) ||
                            fields[iColumn, iRow].GetType() == typeof(Model.GreenStart) ||
                            fields[iColumn, iRow].GetType() == typeof(Model.YellowStart) ||
                            fields[iColumn, iRow].GetType() == typeof(Model.BlueStart))
                        {

                            int iRowCheck = iRow;
                            int iColCheck = iColumn;
                            String sCheck = "north";
                            Boolean bDone = false;
                            
                            while(!bDone)
                            {
                                switch (sCheck)
                                {
                                    case "north":
                                        iRowCheck--;
                                        break;
                                    case "east":
                                        iColCheck++;
                                        break;
                                    case "south":
                                        iRowCheck++;
                                        break;
                                    case "west":
                                        iColCheck--;
                                        break;
                                }

                                //If the next link is null, falls out of range or is a Field type that should you should not be able to navigate to, change direction.
                                if (iColCheck < 0 || 
                                    iColCheck >= iColumns ||
                                    iRowCheck < 0 ||
                                    iRowCheck >= iRows ||
                                    fields[iColCheck, iRowCheck] == null ||
                                    fields[iColCheck, iRowCheck].GetType() == typeof(Model.RedStart) ||
                                    fields[iColCheck, iRowCheck].GetType() == typeof(Model.GreenStart) ||
                                    fields[iColCheck, iRowCheck].GetType() == typeof(Model.YellowStart) ||
                                    fields[iColCheck, iRowCheck].GetType() == typeof(Model.BlueStart))
                                {
                                    switch (sCheck)
                                    {
                                        case "north":
                                            sCheck = "east";
                                            break;
                                        case "east":
                                            sCheck = "south";
                                            break;
                                        case "south":
                                            sCheck = "west";
                                            break;
                                        case "west":
                                            bDone = true;
                                            break;
                                    }

                                    //Reset the row and column to the field we are creating links for.
                                    iRowCheck = iRow;
                                    iColCheck = iColumn;
                                }
                                //If the next link is a line link, do nothing. The whole while loop will start again another position in the current direction. 
                                else if (fields[iColCheck, iRowCheck].GetType() == typeof(Model.LineField))
                                {

                                }
                                //If the next link is one we can navigate to, set the link, reset the field we are checking and change direction.
                                else
                                {   
                                    //Set the link and go to the next direction.
                                    switch (sCheck)
                                    {
                                        case "north":
                                            fields[iColumn, iRow].LinkNorth = fields[iColCheck, iRowCheck];
                                            sCheck = "east";
                                            break;
                                        case "east":
                                            fields[iColumn, iRow].LinkEast = fields[iColCheck, iRowCheck];
                                            sCheck = "south";
                                            break;
                                        case "south":
                                            fields[iColumn, iRow].LinkSouth = fields[iColCheck, iRowCheck];
                                            sCheck = "west";
                                            break;
                                        case "west":
                                            fields[iColumn, iRow].LinkWest = fields[iColCheck, iRowCheck];
                                            bDone = true;
                                            break;
                                    }

                                    //Reset the row and column to the field we are creating links for.
                                    iRowCheck = iRow;
                                    iColCheck = iColumn;
                                }
                            }
                        }
                        //Set the North link of every players base fields to his corresponding starting field. 
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.RedBase))
                        {
                            fields[iColumn, iRow].LinkNorth = redStart;
                        }
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.GreenBase))
                        {
                            fields[iColumn, iRow].LinkNorth = greenStart;
                        }
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.YellowBase))
                        {
                            fields[iColumn, iRow].LinkNorth = yellowStart;
                        }
                        else if (fields[iColumn, iRow].GetType() == typeof(Model.BlueBase))
                        {
                            fields[iColumn, iRow].LinkNorth = blueStart;
                        }
                    }
                }
            }
        }

    }
}
