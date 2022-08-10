using MappingGenerator.Abstraction;

namespace MappingGenerator.Application.ComplexExample;

[MapFrom<Person>]
public class PersonModel
{
    public string Name { get; set; } = default!; 

    [UseDocumentModelMapper]
    public DocumentModel PrimaryDocument { get; set; } = default!; 

    [UseDocumentModelMapper]
    public IList<DocumentModel> Documents { get; set; } = default!;
}
