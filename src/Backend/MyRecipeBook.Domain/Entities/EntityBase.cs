namespace MyRecipeBook.Domain.Entities
{
    public class EntityBase
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public bool Active { get; init; } = true;
        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
    }
}
