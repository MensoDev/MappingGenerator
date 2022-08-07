using MappingGenerator.Application.IntSamples;

namespace MappingGenerator.Application.Tests;

public class IntEntityTests
{
    [Fact]
    public void ShouldReturnSuccessWhenMappingAIntTypePropertyCorrectly()
    {
        //Arrange
        var intEntity = new IntEntity(1, null);

        //Act
        var intModel = intEntity.ToIntModel();

        //Assert
        intModel.Number.Should().Be(intEntity.Number);
    }

    [Fact]
    public void ShouldReturnSuccessWhenMappingAIntNullableOrDefaultTypePropertyCorrectly()
    {
        //Arrange
        var intEntity = new IntEntity(1, null, null);

        //Act
        var intModel = intEntity.ToIntModel();

        //Assert
        intModel.NumberNullableOrDefault.Should().Be(0);
        intModel.NumberNullableOrDefault.Should().NotBe(intEntity.NumberNullable);
    }

    [Fact]
    public void ShouldReturnSuccessWhenMappingAIntNullableTypePropertyCorrectly()
    {
        //Arrange
        var intEntity = new IntEntity(1, null, null);

        //Act
        var intModel = intEntity.ToIntModel();

        //Assert
        intModel.NumberNullable.Should().Be(null);
        intModel.NumberNullable.Should().Be(intEntity.NumberNullable);
    }

    [Fact]
    public void ShouldReturnSuccessWhenMappingAnIntListWithPropertyOfTypeStringCorrectly()
    {
        //Arrange
        var itnEntities = new List<IntEntity>()
        {
            new IntEntity(1, null, 0),
            new IntEntity(1, null, 0),
            new IntEntity(1, null, 0),
            new IntEntity(1, null, 0)
        };

        //Act
        var intModels = itnEntities.AsQueryable().ProjectToIntModel().ToList();

        //Assert
        intModels.Should().BeEquivalentTo(itnEntities);
    }

}
