using MappingGenerator.Abstraction;

namespace MappingGenerator.Application.IntSamples;

[MapFrom<Int32Entity>]
public class Int32Model
{
    public int Number { get; set; }
    public int? NumberNullable { get; set; }
    public int NumberNullableOrDefault { get; set; }
}
