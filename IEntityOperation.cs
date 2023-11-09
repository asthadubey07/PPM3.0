namespace PPM.Model
{
    public interface IEntityOperation<T>
    {
        void Add(T entity);
        List<T> ListAll();
        T ListByID(string entityId);
        void Delete(string entityId);
    }
}
