using Microsoft.AspNetCore.Mvc;
using Northwind.EntityModels.Mysql;
using Northwind.WebApi.Repositories;

namespace Northwind.WebApi.Controllers;

// base address : api/customers
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepositories _repo;

    public CustomersController(ICustomerRepositories repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// 如果country参数不为空，则根据country参数过滤客户数据并返回
    /// GET api/customers
    /// GET api/customers/?country=[country]
    /// </summary>
    /// <param name="country"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
    public async Task<IEnumerable<Customer>> GetCustomers(string? country)
    {
        if (string.IsNullOrEmpty(country))
        {
            return await _repo.RetrieveAllAsync();
        }
        else
        {
            return (await _repo.RetrieveAllAsync())
                .Where(c => c.Country == country);
        }
    }
    
    /// <summary>
    /// 根据id查找用户数据
    /// IActionResult可用于灵活处理多样化响应
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = nameof(GetCustomer))]
    [ProducesResponseType(200, Type = typeof(Customer))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCustomer(string? id)
    {
        Customer? c = await _repo.RetrieveAsync(id);
        if (c == null) return NotFound();
        return Ok(c);
    }

    /// <summary>
    /// 插入响应插入新客户实体的方法
    /// [FromBody]：模型绑定特性，指定如何将HTTP请求的数据（Body中的内容）绑定到控制器方法的参数上
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Customer))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Customer c)
    {
        if (c is null)
        {
            return BadRequest();        // 400 Bad request
        }                               // 由于该类中使用[ApiController]注解，会自动处理空请求体

        Customer? addedCustomer = await _repo.CreateAsync(c);
        if (addedCustomer == null)
        {
            return BadRequest("Repository failed to create customer.");
        }
        else
        {
            return CreatedAtRoute(
                routeName: nameof(GetCustomer),
                routeValues: new {id = addedCustomer.CustomerId!.ToLower()},
                value: addedCustomer);
        }
    }

    /// <summary>
    /// 更新客户数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(string id, [FromBody] Customer c)
    {
        id = id.ToUpper();
        c.CustomerId = c.CustomerId!.ToUpper();
        if (c is null || c.CustomerId != id)
        {
            return BadRequest();
        }
        Customer? existing = await _repo.RetrieveAsync(id);
        if (existing is null)
        {
            return NotFound();
        }
        await _repo.UpdateAsync(c);
        return new NoContentResult();
    }

    /// <summary>
    /// 根据id删除客户数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(string id)
    {
        if (id == "bed")
        {
            ProblemDetails problemDetails = new()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://localhost:5151/customers/failed-to-delete",
                Title = $"Customer {id} was found but failed deleted.",
                Detail = "More details like Company Name,Country and so on.",
                Instance = HttpContext.Request.Path
            };
            return BadRequest(problemDetails);
        }
        
        Customer? existing = await _repo.RetrieveAsync(id);
        if (existing is null)
        {
            return NotFound();
        }
        bool? deleted = await _repo.DeleteAsync(id);
        if (deleted.HasValue && deleted.Value)
        {
            // 成功删除则返回一个没任何内容的结果
            return new NoContentResult();
        }
        else
        {
            // 找到了数据但是删除失败
            return BadRequest($"Customer {id} was found but failed deleted.");
        }
    }
}