namespace BullsAndCows.Services.Data
{
    using System;
    using System.Linq;
    using BullsAndCows.Data.Models;
    using BullsAndCows.Data.Repositories;
    using Contracts;

    public class GameServices : IGameServices
    {
        private const int DefaultPageSize = 10;
        private IGenericRepository<Game> games;

        public GameServices(IGenericRepository<Game> gameRepo)
        {
            this.games = gameRepo;
        }

        public Game CreateGame(string name, string number, string userId)
        {

            var gameToAdd = new Game
            {
                Name = name,
                RedPlayerNumber = number,
                RedPlayerId = userId,
                DateCreated = DateTime.UtcNow,
                GameState = GameState.AwaitingOpponent
            };

            this.games.Add(gameToAdd);
            this.games.SaveChanges();

            return gameToAdd;
        }

        public IQueryable<Game> GetGameDetails(int id)
        {
            return this.games
                            .All()
                            .Where(g => g.Id == id);
        }

        public IQueryable<Game> GetPublicGames(int page = 0, string userId = null)
        {
            return this.games.All()
                .Where(g => g.GameState == GameState.AwaitingOpponent
                || ((g.GameState != GameState.Finished)
                && (g.RedPlayerId == userId || g.BluePlayerId == userId)))
                .OrderBy(g => g.GameState)
                .ThenBy(g => g.Name)
                .ThenBy(g => g.DateCreated)
                .Skip(page * DefaultPageSize)
                .Take(DefaultPageSize);
        }

        public Game JoinGame(int id, string userId, string number)
        {
            var gameToJoin = this.games.All()
                .Where(g => g.Id == id)
                .FirstOrDefault();

            gameToJoin.BluePlayerId = userId;
            gameToJoin.BluePlayerNumber = number;

            this.games.Update(gameToJoin);
            this.games.SaveChanges();

            return gameToJoin;
        }

        public bool CanJoinGame(string userId, int gameId)
        {
            return this.games.All()
                .Where(g => g.Id == gameId
                    && g.GameState == GameState.AwaitingOpponent)
                .Any(g => g.RedPlayerId != userId);
        }

        //private IQueryable<Game> GetFilteredGames(Expression<Func<Game, bool>> condition)
        //{
        //    return this.games.All()
        //                     .Where(condition);
        //}

        //private IQueryable<Game> SortGames()
    }
}
