using System;
using Microsoft.EntityFrameworkCore;
using web_api.Models;

namespace web_api.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<SinhVien> SinhViens { get; set; }
    }
}
