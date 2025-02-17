// Copyright © 2025 Always Active Technologies PTY Ltd

using System.Collections.Concurrent;
using System.Threading;
using TechAptV1.Client.Interfaces;
using TechAptV1.Client.Models;

namespace TechAptV1.Client.Services;

/// <summary>
/// Default constructor providing DI Logger and Data Service
/// </summary>
/// <param name="logger"></param>
/// <param name="dataService"></param>
public sealed class ThreadingService(ILogger<ThreadingService> logger, DataService dataService, IOddNumberService _oddNumberService, IEvenNumberService _evenNumberService, IPrimeNumberService _primeNumberService)
{
    private readonly CancellationTokenSource _cancellationToken = new();

    private ConcurrentBag<Number> _numbers = new();

    private List<int> _oddNumbers = new();
    private List<int> _evenNumbers = new();
    private List<int> _primeNumbers = new();
    private List<int> _totalNumbers = new();

    public List<int> GetOddNumbers() => _oddNumbers;
    public List<int> GetEvenNumbers() => _evenNumbers;
    public List<int> GetPrimeNumbers() => _primeNumbers;
    public List<int> GetTotalNumbers() => _totalNumbers;

    /// <summary>
    /// Start the random number generation process
    /// </summary>
    public async Task Start()
    {
        _oddNumbers.Clear();
        _evenNumbers.Clear();
        _primeNumbers.Clear();
        _totalNumbers.Clear();

        logger.LogInformation("Start");

        var oddNumbers = Task.Run(GenerateOddNumbers, _cancellationToken.Token);
        var evenNumbers = Task.Run(GenerateEvenNumbers, _cancellationToken.Token);
        var primeNumbers = Task.Run(GeneratePrimeNumbers, _cancellationToken.Token);

        await Task.WhenAll(oddNumbers, primeNumbers);

    }

    /// <summary>
    /// Persist the results to the SQLite database
    /// </summary>
    public async Task Save()
    {
        logger.LogInformation("Save");

        var prime = new HashSet<int>(_primeNumbers);

        Parallel.ForEach(_totalNumbers, x =>
        {
            _numbers.Add(new Number
            {
                Value = x,
                IsPrime = prime.Contains(x) ? 1 : 0
            });
        });

        List<Number> sortedList = new();

        sortedList = _numbers.OrderBy(x => x.Value).ToList();

        await dataService.Save(sortedList);
    }


    private void GenerateOddNumbers()
    {
        while (!_cancellationToken.Token.IsCancellationRequested)
        {
            int value = _oddNumberService.GenerateOddNumbers();

            lock (_oddNumbers)
            {
                _oddNumbers.Add(value);
            }

            lock (_totalNumbers)
            {
                if (_totalNumbers.Count >= 10_000_000) break;

                _totalNumbers.Add(value);
            }
        }
    }

    private void GenerateEvenNumbers()
    {
        while (!_cancellationToken.Token.IsCancellationRequested)
        {

            if (_totalNumbers.Count < 2_500_000)
            {
                Task.Delay(100).Wait();
                continue;
            }

            if (_totalNumbers.Count >= 10_000_000) break;

            int value = _evenNumberService.GenerateEvenNumbers();

            lock (_evenNumbers)
            {
                _evenNumbers.Add(value);
            }

            lock (_totalNumbers)
            {
                if (_totalNumbers.Count >= 10_000_000) break;

                _totalNumbers.Add(value);
            }
        }
    }

    private void GeneratePrimeNumbers()
    {
        int lastPrime = 2;

        while (!_cancellationToken.Token.IsCancellationRequested)
        {

            int primeNumber = _primeNumberService.GeneratePrimeNumbers(ref lastPrime);

            lock (_primeNumbers)
            {
                _primeNumbers.Add(primeNumber);
            }

            lock (_totalNumbers)
            {
                if (_totalNumbers.Count >= 10_000_000) break;

                _totalNumbers.Add(primeNumber);
            }
        }
    }

}
