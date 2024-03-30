using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp46
{
    internal class Post
    {
        public void SendPost(User user) 
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Post_Folder";
            


            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                dir.Create();
            }


            DirectoryInfo subDir = dir.CreateSubdirectory(user.UserName);

            Console.Write("Enter Post Name: ");
            string postName = Console.ReadLine();

            DirectoryInfo subSubDir = subDir.CreateSubdirectory(postName);



            FileInfo info = new FileInfo(subSubDir.FullName + $@"\{postName}.txt");

            using (StreamWriter sw = new StreamWriter(info.Create()))
            {
                sw.Write(DateTime.Now + " - ");
                Console.Write("Enter your Post: ");
                string message = Console.ReadLine();
                sw.WriteLine(message);

            }
        }
        public void SeePost(User user)
        {
            Console.Write("Enter the username whose posts you want to see: ");
            string username = Console.ReadLine();

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Post_Folder", username);

            DirectoryInfo userDir = new DirectoryInfo(path);
            if (!userDir.Exists)
            {
                Console.WriteLine("No posts found for this user.");
                return;
            }

            Console.WriteLine($"Posts for user {username}:");
            DirectoryInfo[] postDirectories = userDir.GetDirectories();
            foreach (var postDir in postDirectories)
            {
                Console.WriteLine(postDir.Name);
            }

            Console.Write("Enter the post name to view its content: ");
            string postName = Console.ReadLine();

            string postPath = Path.Combine(userDir.FullName, postName, $"{postName}.txt");
            
            if (!File.Exists(postPath))
            {
                Console.WriteLine("Post not found.");
                return;
            }

            Console.WriteLine($"Post: {postName}");
            Console.WriteLine("-------------");
            string postContent = File.ReadAllText(postPath);
            Console.WriteLine(postContent);

            string[] commentFiles = Directory.GetFiles(Path.Combine(userDir.FullName, postName), "*.txt");
            if (commentFiles.Length > 1)
            {
                bool isFirstFile = true; 
                Console.WriteLine("\nComments:");
                foreach (var commentFile in commentFiles)
                {
                    if (isFirstFile)
                    {
                        isFirstFile = false; 
                        continue; 
                    }

                    string[] comments = File.ReadAllLines(commentFile);
                    foreach (var comment in comments)
                    {
                        Console.WriteLine(comment);
                    }
                }
            }


            Console.Write("leave comments?(Y/N): ");
            string choise = Console.ReadLine();
            if (choise == "Y" || choise == "y")
            {
                string commentPath = Path.Combine(userDir.FullName, postName, $"{user.UserName}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt");
                using (StreamWriter sw = new StreamWriter(commentPath, true))
                {
                    sw.Write(DateTime.Now + " - ");
                    Console.Write("Enter your Comment: ");
                    string newComment = Console.ReadLine();
                    sw.Write(user.UserName + " - ");
                    sw.WriteLine(newComment);
                }
            }

        }
    }
}
