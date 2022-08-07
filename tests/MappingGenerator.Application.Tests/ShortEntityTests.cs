using MappingGenerator.Application.IntSamples;
using MappingGenerator.Application.ShortSamples;

namespace MappingGenerator.Application.Tests;

public class ShortEntityTests
{
    [Fact]
    public void ShouldReturnSuccessWhenMappingAShortTypePropertyCorrectly()
    {
        //Arrange
        var shortEntity = new ShortEntity(1, null);

        //Act
        var shortModel = shortEntity.ToShortModel();

        //Assert
        shortModel.Number.Should().Be(shortEntity.Number);
    }

    [Fact]
    public void ShouldReturnSuccessWhenMappingAShortNullableOrDefaultTypePropertyCorrectly()
    {
        //Arrange
        var shortEntity = new ShortEntity(1, null, null);

        //Act
        var shortModel = shortEntity.ToShortModel();

        //Assert
        shortModel.NumberNullableOrDefault.Should().Be(0);
        shortModel.NumberNullableOrDefault.Should().NotBe(shortEntity.NumberNullable);
    }

    [Fact]
    public void ShouldReturnSuccessWhenMappingAIntNullableTypePropertyCorrectly()
    {
        //Arrange
        var shortEntity = new ShortEntity(1, null, null);

        //Act
        var shortModel = shortEntity.ToShortModel();

        //Assert
        shortModel.NumberNullable.Should().Be(null);
        shortModel.NumberNullable.Should().Be(shortEntity.NumberNullable);
    }

    [Fact]
    public void ShouldReturnSuccessWhenMappingAnIntListWithPropertyOfTypeStringCorrectly()
    {
        //Arrange
        var shortEntities = new List<ShortEntity>()
        {
            new (1, null, 0),
            new (1, null, 0),
            new (1, null, 0),
            new (1, null, 0)
        };

        //Act
        var shortModels = shortEntities.AsQueryable().ProjectToShortModel().ToList();

        //Assert
        shortModels.Should().BeEquivalentTo(shortEntities);
    }
}
