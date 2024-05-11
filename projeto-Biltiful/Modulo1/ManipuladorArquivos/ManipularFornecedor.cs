using projeto_Biltiful.Modulo1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo1.ManipuladorArquivos
{
    internal class ManipularFornecedor
    {
        string Caminho { get; set; }
        string Arquivo { get; set; }

        public ManipularFornecedor(string caminho, string arquivo)
        {
            Caminho = caminho;
            Arquivo = arquivo;

            if (!Directory.Exists(Caminho))
                Directory.CreateDirectory(Caminho);

            if (!File.Exists(Caminho + Arquivo))
            {
                var aux = File.Create(Caminho + Arquivo);
                aux.Close();
            }
        }

        public List<Fornecedor> Recuperar()
        {
            List<Fornecedor> fornecedores = new();

            foreach (string linha in File.ReadAllLines(Caminho + Arquivo))
                fornecedores.Add(new Fornecedor(linha));

            return fornecedores;
        }

        public void Salvar(List<Fornecedor> fornecedores)
        {
            fornecedores.Sort((f1, f2) => f1.RazaoSocial.CompareTo(f2.RazaoSocial));
            var sw = new StreamWriter(Caminho + Arquivo);

            foreach (Fornecedor fornecedor in fornecedores)
            {
                string texto = fornecedor.FormatarParaArquivo();
                sw.WriteLine(texto);
            }

            sw.Close();
        }

        public void Cadastrar()
        {
            List<Fornecedor> fornecedores = Recuperar();

            string cnpj;
            bool cnpjValido;
            string razaoSocial;
            DateOnly dataAbertura;
            string telefone;

            Console.Clear();
            Console.WriteLine("**Cadastrar Fornecedor**");
            do
            {
                cnpj = Fornecedor.FormatarCnpj(MainCadastro.LerString("Digite o CNPJ do fornecedor: "));
                cnpjValido = Fornecedor.VerificarCnpj(cnpj);

                if (!cnpjValido)
                {
                    Console.WriteLine("\n**CNPJ inválido, digite novamente**\n");
                }
                else if (fornecedores.Exists(f => f.Cnpj.Equals(cnpj)))
                {
                    Console.WriteLine("\n**CNPJ já cadastrado, digite outro**\n");
                    cnpjValido = false;
                }
            } while (!cnpjValido);

            razaoSocial = MainCadastro.LerString("Digite a razão social do fornecedor: ");

            dataAbertura = MainCadastro.LerData("Digite a data de abertura: ");

            fornecedores.Add(new Fornecedor(cnpj, razaoSocial, dataAbertura));
            Salvar(fornecedores);

            Console.WriteLine("\n**Fornecedor cadastrado com sucesso!**\n");
            Console.ReadKey();
        }

        public void NavegarListaFornecedores()
        {
            List<Fornecedor> fornecedores = Recuperar();

            Console.Clear();
            Console.WriteLine("**Navegar Lista de Fornecedores**\n");

            if (fornecedores.Count == 0)
            {
                Console.WriteLine("A lista de fornecedores está vazia.");
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
                ImprimirFornecedor(fornecedores[currentIndex]);
                Console.WriteLine();

                Console.WriteLine("Pressione 'N' para navegar para o próximo fornecedor, 'V' para voltar ou 'S' para sair.");

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.N)
                {
                    currentIndex = (currentIndex + increment + fornecedores.Count) % fornecedores.Count;
                }
                else if (key == ConsoleKey.V)
                {
                    currentIndex = (currentIndex - increment + fornecedores.Count) % fornecedores.Count;
                }
            } while (key != ConsoleKey.S);
        }

        public void Localizar()
        {
            List<Fornecedor> fornecedores = Recuperar();

            string cnpj;
            bool existe = false;

            Console.Clear();
            cnpj = Fornecedor.FormatarCnpj(MainCadastro.LerString("Digite o CNPJ do fornecedor: "));

            Console.WriteLine();
            foreach (Fornecedor fornecedor in fornecedores)
            {
                if (cnpj.Equals(fornecedor.Cnpj))
                {
                    ImprimirFornecedor(fornecedor);
                    existe = true;
                    break;
                }
            }

            if (!existe)
            {
                Console.WriteLine("\n**Fornecedor não encontrado**\n");
            }

            Console.ReadKey();
        }

        public void ImprimirFornecedor(Fornecedor fornecedor)
        {
            Console.WriteLine($"CNPJ: {fornecedor.Cnpj}");
            Console.WriteLine($"Razão Social: {fornecedor.RazaoSocial}");
            Console.WriteLine($"Data de Abertura: {fornecedor.DataAbertura}");
            Console.WriteLine($"Ultima Compra: {fornecedor.UltimaCompra}");
            Console.WriteLine($"Data de Cadastro: {fornecedor.DataCadastro}");
            Console.WriteLine("Situação: " + (fornecedor.Situacao == 'A' ? "Ativo" : "Inativo"));
        }

        public void Editar()
        {
            List<Fornecedor> fornecedores = Recuperar();

            string cnpj;
            bool existe = false;
            Fornecedor fornecedor = null;

            cnpj = Fornecedor.FormatarCnpj(MainCadastro.LerString("Digite o CNPJ do fornecedor: "));

            foreach (Fornecedor f in fornecedores)
            {
                if (cnpj.Equals(f.Cnpj))
                {
                    fornecedor = f;
                    existe = true;
                    break;
                }
            }

            if (!existe)
            {
                Console.WriteLine("\n**Fornecedor não encontrado**\n");
            }
            else
            {
                switch (MenuEditar())
                {
                    case 1:
                        fornecedor.RazaoSocial = MainCadastro.LerString("Digite a nova razão social: ");
                        break;
                    case 2:
                        fornecedor.Situacao = fornecedor.Situacao == 'A' ? 'I' : 'A';
                        break;
                }

                Salvar(fornecedores);
                Console.WriteLine("\n**Atributos alterados com sucesso!**");
            }

            Console.ReadKey();
        }

        private int MenuEditar()
        {
            Console.Clear();
            Console.WriteLine("Selecione o atributo que deseja editar:");
            Console.WriteLine("1 - Razão Social");
            Console.WriteLine("2 - Situação");
            Console.WriteLine("0 - Sair");

            int opcao;
            do
            {
                opcao = MainCadastro.LerInt("Digite a opção desejada: ");
            } while (opcao < 0 || opcao > 2);

            return opcao;
        }
    }
}
