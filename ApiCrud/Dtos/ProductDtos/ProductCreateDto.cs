using FluentValidation;
namespace ApiCrud.Dtos;

public class ProductCreateDto
{
    public string? Name { get; set; }
    public double Price { get; set; }
    public double DiscountPrice { get; set; }
    public bool IsActive { get; set; }
}
public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RuleFor(p=>p.Name)
        .MaximumLength(50).WithMessage("50 den boyuk olmaz")
        .NotNull().WithMessage("Bosh qoymaq olmaz");
    }
}
