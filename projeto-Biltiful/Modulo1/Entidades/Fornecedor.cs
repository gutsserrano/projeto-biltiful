using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo1.Entidades
{
    internal class Fornecedor
    {
        public string Cnpj {  get; set; }            //14 (0-13)
        public string RazaoSocial { get; set; }      //50 (14-63)
        public DateOnly DataAbertura { get; set; }   //8 (64-71)
        public DateOnly UltimaCompra { get; set; }   //8 (72-79)
        public DateOnly DataCadastro { get; set; }   //8 (80-87)
        public char Situacao { get; set; }           //1 (88-88)

        public Fornecedor(string cnpj, string razaoSocial, DateOnly dataAbertura)
        {
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            DataAbertura = dataAbertura;
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        public Fornecedor(string data)
        {
            Cnpj = data.Substring(0, 14);
            RazaoSocial = data.Substring(14, 50);
            DataAbertura = ConverterParaData(data.Substring(64, 8));
            UltimaCompra = ConverterParaData(data.Substring(72, 8));
            DataCadastro = ConverterParaData(data.Substring(80, 8));
            Situacao = char.Parse(data.Substring(88, 1));
        }

        public string FormatarParaArquivo()
        {
            return  $"{Cnpj}" +
                    $"{RazaoSocial}" +
                    $"{ConverterDataParaArquivo(DataAbertura)}" +
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

        public static bool VerificarCnpj(string cnpj)
        {
            if (cnpj.Contains(".") || cnpj.Contains("/") || cnpj.Contains("-"))
            {
                cnpj = cnpj.Replace(".", "");
                cnpj = cnpj.Replace("/", "");
                cnpj = cnpj.Replace("-", "");
            }

            // Verifica se o tamanho do cnpj é diferente de 14
            if (cnpj.Length != 14)
            {
                return false;
            }

            bool valido = false;
            for (int i = 0; i < cnpj.Length - 1 && !valido; i++)
            {
                int n1 = int.Parse(cnpj.Substring(i, 1));
                int n2 = int.Parse(cnpj.Substring(i + 1, 1));

                if (n1 != n2)
                {
                    valido = true;
                }
            }

            return valido && ValidacaoDigitoUm(cnpj) && ValidacaoDigitoDois(cnpj);
        }

        private static bool ValidacaoDigitoUm(string str)
        {
            int resultado = 0;
            int[] verificador = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            for (int i = 0; i < 12; i++)
            {
                int digito = int.Parse(str.Substring(i, 1));
                resultado += digito * verificador[i];
            }

            int resto = resultado % 11;
            int digitoUm = int.Parse(str.Substring(12, 1));

            if((resto == 0 || resto == 1) && digitoUm == 0)
            {
                return true;
            }
            else if((resto >= 2 && resto <= 10) && digitoUm == 11 - resto)
            {
                return true;
            }

            return false;
        }

        private static bool ValidacaoDigitoDois(string cnpj)
        {
            int resultado = 0;
            int[] verificador = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            for (int i = 0; i < 13; i++)
            {
                int digito = int.Parse(cnpj.Substring(i, 1));
                resultado += digito * verificador[i];
            }

            int resto = resultado % 11;
            int digitoDois = int.Parse(cnpj.Substring(13, 1));

            if ((resto == 0 || resto == 1) && digitoDois == 0)
            {
                return true;
            }
            else if ((resto >= 2 && resto <= 10) && digitoDois == 11 - resto)
            {
                return true;
            }

            return false;
        }
    }
}
