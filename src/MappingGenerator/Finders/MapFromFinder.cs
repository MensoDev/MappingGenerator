using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using MappingGenerator.Helpers;

namespace MappingGenerator.Finders;

internal class MapFromFinder : ISyntaxReceiver
{
    public MapFromFinder()
    {
        ClassDeclarations = new List<ClassDeclarationSyntax>();
    }

    public List<ClassDeclarationSyntax> ClassDeclarations { get; private set; }

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is ClassDeclarationSyntax model && MapFromAttributeHelper.ContainsMapFromAttribute(model))
        {
            ClassDeclarations.Add(model);
        }
    }
}
