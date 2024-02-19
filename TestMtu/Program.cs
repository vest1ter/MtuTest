using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Microsoft.EntityFrameworkCore;
using MTUBankBase.Auth.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TestMtu;

namespace starteducation
{

    public class UserManager
    {
        public static void Add(RegisterRequest user)
        {
            using (ApplicationContext db = new ApplicationContext())
            {


                db.UsersTable.Add(user);
                db.SaveChanges();

            }
        }

        public static AuthResult GetUser(string EntUserPhone)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var res = db.UsersTabel.FirstOrDefault(res => res.phone_number == EntUserPhone);
                Verification.TwoFactorAuth.Enable2FA(res.username);
                return res;
            }

        }

    }
    /*
    
    public class Checker
    {
        public static AuthResult Check(TwoFARequest TwoFACode)
        {
            
            var result = new AuthResult() { };
            return Verification.TwoFactorAuth.VerifyOTP();
        }
    }
    
    [Table("users")]
    */
    internal class Program
    {

        static void Main(string[] args)
        {


        }
        [Route(HttpVerbs.Post, "/api/registerUser")]
        public async Task<AuthResult> DescribePerson([JsonData] RegisterRequest user)
        {
            UserManager.Add(user);

            return user;
        }

        [Route(HttpVerbs.Post, "/api/loginUser")]
        public async Task<AuthResult> LoginPerson([JsonData] AuthRequest EntUserPhone)
        {
            
            return UserManager.GetUser(EntUserPhone);
        }

        [Route(HttpVerbs.Post, "/api/2FA")]
        public async Task<bool> TwoFAVerification([JsonData] TwoFARequest TwoFACode)
        {
            string x = TwoFACode.TwoFAToken;
            string y = TwoFACode.TwoFAValue;
            return Verification.TwoFactorAuth.VerifyOTP(username: x, userEnteredOTP: y);
        }
    }
}
