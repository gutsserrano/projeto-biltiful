using projeto_Biltiful.Modulo1.Entidades;
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

        public float RecuperarValor(string produto)
        {
            float preco = 0;

            foreach (string linha in File.ReadAllLines(CaminhoDiretorio + CaminhoArquivo))
            {
                if (produto.Equals(linha.Substring(0, 13)))

                    preco = float.Parse(linha.Substring(33, 5));

            }

            return preco;
        }

        public string RecuperarEstaAtivo(string id)
        {
            var estado = "I";

            foreach (string linha in File.ReadAllLines(CaminhoDiretorio + CaminhoArquivo))
            {
                if (id.Equals(linha.Substring(0, 13)))

                    estado = linha.Substring(linha.Length-1, 1);

            }

            return estado;
        }

        public DateOnly ConverterParaData(string data)
        {
            string dia = data.Substring(0, 2);
            string mes = data.Substring(2, 2);
            string ano = data.Substring(4, 4);

            return DateOnly.Parse($"{dia}/{mes}/{ano}");
        }
        public DateOnly RecuperarEData(string? produto) //concertar
        {
            var dataCompra = new DateOnly();

            foreach (string linha in File.ReadAllLines(CaminhoDiretorio + CaminhoArquivo))
            {
                if (produto.Equals(linha.Substring(0, 13)))

                    dataCompra = ConverterParaData(linha.Substring(38, 8));

            }

            return dataCompra;
        }
        public string RecuperarPrecoeDataNascimento(string produto)
        {
            DateOnly nascimento = RecuperarEData(produto);
            float preco = RecuperarValor(produto);

            return " Valor: " + preco.ToString().Insert(preco.ToString().Length - 2, ",") + " Datada compra: " + nascimento;

        }
    }
}