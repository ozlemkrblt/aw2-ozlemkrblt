using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vk.Data.Context;
using Vk.Data.Domain;

namespace Vk.Api.Controllers;

[Route("vk/api/v1/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly VkDbContext dbContext;
    public CustomersController(VkDbContext dbContext)
    {
        this.dbContext = dbContext; 
    }


    [HttpGet]
    public void Get()
    {
        var list = dbContext.Set<Customer>().ToList();

        var list2 = dbContext.Set<Customer>().Include(x=> x.Addresses).ToList();

        var list3 = dbContext.Set<Customer>().Include(x=> x.Accounts).ToList();

        var list4 = dbContext.Set<Customer>().Include(x=> x.Accounts).Include(x=> x.Addresses).ToList();

        var list5 = dbContext.Set<Customer>()
            .Include(x => x.Accounts).ThenInclude(x=> x.AccountTransactions)
            .Include(x => x.Accounts).ThenInclude(x => x.EftTransactions)
            .Include(x => x.Accounts).ThenInclude(x => x.Card)
            .Include(x => x.Addresses).ToList();


    }


}
