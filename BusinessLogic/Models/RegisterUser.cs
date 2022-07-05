namespace BusinessLogic.Models
{
    public class RegisterUser
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }



   
    }



    public class GetUsers
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
