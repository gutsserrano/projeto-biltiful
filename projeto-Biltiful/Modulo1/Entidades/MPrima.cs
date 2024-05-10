using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo1.Entidades
{
    internal class MPrima
    {
        public string Id { get; set; }              // 6  (0-5)
        public string Nome { get; set; }            // 20 (6-25)
        public DateOnly UltimaCompra { get; set; }  // 8  (26-33)
        public DateOnly DataCadastro { get; set; }  // 8  (34-41)
        public char Situacao { get; set; }          // 1  (42-42)

        public MPrima(string id, string nome)
        {
            Id = id;
            Nome = FormatarNome(nome);
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        public MPrima(string data) 
        {
            Id = data.Substring(0, 6);
            Nome = data.Substring(6, 20);
            UltimaCompra = ConverterParaData(data.Substring(26, 8));
            DataCadastro = ConverterParaData(data.Substring(34, 8));
            Situacao = char.Parse(data.Substring(42, 1));
        }

        public string FormatarParaArquivo()
        {
            return  $"{Id}" +
                    $"{Nome}" +
                    $"{ConverterDataParaArquivo(UltimaCompra)}" +
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

        public static bool VerificarId(string id)
        {
            string mp = id.Substring(0, 2);
            if (mp != "MP" || int.TryParse(id.Substring(2, 4), out _) || id.Length != 6)
                return false;

            return true;
        }
    }
}
