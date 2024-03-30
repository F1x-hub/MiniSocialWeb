using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp46
{
    internal class User
    {
        
        public string UserName { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"UserName: {UserName} - Password: {Password}";
        }

        public User LogIn() 
        {
            User user = new User();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\User_Folder";

            Console.Write("Enter UserName: ");
            string userName = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            string filePath = Path.Combine(path, $"{userName}.txt");

            if (File.Exists(filePath))
            {
                string savedPassword = File.ReadAllText(filePath);
                if (savedPassword.Trim() == password)
                {
                    user.UserName = userName;
                    user.Password = password;
                    
                    Console.WriteLine("Login successful!" + "-" + user.UserName);
                    return user;
                }
                else
                {
                    Console.WriteLine("Incorrect password.");
                    return null; 
                }
            }
            else
            {
                Console.WriteLine("User does not exist.");
                return null;
            }


        }
        public void Registration() 
        {
            

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\User_Folder";

            DirectoryInfo dir = new DirectoryInfo(path);

            if (!dir.Exists)
            {
                dir.Create();
            }

            Console.Write("Enter UserName: ");
            string userName = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            FileInfo info = new FileInfo(dir.FullName + $@"\{userName}.txt");

            using (StreamWriter sw = new StreamWriter(info.Create())) 
            {
                sw.WriteLine(password);
            }

            

        }
    }
}
