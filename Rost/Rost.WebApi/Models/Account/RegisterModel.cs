using System.ComponentModel.DataAnnotations;

namespace Rost.WebApi.Models.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]  
        public string Username { get; set; }  
  
        [EmailAddress]  
        [Required(ErrorMessage = "Email is required")]  
        public string Email { get; set; }  
  
        [Required(ErrorMessage = "Password is required")]  
        public string Password { get; set; }  
        
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Required(ErrorMessage = "Password is required")]  
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessage = "Имя обязательное поле")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Фамилия обязательное поле")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Phone is required")]  
        public string Phone { get; set; }
        
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int MunicipalUnionId { get; set; }
    }
}