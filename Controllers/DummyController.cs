using Microsoft.AspNetCore.Mvc;

namespace DummyRestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DummyController : ControllerBase
{
    private static readonly List<DummyModel> _dummyData = new List<DummyModel>
    {
        new DummyModel { Id = 1, Name = "Ansh" },
        new DummyModel { Id = 2, Name = "Shin" }
    };

    // GET: api/dummy
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_dummyData);
    }

    // GET: api/dummy/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var item = _dummyData.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    // POST: api/dummy
    [HttpPost]
    public IActionResult Create([FromBody] DummyModel model)
    {
        if (model == null || string.IsNullOrEmpty(model.Name))
        {
            return BadRequest("Invalid data");
        }

        model.Id = _dummyData.Count + 1;
        _dummyData.Add(model);
        return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
    }

    // PUT: api/dummy/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] DummyModel updatedModel)
    {
        var existingItem = _dummyData.FirstOrDefault(x => x.Id == id);
        if (existingItem == null)
        {
            return NotFound();
        }

        existingItem.Name = updatedModel.Name;
        return NoContent();
    }

    // DELETE: api/dummy/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _dummyData.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return NotFound();
        }

        _dummyData.Remove(item);
        return NoContent();
    }
}

// Dummy Model
public class DummyModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
