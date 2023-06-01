using CompanySystem.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApp.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstructorController : ControllerBase
{
    IInstructorManager instructorManager { get; set; }
    public InstructorController(IInstructorManager _instructorManager)
    {
        instructorManager = _instructorManager;
    }

    [HttpGet]
    public ActionResult<List<InstructorReadDto>> GetAll()
    {
        return instructorManager.GetAll().ToList();
    }

    [HttpGet]
    [Route("{id}")]
    
    public ActionResult<InstructorReadDto> GetById(int id)
    {
        InstructorReadDto? instructor = instructorManager.GetById(id);
        if(instructor == null)
        {
            return NotFound();
        }
        return instructor; //200
    }

    [HttpPost]
    public ActionResult Add(InstructorAddDto instructor)
    {
        int newId=instructorManager.Add(instructor);
        return CreatedAtAction(nameof(GetById),new {id = newId}, new GeneralResponse("instructor has been added sucessfully")); //201
    }
    [HttpPut]
    
    public ActionResult Update(InstructorUpdateDto instructor) 
    {
        bool IsFound=instructorManager.Update(instructor);
        if(!IsFound)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    
    public ActionResult Delete(int id)
    {
        bool IsFound = instructorManager.Delete(id);
        if (!IsFound)
        {
            return NotFound();
        }
        return NoContent();
    }
}
