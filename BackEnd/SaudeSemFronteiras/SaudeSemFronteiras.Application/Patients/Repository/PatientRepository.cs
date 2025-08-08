﻿using Dapper;
using SaudeSemFronteiras.Application.Patients.Domain;
using SaudeSemFronteiras.Common.Factory.Interfaces;

namespace SaudeSemFronteiras.Application.Patients.Repository;
public class PatientRepository(IDatabaseFactory LocalDatabase) : IPatientRepository
{
    public async Task Insert(Patient patient, CancellationToken cancellationToken)
    {
        var sql = @"insert into patients(blood_type, allergies, medical_condition, previous_surgeries, medicines, emergency_number, user_id) 
                                 values (@BloodType, @Allergies, @MedicalCondition, @PreviousSurgeries, @Medicines, @EmergencyNumber, @UserId)";

        var command = new CommandDefinition(sql, patient, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task Update(Patient patient, CancellationToken cancellationToken)
    {
        var sql = @"update patients
                       set blood_type = @BloodType, 
                           allergies  = @Allergies, 
                           medical_condition = @MedicalCondition, 
                           previous_surgeries = @PreviousSurgeries, 
                           medicines = @Medicines, 
                           emergency_number = @EmergencyNumber, 
                           user_id = @UserId
                     where id = @Id";

        var command = new CommandDefinition(sql, patient, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }
}
