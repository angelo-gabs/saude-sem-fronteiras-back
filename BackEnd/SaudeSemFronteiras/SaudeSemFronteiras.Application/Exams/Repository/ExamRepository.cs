﻿using Dapper;
using SaudeSemFronteiras.Application.Exams.Domain;
using SaudeSemFronteiras.Common.Factory.Interfaces;

namespace SaudeSemFronteiras.Application.Exams.Repository;
public class ExamRepository(IDatabaseFactory LocalDatabase) : IExamRepository
{
    public async Task Insert(Exam exam, CancellationToken cancellationToken)
    {
        var sql = @"insert into exams(description, justification, date_exam, local_exam, results, comments, document_id) 
                              values (@Description, @Justification, @DateExam, @LocalExam, @Results, @Comments, @DocumentId)";
        var command = new CommandDefinition(sql, exam, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task Update(Exam exam, CancellationToken cancellationToken)
    {
        var sql = @"update exams
                       set description = @Description, 
                           justification = @Justification,
                           date_exam = @DateExam,
                           local_exam = @LocalExam,
                           results = @Results,
                           comments = @Comments,
                           document_id = @DocumentId
                     where id = @Id";

        var command = new CommandDefinition(sql, exam, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task Delete(long iD, CancellationToken cancellationToken)
    {
        var sql = @"delete from exams
                     where exams.document_id = @iD";

        var command = new CommandDefinition(sql, new { iD }, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }
}
