using ApiCatalogGames.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCatalogGames.Entities;

namespace ApiCatalogGames.Repositories
{
    public interface IGameRepository : IDisposable
    {
         Task<List<Game>> Get(int page,int quantify);

         Task<Game> Get(Guid id);
         
         Task<List<Game>> Get(string name,string produtora);

         Task Insert(Game game);

         Task Update(Game game);

         Task Remove(Guid id);

    }
}