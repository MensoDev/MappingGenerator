namespace MappingGenerator.Application.ComplexExample;

public  class Person
{

    public Person(string name, Document primaryDocument)
    {
        Name = name;
        PrimaryDocument = primaryDocument;
        Documents = new List<Document>();
    }

    public string Name { get; private set; }

    public Document PrimaryDocument { get; private set; }

    public IList<Document>Documents { get; private set; }
}
