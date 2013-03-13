using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
