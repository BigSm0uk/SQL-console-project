using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_sqlTool_projects
{
    interface IMessageService
    {
        void ShowMessage(string message);
    }
    public class MessageService : IMessageService
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine($"Комманда {message} некорректна!");
        }
    }
}
