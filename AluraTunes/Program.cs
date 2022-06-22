// See https://aka.ms/new-console-template for more information
using AluraTunes;
using System.Xml.Linq;

Console.WriteLine("Hello, World!");

//TestandoComObjetos();
TestandoComXML();

void TestandoComXML()
{
	XElement root = XElement.Load(@"DataXML\AluraTunes.xml");
	var queryXML = from g in root.Element("Generos").Elements("Genero")
				   select g;
	foreach (var genero in queryXML)
	{
		Console.WriteLine("{0}\t{1}",genero.Element("GeneroId").Value, genero.Element("Nome").Value);
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

void TestandoComObjetos()
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


