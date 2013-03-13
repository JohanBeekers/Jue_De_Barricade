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
        private int iArrayAmount;
        private int iPawnAmount;

        //Main constructor.
        public Player(Boolean isHuman, int pawns)
        {
            this.bIsHuman = isHuman;
            this.iPawnAmount = pawns;
            aPawns = new Pawn[pawns];
            iArrayAmount = 0;
        }

        public void addPawn(Pawn pawn)
        {
            aPawns[iArrayAmount] = pawn;
            iArrayAmount++;
        }
    }
}
