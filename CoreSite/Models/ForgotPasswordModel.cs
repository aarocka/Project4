namespace CoreSite.Models
{
    public class ForgotPasswordModel
    {
        public string UserName { get; set; }
        public string ResetQuestion { get; set; }
        public string ResetAnswer { get; set; }
    }
}
