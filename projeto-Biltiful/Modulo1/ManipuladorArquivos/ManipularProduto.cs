using projeto_Biltiful.Modulo1.Entidades;
using projeto_Biltiful.Modulo4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo1.ManipuladorArquivos
{
    internal class ManipularProduto
    {
        string Caminho { get; set; }
        string Arquivo { get; set; }

        public ManipularProduto(string caminho, string arquivo)
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

        public List<Produto> Recuperar()
        {
            List<Produto> produtos = new();

            foreach (string linha in File.ReadAllLines(Caminho + Arquivo))
                produtos.Add(new Produto(linha));

            return produtos;
        }   

        public void Salvar(List<Produto> produtos)
        {
            produtos.Sort((p1, p2) => p1.Nome.CompareTo(p2.Nome));
            var sw = new StreamWriter(Caminho + Arquivo);

            foreach (Produto produto in produtos)
            {
                string texto = produto.FormatarParaArquivo();
                sw.WriteLine(texto);
            }

            sw.Close();
        }

        public void Cadastrar()
        {
            List<Produto> produtos = Recuperar();

            string codigoBarras;
            bool cdValido;
            string nome;
            float valorVenda;


            Console.Clear();
            Console.WriteLine("**Cadastrar Produto**");

            do
            {
                codigoBarras = MainCadastro.LerString("Codigo de barras: ");
                cdValido = Produto.VerificarCodigoBarras(codigoBarras);
                if(!cdValido)
                    Console.WriteLine("\n**Codigo de barras invalido, tente novamente**\n");
                else if(produtos.Exists(p => p.CodigoBarras == codigoBarras))
                {
                    Console.WriteLine("\n**Codigo de barras ja cadastrado, tente novamente**\n");
                    cdValido = false;
                }
            }while(!cdValido);

            nome = MainCadastro.LerString("Nome do produto: ");

            do
            {
                valorVenda = MainCadastro.LerFloat("Valor de venda: ");

                if (valorVenda >= 1000)
                    Console.WriteLine("\n**Valor de venda invalido (máximo = R$ 999,99)**\n");
                else if (valorVenda < 0.01)
                    Console.WriteLine("\n**Valor de venda invalido (minimo = R$ 0,01)**\n");
                
            } while(valorVenda >= 1000 || valorVenda < 0.01);

            produtos.Add(new Produto(codigoBarras, nome, valorVenda));
            Salvar(produtos);

            Console.WriteLine("\n**Produto cadastrado com sucesso!**\n");
            Console.ReadKey();
        }

        public void NavegarListaProdutos()
        {
            List<Produto> produtos = Recuperar();

            int currentIndex = 0;
            int increment = 1;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine("=================\n");
                ImprimirProduto(produtos[currentIndex]);
                Console.WriteLine();

                Console.WriteLine("Pressione 'N' para navegar para o próximo produto, 'V' para voltar ou 'S' para sair.");

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.N)
                {
                    currentIndex = (currentIndex + increment + produtos.Count) % produtos.Count;
                }
                else if (key == ConsoleKey.V)
                {
                    currentIndex = (currentIndex - increment + produtos.Count) % produtos.Count;
                }
            } while (key != ConsoleKey.S);
        }

        public void Localizar()
        {
            List<Produto> produtos = Recuperar();

            string codigoBarras;
            bool existe = false;

            codigoBarras = MainCadastro.LerString("Digite o codigo de barras: ");

            foreach (Produto produto in produtos)
            {
                if (produto.CodigoBarras == codigoBarras)
                {
                    Console.Clear();
                    Console.WriteLine("**Produto encontrado**\n");
                    ImprimirProduto(produto);
                    existe = true;
                    break;
                }
            }

            if (!existe)
                Console.WriteLine("\n**Produto não encontrado**\n");

            Console.ReadKey();
        }   

        public void Editar()
        {
            List<Produto> produtos = Recuperar();

            string codigoBarras;
            bool existe = false;

            Produto prod = null;

            codigoBarras = MainCadastro.LerString("Digite o codigo de barras do produto que deseja editar: ");
            
            foreach(Produto p in produtos)
            {
                if(p.CodigoBarras == codigoBarras)
                {
                    prod = p;
                    existe = true;
                    break;
                }
            }

            if (!existe)
                Console.WriteLine("\n**Produto não encontrado**\n");
            else
            {
                switch (MenuEditar())
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("**Editar nome**\n");
                        prod.Nome = MainCadastro.LerString("Digite o novo nome: ");
                        break;
                    case 2:
                        do
                        {
                            prod.ValorVenda = MainCadastro.LerFloat("Digite o novo valor de venda: ");
                            if (prod.ValorVenda >= 1000)
                                Console.WriteLine("\n**Valor de venda invalido (máximo = R$ 999,99)**\n");
                            else if (prod.ValorVenda < 0.01)
                                Console.WriteLine("\n**Valor de venda invalido (minimo = R$ 0,01)**\n");
                        } while(prod.ValorVenda >= 1000 || prod.ValorVenda < 0.01);
                        break;
                    case 3:
                        prod.Situacao = prod.Situacao == 'A' ? 'I' : 'A';
                        break;
                    default:
                        return;
                }

                Console.WriteLine("\n**Atributos alterados com sucesso!**");
            }

            Console.ReadKey();
        }

        private void ImprimirProduto(Produto produto)
        {
            Console.WriteLine($"Codigo de barras: {produto.CodigoBarras}");
            Console.WriteLine($"Nome: {produto.Nome}");
            Console.WriteLine($"Valor de venda: R$ {produto.ValorVenda:000.00}");
            Console.WriteLine($"Ultima venda: {produto.UltimaVenda}");
            Console.WriteLine($"Data de cadastro: {produto.DataCadastro}");
            Console.WriteLine($"Situação: {produto.Situacao}");
        }

        private int MenuEditar()
        {
            int op;
            do
            {
                Console.Clear();
                Console.WriteLine("**Editar Produto**\n");
                Console.WriteLine("1 - Nome");
                Console.WriteLine("2 - Valor de venda");
                Console.WriteLine("3 - Situação");
                Console.WriteLine("0 - Sair");
                op = MainCadastro.LerInt("Escolha uma opção: ");
            }while(op < 0 || op > 3);
            return op;
        }
    }
}
