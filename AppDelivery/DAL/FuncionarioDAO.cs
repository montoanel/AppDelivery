// Arquivo: AppDelivery/DAL/FuncionarioDAO.cs
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace AppDelivery.DAL
{
    public class FuncionarioDAO
    {
        public List<Funcionario> BuscarAtendentesAtivos()
        {
            List<Funcionario> lista = new List<Funcionario>();

            // Atualizamos a query para incluir o status
            string query = @"
                SELECT 
                    id_atendente, nome, status 
                FROM 
                    tb_funcionarios 
                WHERE 
                    status = 'A' 
                ORDER BY 
                    nome";

            using (SqlConnection con = Conexao.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Funcionario func = new Funcionario
                            {
                                IdFuncionario = Convert.ToInt32(reader["id_funcionario"]),
                                Nome = reader["nome"].ToString(),
                                // Convertemos o string(1) do banco para char
                                StatusFuncionario = Convert.ToChar(reader["status"].ToString())
                            };
                            lista.Add(func);
                        }
                    }
                }
            }
            return lista;
        }
    }
}