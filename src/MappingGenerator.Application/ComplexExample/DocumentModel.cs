using MappingGenerator.Abstraction;

namespace MappingGenerator.Application.ComplexExample;

[MapFrom<Document>]
public class DocumentModel
{
    public string Number { get; set; } = default!;
}
