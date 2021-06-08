using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApiCatalogGames.ViewModel;
using ApiCatalogGames.InputModel;

namespace ApiCatalogGames.Services
{
    public interface IGameService
    {
         Task<List<GameViewModel>> Get(int page,int quantity);
         Task<List<GameViewModel>> Get(Guid idGame);
         Task<GameViewModel> Insert(GameInputModel game);
         Task Update(Guid id,GameInputModel game);
         Task Update(Guid id,double price);
         Task Remove(Guid id);

    }
}