using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interface.Services
{
    public interface IGeneryServices<SaveViewModel, ViewModel>
        where SaveViewModel : class
        where ViewModel : class
    {
        Task Update(SaveViewModel vm);
        Task<SaveViewModel> Add(SaveViewModel vm);
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task<List<ViewModel>> GetAllViewModel();
        Task Delete(int id);

    }
}
