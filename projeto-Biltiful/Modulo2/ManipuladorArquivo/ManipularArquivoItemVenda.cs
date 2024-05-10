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
            foreach (ItemVenda iVenda in recievedList)
            {
                Console.WriteLine(iVenda.ToString());
            }
        }

        void SalvarArquivo(List<ItemVenda> l)
        {
            StreamWriter sw = new(CaminhoDiretorio);

            foreach (var item in l)
            {

                sw.WriteLine(item.ToString());
            }

            sw.Close();
        }


        List<ItemVenda> CarregarArquivo()
        {
            List<ItemVenda> l = new();

            /* idVenda                 //5     (0-4)    
             produto                  //13    (5-17)
             quantidade               //3     (18-20)
             valorUnitario             //5     (21-25)
             totalItem                //6     (26-31)*/

            foreach (var linha in File.ReadAllLines(CaminhoDiretorio))
            {
                int idVenda = int.Parse(linha.Substring(0, 5).Trim());
                string produto = linha.Substring(5, 13).Trim();
                int quantidade = int.Parse(linha.Substring(18, 3).Trim());
                int valorUnitario = int.Parse(linha.Substring(21, 5).Trim());
                int totalItem = int.Parse(linha.Substring(26, 6).Trim());
                l.Add(new(idVenda, produto, quantidade, valorUnitario, totalItem));
            }

            return l;
        }
    }
}
