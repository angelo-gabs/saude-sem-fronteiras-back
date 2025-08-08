﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaudeSemFronteiras.Application.Countries.Queries;

namespace SaudeSemFronteiras.WebApi.Controllers;
[ApiController]
[Route("[controller]")]

public class CountryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICountryQueries _countryQueries;

    public CountryController(IMediator mediator, ICountryQueries countryQueries)
    {
        _mediator = mediator;
        _countryQueries = countryQueries;
    }

    [HttpGet("{all}")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var countries = await _countryQueries.GetAll(cancellationToken);

        return Ok(countries);
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetById(long iD, CancellationToken cancellationToken)
    {
        var country = await _countryQueries.GetById(iD, cancellationToken);
        if (country == null)
            return BadRequest("País não encontrado.");

        return Ok(country);
    }

}
