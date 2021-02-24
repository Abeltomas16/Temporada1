using AcessoBancoDados.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
namespace AcessoBancoDados
{
    public class AcessoDadosSqlServer
    {
        //cria conexao
        private SqlConnection CriarConexao()
        {
            return new SqlConnection(Settings.Default.stringConexao);
        }

        //parâmetros que vão ao banco de dados
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }
        public void AdicionarParametros(string nomeParametro, object ValorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, ValorParametro));
        }

        //Persistência - Inserir, ALterar, Excluir
        public object ExecutarManipulacao(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //criar a conexão
                SqlConnection sqlConnection = CriarConexao();
                //abrir conexão
                sqlConnection.Open();
                //cria comando que vai levar a informação para o banco de dados
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //colocando as coisas dentro do comando(dentro da caixa que vai trafegar na conexao)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200;

                //Adicionar os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));

                //executar o comando
                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //Consultar registros do banco de dados
        public DataTable ExecutarConsulta(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                SqlConnection sqlConnection = CriarConexao();
                sqlConnection.Open();
                SqlCommand sqlCommand =sqlConnection.CreateCommand();
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200;
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //criar adaptador
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                //Criei Datatable onde vou colocar os dados vindo do banco
                DataTable table = new DataTable();

                //manda o comando ir até ao banco buscar os dados e prenchr o datatable 
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
