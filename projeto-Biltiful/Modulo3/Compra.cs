using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;


//Contrutor();
//Contrutor(atributos);
//ter um for que limita 3 itens por compra;
//Lista<ItemCompra>
//verificação do cnpj(se não ta nos bloqueados) e com menos de 6 meses de abertura;
//imprimir;
//Arquivar;

namespace projeto_Biltiful.Modulo3
{
    internal class Compra
    {
        int Id { get; set; }
        DateOnly DataCompra { get; set; }
        string Fornecedor { get; set; } 
        int ValorTotal { get; set; }

        public Compra()
        {
            //Nào entendi porque o construtor vazio antes do contrutor atributos(Falar com a Ana amanhã)
        }
       
        public Compra(int id, DateOnly dataCompra, string fornecedor, int valorTotal)
       
        {
            Id = id;
            DataCompra = dataCompra;
            Fornecedor = fornecedor;
            ValorTotal = valorTotal;
        }


        //cadastra compra
        public void CadastrarCompra()
        {
             //Verificar:
             //- Não permitir id de compra iguais
             //- Verificar se não atingiu o limite de valor

        }

        //localiza compra 
        public void LocalizarCompra(int idCompra)
        {
                //Um registro específico.
        }

        // Exclui compra
        public void ExcluirCompra(int idCompra)
        {
            //Apaga permanentemente o registro de uma venda(a venda e TODOS seus itens respectivos)
        }

        // Imprimir
        public void ImpressaoPorRegistro()
        {
            //Impressão por Registro(Onde o usuário poderá “navegar” pelos registros cadastrados,
            //podendo ir para o próximo ou anterior e, também podendo ir para as extremidades (início e final da listagem).
        }
    }

}

