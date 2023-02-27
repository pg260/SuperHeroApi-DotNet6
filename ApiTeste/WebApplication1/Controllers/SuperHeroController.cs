using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]

public class SuperHeroController : Controller
{
    private readonly DataContext context;

    public SuperHeroController(DataContext context)
    {
        this.context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> Get()
    {
        return Ok(await context.SuperHeroes.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SuperHero>> Get(int id)
    {
        var dbHero = await context.SuperHeroes.FindAsync(id);
        if (dbHero == null)
            return BadRequest("Hero not found");
        return Ok(dbHero);
    }
    
    [HttpPost]
    public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
    {
        context.SuperHeroes.Add(hero);
        await context.SaveChangesAsync();
        
        return Ok(await context.SuperHeroes.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
    {
        var dbHero = await context.SuperHeroes.FindAsync(request.Id);
        if (dbHero == null)
            return BadRequest("Hero not found");
        dbHero.Name = request.Name;
        dbHero.FirstName = request.FirstName;
        dbHero.LastName = request.LastName;
        dbHero.Place = request.Place;

        await context.SaveChangesAsync();
        
        return Ok(await context.SuperHeroes.ToListAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<SuperHero>>> Delete(int id)
    {
        var dbHero = await context.SuperHeroes.FindAsync(id);
        if (dbHero == null)
            return BadRequest("Hero not found");

        context.SuperHeroes.Remove(dbHero);
        await context.SaveChangesAsync();
        
        return Ok(await context.SuperHeroes.ToListAsync());
    }
}