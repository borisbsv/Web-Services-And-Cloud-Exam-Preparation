namespace BullsAndCows.Services.Data.Contracts
{
    using System.Linq;
    using BullsAndCows.Data.Models;

    public interface IGameServices
    {
        IQueryable<Game> GetPublicGames(int page = 0, string userId = null);

        IQueryable<Game> GetGameDetails(int id);

        Game CreateGame(string name, string number, string userId);

        Game JoinGame(int id, string userId, string number);

        // tfa e 100t se obidi, nali?
        //Game CreateGame(string name, string number, string v);
    }
}
