using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_De_Barricade_Eindproject.Model
{
    public class Field
    {
        private Field linkNorth, linkEast, linkSouth, linkWest;
        private Controller.Pawn pawn;
        private Barricade barricade;
        
        public Field LinkNorth { get; set; }
        public Field LinkEast { get; set; }
        public Field LinkSouth { get; set; }
        public Field LinkWest { get; set; }
        public Controller.Pawn Pawn { get; set; }
        public Barricade Barricade { get; set; }
    }

    public class Barricade : Field
    {

    }

    public class SafeSpot : Field
    {

    }

    public class Finish : Field
    {

    }

    public class LineField : Field
    {

    }

    public class BlueBase : Field
    {

    }

    public class GreenBase : Field
    {

    }

    public class YellowBase : Field
    {

    }

    public class RedBase : Field
    {

    }

    public class BlueStart : Field
    {

    }

    public class GreenStart : Field
    {

    }

    public class YellowStart : Field
    {

    }

    public class RedStart : Field
    {

    }
}
