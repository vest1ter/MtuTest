using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace starteducation
{

    internal class Program
    {
        public class UserManager
        {
            public static void Add(User user)
            {
                using (ApplicationContext db = new ApplicationContext())
                {


                    db.Users.Add(user);
                    db.SaveChanges();

                    return user;
                }
            }
        }
        class User
        {
            public string Name { get; set; }
            public string Secondname { get; set; }
            public int CardNumber { get; set; }

        }
        static void Main(string[] args)
        {


        }
        [Route(HttpVerbs.Post, "/SignUp")]
        public async Task<User> DescribePerson([JsonData] User user)
        {
            UserManager.Add(user);

            return user;
        }
    }
}
