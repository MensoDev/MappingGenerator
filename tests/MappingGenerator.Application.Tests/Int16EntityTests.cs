using MappingGenerator.Application.ShortSamples;

namespace MappingGenerator.Application.Tests;

public class Int1616EntityTests
{
    [Fact]
    public void ShouldReturnSuccessWhenMappingAInt16TypePropertyCorrectly()
    {
        //Arrange
        var int16Entity = new Int16Entity(1, null);

        //Act
        var int16Model = int16Entity.ToInt16Model();

        //Assert
        int16Model.Number.Should().Be(int16Entity.Number);
    }

    [Fact]
    public void ShouldReturnSuccessWhenMappingAInt16NullableOrDefaultTypePropertyCorrectly()
    {
        //Arrange
        var int16Entity = new Int16Entity(1, null, null);

        //Act
        var int16Model = int16Entity.ToInt16Model();

        //Assert
        int16Model.NumberNullableOrDefault.Should().Be(0);
        int16Model.NumberNullableOrDefault.Should().NotBe(int16Entity.NumberNullable);
    }

    [Fact]
    public void ShouldReturnSuccessWhenMappingAInt16NullableTypePropertyCorrectly()
    {
        //Arrange
        var int16Entity = new Int16Entity(1, null, null);

        //Act
        var int16Model = int16Entity.ToInt16Model();

        //Assert
        int16Model.NumberNullable.Should().Be(null);
        int16Model.NumberNullable.Should().Be(int16Entity.NumberNullable);
    }

    [Fact]
    public void ShouldReturnSuccessWhenMappingAnInt16ListWithPropertyOfTypeStringCorrectly()
    {
        //Arrange
        var int16Entities = new List<Int16Entity>()
        {
            new (1, null, 0),
            new (1, null, 0),
            new (1, null, 0),
            new (1, null, 0)
        };

        //Act
        var int16Models = int16Entities.AsQueryable().ProjectToInt16Model().ToList();

        //Assert
        int16Models.Should().BeEquivalentTo(int16Entities);
    }
}
