//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Talabat.Core.Entities;
//using Talabat.Core.Repositiry.Contract;
//using Talabat.Core.Specification;

//namespace Route.Talabat.Api.Controllers
//{
//	[Route("api/[controller]")]
//	[ApiController]
//	public class EmployeeController : ControllerBase
//	{
//		private readonly IGenericRepository<Employee> _EmpRepository;

//		public EmployeeController(IGenericRepository<Employee> genericRepository)
//        {
//			_EmpRepository = genericRepository;
//		}

//		public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
//		{
//			var spec = new EmployeeSpecification();
//			var employees = await _EmpRepository.GetAllWithSpecAsync(spec);
//			return Ok(employees);
//		}
//		public async Task<ActionResult<Employee>> GetById(int id)
//		{
//			var spec = new EmployeeSpecification(id);
//			var employee = await _EmpRepository.GetByIdAsync(id);

//			return Ok(employee);
//		}

//    }
//}
