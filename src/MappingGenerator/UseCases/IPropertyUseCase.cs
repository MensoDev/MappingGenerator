using MappingGenerator.Models;

namespace MappingGenerator.UseCases;

internal interface IPropertyUseCase
{
    PropertyDeclaration ModelProperty { get; }
    PropertyDeclaration EntityProperty { get; }

    string CreateUseCaseTo(ClassDeclaration entity);
}
