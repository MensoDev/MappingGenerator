using MappingGenerator.Abstraction;

namespace MappingGenerator.Application.IntSamples;

[MapFrom<IntEntity>]
public class IntModel
{
    public int Number { get; set; }
    public int? NumberNullable { get; set; }
    public int NumberNullableOrDefault { get; set; }
}
