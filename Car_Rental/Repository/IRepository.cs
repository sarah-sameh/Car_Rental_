using Car_Rental.Interfaces;

namespace Car_Rental.Repository
{
    public interface IRepository<T> where T : class,ISoftDeletable
    {
         public List<T> getAll();
        public T get(int id);
        public void delete(int id);
        public void Insert(T item);

        public void Update(T item);
        public int save();
    }
}
