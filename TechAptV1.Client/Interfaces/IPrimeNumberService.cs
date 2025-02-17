// Copyright © 2025 Always Active Technologies PTY Ltd

namespace TechAptV1.Client.Interfaces
{
    public interface IPrimeNumberService
    {
        int GeneratePrimeNumbers(ref int lastPrime);
        bool IsPrime(int number);
    }
}
