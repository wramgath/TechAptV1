// Copyright © 2025 Always Active Technologies PTY Ltd

using Microsoft.EntityFrameworkCore;
using TechAptV1.Client.Data;
using TechAptV1.Client.Models;

namespace TechAptV1.Client.Services;

/// <summary>
/// Data Access Service for interfacing with the SQLite Database
/// </summary>
public sealed class DataService
{
    private readonly ILogger<DataService> _logger;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _dbContext;

    /// <summary>
    /// Default constructor providing DI Logger and Configuration
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    public DataService(ILogger<DataService> logger, IConfiguration configuration,AppDbContext dbContext)
    {
        _logger = logger;
        _configuration = configuration;
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreated();
    }

    /// <summary>
    /// Save the list of data to the SQLite Database
    /// </summary>
    /// <param name="dataList"></param>
    public async Task Save(List<Number> dataList)
    {
        try
        {
            _logger.LogInformation("Save");

            if (dataList.Count == 0)
            {
                return;
            }

            var values = string.Join(",", dataList.Select(x => $"({x.Value}, {x.IsPrime})"));
            var sql = $"INSERT INTO Numbers (Value, IsPrime) VALUES {values}";

            await _dbContext.Database.ExecuteSqlRawAsync(sql);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Save");
            throw;
        }
    }

    /// <summary>
    /// Fetch N records from the SQLite Database where N is specified by the count parameter
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public async Task<List<Number>> Get(int count)
    {
        _logger.LogInformation("Get");

        return await _dbContext.Numbers.OrderBy(x => x.Value).Take(count).ToListAsync();
    }

    /// <summary>
    /// Fetch All the records from the SQLite Database
    /// </summary>
    /// <returns></returns>
    public async Task<List<Number>> GetAll()
    {
        this._logger.LogInformation("GetAll");

        return await _dbContext.Numbers.ToListAsync();
    }
}
