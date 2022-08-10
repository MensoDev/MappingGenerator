namespace MappingGenerator.Application.ComplexExample;

public class Document
{
    public string Number { get; private set; }

    public Document(string number)
    {
        Number = number;
    }
}
