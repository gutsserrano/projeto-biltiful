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
        }

        public List<Cliente> Recuperar(string caminho, string arquivo)
        {
            // TODO
            return null;
        }

        public void Salvar(List<Cliente> clientes)
        {
            // TODO
        }

        public void Cadastrar()
        {
            // Recebe os dados dos clientes e adiciona eles na lista
        }

        public void Editar()
        {
            // Edita um Cliente específico
        }
    }
}
