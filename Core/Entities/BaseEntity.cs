namespace Core.Entities;

/// <summary>
/// Represents the base class for all entities in the application, providing a unique identifier.
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public int Id { get; set; }
}
