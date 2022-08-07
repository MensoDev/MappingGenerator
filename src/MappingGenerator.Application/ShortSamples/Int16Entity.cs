namespace MappingGenerator.Application.ShortSamples;

public  class Int16Entity
{
    public Int16Entity(Int16 number, Int16? numberNullable, Int16? numberNullableOrDefault = null)
    {
        Number = number;
        NumberNullable = numberNullable;
        NumberNullableOrDefault = numberNullableOrDefault;
    }

    public Int16 Number { get; private set; }

    public Int16? NumberNullable { get; private set; }
    public Int16? NumberNullableOrDefault { get; private set; }
}
