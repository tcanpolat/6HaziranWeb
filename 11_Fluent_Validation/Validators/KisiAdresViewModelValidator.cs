using _11_Fluent_Validation.ViewModels;
using FluentValidation;

namespace _11_Fluent_Validation.Validators
{
    public class KisiAdresViewModelValidator :AbstractValidator<KisiAdresViewModel>
    {
        public KisiAdresViewModelValidator()
        {
            RuleFor(x => x.Kisi.Ad)
                .NotEmpty().WithMessage("Kişi Adı Boş geçilemez")
                .NotNull().WithMessage("Kişi Adı Null olamaz")
                .MinimumLength(1).WithMessage("Kişi adı en az 2 karakterden oluşmalıdır");
            
            RuleFor(x => x.Kisi.Soyad)
                .NotEmpty().WithMessage("Kişi Soyadı Boş geçilemez")
                .NotNull().WithMessage("Kişi Soyadı Null olamaz")
                .MinimumLength(3).WithMessage("Kişi Soyadı en az 3 karakterden oluşmalıdır");
            
            RuleFor(x => x.Kisi.Yas)
               .NotEmpty().WithMessage("Kişi Yaşı Boş geçilemez")
               .InclusiveBetween(18, 120).WithMessage("Kişi Yaşı 18-120 aralığında olmalıdır");
            
            RuleFor(x => x.Adres.Sehir)
                .NotEmpty().WithMessage("Şehir Boş geçilemez")
                .NotNull().WithMessage("Şehir Null olamaz")
                .MinimumLength(3).WithMessage("Şehir en az 3 karakterden oluşmalıdır");

            RuleFor(x => x.Adres.AdresTanim)
              .NotEmpty().WithMessage("Adres Tanım Boş geçilemez")
              .NotNull().WithMessage("Adres Tanım Null olamaz")
              .MinimumLength(10).WithMessage("Adres Tanım en az 10 karakterden oluşmalıdır");

        }
    }
}
