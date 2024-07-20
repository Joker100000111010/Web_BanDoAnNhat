using Do_An_CNTT_K3.Data;
using Do_An_CNTT_K3.Models.Interfaces;
using MimeKit;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Do_An_CNTT_K3.Models.Service
{
    public class LienLacRepository : ILienLacRepository
    {
        private FoodDbContext dbContext;
        private readonly IConfiguration _configuration;

        public LienLacRepository(FoodDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            _configuration = configuration;
        }

        public void AddBinhLuan(Contact Comment)
        {
            dbContext.contacts.Add(Comment);
            dbContext.SaveChanges();
        }

        public IEnumerable<Contact> GetAllComment()
        {
            return dbContext.contacts.ToList();
        }
    }
}
