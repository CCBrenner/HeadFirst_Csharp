﻿using System.Collections.Generic;
using System.Linq;

namespace Ch9GoFishEndOfChapterProj
{
    public class GameController
    {
        public GameController(string humanPlayerName, IEnumerable<string> computerPlayerNames)
        {
            Deck shuffledDeck = new Deck();
            gameState = new GameState(humanPlayerName, computerPlayerNames, shuffledDeck.Shuffle());

            string tempStatus = $"Starting a new game with players {HumanPlayer}";
            foreach (Player opponent in gameState.Opponents) tempStatus += $", {opponent}";
            Status = tempStatus;
        }
        public static Random Random = new Random();

        public GameState gameState;
        public bool GameOver { get { return gameState.GameOver; } }
        public Player HumanPlayer { get { return gameState.HumanPlayer; } }
        public IEnumerable<Player> Opponents { get { return gameState.Opponents; } }

        public string Status { get; private set; }
        public void NextRound(Player playerToAsk, Value valueToAskFor)
        {
            if(playerToAsk == HumanPlayer && valueToAskFor == Value.Null)
                Status = $"{HumanPlayer.Name} has no cards and the stock is empty.";
            else
                Status = $"{gameState.PlayRound(HumanPlayer, playerToAsk, valueToAskFor, gameState.Stock)}";

            ComputerPlayersPlayNextRound();

            Status += $"{Environment.NewLine}";

            foreach (Player player in gameState.Players)
            {
                Status += $"{Environment.NewLine}{player.Name} has {player.Hand.Count()} card{Player.S(player.Hand.Count())} and {player.Books.Count()} book{Player.S(player.Books.Count())}";
            }

            Status += $"{Environment.NewLine}The stock has {gameState.Stock.Count()} card{Player.S(gameState.Stock.Count())}";
        }
        public void ComputerPlayersPlayNextRound()
        {
            foreach (Player opponent in gameState.Opponents)
            {
                if(opponent.Hand.Count() == 0 && gameState.Stock.Count() == 0)
                    Status += $"{Environment.NewLine}{opponent.Name} has no cards and the stock is empty.";
                else
                {
                    string statusAddition = gameState.PlayRound(opponent, gameState.RandomPlayer(opponent), opponent.RandomValueFromHand(), gameState.Stock);
                    Status += $"{Environment.NewLine}{statusAddition}";
                }
            }
        }
        public void NewGame()
        {
            Status = "Starting a new game";

            List<string> tempOpponents = new List<string>();
            foreach(Player opponent in gameState.Opponents)
            {
                tempOpponents.Add(opponent.Name);
            }
            gameState = new GameState(gameState.HumanPlayer.Name, tempOpponents, new Deck());
        }
    }
}
