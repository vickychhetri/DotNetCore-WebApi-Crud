using Microsoft.EntityFrameworkCore;
using webApi.Models;

namespace webApi.Data
{
    public class ContactsAPIDbContext :DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }

    }
}
