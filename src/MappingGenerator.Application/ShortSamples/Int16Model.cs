using MappingGenerator.Abstraction;

namespace MappingGenerator.Application.ShortSamples;

[MapFrom<Int16Entity>]
public class Int16Model
{
    public Int16 Number { get; set; }
    public Int16? NumberNullable { get; set; }
    public Int16 NumberNullableOrDefault { get; set; }
}
