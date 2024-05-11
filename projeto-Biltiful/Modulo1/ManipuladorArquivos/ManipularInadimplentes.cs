using projeto_Biltiful.Modulo1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo1.ManipuladorArquivos
{
    internal class ManipularInadimplentes
    {
        string Caminho { get; set; }
        string Arquivo { get; set; }

        ManipularCliente Clientes { get; set; }

        public ManipularInadimplentes(string caminho, string arquivo, ManipularCliente clientes)
        {
            Caminho = caminho;
            Arquivo = arquivo;
            Clientes = clientes;

            if (!Directory.Exists(Caminho))
                Directory.CreateDirectory(Caminho);

            if (!File.Exists(Caminho + Arquivo))
            {
                var aux = File.Create(Caminho + Arquivo);
                aux.Close();
            }
        }

        public List<string> Recuperar()
        {
            List<string> inadimplentes = new();

            foreach (string linha in File.ReadAllLines(Caminho + Arquivo))
                inadimplentes.Add(new string(linha));

            return inadimplentes;
        }

        public void Salvar(List<string> inadimplentes)
        {
            var sw = new StreamWriter(Caminho + Arquivo);

            foreach (string inadimplente in inadimplentes)
            {
                string texto = inadimplente;
                sw.WriteLine(texto);
            }

            sw.Close();
        }

        public void Adicionar()
        {
            List<string> inadimplentes = Recuperar();
            List<Cliente> clientes = Clientes.Recuperar();
            string cpf;

            Console.Clear();
            cpf = MainCadastro.LerString("Digite o CPF a ser adicionado na lista de inadimplentes: ");
            cpf = Cliente.FormatarCpf(cpf);

            if (clientes.Exists(c => c.Cpf == cpf))
            {
                if (!inadimplentes.Contains(cpf))
                {
                    inadimplentes.Add(cpf);
                    Salvar(inadimplentes);
                    Console.WriteLine("\n**Cliente adicionado à lista de inadimplentes**\n");
                }
                else
                    Console.WriteLine("\n**Cliente já está na lista de inadimplentes**\n");
            }
            else
                Console.WriteLine("\n**Cliente não encontrado**\n");   

            Console.ReadKey();

        }

        public void Remover()
        {
            List<string> inadimplentes = Recuperar();
            List<Cliente> clientes = Clientes.Recuperar();
            string cpf;

            Console.Clear();
            cpf = MainCadastro.LerString("Digite o CPF a ser removido da lista de inadimplentes: ");
            cpf = Cliente.FormatarCpf(cpf);

            if (clientes.Exists(c => c.Cpf == cpf))
            {
                if (inadimplentes.Contains(cpf))
                {
                    inadimplentes.Remove(cpf);
                    Salvar(inadimplentes);
                    Console.WriteLine("\n**Cliente removido da lista de inadimplentes**\n");
                }
                else
                    Console.WriteLine("\n**Cliente não está na lista de inadimplentes**\n");
            }
            else
                Console.WriteLine("\n**Cliente não encontrado**\n");

            Console.ReadKey();
        }

        public void BuscarPorCpf()
        {
            List<string> inadimplentes = Recuperar();
            List<Cliente> clientes = Clientes.Recuperar();
            string cpf;
            bool existe = false;

            Console.Clear();
            cpf = MainCadastro.LerString("Digite o CPF a ser buscado na lista de inadimplentes: ");
            cpf = Cliente.FormatarCpf(cpf);

            foreach (Cliente cliente in clientes)
            {
                if (cliente.Cpf == cpf)
                {
                    Console.Clear();
                    Console.WriteLine("**Cliente encontrado**\n");
                    Clientes.ImprimirCliente(cliente);
                    existe = true;
                    break;
                }
            }

            if (!existe)
                Console.WriteLine("\n**Cliente não encontrado**\n");

            Console.ReadKey();
        }

        public void NavegarListaInadimplemtes()
        {
            List<string> inadimplentes = Recuperar();

            if (inadimplentes.Count == 0)
            {
                Console.WriteLine("A lista de inadimplentes está vazia.");
                Console.ReadKey();
                return;
            }

            int currentIndex = 0;
            int increment = 1;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine("=================\n");
                ImprimirInadimplente(inadimplentes[currentIndex]);
                Console.WriteLine();

                Console.WriteLine("Pressione 'N' para navegar para o próximo inadimplente, 'V' para voltar ou 'S' para sair.");

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.N)
                {
                    currentIndex = (currentIndex + increment + inadimplentes.Count) % inadimplentes.Count;
                }
                else if (key == ConsoleKey.V)
                {
                    currentIndex = (currentIndex - increment + inadimplentes.Count) % inadimplentes.Count;
                }
            } while (key != ConsoleKey.S);
        }

        private void ImprimirInadimplente(string cpf)
        {
            List<Cliente> clientes = Clientes.Recuperar();

            foreach (Cliente cliente in clientes)
            {
                if (cliente.Cpf == cpf)
                {
                    Console.WriteLine("CPF: " + cliente.Cpf);
                    Console.WriteLine("Nome: " + cliente.Nome);
                    Console.WriteLine("Data de nascimento: " + cliente.DataNascimento);
                    Console.WriteLine("Sexo: " + cliente.Sexo);
                    Console.WriteLine("Situação: " + (cliente.Situacao == 'A' ? "Ativo" : "Inativo"));
                    break;
                }
            }
        }
    }
}
