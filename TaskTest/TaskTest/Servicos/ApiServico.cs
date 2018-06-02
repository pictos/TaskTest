using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskTest.Models;

namespace TaskTest.Servicos
{
    class ApiServico
    {
        public static Task<IEnumerable<Palestra>> ObterPalestras()
        {
            return Task.Factory.StartNew<IEnumerable<Palestra>>(() =>
            {
                Thread.Sleep(3000);
                var palestras = new[]
                {
                new Palestra { TituloPalestra = "Xamarin Avançado"                 },
                new Palestra { TituloPalestra = "Xamarin Iniciante"                },
                new Palestra { TituloPalestra = "Usando o EF corretamente"         },
                new Palestra { TituloPalestra = "Definindo arquitetura de projeto" }
                };
                return palestras;
            });
        }

        public static Task<IEnumerable<Palestrante>> ObterPalestrantes()
        {
            return Task.Factory.StartNew<IEnumerable<Palestrante>>(() =>
            {
                Thread.Sleep(5000);
                var palestrantes = new[]
                {
                new Palestrante { NomePalestrante ="Jefferson" },
                new Palestrante { NomePalestrante ="Pedro"     },
                new Palestrante { NomePalestrante ="Cleidson"  },
                new Palestrante { NomePalestrante ="Zé"        }
            };
                return palestrantes;
            });
        }
    }
}
