﻿using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace SaudeSemFronteiras.Application.Users.Commands;
public class ChangeUserCommand : IRequest<Result>
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string MotherName { get; set; } = string.Empty;
    public DateTime DateBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Phone {  get; set; } = string.Empty;

    public Result Validation()
    {
        if (Id.ToString().IsNullOrEmpty())
            return Result.Failure("Código do usuário não pode ser nulo");
        if (string.IsNullOrEmpty(Name))
            return Result.Failure("Nome não pode ser nulo");
        if (string.IsNullOrEmpty(CPF))
            return Result.Failure("CPF não pode ser nulo");
        if (string.IsNullOrEmpty(MotherName))
            return Result.Failure("Nome da mãe não pode ser nulo");
        if (string.IsNullOrEmpty(Gender))
            return Result.Failure("Genêro não pode ser nulo");
        if (string.IsNullOrEmpty(Language))
            return Result.Failure("Linguagem não pode ser nulo");
        if (string.IsNullOrEmpty(Phone))
            return Result.Failure("Telefone não pode ser nulo");

        return Result.Success();
    }
}
