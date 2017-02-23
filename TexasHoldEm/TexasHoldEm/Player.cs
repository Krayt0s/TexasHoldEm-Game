using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm
{
    class Player
    {

        //VARIABLES
        private static int totalNumberOfPlayers = -1;
        private int playerID;
        private int chips;
        private int chipsForCurrentHand;
        private bool isPlayer;


        //CONSTRUCTOR
        public Player( int numberOfStartingChips, bool isPlayer )
        {
            totalNumberOfPlayers++;
            this.playerID = totalNumberOfPlayers;
            this.chips = numberOfStartingChips;
            this.chipsForCurrentHand = 0;
            this.isPlayer = isPlayer;
        }


        //METHODS
        public void addToChips(int chipsToAdd)
        {
            this.chips += chipsToAdd;
        }


        //GETTERS AND SETTERS
        public int getPlayerID()
        {
            return this.playerID;
        }

        public bool getIsPlayer()
        {
            return this.isPlayer;
        }

        public int getTotalChips()
        {
            return this.chips;
        }

        public int getChipsForCurrentHand()
        {
            return this.chipsForCurrentHand;
        }
        public void setChipsForCurrentHand( int chipsForCurrentHand )
        {
            this.chips -= chipsForCurrentHand;
            this.chipsForCurrentHand = chipsForCurrentHand;
        }

    }
}
