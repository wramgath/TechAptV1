// Copyright © 2025 Always Active Technologies PTY Ltd

using TechAptV1.Client.Interfaces;

namespace TechAptV1.Client.Services
{
    public class OddNumberService : IOddNumberService
    {
        private readonly Random _random = new Random();
        public int GenerateOddNumbers()
        {
            return _random.Next(1, int.MaxValue) | 1;
        }
    }
}
