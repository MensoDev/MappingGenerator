using Microsoft.CodeAnalysis;

namespace MappingGenerator.Benchmarks;

public class EmptySourceGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        return;
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        return;
    }
}

