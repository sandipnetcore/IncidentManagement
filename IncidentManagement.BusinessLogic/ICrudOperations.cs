using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentManagement.BusinessLogic
{
    public interface ICrudOperations<TDBSet, TReturnType>
        where TReturnType : class
    {
        public Task<List<TReturnType>> GetAllItems();

        public Task<bool> AddItem(TDBSet model);

        public Task<bool> EditItem(TDBSet model);

        public Task<bool> DeleteItem<T>(T id);
        public Task<TReturnType> GetItemById<T>(T id);
    }
}
