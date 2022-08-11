﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Ch9GoFishEndOfChapterProj
{
    public class GameState
    {
        public GameState(string humanPlayerName, IEnumerable<string> opponentNames, Deck stock)
        {
            List<Player> tempPlayersList = new List<Player>();
            List<Player> tempOpponentsList = new List<Player>();
            Player human = new Player(humanPlayerName);

            foreach (String opponentName in opponentNames)
                tempOpponentsList.Add(new Player(opponentName));
            Console.WriteLine(tempOpponentsList.Count());
            tempPlayersList.Add(human);
            tempPlayersList.AddRange(tempOpponentsList);
            Console.WriteLine(tempPlayersList.Count());


            foreach (Player player in tempPlayersList)
            {
                player.GetNextHand(stock);
                Console.WriteLine(player.Hand.Count());
            }

            Players = tempPlayersList;

            Opponents = tempOpponentsList;

            HumanPlayer = human;

            GameOver = false;

            Stock = stock;
            Console.WriteLine(Stock.Count);
        }
        public readonly IEnumerable<Player> Players;
        public readonly IEnumerable<Player> Opponents;
        public readonly Player HumanPlayer;
        public bool GameOver { get; set; }
        public readonly Deck Stock;

        public Player RandomPlayer(Player currentPlayer) => throw new NotImplementedException();
        public string PlayRound(Player player, Player playerToAsk, Value valuesToAskFor, Deck stock)
        {
            throw new NotImplementedException();
        }
        public string CheckForWinner()
        {
            throw new NotImplementedException();
        }
    }
}
