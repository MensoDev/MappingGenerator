using System;
using System.Diagnostics;
using MappingGenerator.Extensions;
using MappingGenerator.Finders;
using MappingGenerator.Helpers;
using MappingGenerator.Models;
using MappingGenerator.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using System.Collections.Generic;

namespace MappingGenerator;

[Generator]
public class MappingSourceGenerators : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        try
        {
//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif

            //CreateMappersAttributesInContext(context);

            if (context.SyntaxReceiver is not MapFromFinder mapFromFinder) return;

            CreateMappersInContext(context, mapFromFinder.ClassDeclarations);
            
        }
        catch (Exception e)
        {
            //#if DEBUG
            //            Debugger.Log(0, "MappingSourceGenerators", e.Message);
            //#endif
            throw e;
        }
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new MapFromFinder());
    }

    private static void CreateMappersAttributesInContext(GeneratorExecutionContext context)
    {
        context.AddSource("MapFromAttribute.g.cs", MapFromAttributeTemplate.CreateMapFromAttribute());
    }

    private static void CreateMappersInContext(GeneratorExecutionContext context, List<ClassDeclarationSyntax> modelClassDeclarations)
    {
        foreach (var modelClassDeclarationSyntax in modelClassDeclarations)
        {
            (ClassDeclaration model, ClassDeclaration entity) = CreateTypes(context.Compilation, modelClassDeclarationSyntax);
            var propertyMappingArguments = CreateMappingArguments(model, entity);
            var sourceMapperCode = MappingTemplate.CreateMapper(model, entity, propertyMappingArguments);
            context.AddSource($"{model.Name}Mapper.g.cs", sourceMapperCode);
        }
    }

    private static (ClassDeclaration model, ClassDeclaration entity) CreateTypes(Compilation compilation, ClassDeclarationSyntax modelClassDeclaration)
    {
        var semanticModel = compilation.GetSemanticModel(modelClassDeclaration.SyntaxTree);
        var fromMapAttributeSyntax = MapFromAttributeHelper.GetMapFromAttribute(modelClassDeclaration);
        var entityNameSyntax = MapFromAttributeHelper.GetEntityIdentifierNameSyntax(fromMapAttributeSyntax);
        var entityClassDeclaration = semanticModel.GetEntityClassDeclaration(entityNameSyntax);

        var model = ClassDeclarationFactory.Create(modelClassDeclaration);
        var entity = ClassDeclarationFactory.Create(entityClassDeclaration);
        return (model, entity);
    }

    private static StringBuilder CreateMappingArguments(ClassDeclaration model, ClassDeclaration entity)
    {
        var builder = new StringBuilder();
        foreach (var modelProperty in model.Properties)
        {
            var entityProperty = entity.FindEquivalentProperty(modelProperty);
            if (entityProperty is null) continue;

            var useCase = PropertyUseCaseFactory.CreatePropertyUseCase(modelProperty, entityProperty);
            if (useCase is null) continue;

            builder.Append($"{useCase.CreateUseCaseTo(entity)}, ");
        }

        return builder;
    }
}