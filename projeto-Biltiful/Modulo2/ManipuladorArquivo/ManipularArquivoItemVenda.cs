using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using projeto_Biltiful.Modulo2.Entity;

namespace projeto_Biltiful.Modulo2.ManipuladorArquivo
{
    internal class ManipularArquivoItemVenda
    {
        protected string CaminhoDiretorio { get; set; }
        protected string CaminhoArquivo { get; set; }

        public ManipularArquivoItemVenda(string c, string a)
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

        void MostrarTodos(List<ItemVenda> recievedList)
        {
            foreach (Venda Venda in recievedList)
            {
                Console.WriteLine(Venda.ToString());
            }
        }

        void SalvarArquivo(List<ItemVenda> l)
        {
            StreamWriter sw = new(p + f);

            foreach (var item in l)
            {

                sw.WriteLine(item.ToString());
            }

            sw.Close();
        }


        List<ItemVenda> CarregarArquivo()
        {
            List<ItemVenda> l = new();

            string[] data;

            foreach (var linha in File.ReadAllLines(p + f))
            {
                data = linha.Split(";");
                l.Add(new());
            }

            return l;
        }
    }
}
