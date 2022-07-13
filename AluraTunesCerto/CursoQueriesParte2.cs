using AluraTunesCerto.DataSQL;
using System;
using System.Collections.Generic;
using System.Linq;
namespace AluraTunesCerto
{
	internal class CursoQueriesParte2
	{
		internal static void ListarVendas()
		{
			using (var contexto = new AluraTunesEntities())
			{
				var query = from nf in contexto.NotaFiscals
							orderby nf.NotaFiscalId ascending

							select new
							{
								Numero = nf.NotaFiscalId,
								Data = nf.DataNotaFiscal,
								Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
								Total = nf.Total
							};
				const int ENTRIES = 100;
				var todasNFs = query.Count();
				
				for (int i = 0; ENTRIES * i <= todasNFs; i++)
				{ 
					var pulos = (i) * ENTRIES;
					var dadosPaginados = query.Skip(pulos).Take(ENTRIES);
					Console.WriteLine(" ");
					Console.WriteLine("------------------------");
					Console.WriteLine("Página" + 1 + i);
					Console.WriteLine(" ");
					foreach (var nf in dadosPaginados)
					{
						Console.WriteLine($"{nf.Numero}, {nf.Data}, {nf.Cliente}, {nf.Total}");
					}
				}
				Console.ReadKey();
			}
		}
	}
}
