using projeto_Biltiful.Modulo1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo1.ManipuladorArquivos
{
    internal class ManipularCliente
    {
        string Caminho { get; set; }
        string Arquivo { get; set; }

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

        public List<Cliente> Recuperar()
        {
            List<Cliente> clientes = new();

            foreach (string linha in File.ReadAllLines(Caminho + Arquivo))
                clientes.Add(new Cliente(linha));

            return clientes;
        }

        public void Salvar(List<Cliente> clientes)
        {
            clientes.Sort((c1, c2) => c1.Nome.CompareTo(c2.Nome));
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
            List<Cliente> clientes = Recuperar();

            string cpf;
            string nome;
            DateOnly dataNasc;
            char sexo;
            bool cpfValido;

          
            Console.Clear();
            Console.WriteLine("**Cadastrar Cliente**");
            do
            {
                cpf = Cliente.FormatarCpf(MainCadastro.LerString("Digite o cpf: "));
                cpfValido = Cliente.VerificarCpf(cpf);

                if (!cpfValido)
                {
                    Console.WriteLine("\n**CPF inválido, digite novamente**\n");
                }
                else if (clientes.Exists(c => c.Cpf.Equals(cpf)))
                {
                    Console.WriteLine("\n**CPF já cadastrado, digite outro**\n");
                    cpfValido = false;
                }
            } while (!cpfValido);


            nome = MainCadastro.LerString("Digite o nome: ");

            dataNasc = MainCadastro.LerData("Digite a data de nascimento (dd/mm/aaaa): ");

            do
            {
                sexo = char.ToUpper(MainCadastro.LerChar("Digite o sexo 'M' ou 'F': "));
            } while (sexo != 'M' && sexo != 'F');

            clientes.Add(new Cliente(cpf, nome, dataNasc, sexo));
            Salvar(clientes);

            Console.WriteLine("\n**Cliente cadastrado com sucesso!**\n");
            Console.ReadKey();
        }

        public void NavegarListaClientes()
        {
            List<Cliente> clientes = Recuperar();

            Console.Clear();
            Console.WriteLine("**Navegar Lista de Clientes**\n");

            if (clientes.Count == 0)
            {
                Console.WriteLine("A lista de clientes está vazia.");
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
                ImprimirCliente(clientes[currentIndex]);
                Console.WriteLine();

                Console.WriteLine("Pressione 'N' para navegar para o próximo cliente, 'V' para voltar ou 'S' para sair.");

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.N)
                {
                    currentIndex = (currentIndex + increment + clientes.Count) % clientes.Count;
                }
                else if (key == ConsoleKey.V)
                {
                    currentIndex = (currentIndex - increment + clientes.Count) % clientes.Count;
                }
            } while (key != ConsoleKey.S);
        }

        public void Localizar()
        {
            List<Cliente> clientes = Recuperar();

            string cpf;
            bool existe = false;

            cpf = Cliente.FormatarCpf(MainCadastro.LerString("Digite o cpf: "));

            foreach (Cliente c in clientes)
            {
                if (cpf.Equals(c.Cpf))
                {
                    ImprimirCliente(c);
                    existe = true;
                    break;
                }
            }

            if (!existe)
            {
                Console.WriteLine("\n**Cliente não encontrado**\n");
            }

            Console.ReadKey();  
        }

        public void Editar()
        {
            List<Cliente> clientes = Recuperar();

            string cpf;
            bool existe = false;
            Cliente cliente = null;

            cpf = Cliente.FormatarCpf(MainCadastro.LerString("Digite o cpf: "));

            foreach(Cliente c in clientes)
            {
                if(cpf.Equals(c.Cpf))
                {
                    cliente = c;
                    existe = true;
                    break;
                }
            }

            if (!existe)
            {
                Console.WriteLine("\n**Cliente não encontrado**\n");
            }
            else
            {
                switch (MenuEditar())
                {
                    case 1:
                        cliente.Nome = MainCadastro.LerString("Digite o novo nome: ");
                        break;
                    case 2:
                        cliente.DataNascimento = MainCadastro.LerData("Digite a nova Data de nascimento: ");
                        break;
                    case 3:
                        do
                        {
                            cliente.Sexo = char.ToUpper(MainCadastro.LerChar("Digite o sexo 'M' ou 'F': "));
                        } while (cliente.Sexo != 'M' && cliente.Sexo != 'F');
                        break;
                    case 4:
                        cliente.Situacao = cliente.Situacao == 'A' ? 'I' : 'A';
                        break;
                    default:
                        return;
                }
                
                Salvar(clientes);
                Console.WriteLine("\n**Atributos alterados com sucesso!**");
            }

            Console.ReadKey();
        }

        public void ImprimirCliente(Cliente cliente)
        {
            Console.WriteLine("CPF: " + cliente.Cpf);
            Console.WriteLine("Nome: " + cliente.Nome);
            Console.WriteLine("Data de nascimento: " + cliente.DataNascimento);
            Console.WriteLine("Sexo: " + cliente.Sexo);
            Console.WriteLine("Situação: " + (cliente.Situacao == 'A' ? "Ativo" : "Inativo"));
        }

        private int MenuEditar()
        {
            int op;
            do
            {
                Console.Clear();
                Console.WriteLine("Qual atributo do Cliente deseja editar?");
                Console.WriteLine("1 - Nome");
                Console.WriteLine("2 - Data de nascimento");
                Console.WriteLine("3 - Sexo");
                Console.WriteLine("4 - Alterar situação");
                Console.WriteLine("0 - Sair");
                op = MainCadastro.LerInt("op: ");

            } while (op < 0 || op > 4);

            return op;
        }

    }
}
