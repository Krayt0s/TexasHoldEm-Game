using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm
{
    class Hand
    {

        //VARIABLES
        private Card[] hand = new Card[2];
        private Player player;
        private int score;


        //CONSTRUCTOR
        public Hand(Player player)
        {
            this.player = player;
        }


        //METHODS
        public void calculateScore()
        {
            //https://en.wikipedia.org/wiki/Texas_hold_'em_starting_hands
        }


        //GETTERS AND SETTERS
        public Card[] getHand()
        {
            return this.hand;
        }

        public int getScore()
        {
            return this.score;
        }
        public void setScore( int score )
        {
            this.score = score;
        }

        public Player getPlayer()
        {
            return this.player;
        }

    }
}
