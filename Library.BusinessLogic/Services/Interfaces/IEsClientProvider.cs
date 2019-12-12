using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BusinessLogic.Services.Interfaces
{
    public interface IEsClientProvider
    {
        ElasticClient GetClient();
    }
}
