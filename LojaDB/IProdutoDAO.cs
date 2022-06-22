using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDB
{
	internal interface IProdutoDAO
	{
		void Adicionar(Produto produto);
		void Atualizar(Produto produto);
		void Remover(Produto produto);
		Produto Buscar(Produto produto);
		IList<Produto> Listar();

	}
}
