using projeto_Biltiful.Modulo1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo1.ManipuladorArquivos
{
    internal class ManipularBloqueados
    {
        string Caminho { get; set; }
        string Arquivo { get; set; }

        ManipularFornecedor Fornecedores { get; set; }

        public ManipularBloqueados(string caminho, string arquivo, ManipularFornecedor fornecedores)
        {
            Caminho = caminho;
            Arquivo = arquivo;
            Fornecedores = fornecedores;

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
            List<string> bloqueados = new();

            foreach (string linha in File.ReadAllLines(Caminho + Arquivo))
                bloqueados.Add(new string(linha));

            return bloqueados;
        }

        public void Salvar(List<string> bloqueados)
        {
            var sw = new StreamWriter(Caminho + Arquivo);

            foreach (string bloqueado in bloqueados)
            {
                string texto = bloqueado;
                sw.WriteLine(texto);
            }

            sw.Close();
        }

        public void Adicionar()
        {
            List<string> bloqueados = Recuperar();
            List<Fornecedor> fornecedores = Fornecedores.Recuperar();
            string cnpj;

            Console.Clear();
            cnpj = MainCadastro.LerString("Digite o CNPJ a ser adicionado na lista de bloqueados: ");
            cnpj = Fornecedor.FormatarCnpj(cnpj);

            if (fornecedores.Exists(f => f.Cnpj == cnpj))
            {
                if (!bloqueados.Contains(cnpj))
                {
                    bloqueados.Add(cnpj);
                    Salvar(bloqueados);
                    Console.WriteLine("\n**Fornecedor adicionado à lista de bloqueados**\n");
                }
                else
                    Console.WriteLine("\n**Fornecedor já está na lista de bloqueados**\n");
            }
            else
                Console.WriteLine("\n**Fornecedor não encontrado**\n");

            Console.ReadKey();
        }

        public void Remover()
        {
            List<string> bloqueados = Recuperar();
            string cnpj;

            Console.Clear();
            cnpj = MainCadastro.LerString("Digite o CNPJ a ser removido da lista de bloqueados: ");
            cnpj = Fornecedor.FormatarCnpj(cnpj);

            if (bloqueados.Contains(cnpj))
            {
                bloqueados.Remove(cnpj);
                Salvar(bloqueados);
                Console.WriteLine("\n**Fornecedor removido da lista de bloqueados**\n");
            }
            else
                Console.WriteLine("\n**Fornecedor não está na lista de bloqueados**\n");

            Console.ReadKey();
        }

        public void BuscarPorCnpj()
        {
            List<string> bloqueados = Recuperar();
            List<Fornecedor> fornecedores = Fornecedores.Recuperar();
            string cnpj;
            bool existe = false;

            Console.Clear();
            cnpj = MainCadastro.LerString("Digite o CNPJ a ser buscado na lista de bloqueados: ");
            cnpj = Fornecedor.FormatarCnpj(cnpj);

            foreach (Fornecedor fornecedor in fornecedores)
            {
                if (fornecedor.Cnpj == cnpj)
                {
                    Console.Clear();
                    Console.WriteLine("**Cliente encontrado**\n");
                    Fornecedores.ImprimirFornecedor(fornecedor);
                    existe = true;
                    break;
                }
            }

            if (!existe)
                Console.WriteLine("\n**Cliente não encontrado**\n");

            Console.ReadKey();
        }

        public void NavegarListaBloqueados()
        {
            List<string> bloqueados = Recuperar();

            if (bloqueados.Count == 0)
            {
                Console.WriteLine("A lista de bloqueados está vazia.");
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
                ImprimirBloqueados(bloqueados[currentIndex]);
                Console.WriteLine();

                Console.WriteLine("Pressione 'N' para navegar para o próximo fornecedor, 'V' para voltar ou 'S' para sair.");

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.N)
                {
                    currentIndex = (currentIndex + increment + bloqueados.Count) % bloqueados.Count;
                }
                else if (key == ConsoleKey.V)
                {
                    currentIndex = (currentIndex - increment + bloqueados.Count) % bloqueados.Count;
                }
            } while (key != ConsoleKey.S);
        }

        private void ImprimirBloqueados(string cnpj)
        {
            List<Fornecedor> fornecedores = Fornecedores.Recuperar();

            foreach (Fornecedor fornecedor in fornecedores)
            {
                if (fornecedor.Cnpj == cnpj)
                {
                    Console.WriteLine($"CNPJ: {fornecedor.Cnpj}");
                    Console.WriteLine($"Razão Social: {fornecedor.RazaoSocial}");
                    Console.WriteLine($"Data de Abertura: {fornecedor.DataAbertura}");
                    Console.WriteLine($"Ultima Compra: {fornecedor.UltimaCompra}");
                    Console.WriteLine($"Data de Cadastro: {fornecedor.DataCadastro}");
                    Console.WriteLine("Situação: " + (fornecedor.Situacao == 'A' ? "Ativo" : "Inativo"));
                    break;
                }
            }
        }
    }
}
