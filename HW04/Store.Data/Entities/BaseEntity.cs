namespace Store.Data.Entities;

public abstract record BaseRecord
{
    public DateTime CreatedDate { get; set; }

    public DateTime UpdateDate { get; set; }
}



public abstract record BaseEntity : BaseRecord
{
    public int Id { get; set; }
}



public abstract record BaseEntityWithName : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}



public abstract record BaseEntityWithNameAndDescription : BaseEntityWithName
{
    public string? Description { get; set; }
}