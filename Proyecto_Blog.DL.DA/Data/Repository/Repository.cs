using Microsoft.EntityFrameworkCore;
using Proyecto_Blog.DL.DA.Data.Repository.IRepository;
using System.Linq.Expressions;

namespace Proyecto_Blog.DL.DA.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext cntx;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            cntx = context;
            dbSet = cntx.Set<T>();
        }

        public void Add(T entity)
        {
            cntx.Add(entity);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        //public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string? includeProperties = null)
        //{
        //    //Vamos a crear una consulta IQueryable a partir del DbSet
        //    IQueryable<T> query = dbSet;

        //    //Se aplica filtro si se ha proporsionado
        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    //Si se han incluido propiedades de navegación, se procesan
        //    if(includeProperties != null)
        //    { //Separamos las propiedades por comas y las incluimos en la consulta
        //        //includeProperty = "Categoria,Marca"
        //        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            query = query.Include(includeProperty);
        //        }
        //    }

        //    //Ordenamos
        //    if (orderBy != null)
        //    {
        //        return orderBy(query).ToList();
        //    }
        //    return query.ToList();
        //}

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string? includeProperties = null)
        { 
            var baseQuery = filter != null ? dbSet.Where(filter) : dbSet;

            var queryWithIncludes = (includeProperties?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)//Divide la cadena en un arreglo de acuerdo a las comas
                .Select(p => p.Trim())//trim elimina espacion en blanco
                .Aggregate(baseQuery, (current, include) => //Agrega las propiedades a la navegacion 
                current.Include(include))) ?? baseQuery;

            return orderBy != null ? orderBy(queryWithIncludes).ToList() :
                queryWithIncludes.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            //Se aplica filtro si se ha proporsionado
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //Si se han incluido propiedades de navegación, se procesan
            if (includeProperties != null)
            { //Separamos las propiedades por comas y las incluimos en la consulta
              //includeProperty = "Categoria,Marca"
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();//Devuelve el objeto si es que existo, pero si no existe devuelve un objeto vacio.
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Remove(int id)
        {
            T entity = dbSet.Find(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }
    }
}
