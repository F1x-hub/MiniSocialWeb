using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp46
{
    internal class Message
    {
        public void SendMessage(User user)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Message_Folder";
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\User_Folder";

            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                dir.Create();
            }

            Console.Write("Enter the recipient's UserName: ");
            string recipientUserName = Console.ReadLine();

            if (recipientUserName == user.UserName)
            {
                Console.WriteLine("You can't send a message to yourself.");
                return;
            }

            string recipientFilePath = Path.Combine(pathUser, $"{recipientUserName}.txt");
            if (!File.Exists(recipientFilePath))
            {
                Console.WriteLine("Recipient user does not exist.");
                return;
            }

            string conversationFolder = GetConversationFolder(user.UserName, recipientUserName, path);

            using (StreamWriter sw = new StreamWriter(conversationFolder, true))
            {
                sw.Write($"{DateTime.Now} - {user.UserName} - ");
                Console.Write("Enter your message: ");
                string message = Console.ReadLine();
                sw.WriteLine(message);
            }
        
        }
        public void SeeMessage(User user)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Message_Folder";
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\User_Folder";

            Console.Write("Enter the sender's UserName: ");
            string senderUserName = Console.ReadLine();

            if (senderUserName == user.UserName)
            {
                Console.WriteLine("You can't view messages from yourself.");
                return;
            }

            string senderFilePath = Path.Combine(pathUser, $"{senderUserName}.txt");
            if (!File.Exists(senderFilePath))
            {
                Console.WriteLine("Sender user does not exist.");
                return;
            }

            string conversationFolder = GetConversationFolder(senderUserName, user.UserName, path);

            if (File.Exists(conversationFolder))
            {
                string[] messages = File.ReadAllLines(conversationFolder);
                foreach (var message in messages)
                {
                    Console.WriteLine(message);
                }
            }
            else
            {
                Console.WriteLine("No messages found.");
            }


        }
        private string GetConversationFolder(string user1, string user2, string basePath)
        {
            string conversationFolder1 = Path.Combine(basePath, $"{user1}_{user2}.txt");
            string conversationFolder2 = Path.Combine(basePath, $"{user2}_{user1}.txt");

            if (File.Exists(conversationFolder1))
            {
                return conversationFolder1;
            }
            else if (File.Exists(conversationFolder2))
            {
                return conversationFolder2;
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(conversationFolder1));
                return conversationFolder1;
            }
        }


    }
}
