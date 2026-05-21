using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Blog.DL.DA.Data.Repository.IRepository
{
    //Unit of work se encarga de agrupar todos los repositorios
    //y manejar las tranascciones de manera conjunta.
    public interface IUnitOfWork: IDisposable // IDisposable nos permite liberar recursos
    {
        //Nota: los repositorios se definen como solo lectura
        //porque no se necesita cambiar la referencia del repositorio

        //Agregamos los repositorios
        ICategoryRepository Category { get; }
        IArticleRepository Article { get; }
        ISliderRepository Slider { get; }
        IUserRepository User { get; }

        void Save();
    }
}
