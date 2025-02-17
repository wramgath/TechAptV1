// Copyright © 2025 Always Active Technologies PTY Ltd

using TechAptV1.Client.Interfaces;

namespace TechAptV1.Client.Services
{
    public class PrimeNumberService : IPrimeNumberService
    {
        public int GeneratePrimeNumbers(ref int lastPrime)
        {
            int num = lastPrime + 1;

            while(!IsPrime(num))
            {
                num++;
            }
            lastPrime = num;
            return num;
        }
        public bool IsPrime(int number)
        {
            if (number < 2)
                return false;

            if (number % 2 == 0 && number != 2)
                return false;

            for (int i = 3; i * i <= number; i += 2)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }
    }
}
