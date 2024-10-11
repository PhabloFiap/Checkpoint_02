using CP2.Domain.Interfaces.Dtos;
using FluentValidation;

namespace CP2.Application.Dtos
{
    public class VendedorDto : IVendedorDto
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataContratacao { get; set; }
        public decimal ComissaoPercentual { get; set; }
        public decimal MetaMensal { get; set; }


        public void Validate()
        {
            var validateResult = new VendedorDtoValidation().Validate(this);

            if (!validateResult.IsValid)
                throw new Exception(string.Join(" e ", validateResult.Errors.Select(x => x.ErrorMessage)));
        }
    }

    internal class VendedorDtoValidation : AbstractValidator<VendedorDto>
    {
        public VendedorDtoValidation()
        {
            RuleFor(x => x.Nome)
           .NotEmpty().WithMessage("Nome é obrigatório")
           .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Telefone)
          .NotEmpty().WithMessage("Telefone é obrigatório")
          .Matches(@"^\d{10,11}$").WithMessage("Telefone deve conter 10 ou 11 dígitos");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido");

            RuleFor(x => x.Endereco)
           .NotEmpty().WithMessage("Endereço é obrigatório")
           .MaximumLength(200).WithMessage("Endereço deve ter no máximo 200 caracteres");

            RuleFor(x => x.CriadoEm)
          .LessThanOrEqualTo(DateTime.Now).WithMessage("Data de criação não pode ser futura");

            RuleFor(x => x.DataNascimento)
           .NotEmpty().WithMessage("Data de nascimento é obrigatória")
           .LessThan(DateTime.Now.AddYears(-18)).WithMessage("O vendedor deve ter no mínimo 18 anos");

            RuleFor(x => x.DataContratacao)
           .NotEmpty().WithMessage("Data de contratação é obrigatória")
           .GreaterThanOrEqualTo(x => x.DataNascimento).WithMessage("A data de contratação deve ser posterior à data de nascimento");

            RuleFor(x => x.ComissaoPercentual)
           .InclusiveBetween(0, 100).WithMessage("A comissão deve estar entre 0% e 100%");

            RuleFor(x => x.MetaMensal)
            .GreaterThan(0).WithMessage("A meta mensal deve ser um valor positivo");






        }
    }
}
