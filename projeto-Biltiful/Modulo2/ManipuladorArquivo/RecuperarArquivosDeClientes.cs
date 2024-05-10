using projeto_Biltiful.Modulo2.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo2.ManipuladorArquivo
{
    internal class RecuperarArquivosDeClientes
    {
        protected string CaminhoDiretorio { get; set; }
        protected string CaminhoArquivo { get; set; }

        public RecuperarArquivosDeClientes(string c, string a)
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

        public List<string> RecuperarCpf()
        {
            List<string> cpf = new();

            foreach (string linha in File.ReadAllLines(CaminhoDiretorio))
            {
    
                //cliente    11 (13-23)

                string cliente = linha.Substring(13, 11).Trim();

                cpf.Add(cliente);
            }

            return cpf;
        }

        public List<Venda> CarregarArquivo()
        {
            List<Venda> l = new();

            foreach (string linha in File.ReadAllLines(CaminhoDiretorio))
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
