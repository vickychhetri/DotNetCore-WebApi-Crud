using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApi.Data;
using webApi.Models;

namespace webApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ContactsController : Controller
    {
        //DBCOntext
        private readonly ContactsAPIDbContext _dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContact()
        {
            return Ok(await _dbContext.Contacts.ToListAsync());
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            Contact contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = addContactRequest.Name,
                Phone = addContactRequest.Phone,
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
            };

           await _dbContext.Contacts.AddAsync(contact);
           await _dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest addContactRequest)
        {
            var contact=await _dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.Name = addContactRequest.Name;
                contact.Phone = addContactRequest.Phone;
                contact.Address = addContactRequest.Address;
                contact.Email = addContactRequest.Email;
                await _dbContext.SaveChangesAsync();
                return Ok(contact);
            }


            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                _dbContext.Contacts.Remove(contact);
                 await _dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }


        }
}
