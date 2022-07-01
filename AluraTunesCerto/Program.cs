using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using AluraTunesCerto.DataSQL;

namespace AluraTunesCerto
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//TestandoComObjetos();
			//TestandoComXML();
			//TestandoComBancoDeDados();
			//var textoBusca = "Led";
			//TestandoWhereEJoin(textoBusca);
			//TestandoComWhereSemJoin();
			//RefinandoConsultas("The", "");
			//UsandoCount();
			//UsandoSum();
			//UsandoGroupBy();
			//UsandoMinMaxAvg();
			UsandoMedianaComMetodoDeExtensao();
			Console.ReadKey();
		}

		private static void UsandoMedianaComMetodoDeExtensao()
		{
			using (var contexto = new AluraTunesEntities())
			{
				var mediana = contexto.NotaFiscals.Mediana(nf => nf.Total);
				Console.WriteLine(mediana);
			}
		}

		private static void UsandoMinMaxAvg()
		{
			using (var contexto = new AluraTunesEntities())
			{
				// várias consultas
				contexto.Database.Log = Console.WriteLine;
				var maiorVenda = contexto.NotaFiscals.Max(NotaFiscal => NotaFiscal.Total);
				var menorVenda = contexto.NotaFiscals.Min(NotaFiscal => NotaFiscal.Total);
				var vendaMedia = contexto.NotaFiscals.Average(NotaFiscal => NotaFiscal.Total);

				Console.WriteLine($"Maior venda: {maiorVenda}, Menor venda: {menorVenda}, Venda media: {vendaMedia}");

				//consulta unica

				var vendas = (from nf in contexto.NotaFiscals
							 group nf by 1 into agrupado
							 select new
							 {
								 max = agrupado.Max(nf => nf.Total),
								 min = agrupado.Min(nf => nf.Total),
								 avg = agrupado.Average(nf => nf.Total),
							 }).Single();
			Console.WriteLine($"Maior venda: {vendas.max}, Menor venda: {vendas.min}, Venda media: {vendas.avg}");
			}

		}

		private static void UsandoGroupBy()
		{
			using (var contexto = new AluraTunesEntities())
			{
				//sintaxe de consulta
				var query = from i in contexto.ItemNotaFiscals
							where i.Faixa.Album.Artista.Nome == "Led Zeppelin"
							group i by i.Faixa.Album into grouped
							let totalPorAlbum = grouped.Sum(a => a.Quantidade) //essa variavel poderia ser usada novamente dentro da consulta em um select, por exemplo
							orderby totalPorAlbum descending
							select grouped;

				foreach (var agrupado in query)
				{
					Console.WriteLine($"Album: {agrupado.Key.Titulo} - Quantidade: {agrupado.Sum(a => a.Quantidade)} - Valor total: {agrupado.Sum(a => a.Quantidade * a.PrecoUnitario)}");
				}

				//sintaxe de método

				var soma2 = contexto.ItemNotaFiscals.Where(i => i.Faixa.Album.Artista.Nome == "Led Zeppelin")
									.Sum(i => i.PrecoUnitario * i.Quantidade);
				Console.WriteLine("------");

				Console.WriteLine("somando tudo usando sintaxe de método " + soma2);

			}
		}

		private static void UsandoSum()
		{
			using (var contexto = new AluraTunesEntities())
			{
				//sintaxe de consulta
				var query = from i in contexto.ItemNotaFiscals
							where i.Faixa.Album.Artista.Nome == "Led Zeppelin"
							select i;
				var soma = query.Sum(i => i.PrecoUnitario * i.Quantidade);

				//sintaxe de método

				var soma2 = contexto.ItemNotaFiscals.Where(i => i.Faixa.Album.Artista.Nome == "Led Zeppelin")
									.Sum(i => i.PrecoUnitario * i.Quantidade);

				Console.WriteLine("somando usando sintaxe de consulta " + soma);
				Console.WriteLine("somando usando sintaxe de método " + soma2);

			}
		}

		private static void UsandoCount()
		{
			using (var contexto = new AluraTunesEntities())
			{
				//sintaxe de consulta
				var query = from f in contexto.Faixas
							where f.Album.Artista.Nome == "Led Zeppelin"
							select f;
				var quantidade = query.Count();

				//sintaxe de método

				var quantidade2 = contexto.Faixas.Count(f => f.Album.Artista.Nome == "Led Zeppelin");

				Console.WriteLine("Contando usando sintaxe de consulta " + quantidade);
				Console.WriteLine("Contando usando sintaxe de método " + quantidade2);

			}
		}

		private static void RefinandoConsultas(string v, string d)
		{
			using (var c = new AluraTunesEntities())
			{
				//var query = from a in c.Faixas
				//			where a.Nome.Contains(v)
				//			select a;
				//if (!string.IsNullOrEmpty(d))
				//{
				//	query = query.Where(q => q.Album.Artista.Nome.Contains(d));
				//}

				//query = query.OrderBy(q => q.Album.Titulo).ThenByDescending(q => q.Nome);

				//foreach (var q in query)
				//{
				//	Console.WriteLine($"{q.Album.Titulo.PadRight(30)} - {q.Nome.PadRight(30)} - {q.Album.Artista.Nome}");
				//}

				var query2 = from a in c.Faixas
							 where a.Nome.Contains(v)
							 && a.Album.Artista.Nome.Contains(d)
							 orderby a.Nome
							 select a;

				foreach (var q in query2)
				{
					Console.WriteLine($"{q.Album.Titulo.PadRight(30)} - {q.Nome.PadRight(30)} - {q.Album.Artista.Nome}");
				}
			}
		}

		private static void TestandoComWhereSemJoin()
		{
			using (var contexto = new AluraTunesEntities())
			{
				var query = from albuns in contexto.Albums
							where albuns.Artista.Nome.Contains("Metallica")
							select new { albuns.Artista.Nome, albuns.Titulo };
				foreach (var item in query)
				{
					Console.WriteLine(item);
				}
				Console.ReadKey();
			}
		}

		private static void TestandoComBancoDeDados()
		{
			ConsultaFaixaEGenero();
		}

		private static void ConsultaFaixaEGenero()
		{
			using (var contexto = new AluraTunesEntities())
			{
				var query = from g in contexto.Generos
							join f in contexto.Faixas on g.GeneroId equals f.GeneroId
							select new { NomeFaixa = f.Nome, NomeGenero = g.Nome };
				query = query.Take(10);
				contexto.Database.Log = Console.WriteLine;
				foreach (var genero in query)
				{
					Console.WriteLine("{0}\t{1}", genero.NomeFaixa, genero.NomeGenero);
				}
				Console.ReadKey();
			}
		}

		public static void TestandoComXML()
		{
			XElement root = XElement.Load(@"DataXML\AluraTunes.xml");
			var queryXML = from g in root.Element("Generos").Elements("Genero")
						   select g;
			foreach (var genero in queryXML)
			{
				Console.WriteLine("{0}\t{1}", genero.Element("GeneroId").Value, genero.Element("Nome").Value);
			}

			Console.WriteLine("-------------");

			var queryXMLJoin = from g in root.Element("Generos").Elements("Genero")
							   join m in root.Element("Musicas").Elements("Musica") on g.Element("GeneroId").Value equals m.Element("GeneroId").Value
							   select new { m, g };

			foreach (var g in queryXMLJoin)
			{
				Console.WriteLine("{0}\t{1}\t{2}", g.m.Element("MusicaId").Value, g.m.Element("Nome").Value, g.g.Element("Nome").Value);
			}
			Console.ReadLine();
		}

		public static void TestandoComObjetos()
		{
			var generos = new List<Genero>()
			{
				new Genero { Id = 1, Nome = "Rock Alternativo"},
				new Genero { Id = 2, Nome = "Axé" },
				new Genero { Id = 3, Nome = "Rock Clássico" },
				new Genero { Id = 4, Nome = "Eletrônicas" },
				new Genero { Id = 5, Nome = "Pop" },
			};

			var musicas = new List<Musica>()
			{
				new Musica { Id = 1, Nome = "Paradise", GeneroId = 4 },
				new Musica { Id = 2, Nome = "I Want to break free", GeneroId = 3 },
				new Musica { Id = 3, Nome = "Roar", GeneroId = 5 },
				new Musica { Id = 4, Nome = "Wish you were here", GeneroId = 3 },
			};

			var musicaQuery = from m in musicas
							  join g in generos on m.GeneroId equals g.Id
							  select new { m, g };
			foreach (var mg in musicaQuery)
			{
				Console.WriteLine("{0}\t{1}", mg.m.Nome, mg.g.Nome);//não traz musicas que não contem genero existente
			}

			Console.WriteLine("-------------------");

			var rockClassico = from m in musicas
							   join g in generos on m.GeneroId equals g.Id
							   where g.Id == 3
							   select new { m, g };
			foreach (var rock in rockClassico)
			{
				Console.WriteLine("{0}\t{1}", rock.m.Nome, rock.g.Nome);
			}

			//foreach (var genero in generos)
			//{
			//	if (genero.Nome.Contains("Rock"))
			//	{
			//		Console.WriteLine("{0}\t{1}", genero.Id, genero.Nome);
			//	}
			//}

			//var query = from g in generos
			//			where g.Nome.Contains("Rock")
			//			select g;//linq

			//foreach (var g in query)
			//{
			//	Console.WriteLine("{0}\t{1}", g.Id, g.Nome);
			//}

			Console.ReadKey();

		}

		public static void TestandoWhereEJoin(string textoBusca)
		{
			using (var contexto = new AluraTunesEntities())
			{
				var query = from a in contexto.Artistas
							join alb in contexto.Albums on a.ArtistaId equals alb.ArtistaId
							where a.Nome.Contains(textoBusca)
							select new { a.Nome, alb.Titulo };
				foreach (var item in query)
				{
					Console.WriteLine($"{item.Nome} - {item.Titulo}");
				}
				Console.WriteLine("Deseja fazer outra busca?");
				var opcao = Console.ReadLine();
				if (opcao == "s")
				{
					Console.WriteLine("Digite o nome");
					textoBusca = Console.ReadLine();
					
					TestandoWhereEJoin(textoBusca);
				}

			}
		}

	}
}
