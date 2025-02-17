// Copyright © 2025 Always Active Technologies PTY Ltd

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechAptV1.Client.Models;

public class Number
{
    public int Value { get; set; }
    public int IsPrime { get; set; } = 0;
}
