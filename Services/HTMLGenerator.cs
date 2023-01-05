using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Services
{
    public class HTMLGenerator
    {
        public static string GenerateHTML()
        {

            List<string> lines = new List<string>(){
                "<!DOCTYPE html>",
                "<head>",
                "<title>ATM</title>",
                "<style>",
                "</style>",
                "</head>",
                "<body>",
                "<p>This is the HTML</p>",
                "</body>",
                "</html>"
            };

            File.WriteAllLines(@"C:\Users\vytas\source\repos\CodeAcademy\ATM\ATM.html", lines);
            return string.Join("\n", lines);
        }
    }
}
