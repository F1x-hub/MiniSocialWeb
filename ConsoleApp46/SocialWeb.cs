using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp46
{
    internal class SocialWeb
    {
        public void SocialMenu()
        {
            User user = new User();
            string control = "";
            while (control != "3")
            {
                Console.WriteLine("1. LogIn\n2. Registration\n3. Exit");

                control = Console.ReadLine();

                if (control == "1") 
                {
                    MyPage();
                }
                if (control == "2") 
                {
                    user.Registration();
                }

            }
        }
        public void MyPage() 
        {
            User user = new User();
            Message message = new Message();
            Post post = new Post();
            var find = user.LogIn();

            if (find != null)
            {
                string control = "";

                while (control != "5")
                {
                    Console.WriteLine("1. Send Message\n2. See your Message\n3. Send Post\n4. See Post\n5. Back");

                    control = Console.ReadLine();

                    if (control == "1") 
                    {
                        message.SendMessage(find);
                    }
                    if (control == "2") 
                    {
                        message.SeeMessage(find);
                    }
                    if (control == "3")
                    {
                        post.SendPost(find);
                    }
                    if (control == "4")
                    {
                        post.SeePost(find);
                    }
                }
            }
        }
    }
}
