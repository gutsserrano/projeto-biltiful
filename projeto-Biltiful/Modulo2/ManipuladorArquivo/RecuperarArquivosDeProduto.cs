using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo2.ManipuladorArquivo
{
    internal class RecuperarArquivosDeProduto
    {
        protected string CaminhoDiretorio { get; set; }
        protected string CaminhoArquivo { get; set; }

        public RecuperarArquivosDeProduto(string c, string a)
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

        public List<string> RecuperarCosmetico()
        {
            List<string> listaProdutos = new();

            foreach (string linha in File.ReadAllLines(CaminhoDiretorio + CaminhoArquivo))
            {

                string produto = linha.Substring(0, 13).Trim();

                listaProdutos.Add(produto);
            }

            return listaProdutos;
        }

        internal float RecuperarValor(string produto)
        {
            float preco = 0;

            foreach (string linha in File.ReadAllLines(CaminhoDiretorio + CaminhoArquivo))
            {
                if (produto.Equals(linha.Substring(0, 13)))

                    preco = float.Parse(linha.Substring(33, 5));

            }

            return preco;
        }

        internal string recuperarEstaAtivo(string id)
        {
            var estado = "I";

            foreach (string linha in File.ReadAllLines(CaminhoDiretorio + CaminhoArquivo))
            {
                if (id.Equals(linha.Substring(0, 13)))

                    estado = linha.Substring(56, 1);

            }

            return estado;
        }
    }
}
