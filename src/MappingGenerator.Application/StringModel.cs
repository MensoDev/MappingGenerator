using MappingGenerator.Abstraction;

namespace MappingGenerator.Application;

[MapFrom<StringEntity>]
public class StringModel
{
    public string Name { get; set; } = default!;
    public string Nullable { get; set; } = default!;
}
