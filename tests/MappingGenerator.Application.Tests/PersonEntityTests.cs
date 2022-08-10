using MappingGenerator.Application.ComplexExample;
using System.Linq.Expressions;

namespace MappingGenerator.Application.Tests;

public class PersonEntityTests
{

    [Fact]
    public void ShouldReturnSuccessWhenMappingAPersonTypeCorrectly()
    {
        var doc = new Document("44444");
        var person = new Person("Emerson", new Document("234234"));
        person.Documents.Add(doc);
        
        var model = person.ToPersonModel();

        model.Name.Should().Be(person.Name);
        model.PrimaryDocument.Number.Should().Be(person.PrimaryDocument.Number);
        model.Documents[0].Number.Should().Be(doc.Number);
    }
}