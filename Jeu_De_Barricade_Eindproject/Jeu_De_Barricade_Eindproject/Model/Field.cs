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

    //A barricade field
    public class Barricade : Field
    {

    }

    //A safespot where the pawn can't be hit. 
    public class SafeSpot : Field
    {

    }

    //The finish
    public class Finish : Field
    {

    }

    //A vertical line, connecting two other fields. 
    public class LineField : Field
    {

    }

    //A field where the blue player starts. 
    public class BlueBase : Field
    {

    }

    //A field where the green player starts. 
    public class GreenBase : Field
    {

    }

    //A field where the yellow player starts. 
    public class YellowBase : Field
    {

    }

    //A field where the red player starts. 
    public class RedBase : Field
    {

    }

    //The starting location for every pawn of the blue player.
    public class BlueStart : Field
    {

    }

    //The starting location for every pawn of the green player.
    public class GreenStart : Field
    {

    }

    //The starting location for every pawn of the yellow player.
    public class YellowStart : Field
    {

    }

    //The starting location for every pawn of the red player.
    public class RedStart : Field
    {

    }
}
