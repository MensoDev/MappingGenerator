namespace MappingGenerator.Application.IntSamples;

public class IntEntity
{
	public IntEntity(int number, int? numberNullable, int? numberNullableOrDefault = null)
	{
		Number = number;
		NumberNullable = numberNullable;
		NumberNullableOrDefault = numberNullableOrDefault;
	}

	public int Number { get; private set; }

	public int? NumberNullable { get; private set; }
	public int? NumberNullableOrDefault { get; private set; }
}
