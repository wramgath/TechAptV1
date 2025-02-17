// Copyright © 2025 Always Active Technologies PTY Ltd

using TechAptV1.Client.Models;

namespace TechAptV1.Client.Interfaces
{
    public interface IBinaryService
    {
        Stream ConvertToBinary(List<Number> data);
    }
}
