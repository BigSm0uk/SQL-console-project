using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

#region User Manual
/* 
    1. Выход - exit

 */
#endregion

namespace StudentAPP
{
    internal class Program        
    {
        private static string connection_string = ConfigurationManager.ConnectionStrings["StudentsDB"].ConnectionString;
        private static SqlConnection connection = null;
        static void Main(string[] args)
        {
            connection = new SqlConnection(connection_string);
            connection.Open();
            Console.WriteLine("Hello");
            SqlDataReader dataReader = null;
            string command = string.Empty;

            while(true)
            {
                Console.Write("> ");
                command = Console.ReadLine();

                #region Exit
                if (command.ToLower().Trim() == "exit")
                {
                    if(connection.State != ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                    break;
                }
                #endregion
                try
                {
                    SqlCommand sql = new SqlCommand(command, connection);

                    switch (command.Split(' ')[0].ToLower())
                    {
                        case "select":
                            dataReader = sql.ExecuteReader();
                            while (dataReader.Read())
                            {
                                Console.WriteLine($"{dataReader["Id"]} {dataReader["FIO"]} {dataReader["Birthday"]} {dataReader["University"]} " +
                                    $"{dataReader["Group_number"]} {dataReader["Course"]} {dataReader["AVG_score"]}");
                                Console.WriteLine(new string('-', 60));
                            }
                            if (dataReader != null)
                            {
                                dataReader.Close();
                            }

                            break;
                        case "insert":

                            Console.WriteLine($"Добавлено: {sql.ExecuteNonQuery()} строк(а)");


                            break;
                        case "update":

                            Console.WriteLine($"Изменено: {sql.ExecuteNonQuery()} строк(а)");

                            break;



                        case "delete":

                            Console.WriteLine($"Удалено: {sql.ExecuteNonQuery()} строк(а)");
                            break;
                        default:
                            Console.WriteLine($"Комманда {command} некорректна!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
