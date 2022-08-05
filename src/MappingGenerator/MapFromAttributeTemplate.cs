namespace MappingGenerator;

internal class MapFromAttributeTemplate
{
    public static string CreateMapFromAttribute()
    {
        return $@"
namespace MappingGenerator;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class MapFromAttribute<TEntity> : Attribute where TEntity : class
{{ }}
";
    }
}
