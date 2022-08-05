using FluentAssertions;

namespace MappingGenerator.Application.Tests
{
    public class PersonTest
    {
        [Fact]
        public void ShouldReturnSuccessWhenMapperEntityWithStringProperty()
        {
            var person = new Person("Emerson", 10);
            var model = person.ToPersonModel();

            model.FirstName.Should().Be(person.FirstName);
        }


        [Fact]
        public void ShouldReturnSuccessWhenMapperEntityWithIntProperty()
        {
            var person = new Person("Emerson", 26);
            var model = person.ToPersonModel();

            model.Age.Should().Be(person.Age);
        }
    }
}