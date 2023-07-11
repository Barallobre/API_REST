namespace API_REST.DTOs
{
    public class PasswordDTO
    {
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
