using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogGames.Controllers.v1
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<object>>> Obter()
        {   
            return Ok();
        }

        [HttpGet("{:idGame:guid}")]
        public async Task<ActionResult<List<object>>> Obter(Guid idGame)
        {   
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<object>> InsertGame(object game)
        {
            return Ok();
        }

        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame(Guid idGame,object game)
        {
            return Ok();
        }

        [HttpPatch("{idGame:guid}/price/[price:double]")]
        public async Task<ActionResult> UpdateGame(Guid idGame,double price)
        {
            return Ok();
        }

        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> DeleteGame(Guid idGame)
        {
            return Ok();
        }


    }
}