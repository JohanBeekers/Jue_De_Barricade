using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_De_Barricade_Eindproject.Controller
{
    class Player
    {
        private Boolean bIsHuman;
        private Pawn[] aPawns;
        private int iPawnAmount;

        //Main constructor. Creates the player and sets the start and current locations of his pawns. (For loading a game)
        public Player(Boolean isHuman, int pawns, Model.Field[] startLocations, Model.Field[] locations)
        {
            this.bIsHuman = isHuman;
            this.iPawnAmount = pawns;

            aPawns = new Pawn[iPawnAmount];
            for (int i = 0; i < iPawnAmount; i++)
            {
                aPawns[i] = new Pawn(startLocations[i], locations[i]);
            }
        }

        //Constructor that sets the current locations of pawns to the same location as the start locations. (For starting a new game)
        public Player(Boolean isHuman, int pawns, Model.Field[] startLocations) 
            : this(isHuman, pawns, startLocations, startLocations) 
        {

        }

        public Model.Field[] getPossibleMoves()
        {
            return null;
        }
    }
}
