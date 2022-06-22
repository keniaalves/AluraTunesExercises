using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDB
{
	public class ProdutoDAO: IDisposable, IProdutoDAO
	{
		public ProdutoDAO()
		{

		}
		public void Adicionar(Produto p)
		{

		}

		public IList<Produto> Listar()
		{
			throw new NotImplementedException();
		}

		public void Atualizar(Produto p)
		{

		}

		public void Remover(Produto p)
		{

		}

		public Produto Buscar(Produto p)
		{
			return p;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
