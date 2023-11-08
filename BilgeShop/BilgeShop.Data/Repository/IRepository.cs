using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Data.Repository
{
    public interface IRepository <TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int id);
        void Update(TEntity entity);
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);

        // Bir sql sorgusu (Linq) parametre olarka göndermek istiyorsanız 
        // Parametre tipi -> Expression<Func<TEntity, bool>> 

        // = null diyerek bu metodun parametre alarak veya almayarak çalışabileceğini gösteriyorum

        // Parametre gönderilirse, o filtrelmeyle bütün yapılar.
        // Parametre gönderilmezse, filrelenmez bütün yapılar.

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

    }
}
