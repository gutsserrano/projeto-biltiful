using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo1.Entidades
{
    internal class Cliente
    {
        public string Cpf { get; set; }                 //11 (0-10)
        public string Nome { get; set; }                //50 (11-60)
        public DateOnly DataNascimento { get; set; }    //8  (61-68)
        public char Sexo { get; set; }                  //1  (69-69)
        public DateOnly UltimaCompra { get; set; }      //8  (70-77)
        public DateOnly DataCadastro { get; set; }      //8  (78-85)
        public char Situacao { get; set; }              //1  (86-86)

        public Cliente(string cpf, string nome, DateOnly dataNascimento, char sexo)
        {
            Cpf = FormatarCpf(cpf);
            Nome = FormatarNome(nome);
            DataNascimento = dataNascimento;
            Sexo = sexo;
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }

        public Cliente(string data)
        {
            Cpf = data.Substring(0, 11);
            Nome = data.Substring(11, 50);

            DataNascimento = ConverterParaData(data.Substring(61, 8));

            Sexo = char.Parse(data.Substring(69, 1));

            UltimaCompra = ConverterParaData(data.Substring(70, 8));

            DataCadastro = ConverterParaData(data.Substring(78, 8));
            Situacao = char.Parse(data.Substring(86, 1));

        }

        public string FormatarParaArquivo()
        {
            return  $"{Cpf}" +
                    $"{FormatarNome(Nome)}" +
                    $"{ConverterDataParaArquivo(DataNascimento)}" +
                    $"{Sexo}" +
                    $"{ConverterDataParaArquivo(DataCadastro)}" +
                    $"{ConverterDataParaArquivo(UltimaCompra)}" +
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
            return nome.PadRight(50).Substring(0, 50);
        }

        public static string FormatarCpf(string cpf)
        {
            if (cpf.Contains(".") || cpf.Contains("-"))
            {
                cpf = cpf.Replace(".", "");
                cpf = cpf.Replace("-", "");
            }

            return cpf;
        }

        public static bool VerificarCpf(string cpf)
        {
            if (cpf.Contains(".") || cpf.Contains("-"))
            {
                cpf = cpf.Replace(".", "");
                cpf = cpf.Replace("-", "");
            }

            // Verifica se o tamanho do cpf é diferente de 11
            if (cpf.Length != 11)
            {
                return false;
            }

            bool valido = false;
            for (int i = 0; i < cpf.Length - 1 && !valido; i++)
            {
                int n1 = int.Parse(cpf.Substring(i, 1));
                int n2 = int.Parse(cpf.Substring(i + 1, 1));

                if (n1 != n2)
                {
                    valido = true;
                }
            }

            return valido && ValidacaoDigitoUm(cpf) && ValidacaoDigitoDois(cpf);
        }

        private static bool ValidacaoDigitoUm(string str)
        {
            int resultado = 0;
            for (int i = 0, multiplica = 10; i < 9; i++, multiplica--)
            {
                int digito = int.Parse(str.Substring(i, 1));
                resultado += digito * multiplica;
            }

            int resto = (resultado * 10) % 11;
            if (resto == 10)
                resto = 0;

            int digitoUm = int.Parse(str.Substring(9, 1));

            if(resto == digitoUm)
            {
                return true;
            }

            return false;
        }

        private static bool ValidacaoDigitoDois(string str)
        {
            int resultado = 0;
            for (int i = 0, multiplica = 11; i < 10; i++, multiplica--)
            {
                int digito = int.Parse(str.Substring(i, 1));
                resultado += digito * multiplica;
            }

            int resto = (resultado * 10) % 11;

            int digito2 = int.Parse(str.Substring(10, 1));

            if(resto == digito2)
            {
                return true;
            }

            return false;
        }
    }
}
