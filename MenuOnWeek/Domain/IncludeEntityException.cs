namespace MenuOnWeek.Domain;

// TODO: Коментарии
/// <summary>
/// Исключение, выбрасываемое когда не подключено вавигационное свойство
/// </summary>
public sealed class IncludeEntityException<TEntity, TRelationEntity> : Exception
{
    /// <inheritdoc/>
    public IncludeEntityException(string navigationProperty) :
        base($"У сущности {typeof(TEntity).Name} не задано значение навигационного свойства {navigationProperty} к сущности {typeof(TRelationEntity).Name}. Проверьте наличие Include")
    {
        NavigationProperty = navigationProperty;
    }

    /// <summary>
    /// Имя типа навигационного свойства в котором не задано значение
    /// </summary>
    public string NavigationProperty { get; init; }
}
