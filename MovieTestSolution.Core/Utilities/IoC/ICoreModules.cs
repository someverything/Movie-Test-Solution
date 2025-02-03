using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Core.Utilities.IoC
{
    public interface ICoreModules
    {
        void Load(IServiceCollection serviceDescriptors);
    }
}
