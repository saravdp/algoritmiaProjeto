using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    class user
    {
        public string tipoUser;
        public string nome;
        public string email;
        public user()
        {

        }
        public user(string nome)
        {
            this.nome = nome;
            String line;
            StreamReader sr = new StreamReader("Ficheiros de Texto/utilizadores.txt");
            //Read the first line of text
            line = sr.ReadLine();

            //Continue to read until you reach end of file
            while (line != null)
            {

                char delimiters = ';';
                string[] parts = line.Split(delimiters);

                if (parts[1] == nome)
                {
                    tipoUser = parts[4];
                    email = parts[2];
                    break;
                }
                line = sr.ReadLine();
            }
        }
    }
}
