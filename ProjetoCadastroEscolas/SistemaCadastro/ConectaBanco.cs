using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SistemaCadastro
{
    internal class ConectaBanco
    {
        MySqlConnection conexao = new MySqlConnection("server=localhost;port=3306;user id=root;password=123456789;database=banco_siscadastro");
        public string mensagem;


        public bool insereEscola(Escola novaEscola)
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd =
                    new MySqlCommand("sp_insereEscola", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nome", novaEscola.Nome);
                cmd.Parameters.AddWithValue("alunos", novaEscola.Alunos);
                cmd.Parameters.AddWithValue("dataHora", novaEscola.DataHora);
                cmd.Parameters.AddWithValue("oficinas", novaEscola.Oficinas);
                cmd.ExecuteNonQuery();//executar no banco
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }

        }// fim do insereEscola

        public DataTable listaOficinas()
        {
            // comentario
            MySqlCommand cmd = new MySqlCommand("sp_listaOficinas", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conexao.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable tabela = new DataTable();
                da.Fill(tabela);
                return tabela;
            }// fim try
            catch (MySqlException e)
            {
                mensagem = "Erro:" + e.Message;
                return null;
            }
            finally
            {
                conexao.Close();
            }

        }// fim lista_generos 

        public DataTable listaEscolas()
        {
            MySqlCommand cmd = new MySqlCommand("sp_listaEscolas", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conexao.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable tabela = new DataTable();
                da.Fill(tabela);
                return tabela;
            }// fim try
            catch (MySqlException e)
            {
                mensagem = "Erro:" + e.Message;
                return null;
            }
            finally
            {
                conexao.Close();
            }

        }// fim lista_Esscolas

        public bool deletaEscola(int idRemoveescola)
        {
            MySqlCommand cmd = new MySqlCommand("sp_removeEscola", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idescolas", idRemoveescola );
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery(); // executa o comando
                return true;
            }
            catch (MySqlException e)
            {
                mensagem = "Erro:" + e.Message;
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }// fim deletaEscola

        public bool alteraEscola(Escola e , int idescola)
        {
            MySqlCommand cmd = new MySqlCommand("update_escola", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idescolas", Idescolas);
            cmd.Parameters.AddWithValue("nome", e.Nome);
            cmd.Parameters.AddWithValue("alunos", e.Alunos);
            cmd.Parameters.AddWithValue("dataHora", e.DataHora);
            cmd.Parameters.AddWithValue("oficinas", e.Oficinas);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery(); // executa o comando
                return true;
            }
            catch (MySqlException e)
            {
                mensagem = "Erro:" + e.Message;
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }// fim alteraEscola

    }//fim class

}
