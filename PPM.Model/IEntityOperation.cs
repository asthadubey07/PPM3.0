namespace PPM.Model
{
    // Represents an interface for basic CRUD operations on entities.
    public interface IEntityOperation<T>
    {
        // Adds a new entity.
        void Add(T entity);

        // Retrieves a list of all entities.
        List<T> ListAll();

        // Retrieves a specific entity by its unique identifier.
        T? ListByID(string entityId);

        // Deletes an entity by its unique identifier.
        void Delete(string entityId);
    }
}
