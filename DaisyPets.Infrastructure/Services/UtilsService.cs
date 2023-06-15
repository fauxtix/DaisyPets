using DaisyPets.Core.Application.Interfaces.DapperContext;
using DaisyPets.Core.Domain;
using DaisyPets.Infrastructure.Repositories;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaisyPets.Infrastructure.Services
{
    public class UtilsService
    {
        private readonly IDapperContext _context;
        private readonly ILogger<UtilsService> _logger;

        public UtilsService(IDapperContext context, ILogger<UtilsService> logger)
        {
            _context = context;
            _logger = logger;
        }


    }
}
