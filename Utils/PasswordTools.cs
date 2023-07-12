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

            //byte[] saltBytes = BitConverter.GetBytes(user.PasswordSalt.ToCharArray());
            byte[] saltBytes = Encoding.UTF8.GetBytes(user.PasswordSalt);
            byte[] hashBytes = Encoding.UTF8.GetBytes(user.PasswordHash);

            using (var hmac = new HMACSHA512(saltBytes))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(hashBytes);
            }
        }
    }

}
