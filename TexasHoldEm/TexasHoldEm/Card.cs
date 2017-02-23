using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm
{
    enum Suit { DIAMONDS, HEARTS, CLUBS, SPADES };
    enum Value { TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE };

    class Card
    {

        //VARIABLES
        private Suit cardSuit;
        private Value cardValue;


        //CONSTRUCTOR
        public Card( Suit cardSuit, Value cardValue )
        {
            this.cardSuit = cardSuit;
            this.cardValue = cardValue;
        }


        //GETTERS AND SETTERS
        public Suit getCardSuit()
        {
            return this.cardSuit;
        }

        public Value getCardValue()
        {
            return this.cardValue;
        }
    }
}
