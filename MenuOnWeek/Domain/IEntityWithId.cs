namespace MenuOnWeek.Domain;

/// <summary>
/// Сущность с идентификатором 
/// </summary>
public interface IEntityWithId
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public Guid Id { get; set; }
}
