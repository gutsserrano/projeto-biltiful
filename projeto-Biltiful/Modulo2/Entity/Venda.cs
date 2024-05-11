using projeto_Biltiful.Modulo2.ManipuladorArquivo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo2.Entity
{
    internal class Venda
    {
        public int id { get; }                       // 5  (0 - 4)
        public DateOnly dataVenda { get; set; }      // 8  (5 - 12)
        public string cliente { get; set; }          // 11 (13-23)
        public float valorTotal { get; set; }        // 7  (24-30)

        public Venda() { }

        public Venda(int id, DateOnly dataVenda, string cliente, float valorTotal)
        {
            this.id = id;
            this.dataVenda = dataVenda;
            this.cliente = cliente;
            this.valorTotal = valorTotal;
        }

        private int gerarId()
        {
            string path = @"C:\Biltiful\";
            string file = "Venda.dat";
            int novoId = 1;

            ManipularArquivoVenda mav = new ManipularArquivoVenda(path, file);

            List<Venda> listaVenda = mav.CarregarArquivo();

            if (listaVenda.Count > 0)
            {
                novoId = listaVenda.Last().id;
                novoId++;
            }

            return novoId;
        }

        public override string? ToString()
        {
            return "id: " + id + "dataVenda: " + dataVenda.ToString() + " Cliente: " + ConverterClienteString(cliente) + " valor Total: " + valorTotal.ToString().Insert(valorTotal.ToString().Length - 2, ",");
        }

        private string ConverterClienteString(string cliente)
        {
            string path = @"C:\Biltiful\";
            string file = "Cliente.dat";

            RecuperarArquivosDeClientes rpC = new RecuperarArquivosDeClientes(path, file);
            string nomeData = rpC.recuperarNomeEDataNascimento(cliente);

            return nomeData;
        }

        public string FormatarParaArquivo()
        {
            return $"{ConverterIdParaArquivo(id)}" +
                    $"{ConverterDataParaArquivo(dataVenda)}" +
                    $"{cliente}" +
                    $"{ConverterValorParaArquivo(valorTotal)}";
        }

        private string ConverterValorParaArquivo(float valor)
        {

            return valor.ToString().Replace(",", "").PadLeft(7, '0');
        }

        private string ConverterIdParaArquivo(int id)
        {
            return id.ToString().PadLeft(5, '0');
        }

        private string ConverterDataParaArquivo(DateOnly data)
        {
            return $"{data.Day:00}{data.Month:00}{data.Year:0000}";
        }

        private bool validarAtividade(string? cpf)
        {
            string path = @"C:\Biltiful\";
            string file = "Cliente.dat";

            RecuperarArquivosDeClientes rpC = new RecuperarArquivosDeClientes(path, file);
            string estaAtivo = rpC.recuperarEstaAtivo(cpf);

            return estaAtivo.Equals("A");

        }

        private bool validarIdade(string? cpf)
        {
            string path = @"C:\Biltiful\";
            string file = "Cliente.dat";

            RecuperarArquivosDeClientes rpC = new RecuperarArquivosDeClientes(path, file);
            DateOnly dataNascimento = rpC.recuperarEData(cpf);
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);
            int idade = dataAtual.Year - dataNascimento.Year;

            return idade >= 18;
        }

        private bool validarCpf(string? cpf)
        {
            string path = @"C:\Biltiful\";
            string file = "Cliente.dat";
            string fileRisco = "Risco.dat";

            RecuperarArquivosDeClientes rpC = new RecuperarArquivosDeClientes(path, file);
            List<string> cpfCadastrado = rpC.RecuperarCpf();

            RecuperarArquivosDeClientes rpcRisco = new RecuperarArquivosDeClientes(path, fileRisco);
            List<string> cpfCadastraRisco = rpcRisco.RecuperarCpf();

            if (cpfCadastrado.Contains(cpf))
            {
                if (!cpfCadastraRisco.Contains(cpf))
                {
                    return true;
                }
            }

            return false;

        }

        internal Venda receberDados(string cpf)
        {

            DateOnly dataVenda = DateOnly.FromDateTime(DateTime.Now);
            float valorT = 0;
            ItemVenda itemVenda = new ItemVenda();
            int id = gerarId();

            valorT = itemVenda.gerarItemVenda(cpf, id);

            if (valorT > 9999999)
            {
                Console.WriteLine("Venda Invalida");
                new MainVenda().Executar();

            }

            Venda venda = new(id, dataVenda, cpf, valorT);

            return venda;
        }

        internal bool clienteValido(string cpf)
        {
            if (validarCpf(cpf))
            {
                if (validarIdade(cpf))
                {
                    if (validarAtividade(cpf))
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Usuario Inativo");
                    }

                }
                else
                {
                    Console.WriteLine("Menor de Idade");
                }

            }
            else
            {
                Console.WriteLine("CPF Iusing projeto_Biltiful.Modulo2.ManipuladorArquivo;\r\nusing System;\r\nusing System.Collections.Generic;\r\nusing System.ComponentModel.DataAnnotations;\r\nusing System.Linq;\r\nusing System.Text;\r\nusing System.Threading.Tasks;\r\n\r\nnamespace projeto_Biltiful.Modulo2.Entity\r\n{\r\n    internal class Venda\r\n    {\r\n        // Propriedades da classe\r\n        public int id { get; }                       // 5  (0 - 4)\r\n        public DateOnly dataVenda { get; set; }      // 8  (5 - 12)\r\n        public string cliente { get; set; }          // 11 (13-23)\r\n        public float valorTotal { get; set; }        // 7  (24-30)\r\n\r\n        // Construtores\r\n        public Venda() { }\r\n\r\n        public Venda(int id, DateOnly dataVenda, string cliente, float valorTotal)\r\n        {\r\n            this.id = id;\r\n            this.dataVenda = dataVenda;\r\n            this.cliente = cliente;\r\n            this.valorTotal = valorTotal;\r\n        }\r\n\r\n        // Métodos privados de apoio\r\n        private int gerarId()\r\n        {\r\n            string path = @\"C:\\Biltiful\\\";\r\n            string file = \"Venda.dat\";\r\n            int novoId = 1;\r\n\r\n            ManipularArquivoVenda mav = new ManipularArquivoVenda(path, file);\r\n            List<Venda> listaVenda = mav.CarregarArquivo();\r\n\r\n            if (listaVenda.Count > 0)\r\n            {\r\n                novoId = listaVenda.Last().id + 1;\r\n            }\r\n\r\n            return novoId;\r\n        }\r\n\r\n        private string ConverterClienteString(string cliente)\r\n        {\r\n            string path = @\"C:\\Biltiful\\\";\r\n            string file = \"Cliente.dat\";\r\n\r\n            RecuperarArquivosDeClientes rpC = new RecuperarArquivosDeClientes(path, file);\r\n            string nomeData = rpC.recuperarNomeEDataNascimento(cliente);\r\n\r\n            return nomeData;\r\n        }\r\n\r\n        private string ConverterValorParaArquivo(float valor)\r\n        {\r\n            return valor.ToString().Replace(\",\", \"\").PadLeft(7, '0');\r\n        }\r\n\r\n        private string ConverterIdParaArquivo(int id)\r\n        {\r\n            return id.ToString().PadLeft(5, '0');\r\n        }\r\n\r\n        private string ConverterDataParaArquivo(DateOnly data)\r\n        {\r\n            return $\"{data.Day:00}{data.Month:00}{data.Year:0000}\";\r\n        }\r\n\r\n        private bool validarAtividade(string? cpf)\r\n        {\r\n            string path = @\"C:\\Biltiful\\\";\r\n            string file = \"Cliente.dat\";\r\n\r\n            RecuperarArquivosDeClientes rpC = new RecuperarArquivosDeClientes(path, file);\r\n            string estaAtivo = rpC.recuperarEstaAtivo(cpf);\r\n\r\n            return estaAtivo.Equals(\"A\");\r\n        }\r\n\r\n        private bool validarIdade(string? cpf)\r\n        {\r\n            string path = @\"C:\\Biltiful\\\";\r\n            string file = \"Cliente.dat\";\r\n\r\n            RecuperarArquivosDeClientes rpC = new RecuperarArquivosDeClientes(path, file);\r\n            DateOnly dataNascimento = rpC.recuperarEData(cpf);\r\n            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);\r\n            int idade = dataAtual.Year - dataNascimento.Year;\r\n\r\n            return idade >= 18;\r\n        }\r\n\r\n        private bool validarCpf(string? cpf)\r\n        {\r\n            string path = @\"C:\\Biltiful\\\";\r\n            string file = \"Cliente.dat\";\r\n            string fileRisco = \"Risco.dat\";\r\n\r\n            RecuperarArquivosDeClientes rpC = new RecuperarArquivosDeClientes(path, file);\r\n            List<string> cpfCadastrado = rpC.RecuperarCpf();\r\n\r\n            RecuperarArquivosDeClientes rpcRisco = new RecuperarArquivosDeClientes(path, fileRisco);\r\n            List<string> cpfCadastraRisco = rpcRisco.RecuperarCpf();\r\n\r\n            return cpfCadastrado.Contains(cpf) && !cpfCadastraRisco.Contains(cpf);\r\n        }\r\n\r\n        // Métodos públicos da classe\r\n        public override string? ToString()\r\n        {\r\n            return \"id: \" + id + \" dataVenda: \" + dataVenda.ToString(\"\") + \" Cliente: \" + ConverterClienteString(cliente) + \" valor Total: \" + valorTotal.ToString().Insert(valorTotal.ToString().Length - 2, \",\");\r\n        }\r\n\r\n        public string FormatarParaArquivo()\r\n        {\r\n            return $\"{ConverterIdParaArquivo(id)}\" +\r\n                    $\"{ConverterDataParaArquivo(dataVenda)}\" +\r\n                    $\"{cliente}\" +\r\n                    $\"{ConverterValorParaArquivo(valorTotal)}\";\r\n        }\r\n\r\n        internal Venda receberDados(string cpf)\r\n        {\r\n            DateOnly dataVenda = DateOnly.FromDateTime(DateTime.Now);\r\n            float valorT = 0;\r\n            ItemVenda itemVenda = new ItemVenda();\r\n            int id = gerarId();\r\n\r\n            valorT = itemVenda.gerarItemVenda(cpf, id);\r\n\r\n            if (valorT > 9999999)\r\n            {\r\n                Console.WriteLine(\"Venda Invalida\");\r\n                new MainVenda().Executar();\r\n            }\r\n\r\n            Venda venda = new(id, dataVenda, cpf, valorT);\r\n\r\n            return venda;\r\n        }\r\n\r\n        internal bool clienteValido(string cpf)\r\n        {\r\n            if (validarCpf(cpf))\r\n            {\r\n                if (validarIdade(cpf))\r\n                {\r\n                    if (validarAtividade(cpf))\r\n                    {\r\n                        return true;\r\n                    }\r\n                    else\r\n                    {\r\n                        Console.WriteLine(\"Usuario Inativo\");\r\n                    }\r\n                }\r\n                else\r\n                {\r\n                    Console.WriteLine(\"Menor de Idade\");\r\n                }\r\n            }\r\n            else\r\n            {\r\n                Console.WriteLine(\"CPF Invalido\");\r\n            }\r\n            return false;\r\n        }\r\n    }\r\n}\r\nnvalido");
            }
            return false;

        }

    }
}
