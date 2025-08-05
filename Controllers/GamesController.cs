using GameLibraryAPI.Data;
using GameLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameLibraryAPI.Services;
using GameLibraryAPI.Dtos;
using System.Threading.Tasks;
using System.Collections.Generic;  
using System.Linq;
using System;

namespace GameLibraryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost]
    public async Task<ActionResult<GameDto>> Create(GameCreateDto dto)
    {
        var result = await _gameService.CreateAsync(dto).ConfigureAwait(false);
        return CreatedAtAction(nameof(GetType), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, GameUpdateDto dto)
    {
        var success = await _gameService.UpdateAsync(id, dto).ConfigureAwait(false);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _gameService.SoftDeleteAsync(id).ConfigureAwait(false);
        return success ? NoContent() : NotFound();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameDto>> Get(int id)
    {
        var game = await _gameService.GetByIdAsync(id).ConfigureAwait(false);
        if (game == null)
            return NotFound();
        return Ok(game);
    }
}