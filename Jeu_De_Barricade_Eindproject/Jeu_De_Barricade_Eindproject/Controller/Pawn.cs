using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_De_Barricade_Eindproject.Controller
{
    public class Pawn
    {
        Model.Field startLocation;
        Model.Field currentLocation;

        public Pawn(Model.Field startLocation, Model.Field currentLocation)
        {
            this.startLocation = startLocation;
            this.currentLocation = currentLocation;

            currentLocation.Pawn = this;
        }
    }
}
