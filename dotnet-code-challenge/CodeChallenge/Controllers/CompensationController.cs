﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }
        [Route("")]
        [HttpPost]
        public IActionResult CreateCompensation(Compensation compensation)
        {

            _logger.LogDebug($"'{ compensation.Employee.EmployeeId}'");
            var emp = compensation.Employee;
            compensation.CompensationId = Guid.NewGuid().ToString();
            _compensationService.Create(compensation);
            return CreatedAtRoute("getCompensationById", new { id = emp.EmployeeId }, compensation);
        }

        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(string id)
        {
            _logger.LogDebug($"'{id}'");
            Compensation compensation = _compensationService.GetById(id);
            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }

        

    }
}

