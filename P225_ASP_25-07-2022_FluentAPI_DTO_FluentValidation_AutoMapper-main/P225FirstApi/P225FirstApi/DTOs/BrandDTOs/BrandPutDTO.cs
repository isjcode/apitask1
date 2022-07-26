using FluentValidation;
using P225FirstApi.DTOs.BrandDTOs;
using P225FirstApi.Data;
using System.Linq;

namespace P225FirstApi.DTOs.BrandDTOs
{
    public class BrandPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class BrandPutDTOValidator : AbstractValidator<BrandPutDTO>
    {

        private readonly AppDbContext _context;

        public BrandPutDTOValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(b => b.Name).MaximumLength(255).MinimumLength(2).NotEmpty().WithMessage("Required");

            RuleFor(x => x).Custom((b, ctx) =>
            {
                if (b.Name != null && _context.Brands.Any(x => x.Name.ToLower() == b.Name.Trim().ToLower()))
                {
                    ctx.AddFailure(nameof(b.Name), "Name already exists.");
                }
            });
        }
    }
}
