namespace WorkNet.BLL.SecurityServices
{
    public static class HashingService
    {
        public static string? HashPassword(string password)
        {
            if (password == null)
            {
                return null;
            }
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
