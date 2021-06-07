using System.Collections.Generic;
using CRUD.Series.Interfaces;

namespace CRUD.Series.Classes
{
    public class SerieRepository : IRepository<Serie>
    {
        private List<Serie> listaSerie = new List<Serie>();
        
        public void Atualiza(int id,Serie entidade)
        {
            listaSerie[id] = entidade;
        }
        
        public void Exclui(int id)
        {
            listaSerie[id].Excluir();
        }

        public void Insere(Serie entidade)
        {
            listaSerie.Add(entidade);
        }

        public List<Serie> Lista()
        {
            return listaSerie;
        }

        public int ProximoId()
        {
            return listaSerie.Count;
        }

        public Serie RetornarPorId(int id)
        {
            return listaSerie[id];
        }

        

    }
}