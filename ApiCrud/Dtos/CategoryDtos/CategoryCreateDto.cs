using FluentValidation;
namespace ApiCrud.Dtos;

public class CategoryCreateDto
{
    public string? Name { get; set; }

}
public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(p=>p.Name)
        .MaximumLength(50).WithMessage("50 den boyuk olmaz")
        .NotNull().WithMessage("Bosh qoymaq olmaz");
    }
}
