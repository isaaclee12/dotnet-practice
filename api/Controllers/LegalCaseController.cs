// Handles HTTP requests & routes
using Microsoft.AspNetCore.Mvc;
using LiteDB;
using System.Collections.Generic;

[ApiController]
[Route("cases")]

public class LegalCasesController: ControllerBase
{
    private readonly CaseRepository _repo;

    public LegalCasesController(CaseRepository repo)
    {
        _repo = repo;
    }

    // GET all
    [HttpGet] 
    public IEnumerable<LegalCase> Get() => _repo.GetAll();

    // GET by id
    [HttpGet("{id}")]
    public ActionResult<LegalCase> Get(int id)
    {
        var c = _repo.GetById(id);
        if (c == null) return NotFound();
        return c;
    } 

    // GET filter by client id
    [HttpGet("{clientId}")]
    public ActionResult<List<LegalCase>> GetByClientId(int clientId)
    {
        var c = _repo.GetAll().Where(c => c.ClientId == clientId).ToList();
        if (c == null) return NotFound();
        return c;
    }

    // POST create
    [HttpPost]
    public ActionResult Post([FromBody] LegalCase c)
    {
        _repo.Add(c);
        return CreatedAtAction(nameof(Get), new { id = c.Id }, c);
    }

    // PUT update
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] LegalCase updated)
    {
        var existing = _repo.GetById(id);
        if (existing == null) return NotFound();

        updated.Id = id;
        _repo.Update(updated);
        // NOTE: What's this return? Why not return id/success?
        return NoContent();
    }

    // Delete
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _repo.Delete(id);
        return NoContent();
    }
}
