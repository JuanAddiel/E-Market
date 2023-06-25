using E_Market.Core.Application.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interface.Services
{
    public interface ICategoryServices:IGeneryServices<CategorySaveViewModel, CategoryViewModel>
    {
    }
}
