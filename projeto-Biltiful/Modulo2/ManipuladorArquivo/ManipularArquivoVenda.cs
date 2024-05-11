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

        public void SalvarArquivo(List<Venda> l)
        {
            StreamWriter sw = new(CaminhoDiretorio + CaminhoArquivo);

            foreach (var item in l)
            {

                sw.WriteLine(item.FormatarParaArquivo());
            }

            sw.Close();
        }


        public List<Venda> CarregarArquivo()
        {
            List<Venda> l = new();

            foreach (string linha in File.ReadAllLines(CaminhoDiretorio + CaminhoArquivo))
            {
                /*  id                  // 5  (0 - 4)
                    dataVenda          // 8  (5 - 12)
                    cliente            // 11 (13-23)
                    valorTotal         // 7  (24-30)*/

                int id = int.Parse(linha.Substring(0, 5).Trim());
                int dia = int.Parse(linha.Substring(5, 2).Trim()); 
                int mes = int.Parse(linha.Substring(7, 2).Trim()); 
                int ano = int.Parse(linha.Substring(9, 4).Trim()); 
                string cliente = linha.Substring(13, 11).Trim();   
                int valorTotal = int.Parse(linha.Substring(24, 7).Trim());



                l.Add(new(id, new DateOnly(ano, mes, dia), cliente, valorTotal));
            }

            return l;
        }

    }
}
