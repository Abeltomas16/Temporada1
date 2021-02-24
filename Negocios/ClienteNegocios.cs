using AcessoBancoDados;
using ObjectoDeTransferencia;
using System;
using System.Data;

namespace Negocios
{
    public class ClienteNegocios
    {
        AcessoDadosSqlServer AcessoDados = new AcessoDadosSqlServer();

        public string Inserir(Cliente cliente)
        {
            try
            {
                AcessoDados.LimparParametros();
                AcessoDados.AdicionarParametros("@Nome", cliente.Nome);
                AcessoDados.AdicionarParametros("@DataNascimento", cliente.Datanascimento);
                AcessoDados.AdicionarParametros("@Sexo", cliente.Sexo);
                AcessoDados.AdicionarParametros("@LimiteCompra", cliente.LimiteCompra);
                string Idcliente = AcessoDados.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteInserir").ToString();
                return Idcliente;
            }
            catch (Exception erro)
            {
                return erro.Message;
            }
        }

        public string Alterar(Cliente cliente)
        {
            try
            {
                AcessoDados.LimparParametros();
                AcessoDados.AdicionarParametros("@IdCliente", cliente.IdCliente);
                AcessoDados.AdicionarParametros("@Nome", cliente.Nome);
                AcessoDados.AdicionarParametros("@DataNascimento", cliente.Datanascimento);
                AcessoDados.AdicionarParametros("@Sexo", cliente.Sexo);
                AcessoDados.AdicionarParametros("@LimiteCompra", cliente.LimiteCompra);
                string Idcliente = AcessoDados.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteAlterar").ToString();
                return Idcliente;
            }
            catch (Exception erro)
            {
                return erro.Message;
            }
        }

        public string Excluir(Cliente cliente)
        {
            try
            {
                AcessoDados.LimparParametros();
                AcessoDados.AdicionarParametros("@IdCliente", cliente.IdCliente);
                string Idcliente = AcessoDados.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteExcluir").ToString();
                return Idcliente;
            }
            catch (Exception erro)
            {
                return erro.Message;
            }
        }

        public ClienteColecao PesuisaId(int idCliente)
        {
            try
            {
                ClienteColecao clientes = new ClienteColecao();
                AcessoDados.LimparParametros();
                AcessoDados.AdicionarParametros("@IdCliente", idCliente);
                DataTable dataTableCliente = AcessoDados.ExecutarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorId");

                foreach (DataRow row in dataTableCliente.Rows)
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = Convert.ToInt32(row["idCliente"]);
                    cliente.Nome = Convert.ToString(row["Nome"]);
                    cliente.Datanascimento = Convert.ToDateTime(row["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(row["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(row["LimiteCompra"]);

                    clientes.Add(cliente);
                }

                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível consultar o cliente por ID. Detalhes: " + ex.Message);
            }

        }

        public ClienteColecao PesquisaPorNome(string clienteNome)
        {
            try
            {
                ClienteColecao cliente_colecao = new ClienteColecao();

                AcessoDados.LimparParametros();
                AcessoDados.AdicionarParametros("@Nome", clienteNome);
                DataTable dataTableCliente = AcessoDados.ExecutarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorNome");

                foreach (DataRow row in dataTableCliente.Rows)
                {
                    Cliente cliente = new Cliente();

                    cliente.IdCliente = Convert.ToInt32(row["idCliente"]);
                    cliente.Nome = Convert.ToString(row["Nome"]);
                    cliente.Datanascimento = Convert.ToDateTime(row["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(row["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(row["LimiteCompra"]);

                    cliente_colecao.Add(cliente);
                }

                return cliente_colecao;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel consultar cliente por Nome. Detalhes: " + ex.Message);
            }

        }
    }
}
