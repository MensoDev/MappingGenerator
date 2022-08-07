

namespace MappingGenerator.Application.Tests;

public class StringEntityTests
{
    [Fact]
    public void ShouldReturnSuccessWhenMappingAStringTypePropertyCorrectly()
    {
        //Arrange
        var stringEntity = new StringEntity("String Value", "...");
        
        //Act
        var stringModel = stringEntity.ToStringModel();

        //Assert
        stringModel.Name.Should().Be(stringEntity.Name);
    }

    [Fact]
    public void ShouldReturnSuccessWhenMappingAStringNullableTypePropertyCorrectly()
    {
        //Arrange
        var stringEntity = new StringEntity("String Value", null);

        //Act
        var stringModel = stringEntity.ToStringModel();

        //Assert
        stringModel.Nullable.Should().BeNull();
        stringModel.Nullable.Should().Be(stringEntity.Nullable);
    }

    [Fact]
    public void ShouldReturnSuccessWhenMappingAnEntityListWithPropertyOfTypeStringCorrectly()
    {
        //Arrange
        var stringEntities = new List<StringEntity>()
        {
            new ($"String Content {Guid.NewGuid()}", ".."),
            new ($"String Content {Guid.NewGuid()}", ".."),
            new ($"String Content {Guid.NewGuid()}", ".."),
            new ($"String Content {Guid.NewGuid()}", "..")
        };

        //Act
        var stringModels = stringEntities.AsQueryable().ProjectToStringModel().ToList();

        //Assert
        stringModels.Should().BeEquivalentTo(stringEntities);
    }
}
