using System;

namespace ApiCatalogGames.Exceptions
{
    public class UnregisteredGameException : Exception
    {

        public UnregisteredGameException() : base("This game has not been registered!")
        {}
        
    }
}