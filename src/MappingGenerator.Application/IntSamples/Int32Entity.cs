namespace MappingGenerator.Application.IntSamples;

public class Int32Entity
{
    public Int32Entity(int number, int? numberNullable, int? numberNullableOrDefault = null)
    {
        Number = number;
        NumberNullable = numberNullable;
        NumberNullableOrDefault = numberNullableOrDefault;
    }

    public Int32 Number { get; private set; }

    public Int32? NumberNullable { get; private set; }
    public Int32? NumberNullableOrDefault { get; private set; }
}
