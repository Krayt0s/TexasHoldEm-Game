using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm
{
    //An enum to be used to check the player's choice
    enum Action { CHECK, CALL, RAISE, ALLIN, FOLD, UNCHOSEN }
    class Round
    {

        private int numberOfPlayers;
        private int pot;
        private List<Hand> playerHands;
        private List<Card> cardsOnTable = new List<Card>();
        private Deck playingDeck;
        private List<Player> players;

        public Round( List<Player> players, Deck playingDeck )
        {
            this.players = players;
            this.numberOfPlayers = players.Count;
            this.pot = 0;
            this.playingDeck = playingDeck;
            playingDeck.shuffleDeck();
            this.playerHands = new List<Hand>();
            for ( int i = 0; i < numberOfPlayers; i++ )
            {
                playerHands.Add( new Hand( players[i] ) );
            }
            dealHand();
            //BETTING
            playerBetting();
            //CHECK FOR WINNER
            if (checkForWinner())
            {
                return;
            }
            //BURN CARD
            burnCard();
            //FLOP
            dealFlop();
            //BETTING
            playerBetting();
            //CHECK FOR WINNER
            if (checkForWinner())
            {
                return;
            }
            //BURN CARD
            burnCard();
            //TURN
            dealTurn();
            //BETTING
            playerBetting();
            //CHECK FOR WINNER
            if (checkForWinner())
            {
                return;
            }
            //BURN CARD
            burnCard();
            //RIVER
            dealRiver();
            //BETTING
            playerBetting();
            //CHECK FOR WINNER
            if (checkForWinner())
            {
                return;
            }
            //DECIDE WINNER
            //Console.WriteLine(playingDeck.getDeck().Count);

        }


        //Deals a hand to all playing players.
        private void dealHand()
        {
            for ( int i = 0; i < 2; i++ )
            {
                for (int j = 0; j < this.numberOfPlayers; j++)
                {
                    this.playerHands[j].getHand()[i] = playingDeck.getDeck().First<Card>();
                    this.playingDeck.getDeck().RemoveAt( 0 );
                }
            }

            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine("Player " + i + "'s Hand is: " + playerHands[i].getHand()[0].getCardValue() + " of " + playerHands[i].getHand()[0].getCardSuit() + "\t" + playerHands[i].getHand()[1].getCardValue() + " of " + playerHands[i].getHand()[1].getCardSuit());
            }
        }

        //Deals the flop and removes the cards from the deck into a seperate list.
        private void dealFlop()
        {
            Console.WriteLine("Flop:");
            //Gets the first card each time places it into the cardsOnTable list
            //then deletes the card from the deck.
            for ( int i = 0; i < 3; i++ )
            {
                cardsOnTable.Add(playingDeck.getDeck().First<Card>());
                playingDeck.getDeck().Remove(playingDeck.getDeck().First<Card>());
                Console.WriteLine( cardsOnTable[i].getCardValue() + " of " + cardsOnTable[i].getCardSuit() );
            }


        }

        //Deals the turn and removes the card from the deck and puts it
        //into a seperate list.
        private void dealTurn()
        {
            cardsOnTable.Add(playingDeck.getDeck().First<Card>());
            playingDeck.getDeck().Remove(playingDeck.getDeck().First<Card>());
            Console.WriteLine(cardsOnTable.Last<Card>().getCardValue() + " of " + cardsOnTable.Last<Card>().getCardSuit());
        }

        //Deals the river and removes the card from the deck and puts it
        //into a seperate list.
        private void dealRiver()
        {
            cardsOnTable.Add(playingDeck.getDeck().First<Card>());
            playingDeck.getDeck().Remove(playingDeck.getDeck().First<Card>());
            Console.WriteLine(cardsOnTable.Last<Card>().getCardValue() + " of " + cardsOnTable.Last<Card>().getCardSuit());
        }

        //Burns the card prior to Flop, Turn and River.
        private void burnCard()
        {
            //Get the first card and copy it to the back of the deck.
            playingDeck.getDeck().Add(playingDeck.getDeck().First<Card>());
            //Delete the first card.
            playingDeck.getDeck().Remove(playingDeck.getDeck().First<Card>());
        }

        //If the player is the last left, the pot is added to their total
        //number of chips. This returns true, so that the constructor
        //can terminate the instance and return to main.
        private bool checkForWinner()
        {
            if (players.Count == 1)
            {
                players.First<Player>().addToChips( pot );
                return true;
            }
            
            return false;
        }

        //Deals with bets made, all players must have an equal number of
        //chips to put in the pot by the end of the loop for it to
        //terminate and go onto the next fase of the round.
        private void playerBetting()
        {
            bool allBetsEqual = false;
            //Used to check against so that the players can
            //match this or increase it.
            int largestBet = 0;

            while (!allBetsEqual)
            { 
                //Loop through all players, check whether they're an AI
                //or not, if they're not, take in user input.
                for (int i = 0; i < players.Count; i++)
                {
                    Player currentPlayer = players[i];

                    //If the player is a human check the human's input
                    //otherwise let AI calculate and perform move.
                    if (true == currentPlayer.getIsPlayer())
                    {
                        //Gives back general information about the player
                        //and the current largest bet.
                        Console.WriteLine("\nPlayer " + currentPlayer.getPlayerID() + "'s turn");
                        Console.WriteLine("\tCurrent largest bet: " + largestBet + "\n\tYour bet: " + currentPlayer.getChipsForCurrentHand());

                        //This loops through to get the player's action, this will keep
                        //looping until the player chooses a valid action.
                        bool actionUnchosen = false;
                        while (!actionUnchosen)
                        {
                            //If the player currently has put in less than the largest bet
                            //call this (prevents checking below the largest bet). Else allow the
                            //player to check, raise etc.
                            if (currentPlayer.getChipsForCurrentHand() < largestBet)
                            {
                                //The value required to call the largest bet.
                                int toCall = largestBet - currentPlayer.getChipsForCurrentHand();
                                Console.WriteLine("It costs: " + toCall + " to call.\nYou can:\n\tCall\n\tRaise\n\tAllin\n\tFold");
                                String choice = Console.ReadLine();
                                Action action = checkAction(choice.ToLower());

                                /******************************
                                 *    Can be functioned out.
                                 ******************************/
                                switch (action)
                                {
                                    case Action.CALL:
                                        call(currentPlayer, largestBet);
                                        actionUnchosen = true;
                                        Console.WriteLine( largestBet );
                                        break;
                                    case Action.RAISE:
                                        raise(currentPlayer, largestBet, 10);//CHANGE TO DYNAMICALLY GE THE RAISE AMOUNT.
                                        actionUnchosen = true;
                                        Console.WriteLine(largestBet);
                                        break;
                                    case Action.ALLIN:
                                        allin(currentPlayer, largestBet);
                                        actionUnchosen = true;
                                        Console.WriteLine(largestBet);
                                        break;
                                    case Action.FOLD:
                                        fold(currentPlayer);
                                        actionUnchosen = true;
                                        Console.WriteLine(largestBet);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("You can:\n\tCheck\n\tRaise\n\tAllin\n\tFold\nI will:");
                                String choice = Console.ReadLine();
                                Action action = checkAction(choice.ToLower());

                                /******************************
                                 *    Can be functioned out.
                                 ******************************/
                                //Switch on the player's action, then call the appropriate
                                //functions. Then break out of the while loop by changing the
                                //boolean value.
                                switch (action)
                                {
                                    case Action.CHECK:
                                        actionUnchosen = true;
                                        Console.WriteLine(largestBet);
                                        break;
                                    case Action.RAISE:
                                        largestBet = raise(currentPlayer, largestBet, 10);//CHANGE TO DYNAMICALLY GE THE RAISE AMOUNT.
                                        actionUnchosen = true;
                                        Console.WriteLine(largestBet);
                                        break;
                                    case Action.ALLIN:
                                        largestBet = allin(currentPlayer, largestBet);
                                        actionUnchosen = true;
                                        Console.WriteLine(largestBet);
                                        break;
                                    case Action.FOLD:
                                        fold(currentPlayer);
                                        actionUnchosen = true;
                                        Console.WriteLine(largestBet);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        //THIS IS FOR THE AI
                    }
                }

                for (int j = 0; j < players.Count; j++)
                {
                    if (players[j].getChipsForCurrentHand() != largestBet)
                    {
                        break;
                    }
                    else if ( j == players.Count - 1 && players[j].getChipsForCurrentHand() == largestBet)
                    {
                        allBetsEqual = true;
                    }
                }

            }
            for ( int i = 0; i < players.Count; i++ )
            {
                pot += players[i].getChipsForCurrentHand();
                players[i].setChipsForCurrentHand( 0 );
            }
            Console.WriteLine( pot );
        }

        //This takes a string (preferably forced to lower case)
        //and checks to see if it matches any of the enums.
        private Action checkAction( String action )
        {
            switch ( action )
            {
                case "check":
                    return Action.CHECK;
                case "call":
                    return Action.CALL;
                case "raise":
                    return Action.RAISE;
                case "allin":
                    return Action.ALLIN;
                case "fold":
                    return Action.FOLD;
                default:
                    return Action.UNCHOSEN;
            }
        }

        //Used to check.
        private void check(){}

        //Used to call the largest bet.
        private void call( Player currentPlayer, int difference )
        {
            //Sets the player's chips for the curent hand to be equal to the
            //largest bet.
            currentPlayer.setChipsForCurrentHand( currentPlayer.getChipsForCurrentHand() + difference);
        }

        //Used to raise the largest bet. Returns a new
        //current max.
        private int raise( Player currentPlayer, int currentMax, int amountToRaise)
        {
            //Sets the player's chips for the current hand to be equal to the
            //current max + the amount to be raised by.
            currentPlayer.setChipsForCurrentHand(currentMax + amountToRaise);
            return currentMax + amountToRaise;
        }

        //Used to go all in, this raises the current max
        //to be that of the total numer of chips of the
        //player.
        private int allin(Player currentPlayer, int currentMax)
        {
            //Sets the play'ers chips for the curent hand to be equal
            //to the all of their chips.
            currentPlayer.setChipsForCurrentHand( currentPlayer.getTotalChips() );
            Console.WriteLine(currentPlayer.getChipsForCurrentHand());
            if ( currentPlayer.getChipsForCurrentHand() > currentMax )
            {
                return currentPlayer.getChipsForCurrentHand();
            }
            return currentMax;
        }

        //Used to fold the hand. This will put the
        //player's chips into the pot and remove the
        //players from the players list for this round.
        private void fold(Player currentPlayer)
        {
            this.pot += currentPlayer.getChipsForCurrentHand();
            currentPlayer.addToChips( -currentPlayer.getChipsForCurrentHand() );
            players.Remove(currentPlayer);
        }

        /*
        //Fold hand and return cards to deck.
        private void foldHand( Hand playerHand )
        {
            playingDeck.getDeck().Add(playerHand.getHand()[0]);
            playingDeck.getDeck().Add(playerHand.getHand()[1]);
            playerHands.Remove( playerHand );
            players.Remove(playerHand.getPlayer());
        }
        */

        //GETTERS AND SETTERS
    }
}
