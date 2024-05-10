using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo1.Entidades
{
    internal class Produto
    {
        string CodigoBarras { get; set; }    // 13 (00-12)
        string Nome { get; set; }            // 20 (13-32)
        float ValorVenda { get; set; }       // 5  (33-37)
        DateOnly UltimaVenda { get; set; }   // 8  (38-45)
        DateOnly DataCadastro { get; set; }  // 8  (46-53)
        char Situacao { get; set; }          // 1  (54-54)

        public Produto(string codigoBarras, string nome, float valorVenda)
        {
            CodigoBarras = codigoBarras;
            Nome = FormatarNome(nome);
            ValorVenda = valorVenda;
            UltimaVenda = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        public Produto (string data)
        {
            CodigoBarras = data.Substring(0, 13);
            Nome = data.Substring(13, 20);

            string valor = (data.Substring(33, 5));
            valor = valor.Insert(3, ",");
            ValorVenda = float.Parse(valor);

            UltimaVenda = ConverterParaData(data.Substring(38, 8));
            DataCadastro = ConverterParaData(data.Substring(46, 8));
            Situacao = char.Parse(data.Substring(54, 1));
        }

        public string FormatarParaArquivo()
        {
            string valor = $"{ValorVenda:000.00}";
            valor = valor.Replace(",", "");

            return  $"{CodigoBarras}" +
                    $"{Nome}" +
                    $"{valor}" +
                    $"{ConverterDataParaArquivo(UltimaVenda)}" +
                    $"{ConverterDataParaArquivo(DataCadastro)}" +
                    $"{Situacao}";
        }

        private DateOnly ConverterParaData(string data)
        {
            string dia = data.Substring(0, 2);
            string mes = data.Substring(2, 2);
            string ano = data.Substring(4, 4);

            return DateOnly.Parse($"{dia}/{mes}/{ano}");
        }
        private string ConverterDataParaArquivo(DateOnly data)
        {
            return $"{data.Day:00}{data.Month:00}{data.Year:0000}";
        }

        private string FormatarNome(string nome)
        {
            return nome.PadRight(20).Substring(0, 20);
        }

        public static bool VerificarCodigoBarras(string cod)
        {
            int inicio;
            int.TryParse(cod.Substring(0, 3), out inicio);
            if (inicio != 789 || cod.Length != 13)
            {
                return false;
            }

            return true;
        }
    }
}
