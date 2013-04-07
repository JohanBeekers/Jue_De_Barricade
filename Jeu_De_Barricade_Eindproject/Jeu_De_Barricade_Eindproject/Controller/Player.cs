using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jeu_De_Barricade_Eindproject.Controller
{
    public class Player
    {
        private Game game;
        private Boolean bIsHuman;
        private Pawn[] aPawns;
        private int iArrayAmount;
        private int iPawnAmount;
        Random random;

        public Boolean BIsHuman
        {
            get { return bIsHuman; }
            set { bIsHuman = value; }
        }

        public Pawn[] APawns
        {
            get { return aPawns; }
        }

        //Main constructor.
        public Player(Boolean isHuman, int pawns, Game game)
        {
            this.game = game;
            this.bIsHuman = isHuman;
            this.iPawnAmount = pawns;
            aPawns = new Pawn[pawns];
            iArrayAmount = 0;
            random = new Random();
        }

        //Add a new pawn to this player.
        public void addPawn(Pawn pawn)
        {
            aPawns[iArrayAmount] = pawn;
            iArrayAmount++;
        }

        //Get a random pawn of this player (for the AI)
        private Pawn getRandomPawn()
        {
            int pawn = random.Next(0, iPawnAmount);
            return aPawns[pawn];
        }

        //Place a random pawn on a random possible location (for the AI)
        //If placed on a barricade, it will place the barricade on a random field. 
        public void automateTurn(int worp)
        {
            List<Pawn> pawnNoOption = new List<Pawn>();

            //Select a random pawn
            Pawn tempPawn = getRandomPawn();
            Model.Field[] possibleMoves = tempPawn.getPossibleMoves(worp).ToArray();
            while (possibleMoves.Count() == 0 && pawnNoOption.Count() < aPawns.Count())
            {
                if (!pawnNoOption.Contains(tempPawn))
                {
                    pawnNoOption.Add(tempPawn);
                }

                tempPawn = getRandomPawn();
                possibleMoves = tempPawn.getPossibleMoves(worp).ToArray();
            }

            if (pawnNoOption.Count() == aPawns.Count())
            {
                game.nextPlayer();
                return;
            }

            game.fieldClick(tempPawn.CurrentLocation);
            game.fieldClick(possibleMoves[random.Next(0, possibleMoves.Count())]);

            if (game.Barricade != null)
            {
                game.moveBarricadeRandom();
            }
        }
    }
}
