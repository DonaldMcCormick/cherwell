using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Geometric_Layouts.Models
{
    public class TriangleContext : DbContext
    {
        public TriangleContext(DbContextOptions<TriangleContext> options)
            : base(options)
        {
        }

        public DbSet<Triangle> Triangles { get; set; }
    }
}

