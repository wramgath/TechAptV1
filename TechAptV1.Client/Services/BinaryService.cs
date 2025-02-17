// Copyright © 2025 Always Active Technologies PTY Ltd

using System.Runtime.Serialization.Formatters.Binary;
using TechAptV1.Client.Interfaces;
using TechAptV1.Client.Models;

namespace TechAptV1.Client.Services
{
    public class BinaryService : IBinaryService
    {
        public Stream ConvertToBinary(List<Number> data)
        {
            var memoryStream = new MemoryStream();
            var writer = new BinaryWriter(memoryStream);

            foreach (var number in data)
            {
                writer.Write(number.Value);
                writer.Write(number.IsPrime);
            }

            writer.Flush();
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}
