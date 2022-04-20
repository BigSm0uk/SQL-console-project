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
    2. Выборка - Select
    3.Изменение - Update
    4.Удаление - Delete

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
            
            while (true)
            {               
                try
                {
                    Console.Write("> ");
                    command = Console.ReadLine();

                    #region Exit
                    if (command.ToLower().Trim() == "exit")
                    {
                        if (connection.State != ConnectionState.Open)
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
                    SqlCommand sqlcommand = null;
                    string [] command_array = command.ToLower().Split(' ');

                    switch (command_array[0])
                    {
                        case "select":

                            sqlcommand = new SqlCommand(command, connection);

                            dataReader = sqlcommand.ExecuteReader();
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
                             sqlcommand = new SqlCommand(command, connection);
                            Console.WriteLine($"Добавлено: {sqlcommand.ExecuteNonQuery()} строк(а)");
                            break;
                        case "update":
                            sqlcommand = new SqlCommand(command, connection);
                            Console.WriteLine($"Изменено: {sqlcommand.ExecuteNonQuery()} строк(а)");
                            break;
                        case "delete":
                             sqlcommand = new SqlCommand(command, connection);
                            Console.WriteLine($"Удалено: {sqlcommand.ExecuteNonQuery()} строк(а)");
                            break;
                        case "sortby":
                            sqlcommand = new SqlCommand($"Select * from students order by {command_array[1]} {command_array[2]}", connection);
                            dataReader = sqlcommand.ExecuteReader();
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

                        case "search":
                            if (command_array[1].Equals("fio"))
                            {
                                sqlcommand = new SqlCommand($"Select * from Students where fio like N'%{command_array[2]}%'" , connection);
                            }
                            else if (command_array[1].Equals("birthday"))
                            {
                                sqlcommand = new SqlCommand($"Select * from Students where birthday = '{command_array[2]}'", connection);
                            }
                            else
                            {
                                Console.WriteLine($"Аргумент {command} не корректен!");
                            }
                            try
                            {
                                dataReader = sqlcommand.ExecuteReader();
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
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            finally
                            {
                                if (dataReader != null)
                                {
                                    dataReader.Close();
                                }
                            }
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
