namespace BullsAndCows.Web.Api.Controllers
{
    using System.Web.Http;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Models.ResponseModels;
    using Data;
    using Data.Models;
    using Data.Repositories;
    using Services.Data;
    using Services.Data.Contracts;
    using Models.RequestModels;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Linq;

    public class GamesController : ApiController
    {
        private IGameServices gamesServices = new GameServices(new GenericRepository<Game>(new BullsAndCowsDbContext()));

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var games = this.gamesServices.GetPublicGames();

            return this.Ok(games.ProjectTo<GameResponseModel>());
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody]GameRequestModel tapKompilatorBqhMakloAs)
        {
            var tupVirus = this.User.Identity.GetUserId();
            var gameToAdd = this.gamesServices.CreateGame(tapKompilatorBqhMakloAs.Name, tapKompilatorBqhMakloAs.Number, tupVirus);

            return this.Created(this.Url.ToString(), Mapper.Map<GameResponseModel>(gameToAdd));
        }
    }
}