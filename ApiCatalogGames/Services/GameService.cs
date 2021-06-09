using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogGames.InputModel;
using ApiCatalogGames.Repositories;
using ApiCatalogGames.ViewModel;
using ApiCatalogGames.Entities;
using ApiCatalogGames.Exceptions;


namespace ApiCatalogGames.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Get(int page, int quantity)
        {
            var Games = await _gameRepository.Get(page, quantity);

            return Games.Select(Game => new GameViewModel
                                {
                                    Id = Game.Id,
                                    Name = Game.Name,
                                    Producer = Game.Producer,
                                    Price = Game.Price
                                })
                               .ToList();
        }

        public async Task<GameViewModel> Get(Guid id)
        {
            var Game = await _gameRepository.Get(id);

            if (Game == null)
                return null;

            return new GameViewModel
            {
                Id = Game.Id,
                Name = Game.Name,
                Producer = Game.Producer,
                Price = Game.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var entityGame = await _gameRepository.Get(game.Name, game.Producer);

            if (entityGame.Count > 0)
                throw new AlreadyRegisteredGameException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };

            await _gameRepository.Insert(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var entityGame = await _gameRepository.Get(id);

            if (entityGame == null)
                throw new UnregisteredGameException();

            entityGame.Name = game.Name;
            entityGame.Producer = game.Producer;
            entityGame.Price = game.Price;

            await _gameRepository.Update(entityGame);
        }

        public async Task Update(Guid id, double Price)
        {
            var entityGame = await _gameRepository.Get(id);

            if (entityGame == null)
                throw new UnregisteredGameException();

            entityGame.Price = Price;

            await _gameRepository.Update(entityGame);
        }

        public async Task Remove(Guid id)
        {
            var game = await _gameRepository.Get(id);

            if (game == null)
                throw new UnregisteredGameException();

            await _gameRepository.Remove(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}