using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsgphPlanning
{
    public class UsgphPlanningDbContext : DbContext
    {
        public UsgphPlanningDbContext(DbContextOptions<UsgphPlanningDbContext> options) : base(options) { }

        public DbSet<Lieu> Lieux { get; set; }
        public DbSet<User> Users { get; set; }
    }

    [Table("lieux")]
    public class Lieu
    {
        [Key]
        [Column("id")]
        public int? Id { get; set; }

        [Column("nom")]
        public string? Nom { get; set; }

        [Column("user_id_creation")]
        public int? UserIdCreation { get; set; }
    }

    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int? Id { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("password")]
        public string? Password { get; set; }
    }
}
