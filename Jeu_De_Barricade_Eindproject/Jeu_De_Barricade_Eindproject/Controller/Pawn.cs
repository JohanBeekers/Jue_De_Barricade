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
    public class Pawn
    {
        private Model.Field startLocation;
        private Model.Field currentLocation;
        private Ellipse image;
        private int playerNumber;

        private List<Model.Field> possibleOptions = new List<Model.Field>();

        public Model.Field CurrentLocation
        {
            get { return currentLocation; }
            set { currentLocation = value; }
        }
        public int PlayerNumber
        {
            get { return playerNumber; }
            set { playerNumber = value; }
        }

        public Pawn(Ellipse image, Model.Field startLocation, int playerNumber)
        {
            this.startLocation = startLocation;
            this.currentLocation = startLocation;
            this.startLocation.Pawn = this;
            this.image = image;
            this.playerNumber = playerNumber;
        }

        public void setLocation(Model.Field location)
        {
            image.SetValue(Grid.ColumnProperty, location.X);
            image.SetValue(Grid.RowProperty, location.Y);

            currentLocation.Pawn = null;
            currentLocation = location;
            location.Pawn = this;
        }

        public Model.Field[] getPossibleMoves()
        {
            return null;
        }

        public void move(Model.Field field)
        {

        }

        //Method to get the possible options where to it could walk
        public List<Model.Field> getPossibleMoves(Model.Field curSpot, int remainingMoves, List<Model.Field> moveHistory)
        {
            List<Model.Field> retMoves = new List<Model.Field>();
            if( remainingMoves == 0 )
            {
                retMoves.Add(curSpot);
                return retMoves;
            }
            else if( remainingMoves >= 1 && curSpot.Barricade != null)
            {
                return retMoves;
            }
            else
            {
                moveHistory.Add(curSpot);
                if( curSpot.LinkNorth != null && !moveHistory.Contains(curSpot.LinkNorth) )
                {
                    retMoves.AddRange( getPossibleMoves( curSpot.LinkNorth, remainingMoves - 1, moveHistory ));
                }

                if (curSpot.LinkEast != null && !moveHistory.Contains(curSpot.LinkEast))
                {
                    retMoves.AddRange( getPossibleMoves( curSpot.LinkEast, remainingMoves - 1, moveHistory ));
                }

                if (curSpot.LinkSouth != null && !moveHistory.Contains(curSpot.LinkSouth))
                {
                    retMoves.AddRange( getPossibleMoves( curSpot.LinkSouth, remainingMoves - 1, moveHistory ));
                }

                if (curSpot.LinkWest != null && !moveHistory.Contains(curSpot.LinkWest))
                {
                    retMoves.AddRange( getPossibleMoves( curSpot.LinkWest, remainingMoves - 1, moveHistory ));
                }

                return retMoves;
            }
        }

        public List<Model.Field> getPossibleMoves(int worp)
        {
            return getPossibleMoves(currentLocation, worp, new List<Model.Field>());
        }

    }
}
