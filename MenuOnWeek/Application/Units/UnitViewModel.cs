namespace Application.Units;

public sealed class UnitViewModel
{
    public required Guid Id { get; set; }

    public required string? Name { get; set; }

    public override string ToString() => Name ?? "";
}
