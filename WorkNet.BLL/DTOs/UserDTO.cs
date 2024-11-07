namespace WorkNet.BLL.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public required string Email { get; set; }

        public required string Role { get; set; }
    }
}