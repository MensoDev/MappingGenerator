using MappingGenerator.Extensions;
using MappingGenerator.Finders;
using MappingGenerator.Helpers;
using MappingGenerator.Models;
using MappingGenerator.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using System.Linq;

namespace MappingGenerator;

[Generator]
internal class MappingSourceGenerators : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        throw new System.NotImplementedException();
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new MapFromFinder());
    }

    private static (ClassDeclaration model, ClassDeclaration entity) CreateTypes(Compilation compilation, ClassDeclarationSyntax modelClassDeclaration)
    {
        SemanticModel semanticModel = compilation.GetSemanticModel(modelClassDeclaration.SyntaxTree);
        AttributeSyntax fromMapAttributeSyntax = MapFromAttributeHelper.GetMapFromAttribute(modelClassDeclaration);
        IdentifierNameSyntax entityNameSyntax = MapFromAttributeHelper.GetEntityIdentifierNameSyntax(fromMapAttributeSyntax);
        ClassDeclarationSyntax entityClassDeclaration = semanticModel.GetEntityClassDeclaration(entityNameSyntax);

        ClassDeclaration model = ClassDeclarationFactory.Create(modelClassDeclaration);
        ClassDeclaration entity = ClassDeclarationFactory.Create(entityClassDeclaration);
        return (model, entity);
    }

    private static void CreateMappingArguments(ClassDeclaration model, ClassDeclaration entity)
    {
        StringBuilder builder = new StringBuilder();
        foreach (var modelProperty in model.Properties)
        {
            PropertyDeclaration? entityProperty = entity.FindEquivalentProperty(modelProperty);
            if (entityProperty is null) continue;

            var useCase = PropertyUseCaseFactory.CreatePropertyUseCase(modelProperty, entityProperty);
            if (useCase is null) continue;

            builder.Append($"{useCase.CreateUseCaseTo(entity)}, ");
        }
    }
}
