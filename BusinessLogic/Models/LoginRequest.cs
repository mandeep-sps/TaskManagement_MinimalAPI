namespace BusinessLogic.Models
{
    public class LoginRequest
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }


    public class LoginResponse
    {

        public string Email { get; set; }
        public int Id { get; set; }

        public string UserRole { get; set; }

        public string Token { get; set; }

        public string Name { get; set; }


    }
}
