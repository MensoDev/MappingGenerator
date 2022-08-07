using MappingGenerator.Abstraction;

namespace MappingGenerator.Application.ShortSamples;

[MapFrom<ShortEntity>]
public class ShortModel
{   
    public short Number { get; set; }
    public short? NumberNullable { get; set; }
    public short NumberNullableOrDefault { get; set; }
}