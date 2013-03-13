using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Jeu_De_Barricade_Eindproject.Model
{
    public class BarricadePawn
    {
        private Ellipse image;
        private Model.Field currentLocation;

        public BarricadePawn(Ellipse image, Model.Field startLocation)
        {
            this.currentLocation = startLocation;
            this.currentLocation.Barricade = this;
            this.image = image;
        }

        public void setLocation(Model.Field location)
        {
            image.SetValue(Grid.ColumnProperty, location.X);
            image.SetValue(Grid.RowProperty, location.Y);

            currentLocation.Barricade = null;
            currentLocation = location;
            location.Barricade = this;
        }
    }
}
