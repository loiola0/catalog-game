using System;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogGames.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100,MinimumLength = 3, ErrorMessage = "The game name must contain between 3 and 100 characters!")]
        public string Name {get;set;}

        [Required]
        [StringLength(100,MinimumLength = 3, ErrorMessage = "The game producer must contain between 3 and 100 characters!")]
        public string  Producer {get;set;}

        [Required]
        [Range(1,1000, ErrorMessage = "The price must be at least BRL 1.0 and maximum BRL 1000.0 !")]
        public double Price  {get;set;} 
    }
}