// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;

namespace LojaDB
{
	public class LojaContext : DbContext
	{
		public DbSet<Produto> Produtos { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LojaDB;Trusted_Connection=true;");
		}
	}

}