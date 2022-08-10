using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System;

namespace MappingGenerator.Extensions;

internal static class SemanticModelExtension
{
    public static ClassDeclarationSyntax GetEntityClassDeclaration(this SemanticModel semanticModel, IdentifierNameSyntax entityIdentifierName)
    {
        var result = semanticModel.GetTypeInfo(entityIdentifierName);
        if (result.Type is null) throw new InvalidOperationException();
        return (ClassDeclarationSyntax)result.Type.DeclaringSyntaxReferences[0].GetSyntax();
    }    
}
