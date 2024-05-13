using projeto_Biltiful.Modulo2.ManipuladorArquivo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
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

        public int GerarId()
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
            return "id: " + id + " dataVenda: " + dataVenda.ToString() + " Cliente: " +
                ConverterClienteString(cliente) + " valor Total: " + 
                valorTotal.ToString().Insert(valorTotal.ToString().Length - 2, ",");
        }

        public string ConverterClienteString(string cliente)
        {
            string path = @"C:\Biltiful\";
            string file = "Cliente.dat";

            RecuperarArquivosDeClientes rpC = new RecuperarArquivosDeClientes(path, file);
            string nomeData = rpC.RecuperarNomeEDataNascimento(cliente);

            return nomeData;
        }

        public string FormatarParaArquivo()
        {
            return $"{id.ToString().PadLeft(5, '0')}" +
                    $"{dataVenda.Day:00}{dataVenda.Month:00}{dataVenda.Year:0000}" +
                    $"{cliente}" +
                    $"{valorTotal.ToString().Replace(",", "").PadLeft(7, '0')}";
        }

        public bool ValidarAtividade(string? cpf)
        {
            string path = @"C:\Biltiful\";
            string file = "Cliente.dat";

            RecuperarArquivosDeClientes rpC = new RecuperarArquivosDeClientes(path, file);
            string estaAtivo = rpC.RecuperarEstaAtivo(cpf);

            return estaAtivo.Equals("A");

        }

        public bool ValidarIdade(string? cpf)
        {
            string path = @"C:\Biltiful\";
            string file = "Cliente.dat";

            RecuperarArquivosDeClientes rpC = new RecuperarArquivosDeClientes(path, file);
            DateOnly dataNascimento = rpC.RecuperarEData(cpf);
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);
            int idade = dataAtual.Year - dataNascimento.Year;

            return idade >= 18;
        }

        public bool ValidarCpf(string? cpf)
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

        public Venda ReceberDados(string cpf)
        {

            DateOnly dataVenda = DateOnly.FromDateTime(DateTime.Now);
            float valorT = 0;
            ItemVenda itemVenda = new ItemVenda();
            int id = GerarId();

            valorT = itemVenda.GerarItemVenda(cpf, id);

            if (valorT > 9999999)
            {
                Console.WriteLine("Venda Invalida");
                new MainVenda().Executar();

            }

            Venda venda = new(id, dataVenda, cpf, valorT);

            return venda;
        }

        public bool ClienteValido(string cpf)
        {
            if (ValidarCpf(cpf))
            {
                if (ValidarIdade(cpf))
                {
                    if (ValidarAtividade(cpf))
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
                Console.WriteLine("Cpf Invalida");

            }
            return false;

        }

    }
}
