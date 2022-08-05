﻿using System.Text;
using MappingGenerator.Models;

namespace MappingGenerator;

internal class MappingTemplate
{
    public static string CreateMapper(ClassDeclaration model, ClassDeclaration entity, StringBuilder propertyMappingArguments)
    {
        return $@"using System.Linq.Expressions;

namespace MappingGenerator.Application;

public static class {model.Name}Mapper
{{    

    public static Expression<Func<{entity.Name}, {model.Name}>> MapFrom{entity.Name} = {entity.CamelCaseName} => new {model.Name} {{ {propertyMappingArguments} }};

    public static IQueryable<{model.Name}> ProjectTo{model.Name}(this IQueryable<{entity.Name}> queryable) => queryable.Select(MapFrom{entity.Name});

    public static {model.Name} To{model.Name}(this {entity.Name} {entity.CamelCaseName})
    {{
        return MapFrom{entity.Name}.Compile().Invoke({entity.CamelCaseName});
    }}
}}";
    }
}