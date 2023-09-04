using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobPortalController : ControllerBase
    {
        private readonly HeavensPlaceContext _heavensPlaceContext;

        public JobPortalController(HeavensPlaceContext dbContext)
        {
            _heavensPlaceContext = dbContext;
        }
        

        [HttpPost]
        public void Post([FromBody] FromBodyData data)
        {
            //HeavensPlaceContext heavensPlaceContext = new HeavensPlaceContext();
            Member member = new Member { Firstname = data.FirstName, Lastname = data.LastName};
            _heavensPlaceContext.Members.Add(member);
            _heavensPlaceContext.SaveChanges();
            //return;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Member>> Get()
        {
            //var items = new List<string> { "Item 1", "Item 2", "Item 3" };
            //return Ok(items);
            var memberList  = _heavensPlaceContext.Members.ToList();
            return memberList;
        }

        [HttpDelete]
        public void Delete(int id)
        {
           var member = _heavensPlaceContext.Members.Find(id);
            _heavensPlaceContext.Remove(member!);
            _heavensPlaceContext.SaveChanges(); 
        }

        [HttpPut]
        public void Update([FromBody] FromBodyMember data)
        {
            Member member = _heavensPlaceContext.Members.Find(data.Id);
            if (member == null)
                return;
            member.Firstname = data.Firstname;
            member.Lastname = data.Lastname;
            _heavensPlaceContext.Update(member);
            _heavensPlaceContext.SaveChanges();
        }

    }

    
}


public class FromBodyMember
{
    public int? Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }  
}

public class FromBodyData
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    //public Address? Address { get; set; }
}

public class Address
{
   public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
}


