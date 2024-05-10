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
            return "id: " + id + " dataVenda: " + dataVenda.ToString("") + " Cliente: " + ConverterClienteString(cliente) + " valor Total: " + valorTotal.ToString().Insert(valorTotal.ToString().Length - 2,",") ;
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

            return valor.ToString().Replace(",","").PadLeft(7, '0');
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

            if (estaAtivo.Equals("A"))
            {
                return true;
            }

            return false;

        }

        private bool validarIdade(string? cpf)
        {
            string path = @"C:\Biltiful\";
            string file = "Cliente.dat";

            RecuperarArquivosDeClientes rpC = new RecuperarArquivosDeClientes(path, file);
            DateOnly dataNascimento = rpC.recuperarEData(cpf);
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);
            int idade = dataAtual.Year - dataNascimento.Year;

            if (idade >= 18)
            {
                return true;
            }

            return false;
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
          
            do
            {
                Console.WriteLine("Digit o valor Total: ");

                try
                {
                    valorT = float.Parse(Console.ReadLine());


                }
                catch (Exception ex)
                {
                    ex.ToString();
                }


            } while (valorT > 99999.99);
           


            Venda venda = new(gerarId(), dataVenda, cpf, valorT);

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
                Console.WriteLine("CPF Invalido");
            }
            return false;

        }

        internal bool validarId(int id)
        {
            string path = @"C:\Biltiful\";
            string file = "Venda.dat";
            bool scopo = true;

            ManipularArquivoVenda mav = new ManipularArquivoVenda(path, file);

            List<Venda> listaVenda = mav.CarregarArquivo();

            if (listaVenda.Count < id)
            {
                scopo = false;
            }

            return scopo;
        }
    }
}
