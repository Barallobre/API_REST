using API_REST.DTOs;
using API_REST.Infrastructure.Data;
using API_REST.Infrastructure.Models;
using System.Security.Cryptography;
using System.Text;

namespace API_REST.Utils
{
    public class PasswordTools
    {
        private readonly MasterContext _masterContext;

        public PasswordTools(MasterContext masterContext)
        {
            _masterContext = masterContext;
        }

        public static void CreatePassword(string password, out PasswordDTO passwordDTO)
        {
            passwordDTO = new PasswordDTO();
            using (var hmac = new HMACSHA512())
            {
                passwordDTO.PasswordSalt = hmac.Key;
                passwordDTO.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPassword(string password, string userName, List<User> allUsers)
        {
            
            User user = allUsers.FirstOrDefault(x => x.UserName == userName);

            var saltAndHash = GetPasswordSaltAndHash(user.Password);

            using (var hmac = new HMACSHA512(Encoding.ASCII.GetBytes(saltAndHash.Item1)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(Encoding.ASCII.GetBytes(saltAndHash.Item2));
            }
        }

        public static (string,string) GetPasswordSaltAndHash(string password)
        {
            string salt = string.Empty;
            string hash = string.Empty;
            string stopAt = ".";

            if (!String.IsNullOrWhiteSpace(password))
            {
                int charLocation = password.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    salt = password.Substring(0, charLocation);
                    hash = password.Substring(charLocation+1, password.Last());

                }
            }

            return (salt, hash);
        }

    }

}
