using projeto_Biltiful.Modulo1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo1.ManipuladorArquivos
{
    internal class ManipularCliente
    {
        string Caminho { get; set; }
        string Arquivo {  get; set; }

        public ManipularCliente(string caminho, string arquivo)
        {
            this.Caminho = caminho;
            this.Arquivo = arquivo;

            if (!Directory.Exists(Caminho))
                Directory.CreateDirectory(Caminho);

            if (!File.Exists(Caminho + Arquivo))
            {
                var aux = File.Create(Caminho + Arquivo);
                aux.Close();
            }
        }

        public List<Cliente> Recuperar(string caminho, string arquivo)
        {
            List<Cliente> clientes = new();

            foreach (string linha in File.ReadAllLines(Caminho + Arquivo))
                clientes.Add(new Cliente(linha));

            return clientes;
        }

        public void Salvar(List<Cliente> clientes)
        {
            var sw = new StreamWriter(Caminho + Arquivo);

            foreach(Cliente cliente in clientes)
            {
                string texto = cliente.FormatarParaArquivo();
                sw.WriteLine(texto);
            }

            sw.Close();
        }

        public void Cadastrar()
        {
            Console.Clear();
            Console.WriteLine("**Cadastrar Cliente");

            string cpf;
            string nome;
            DateOnly dataNasc;
            char sexo;
            DateOnly ultimaCompra;
            DateOnly dataCadastro;
            char situacao;

            bool cpfValido;

            do
            {
                do
                {
                    Console.Write("\nDigite o cpf: ");
                    cpf = Console.ReadLine();
                    cpfValido = Cliente.VerificarCpf(cpf);

                    if (!cpfValido)
                    {
                        Console.WriteLine("\n**CPF inválido, digite novamente**\n");
                    }
                } while (!cpfValido);

                Console.Write("\nDigite o nome: ");
            } while ();
        }

        public void Editar()
        {
            // Edita um Cliente específico
        }
    }
}
