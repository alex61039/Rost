namespace Rost.WebApi.Models.Account
{
    public class ConfirmEmailModel
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}