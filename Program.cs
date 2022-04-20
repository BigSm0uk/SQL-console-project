using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Console_sqlTool_projects
{
    class Program
    {
        private static string connection_string = ConfigurationManager.ConnectionStrings["StudentsDB"].ConnectionString;
        private static SqlConnection connection = null;
        static void Main(string[] args)
        {

            connection = new SqlConnection(connection_string);
            connection.Open();
            SQL_Tools tools = new SQL_Tools();
            MessageService messageService = new MessageService();
            Console.WriteLine("hello!");

            while (true)
            {
                try
                {
                    string command = string.Empty;
                    Console.Write("> ");
                    command = Console.ReadLine();
                    string[] command_array = command.ToLower().Split(' ');

                    switch(command_array[0])
                    {
                        case "select":
                            tools.Select(command,connection);
                            break;
                        case "update":
                            tools.Update(command,connection);
                            break;
                        case "insert":
                            tools.Insert(command, connection);
                            break;
                        case "delete":
                            tools.Delete(command, connection);
                            break;
                        case "sortby":
                            tools.SortBy(command_array[1], command_array[2] , connection);
                            break;
                        case "selall":
                            tools.SelectAll(connection);
                            break;
                        case "search":
                            tools.Search(command_array[1], command_array[2], connection);
                            break;
                        default:
                            messageService.ShowMessage(command);
                            break; 

                    }
                }
                catch (Exception ex)
                {
                    tools.ErrorShow(ex.Message);
                }
            }
        }
    }
}
