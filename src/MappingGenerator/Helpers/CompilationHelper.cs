using System;
using Microsoft.CodeAnalysis;

namespace MappingGenerator.Helpers;

public class CodeContextHelper
{
	private static CodeContextHelper? _instance;

	private readonly GeneratorExecutionContext _context;

	protected CodeContextHelper(GeneratorExecutionContext context)
	{
		_context = context;
	}

	public Compilation Compilation => _context.Compilation;

    public void RegisterCode(string fileName, string code)
	{
		_context.AddSource(fileName, code);
	}


    public static void Configure(GeneratorExecutionContext context)
	{
		if (_instance is not null) return;
		_instance = new CodeContextHelper(context);
    }

	public static CodeContextHelper Instance()
	{        
        return _instance ?? throw new NotImplementedException();
    }

}
