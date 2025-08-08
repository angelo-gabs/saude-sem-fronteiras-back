﻿using CSharpFunctionalExtensions;
using MediatR;
using SaudeSemFronteiras.Application.Doctors.Commands;
using SaudeSemFronteiras.Application.Doctors.Domain;
using SaudeSemFronteiras.Application.Doctors.Queries;
using SaudeSemFronteiras.Application.Doctors.Repository;

namespace SaudeSemFronteiras.Application.Doctors.Handlers;
public class DoctorHandler : IRequestHandler<CreateDoctorCommand, Result>,
                             IRequestHandler<ChangeDoctorCommand, Result>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IDoctorQueries _doctorQueries;

    public DoctorHandler(IDoctorRepository doctorRepository, IDoctorQueries doctorQueries)
    {
        _doctorRepository = doctorRepository;
        _doctorQueries = doctorQueries;
    }

    public async Task<Result> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        var validationResult = request.Validation();

        if (validationResult.IsFailure)
            return validationResult;

        var doctor = Doctor.Create(request.RegistryNumber, request.InitialHour, request.FinalHour, request.ConsultationPrice, request.Days, request.UserId);

        await _doctorRepository.Insert(doctor, cancellationToken);

        return Result.Success();
    }

    public async Task<Result> Handle(ChangeDoctorCommand request, CancellationToken cancellationToken)
    {
        var validationResult = request.Validation();

        if (validationResult.IsFailure)
            return validationResult;

        var doctorDto = await _doctorQueries.GetDtoById(request.Id, cancellationToken);
        if (doctorDto == null)
            return Result.Failure("Não foi possível encontrar o médico.");

        var doctor = new Doctor(doctorDto.Id,
                                request.RegistryNumber,
                                request.InitialHour,
                                request.FinalHour,
                                request.ConsultationPrice,
                                request.Days,
                                doctorDto.UserId);

        doctor.Update(request.RegistryNumber, request.InitialHour, request.FinalHour, request.ConsultationPrice, request.Days, request.UserId);

        await _doctorRepository.Update(doctor, cancellationToken);

        return Result.Success();
    }
}
