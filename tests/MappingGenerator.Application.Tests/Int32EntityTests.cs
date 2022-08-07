using MappingGenerator.Application.IntSamples;

namespace MappingGenerator.Application.Tests;

public class Int32EntityTests
{
    [Fact]
    public void ShouldReturnSuccessWhenMappingAInt32TypePropertyCorrectly()
    {
        //Arrange
        var int32Entity = new Int32Entity(1, null);

        //Act
        var int32Model = int32Entity.ToInt32Model();

        //Assert
        int32Model.Number.Should().Be(int32Entity.Number);
    }

    [Fact]
    public void ShouldReturnSuccessWhenMappingAInt32NullableOrDefaultTypePropertyCorrectly()
    {
        //Arrange
        var int32Entity = new Int32Entity(1, null, null);

        //Act
        var int32Model = int32Entity.ToInt32Model();

        //Assert
        int32Model.NumberNullableOrDefault.Should().Be(0);
        int32Model.NumberNullableOrDefault.Should().NotBe(int32Entity.NumberNullable);
    }

    [Fact]
    public void ShouldReturnSuccessWhenMappingAInt32NullableTypePropertyCorrectly()
    {
        //Arrange
        var int32Entity = new Int32Entity(1, null, null);

        //Act
        var int32Model = int32Entity.ToInt32Model();

        //Assert
        int32Model.NumberNullable.Should().Be(null);
        int32Model.NumberNullable.Should().Be(int32Entity.NumberNullable);
    }

    [Fact]
    public void ShouldReturnSuccessWhenMappingAnInt32ListWithPropertyOfTypeStringCorrectly()
    {
        //Arrange
        var itn32Entities = new List<Int32Entity>()
        {
            new (1, null, 0),
            new (1, null, 0),
            new (1, null, 0),
            new (1, null, 0)
        };

        //Act
        var int32Models = itn32Entities.AsQueryable().ProjectToInt32Model().ToList();

        //Assert
        int32Models.Should().BeEquivalentTo(itn32Entities);
    }
}
