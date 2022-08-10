using System;
using MappingGenerator.Extensions;
using MappingGenerator.Finders;
using MappingGenerator.Helpers;
using MappingGenerator.Models;
using MappingGenerator.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;

namespace MappingGenerator;

[Generator]
public class MappingSourceGenerators : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        try
        {
            // Setup
            CodeContextHelper.Configure(context);

//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif

            //CreateMappersAttributesInContext(context);

            if (context.SyntaxReceiver is not MapFromFinder mapFromFinder) return;

            CreateMappersInContext(mapFromFinder.ClassDeclarations);
            
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

    private static void CreateMappersInContext(List<ClassDeclarationSyntax> modelClassDeclarations)
    {
        foreach (var modelClassDeclarationSyntax in modelClassDeclarations)
        {
            var (model, entity) = CreateTypes(modelClassDeclarationSyntax);

            var propertyMappingArguments = CreateMappingArguments(model, entity);

            var sourceMapperCode = MappingTemplate.CreateMapper(model, entity, propertyMappingArguments);
            var sourceMapperAttributeCode = MappingTemplate.CreateUseMapper(model, entity);

            CodeContextHelper.Instance().RegisterCode($"{model.Name}Mapper.g.cs", sourceMapperCode);
            CodeContextHelper.Instance().RegisterCode($"Use{model.Name}MapperAttribute.g.cs", sourceMapperAttributeCode);
        }
    }

    private static (ClassDeclaration model, ClassDeclaration entity) CreateTypes(ClassDeclarationSyntax modelClassDeclaration)
    {
        var semanticModel = CodeContextHelper.Instance().Compilation.GetSemanticModel(modelClassDeclaration.SyntaxTree);
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