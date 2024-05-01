using Car_Rental.Interfaces;
using Car_Rental.Models;

namespace Car_Rental.Repository
{
    public class Repository<T> : IRepository<T> where T : class, ISoftDeletable
    {

        protected Context _context;
        public Repository(Context context)
        {

            _context = context;
        }

        public List<T> getAll()
        {



            return _context.Set<T>().ToList();
        }


        public T get(int id)
        {
            return _context.Set<T>().Find(id);

        }

        public void Update(T item)
        {




            _context.Update(item);

        }

        public void delete(int id)
        {
            T item = _context.Set<T>().Find(id);

            if (item != null)
            {
                item.IsDeleted = true;
                Update(item);
            }

        }


        public void Insert(T item)
        {
            _context.Set<T>().Add(item);
        }

        public int save()
        {
            return _context.SaveChanges();
        }


    }
}
