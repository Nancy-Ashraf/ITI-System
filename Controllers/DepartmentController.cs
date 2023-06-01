using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompanySystem.DAL;
using WebApiApp.Filters;
using WebApiApp.APIs;
using CompanySystem.BL;
namespace WebApiApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    #region simple crud
    #region Department List
    //private static List<Department> _departments = new List<Department>
    //{
    //    new (1,"PD","Egypt"),
    //    new (2,"SA", "Egypt"),
    //    new (3,"MA","UK"),
    //    new (4,"AI","USA"),
    //  };
    #endregion
    #region GetAll
    //[HttpGet]
    //public ActionResult<List<Department>> GetAll()
    //{
    //    if (!_departments.Any())
    //    {
    //        return NotFound(); //404
    //    }
    //    return _departments; //200
    //}
    #endregion
    #region GetById
    //[HttpGet]
    //[Route("{id}")]
    //public ActionResult<DepartmentReadDto> GetById(int id)
    //{
    //    Department? department = _departments.FirstOrDefault(x => x.Id == id);
    //    if (department == null)
    //    {
    //        return NotFound(); //404
    //    }
    //    return Ok(new DepartmentReadDto
    //    {
    //        Id= department.Id,
    //        Name=department.Name,
    //        //Location=department.Location,
    //    }); //200
    //}
    #endregion
    #region AddV1
    //[HttpPost]
    //[Route("v1")]
    //public ActionResult AddV1(Department department)
    //{
    //    department.Id = _departments.Count() + 1;
    //    department.Location = "Egypt";
    //    _departments.Add(department);
    //    return CreatedAtAction( //201
    //        nameof(GetById),
    //        new { id = department.Id },
    //        new GeneralResponse("Department has been added sucessfully")
    //        );
    //} 
    #endregion
    #region AddV2
    //[HttpPost]
    //[Route("v2")]
    //[LocVld]
    //public ActionResult AddV2(Department department)
    //{
    //    department.Id = _departments.Count() + 1;
    //    _departments.Add(department);
    //    return CreatedAtAction( //201
    //        nameof(GetById),
    //        new { id = department.Id },
    //        new GeneralResponse("Department has been added sucessfully")
    //        );
    //}
    #endregion
    #region Update
    //[HttpPut]
    //[Route("{idFromUrl}")]
    //public ActionResult Update(Department departmentFromReqBody, int idFromUrl)
    //{
    //    if (departmentFromReqBody.Id != idFromUrl)
    //    {
    //        return BadRequest(); //400
    //    }
    //    Department? departmentToEdit = _departments.FirstOrDefault(_x => _x.Id == idFromUrl);
    //    if (departmentToEdit == null)
    //    {
    //        return NotFound(); //404
    //    }
    //    departmentToEdit.Name = departmentFromReqBody.Name;
    //    return NoContent(); //204
    //}
    #endregion
    #region Delete
    //#region Delete
    //[HttpDelete]
    //[Route("{id}")]
    //public ActionResult Delete(int id)
    //{
    //    Department? department = _departments.FirstOrDefault(x => x.Id == id);
    //    if (department == null)
    //    {
    //        return NotFound();
    //    }
    //    _departments.Remove(department);
    //    return Ok();
    //} 
    #endregion
    #endregion
    private DepartmentManager departmentManager { get; set; }
    public DepartmentController(DepartmentManager _departmentManager)
    {
        departmentManager = _departmentManager;
    }
    [HttpGet]
    [Route("/Details/{id}")]
    public ActionResult<DepartmentDetailsDto> GetDepartmentDetails(int id)
    {
        DepartmentDetailsDto? departmentDetailsDto = departmentManager.GetDepartmentDetails(id);
        if (departmentDetailsDto == null )
        {
            return NotFound();
        }
        return departmentDetailsDto;
    }

} 


