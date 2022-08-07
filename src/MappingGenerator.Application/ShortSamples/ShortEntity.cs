namespace MappingGenerator.Application.ShortSamples;

public class ShortEntity
{
    public ShortEntity(short number, short? numberNullable, short? numberNullableOrDefault = null)
    {
        Number = number;
        NumberNullable = numberNullable;
        NumberNullableOrDefault = numberNullableOrDefault;
    }

    public short Number { get; private set; }

    public short? NumberNullable { get; private set; }
    public short? NumberNullableOrDefault { get; private set; }
}
