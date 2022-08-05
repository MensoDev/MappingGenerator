using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MappingGenerator.Abstraction;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using System.Text;

namespace MappingGenerator.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<GenerationTests>();        
    }
}

[SimpleJob]
[MemoryDiagnoser]
[ThreadingDiagnoser]
public class GenerationTests
{
    private static readonly GeneratorDriver _driver = CSharpGeneratorDriver.Create(new MappingSourceGenerators());
    private static readonly GeneratorDriver _emptyDriver = CSharpGeneratorDriver.Create(new EmptySourceGenerator());

    [Benchmark]
    public object DriverAndCompliationOverheadForSimpleCaseTest()
    {
        return _emptyDriver.RunGenerators(CreateCompilation(SimpleSource));
    }

    [Benchmark]
    public object SimpleGenerationTest()
    {
        return _driver.RunGenerators(CreateCompilation(SimpleSource));
    }

    [Benchmark]
    public object ComplicatedGenerationTest()
    {
        return _driver.RunGenerators(CreateCompilation(ComplicatedSource, ComplicatedSource2, ComplicatedSource3));
    }

    private static Compilation CreateCompilation(params SyntaxTree[] syntaxTrees)
    {
        return CSharpCompilation.Create("c" + Guid.NewGuid().ToString("N"),
                        syntaxTrees,
                        _references,
                        new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }

    private readonly static SyntaxTree SimpleSource = CSharpSyntaxTree.ParseText(GenerateEntityAndBuilder(1, 5));
    private readonly static SyntaxTree ComplicatedSource = CSharpSyntaxTree.ParseText(GenerateEntityAndBuilder(1, 0));
    private readonly static SyntaxTree ComplicatedSource2 = CSharpSyntaxTree.ParseText(GenerateEntityAndBuilder(1, 0));
    private readonly static SyntaxTree ComplicatedSource3 = CSharpSyntaxTree.ParseText(GenerateEntityAndBuilder(1, 0));

    private readonly static PortableExecutableReference[] _references = new[]
        {
            MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location),
            MetadataReference.CreateFromFile(typeof(MapFromAttribute<>).GetTypeInfo().Assembly.Location)
        };

    private static string GenerateEntityAndBuilder(int entityCount = 3, int propertiesCount = 10)
    {
        var code = new StringBuilder();
        var properties = new StringBuilder();

       GenerateNameList(propertiesCount)
            .ToList()
            .ForEach(x => properties.AppendLine($@"public string {x} {{ get; set; }}"));

        GenerateNameList(entityCount)
            .ToList()
            .ForEach(className => AddClassInStringBuilder(code, className, properties));

       return code.ToString();

    }

    private static void AddClassInStringBuilder(StringBuilder builder, string className, StringBuilder properties)
    {

        builder.Append($@"using MappingGenerator.Abstraction;

namespace MappingGenerator.IntegrationTests.Source.Builders
{{
    [MapFrom<{className}>]
    public partial class {className}Model
    {{
        {properties}
    }}

    public class {className}
    {{
        {properties}
    }}
}}");

    }

    private static IEnumerable<string> GenerateNameList(int entityCount)
    {
        return Enumerable.Range(0, entityCount).Select(x => "A" + Guid.NewGuid().ToString("N"));
    }
}