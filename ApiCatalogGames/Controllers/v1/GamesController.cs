using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogGames.ViewModel;
using ApiCatalogGames.InputModel;
using ApiCatalogGames.Services;
using System.ComponentModel.DataAnnotations;
using ApiCatalogGames.Exceptions;


namespace ApiCatalogGames.Controllers.v1
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        private readonly IGameService _gameService;

        GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogos sem paginação
        /// </remarks>
        /// <param name="page">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantity">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {   
            var games = await _gameService.Get(page,quantity);

            if (games == null || games.Count() == 0){
                return NoContent();
            }
                

            return Ok(games);
        }

        /// <summary>
        /// Buscar um jogo pelo seu Id
        /// </summary>
        /// <param name="idGame">Id do jogo buscado</param>
        /// <response code="200">Retorna o jogo filtrado</response>
        /// <response code="204">Caso não haja jogo com este id</response>   
        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> Get([FromRoute] Guid idGame)
        {
            var game = await _gameService.Get(idGame);

            if (game == null){
                return NoContent();
            }
                

            return Ok(game);
        }

        /// <summary>
        /// Inserir um jogo no catálogo
        /// </summary>
        /// <param name="GameInputModel">Dados do jogo a ser inserido</param>
        /// <response code="200">Cao o jogo seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um jogo com mesmo nome para a mesma produtora</response>   
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.Insert(gameInputModel);

                return Ok(game);
            }
            catch (AlreadyRegisteredGameException)
            {
                return UnprocessableEntity("There is already a game with this name for this producer");
            }
        }

        

        /// <summary>
        /// Atualizar um jogo no catálogo
        /// </summary>
        /// /// <param name="idGame">Id do jogo a ser atualizado</param>
        /// <param name="gameInputModel">Novos dados para atualizar o jogo indicado</param>
        /// <response code="200">Cao o jogo seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(idGame, gameInputModel);

                return Ok();
            }
            catch (UnregisteredGameException)
            {
                return NotFound("There is no such game");
            }
        }

        /// <summary>
        /// Atualizar o preço de um jogo
        /// </summary>
        /// /// <param name="idGame">Id do jogo a ser atualizado</param>
        /// <param name="preco">Novo preço do jogo</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpPatch("{idGame:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(idGame, price);

                return Ok();
            }
            catch (UnregisteredGameException)
            {
                return NotFound("There is no surch game");
            }
        }   

        /// <summary>
        /// Excluir um jogo
        /// </summary>
        /// /// <param name="idGame">Id do jogo a ser excluído</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid idGame)
        {
            try
            {
                await _gameService.Remove(idGame);

                return Ok();
            }
            catch (UnregisteredGameException)
            {
                return NotFound("There is no surch game");
            }
        }


    }
}