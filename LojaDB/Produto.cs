using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDB
{
	public class Produto
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Categoria { get; set; }
		public double Preco { get; set; }
	}
}
