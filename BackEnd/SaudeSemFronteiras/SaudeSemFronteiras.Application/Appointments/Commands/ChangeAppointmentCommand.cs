﻿using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace SaudeSemFronteiras.Application.Appointments.Commands;
public class ChangeAppointmentCommand : IRequest<Result>
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Duration { get; set; }
    public long DoctorId { get; set; }
    public long PatientId { get; set; }

    public Result Validation()
    {
        if (Id.ToString().IsNullOrEmpty())
            return Result.Failure("Código da consulta não pode ser nulo");
        if (Date.ToString().IsNullOrEmpty())
            return Result.Failure("Horário não pode ser nulo");
        if (PatientId.ToString().IsNullOrEmpty())
            return Result.Failure("Código do paciente não pode ser nulo");

        return Result.Success();
    }
}
