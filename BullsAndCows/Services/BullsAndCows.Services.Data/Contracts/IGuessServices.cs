namespace BullsAndCows.Services.Data.Contracts
{
    using BullsAndCows.Data.Models;

    public interface IGuessServices
    {
        Guess MakeGuess(string number, int gameId);

        bool CanMakeGuess(string userId, int gameId);
    }
}
