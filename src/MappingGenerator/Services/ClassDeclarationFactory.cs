using MappingGenerator.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace MappingGenerator.Services;

internal sealed class ClassDeclarationFactory
{

    public static ClassDeclaration Create(ClassDeclarationSyntax classSyntax)
    {
        string name = IdentifyClassName(classSyntax);
        string selfNamespace = IdentifyNamespace(classSyntax);
        PropertyDeclaration[] properties = CreateProperties(classSyntax);

        ClassDeclaration classDeclaration = new(name, selfNamespace);
        classDeclaration.RegisterProperties(properties);

        return classDeclaration;
    }

    private static string IdentifyClassName(ClassDeclarationSyntax classSyntax)
    {
        return classSyntax.Identifier.ValueText;
    }

    public static PropertyDeclaration[] CreateProperties(ClassDeclarationSyntax classSyntax)
    {
        return classSyntax
            .Members
            .OfType<PropertyDeclarationSyntax>()
            .Select(PropertyDeclarationFactory.Create)
            .ToArray();
    }

    private static string IdentifyNamespace(ClassDeclarationSyntax classSyntax)
    {
        return GetNamespace(classSyntax);
    }

    private static string GetNamespace(BaseTypeDeclarationSyntax syntax)
    {
        var nameSpace = string.Empty;
        var potentialNamespaceParent = syntax.Parent;

        while (potentialNamespaceParent != null &&
               potentialNamespaceParent is not NamespaceDeclarationSyntax
               && potentialNamespaceParent is not FileScopedNamespaceDeclarationSyntax)
        {
            potentialNamespaceParent = potentialNamespaceParent.Parent;
        }

        if (potentialNamespaceParent is not BaseNamespaceDeclarationSyntax namespaceParent) return nameSpace;

        nameSpace = namespaceParent.Name.ToString();

        while (true)
        {
            if (namespaceParent.Parent is not NamespaceDeclarationSyntax parent) { break; }

            nameSpace = $"{namespaceParent.Name}.{nameSpace}";
            namespaceParent = parent;
        }

        return nameSpace;
    }
}
