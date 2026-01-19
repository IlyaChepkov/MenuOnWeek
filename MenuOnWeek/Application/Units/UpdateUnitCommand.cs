namespace Application.Units;


/// <summary>
/// Комманда обновления единицы измерения
/// </summary>
public sealed record UpdateUnitCommand(Guid Id, string? Name);
