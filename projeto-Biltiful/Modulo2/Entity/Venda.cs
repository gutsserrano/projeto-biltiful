using projeto_Biltiful.Modulo2.ManipuladorArquivo;
using System;
using System.Collections.Generic;
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
        public float valorTotal { get; set; }          // 7  (24-30)

        public Venda() { }

        public Venda(int id,  DateOnly dataVenda, string cliente, float valorTotal)
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
            int novoId = 0;

            ManipularArquivoVenda mav = new ManipularArquivoVenda(path, file);

            List<Venda> listaVenda = mav.CarregarArquivo();

            novoId = listaVenda.Last().id;
            novoId++;

            return novoId;
        }

        public override string? ToString()
        {
            return "id: " + id + " dataVenda: " + dataVenda.ToString("") + " Cliente: " + cliente + " valor TOtal: " + valorTotal;
        }

        public string FormatarParaArquivo()
        {
            return $"{id}" +
                    $"{ConverterDataParaArquivo(dataVenda)}" +
                    $"{cliente}" +
                    $"{valorTotal}";
        }

        private string ConverterDataParaArquivo(DateOnly data)
        {
            return $"{data.Day:00}{data.Month:00}{data.Year:0000}";
        }

        internal Venda receberDados()
        {

            DateOnly dataVenda = DateOnly.FromDateTime(DateTime.Now);
            string cpf;
            float valorT = 0;

            Console.WriteLine("Digit Cpf da venda: ");
            cpf = Console.ReadLine();

            if(validarCpf(cpf))
            {
                if (validarIdade(cpf))
                {
                    if (validarAtividade(cpf))
                    {
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

            Venda venda = new(gerarId(), dataVenda, cpf, valorT);

            return venda;
        }

        private bool validarAtividade(string? cpf)
        {
            string path = @"C:\Biltiful\";
            string file = "Clientes.dat";

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
            string file = "Clientes.dat";
             
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
            string file = "Clientes.dat";
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
    }
}
