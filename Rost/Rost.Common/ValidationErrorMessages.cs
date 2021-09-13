namespace Rost.Common
{
    public static class ValidationErrorMessages
    {
        public const string AddressIsNullOrEmpty = "Не указан арес";
        public const string BirthdateIsNullOrEmpty = "Не указана дата рождения";
        public const string CityIsNullOrEmpty = "Не указан город";
        public const string DistrictIsNullOrEmpty = "Не указан район";
        public const string EmailIsEmptyOrInvalid = "Проверьте правильность ввода электронной почты";
        public const string ImageIsNullOrEmpty = "Не найдено изображение";
        public const string MunicipalIsNullOrEmpty = "Не указан муниципальный округ";
        public const string NameIsNullOrEmpty = "Не указано Имя";
        public const string SexIsEmptyOrInvalid = "Не указан пол";
        public const string SurnameIsNullOrEmpty = "Не указана фамилия";
        public const string PasswordOldIsNullOrEmpty = "Не указан старый пароль";
        public const string PasswordNewIsNullOrEmpty = "Не указан новый пароль";
        public const string PasswordNewConfirmIsNullOrEmpty = "Не указан пароль для подтверждения";
        public const string PasswordsIsNotEqual = "Пароли не совпадают";
        public const string PhoneIsNullOrEmpty = "Не указан телефон";
        public const string UserIdIsNullOrEmpty = "Не указан обязательный параметр UserId";
        
        
        public static string UserIdIsNotExists(string userId) => $"Не найден пользователь с Id = {userId}";
        public static string UserEmailIsNotExists(string email) => $"Не найден пользователь с Email = {email}";
        public static string ChildWithNameAlreadyExists(string name) => $"Ребенок с именем {name} уже зарегистрирован";
        public static string ChildWithIdNotFound(int id) => $"Профиль ребенка с Id = {id} не найден";

        public static string NotFoundWithParameter(string field, string parameter, string value) => $"Не найден {field} с {parameter} = {value}";
        public static string FieldIsRequired(string field) => $"Поле {field} обязательно для заполнения";
        public static string FieldShouldBeLessThan(string field, int size) => $"Поле {field} должно быть не более {size} символов";
    }
}