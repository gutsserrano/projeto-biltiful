using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using projeto_Biltiful.Modulo2.Entity;

namespace projeto_Biltiful.Modulo2.ManipuladorArquivo
{
    internal class ManipularArquivoVenda
    {
        protected string CaminhoDiretorio { get; set; }
        protected string CaminhoArquivo { get; set; }

        public ManipularArquivoVenda(string c, string a)
        {
            CaminhoDiretorio = c;
            CaminhoArquivo = a;
            if (!Directory.Exists(CaminhoDiretorio))
                Directory.CreateDirectory(CaminhoDiretorio);
            if (!File.Exists(Path.Combine(CaminhoDiretorio, CaminhoArquivo)))
            {
                var aux = File.Create(Path.Combine(CaminhoDiretorio, CaminhoArquivo));
                aux.Close();
            }

        }

        void MostrarTodos(List<Venda> recievedList)
        {
            foreach (Venda Venda in recievedList)
            {
                Console.WriteLine(Venda.ToString());
            }
        }

        void SalvarArquivo(List<Venda> l)
        {
            StreamWriter sw = new(CaminhoDiretorio);

            foreach (var item in l)
            {

                sw.WriteLine(item.ToString());
            }

            sw.Close();
        }


        List<Venda> CarregarArquivo()
        {
            List<Venda> l = new();

            string[] data;

            foreach (var linha in File.ReadAllLines(CaminhoDiretorio))
            {
                data = linha.Split(";");
                l.Add(new());
            }

            return l;
        }

    }
}
