using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm
{
    class Deck
    {

        //VARIABLES
        private List<Card> deck = new List<Card>();
        private Random rand = new Random();


        //CONSTRUCTOR
        //Initialises the Deck to have 52 different cards.
        public Deck()
        {
            //Initialise the deck.
            for ( int i = 0; i < 4; i++ )
            {
                for ( int j = 0; j < 13; j++ )
                {
                    this.deck.Add( new Card( (Suit)i, (Value)j ) );
                }
            }

            //Shuffle the deck before starting the hand.
            shuffleDeck();
        }


        //METHODS
        //Shuffles the deck.
        public void shuffleDeck()
        {
            //Gets a pseudo random integer between 0 and 51.
            for ( int i = 0; i < 52; i++ )
            {
                int newCurrentCardPosition = rand.Next( 0, 52 );
                //Holds the object at the position to be filled.
                Card secondCard = this.deck[newCurrentCardPosition];
                //Replace card.
                this.deck[newCurrentCardPosition] = this.deck[i];
                //Put swapped card in the first index position.
                this.deck[i] = secondCard;
            }
        }


        //GETTERS AND SETTERS
        public List<Card> getDeck()
        {
            return this.deck;
        }
        private void setDeck( List<Card> deck )
        {
            this.deck = deck;
        }

    }
}
