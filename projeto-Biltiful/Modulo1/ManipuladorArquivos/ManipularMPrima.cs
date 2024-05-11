using projeto_Biltiful.Modulo1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_Biltiful.Modulo1.ManipuladorArquivos
{
    internal class ManipularMPrima
    {
        string Caminho { get; set; }
        string Arquivo { get; set; }

        public ManipularMPrima(string caminho, string arquivo)
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

        public List<MPrima> Recuperar()
        {
            List<MPrima> materiasPrimas = new();

            foreach (string linha in File.ReadAllLines(Caminho + Arquivo))
                materiasPrimas.Add(new MPrima(linha));

            return materiasPrimas;
        }

        public void Salvar(List<MPrima> materiasPrimas)
        {
            materiasPrimas.Sort((m1, m2) => m1.Id.CompareTo(m2.Id));
            var sw = new StreamWriter(Caminho + Arquivo);

            foreach (MPrima materiaPrima in materiasPrimas)
            {
                string texto = materiaPrima.FormatarParaArquivo();
                sw.WriteLine(texto);
            }

            sw.Close();
        }

        public void Cadastrar()
        {
            List<MPrima> materiasPrimas = Recuperar();

            string id;
            int numID;
            string nome;

            Console.Clear();
            Console.WriteLine("**Cadastrar Matéria-prima**");
            nome = MainCadastro.LerString("Digite o nome da matéria-prima: ");

            if(materiasPrimas.Count == 0)
            {
                id = "MP0001";
            }
            else
            {
                numID = int.Parse(materiasPrimas.Last().Id.Substring(2, 4)) + 1;
                id = "MP" + numID.ToString("0000");
            }
            
            materiasPrimas.Add(new MPrima(id, nome));
            Salvar(materiasPrimas);

            Console.WriteLine("\n**Materia-prima cadastrada com sucesso!**\n");
            Console.ReadKey();
        }

        public void NavegarListaMPrimas()
        {
            List<MPrima> materiasPrimas = Recuperar();

            int currentIndex = 0;
            int increment = 1;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine("=================\n");
                ImprimirMPrima(materiasPrimas[currentIndex]);
                Console.WriteLine();

                Console.WriteLine("Pressione 'N' para navegar para a próxima matéria-prima, 'V' para voltar ou 'S' para sair.");

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.N)
                {
                    currentIndex = (currentIndex + increment + materiasPrimas.Count) % materiasPrimas.Count;
                }
                else if (key == ConsoleKey.V)
                {
                    currentIndex = (currentIndex - increment + materiasPrimas.Count) % materiasPrimas.Count;
                }
            } while (key != ConsoleKey.S);
        }

        public void Localizar()
        {
            List<MPrima> materiasPrimas = Recuperar();

            string id;
            bool existe = false;

            id = MainCadastro.LerString("Digite o ID da matéria-prima: ");

            foreach (MPrima materiaPrima in materiasPrimas)
            {
                if (materiaPrima.Id == id)
                {
                    Console.Clear();
                    Console.WriteLine("**Matéria-prima encontrada**\n");
                    ImprimirMPrima(materiaPrima);
                    existe = true;
                    break;
                }
            }

            if (!existe)
                Console.WriteLine("\n**Matéria-prima não encontrada**\n");

            Console.ReadKey();
        }

        public void Editar()
        {
            List<MPrima> materiasPrimas = Recuperar();

            string id;
            bool existe = false;

            MPrima materiaPrima = null;

            id = MainCadastro.LerString("Digite o ID da matéria-prima: ");

            foreach (MPrima materia in materiasPrimas)
            {
                if (id.Equals(materia.Id))
                {
                    materiaPrima = materia;
                    existe = true;
                    break;
                }
            }

            if (!existe)
                Console.WriteLine("\n**Matéria-prima não encontrada**\n");
            else
            {
                switch (MenuEditar())
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("**Editar nome**\n");
                        materiaPrima.Nome = MainCadastro.LerString("Digite o novo nome: ");
                        break;
                    case 2:
                        materiaPrima.Situacao = materiaPrima.Situacao == 'A' ? 'I' : 'A';
                        break;
                    default:
                        return;
                }

                Console.WriteLine("\n**Atributos alterados com sucesso!**");
            }

            Salvar(materiasPrimas);
            Console.ReadKey();
        }

        private void ImprimirMPrima(MPrima materiaPrima)
        {
            Console.WriteLine($"ID: {materiaPrima.Id}");
            Console.WriteLine($"Nome: {materiaPrima.Nome}");
            Console.WriteLine($"Ultima compra: {materiaPrima.UltimaCompra}");
            Console.WriteLine($"Data de cadastro: {materiaPrima.DataCadastro}");
            Console.WriteLine($"Situação: {materiaPrima.Situacao}");
        }

        public static int MenuEditar()
        {
            int op;
            do
            {
                Console.Clear();
                Console.WriteLine("**Editar Matéria-prima**\n");
                Console.WriteLine("1 - Editar nome");
                Console.WriteLine("2 - Editar situação");
                Console.WriteLine("0 - Sair");
                op = MainCadastro.LerInt("Digite a opção desejada: ");
            } while (op < 0 || op > 2);

            return op;
        }
    }
}
