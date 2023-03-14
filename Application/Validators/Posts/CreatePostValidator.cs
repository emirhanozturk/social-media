using Application.Dtos.Posts;
using FluentValidation;

namespace Application.Validators.Posts
{
    public class CreatePostValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostValidator()
        {
            RuleFor(post => post.Description)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Açıklama boş olamaz!");

            RuleFor(post => post.Description)
                .MaximumLength(500)
                .MinimumLength(2)
                    .WithMessage("Açıklama 10 karakter ve 500 karakter arasında olmalıdır.");

            RuleFor(post => post.Title)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Başlık boş olmamalıdır. Lütfen başlık giriniz.");

        }
    }
}

