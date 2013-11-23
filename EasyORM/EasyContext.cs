using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyORM.Domain;

namespace EasyORM
{
    public abstract class EasyContext : IEasyContext
    {
        public EasyConfiguration Configuration { get; set; }

        protected EasyContext()
        {
            Configuration = new EasyConfiguration(this);

            //Configurate the context
            Configure();

            //Build the factories
            Configuration.Initialize();
        }

        public abstract void Configure();

        public IQueryable<T> Query<T>()
        {
            return null;
        }
        public void Store<T>(T item) { }
        public void Update<T>(T item, object primaryKey) { }
        public void Delete<T>(object primaryKey) { }
    }
}
