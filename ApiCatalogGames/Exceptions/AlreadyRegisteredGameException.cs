using System;

namespace ApiCatalogGames.Exceptions
{
    public class AlreadyRegisteredGameException : Exception
    {
        public AlreadyRegisteredGameException()
            : base("This game has already been registered!")
        { }
    }
}