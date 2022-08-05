using System;

namespace MappingGenerator.Abstraction;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class MapFromAttribute<TEntity> : Attribute
{

}
