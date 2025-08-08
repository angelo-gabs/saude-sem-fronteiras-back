﻿using Dapper;
using SaudeSemFronteiras.Application.Invoices.Domain;
using SaudeSemFronteiras.Common.Factory.Interfaces;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace SaudeSemFronteiras.Application.Invoices.Repository;
public class InvoiceRepository(IDatabaseFactory LocalDatabase) : IInvoiceRepository
{
    public async Task Insert(Invoice invoice, CancellationToken cancellationToken)
    {
        var sql = @"insert into invoices(issuance_date, due_date, value, status, description, agency, account, digit, standard_wallet, patient_id, doctor_id, appointment_id) 
                    values (@IssuanceDate, @DueDate, @Value, @Status, @Description, @Agency, @Account, @Digit, @StandardWallet, @PatientId, @DoctorId, @AppointmentId)"
        ;

        var command = new CommandDefinition(sql, invoice, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
        
    }

    public async Task Update(Invoice invoice, CancellationToken cancellationToken)
    {
        var sql = @"update invoices
                       set issuance_date = @IssuanceDate,
                           due_date = @DueDate, 
                           value = @Value,
                           status = @Status,
                           description = @Description,
                           agency = @Agency,
                           account = @Account,
                           digit = @Digit,
                           standard_wallet = @StandardWallet,
                           patient_id = @PatientId,
                           doctor_id = @DoctorId,
                           appointment_id = @AppointmentId
                     where id = @Id";

        var command = new CommandDefinition(sql, invoice, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task Delete(long iD, CancellationToken cancellationToken)
    {
        var sql = @"delete from invoices
                     where id = @iD";

        var command = new CommandDefinition(sql, new { iD }, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task UpdatePatientInvoices(long Id, CancellationToken cancellationToken)
    {
        const short statusExpired = 3;

        var sql = @" UPDATE invoices
                        SET status = @statusExpired
                      WHERE patient_id = @Id
                        AND status = 1
                        AND due_date < CURRENT_DATE ";

        var command = new CommandDefinition(sql, new { statusExpired, Id }, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task UpdateDoctorInvoices(long Id, CancellationToken cancellationToken)
    {
        const short statusExpired = 3;

        var sql = @" UPDATE invoices
                        SET status = @statusExpired
                      WHERE doctor_id = @Id
                        AND status = 1
                        AND due_date < CURRENT_DATE ";

        var command = new CommandDefinition(sql, new { statusExpired, Id }, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }
}
