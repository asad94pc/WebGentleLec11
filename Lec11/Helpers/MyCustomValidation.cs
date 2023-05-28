using System.ComponentModel.DataAnnotations;

namespace Lec11.Helpers
{
    public class MyCustomValidation : ValidationAttribute
    {
        public MyCustomValidation(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string bookName = value.ToString();
                if (bookName.Contains(Text))
                {
                    return ValidationResult.Success;
                }
            }


            return new ValidationResult(ErrorMessage?? $"Book name does not containe {Text}");
        }
    }
}
