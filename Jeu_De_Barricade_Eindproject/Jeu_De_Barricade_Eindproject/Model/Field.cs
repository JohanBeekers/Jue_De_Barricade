using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_De_Barricade_Eindproject.Model
{
    public class Field
    {
        protected Field linkNorth, linkEast, linkSouth, linkWest;
        protected Controller.Pawn pawn;
        protected Model.BarricadePawn barricadePawn;
        protected int x, y;

        //Properties:
        public Field LinkNorth
        {
            get { return linkNorth; }
            set { linkNorth = value; }
        }
        public Field LinkEast
        {
            get { return linkEast; }
            set { linkEast = value; }
        }
        public Field LinkSouth
        {
            get { return linkSouth; }
            set { linkSouth = value; }
        }
        public Field LinkWest
        {
            get { return linkWest; }
            set { linkWest = value; }
        }
        public Controller.Pawn Pawn
        {
            get { return pawn; }
            set { pawn = value; }
        }
        public BarricadePawn Barricade
        {
            get { return barricadePawn; }
            set { barricadePawn = value; }
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
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
