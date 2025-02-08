using Castle.DynamicProxy;
using MovieTestSolution.Core.CrossCuttingConcerns.Caching;
using MovieTestSolution.Core.Utilities.Interceptors;
using MovieTestSolution.Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = (ICacheManager)ServiceTool.ServiceProvider.GetService(typeof(ICacheManager));
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
