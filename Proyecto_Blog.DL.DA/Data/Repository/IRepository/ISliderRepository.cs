using Proyecto_Blog.BE.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Blog.DL.DA.Data.Repository.IRepository
{
    public interface ISliderRepository :IRepository<Slider>
    {
        void Update(Slider slider);
    }
}
