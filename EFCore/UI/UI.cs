using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore.Model;

namespace EFCore.UI
{
    public class Ui
    {
        public string GetCommand()
        {
            Console.WriteLine("Enter command: ");
            string command = "";
            command += Console.ReadLine();
            return command;
        }

        public void DisplayHelp()
        {
            Display("Queries: \n");
            Display("[f, facilities] : display all facility names and GPS coordinates\n");
            Display("[f ordered] : display facilities ordered by type\n");
            Display("[r, reservations] : display all reservations with facility name, user name and timeslot \n");
            Display("[r p, reservations participants] : display all reservations with Participants included \n");
            Display("[m, maintenance] : display history of all interventions");


            Display("\nAdditional commands:\n");
            //Display("[s, show] : show database tables\n");
            Display("[h, help] : show this message\n");
            Display("[q, quit] : quit application\n");
        }

        public void Display(string text)
        {
            Console.Write(text);
        }

        public string? GetTable()
        {
            Display("Enter table name: \n");
            string? table = Console.ReadLine();
            return table;
        }

        public string GetLine(string text = "")
        {
            Console.WriteLine(text);
            string line = Console.ReadLine();
            return line;
        }
    }
}
