namespace BullsAndCows.Services.Data
{
    using System;
    using System.Linq;
    using BullsAndCows.Data.Models;
    using BullsAndCows.Data.Repositories;
    using Contracts;

    public class GuessServices : IGuessServices
    {
        private IGenericRepository<Guess> guesses;
        private IGenericRepository<Game> games;

        public GuessServices(IGenericRepository<Guess> guesses, IGenericRepository<Game> games)
        {
            this.games = games;
            this.guesses = guesses;
        }

        public bool CanMakeGuess(string userId, int gameId)
        {
            return this.games.All()
                                .Any(g => g.Id == gameId
                                    && ((g.RedPlayerId == userId && g.GameState == GameState.RedTurn)
                                        || (g.BluePlayerId == userId && g.GameState == GameState.BlueTurn)));
        }

        public Guess MakeGuess(string number, int gameId)
        {
            var game = this.games.GetById(gameId);

            var playersNumber = game.GameState == GameState.RedTurn
                ? game.RedPlayerNumber
                : game.BluePlayerNumber;

            var player = game.GameState == GameState.RedTurn
                ? game.RedPlayerId
                : game.BluePlayerId;

            var guessToMake = new Guess
            {
                MadeOn = DateTime.UtcNow,
                UserId = player,
                Number = number,
                Bulls = CalculatBulls(number, playersNumber),
                Cows = CalculateCows(number, playersNumber)
            };

            this.guesses.Add(guessToMake);

            //((guessToMake.Bulls == 4
            //                ? (Action)(() => game.State = GameState.Finished)
            //                : () => { game.State = game.State == GameState.RedTurn ? GameState.BlueTurn : GameState.RedTurn; }))();

            if(guessToMake.Bulls == 4)
            {
                game.GameState = GameState.Finished;
                game.Result = game.GameState == GameState.RedTurn ? GameResult.WonByRed : GameResult.WonByBlue;
            }
            else
            {
                game.GameState = game.GameState == GameState.RedTurn ? GameState.BlueTurn : GameState.RedTurn;
            }

            game.Guesses.Add(guessToMake);

            this.games.Update(game);

            this.games.SaveChanges();
            this.guesses.SaveChanges();
            // TODO: Add player.AddGuess(); && game.AddGuess();

            return guessToMake;
        }

        private static int CalculatBulls(string number, string playersNumber)
        {
            var bulls = 0;

            for (int i = 0; i < 4; i++)
            {
                if (number[i] == playersNumber[i])
                {
                    bulls++;
                }
            }

            return bulls;
        }

        private static int CalculateCows(string number, string playersNumber)
        {
            var cows = 0;

            for (int i = 0; i < 4; i++)
            {
                if (playersNumber[i] != number[i])
                {
                    cows += number.Contains(playersNumber[i]) ? 1 : 0;
                }
            }

            return cows;
        }
    }
}
