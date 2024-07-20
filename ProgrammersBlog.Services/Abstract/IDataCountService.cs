using ProgrammersBlog.Entities.RequestParameters;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Abstract
{
    public interface IDataCountService
    {
        public Task<int> CountAsync();
        public Task<int> GetDeletedDataCountAsync();
        public Task<int> GetActiveDataCountAsync();
    }
}
