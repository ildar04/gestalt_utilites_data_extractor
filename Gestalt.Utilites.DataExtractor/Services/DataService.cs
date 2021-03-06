﻿using System.IO;
using System.Threading.Tasks;
using Gestalt.Common.Exceptions;
using Gestalt.Common.Models;
using Gestalt.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Gestalt.Utilites.DataExtractor.Services
{
    public class DataService : IDataService
    {
        private readonly ILogger<DataService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPopularControl _popularControl;

        public DataService(
            ILogger<DataService> logger,
            IConfiguration configuration,
            IPopularControl popularControl)
        {
            _logger = logger;
            _configuration = configuration;
            _popularControl = popularControl;
        }

        public async Task WriteToFileFromApi()
        {
            await _popularControl.ExtractData();

            _logger.LogInformation("Data saved successfully.");
        }

        public async Task ReadFromFile()
        {
            var jsonData = await File.ReadAllTextAsync(_configuration["FilesPath"]
                                                       ?? throw new NoConfigurationValueException("FilesPath"));
            var result = JsonConvert.DeserializeObject<RequestList>(jsonData);
            _logger.LogInformation(JsonConvert.ToString(result));
        }
    }
}