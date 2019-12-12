using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Services.Interfaces
{
    public interface IElasticsearchService
    {
        Task Run();
    }
}
