// See https://aka.ms/new-console-template for more information
using LojaDB;

Console.WriteLine("Hello, World!");
//GravarUsandoEntity();
RecuperarProdutos();
//ExcluirProdutos();
//AtualizarProdutos();
//RecuperarProdutos();

void AtualizarProdutos()
{
	using (var contexto = new LojaContext())
	{
		Produto produto = contexto.Produtos.First();
		Console.WriteLine(produto.Nome);
		produto.Nome = "teste";
		produto.Categoria = "teste";
		contexto.Produtos.Update(produto);
		contexto.SaveChanges();
	}
}

void ExcluirProdutos()
{
	using (var contexto = new LojaContext())
	{
		IList<Produto> produtos = contexto.Produtos.ToList();
		Console.WriteLine(produtos.Count);
		foreach (Produto produto in produtos)
		{
			contexto.Produtos.Remove(produto);
			contexto.SaveChanges();
		}
	}
}

void RecuperarProdutos()
{
	using (var contexto = new LojaContext())
	{
		IList<Produto> produtos = contexto.Produtos.ToList();
		foreach (Produto produto in produtos)
		{
			Console.WriteLine(produto.Nome);
		}
	}
}

void GravarUsandoEntity()
{
	Produto p = new Produto();
	p.Nome = "Outra Coisa";
	p.Categoria = "Utilidades";
	p.Preco = 29.90;
	using (var contexto = new LojaContext())
	{
		contexto.Produtos.Add(p);
		contexto.SaveChanges();
	}
}