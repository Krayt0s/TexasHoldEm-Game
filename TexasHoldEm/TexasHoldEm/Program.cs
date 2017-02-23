using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldEm
{
    class Program
    {
        private static Deck playingDeck = new Deck();
        private static List<Player> players = new List<Player>();

        static void Main(string[] args)
        {
            players.Add(new Player(1000, true));
            players.Add(new Player(10, true));

            /*********************************************************************
             * https://en.wikipedia.org/wiki/Poker_probability_(Texas_hold_'em)
             *********************************************************************/


            Round firstRound = new Round( players, playingDeck );
            for ( int i = 0; i < players.Count; i++)
            {
                if ( 0 == players[i].getTotalChips() )
                {
                    players.RemoveAt(i);
                    Console.WriteLine("Player " + i + " has been eliminated from the game");
                }
            }
            Console.ReadKey();
        }
    }
}
