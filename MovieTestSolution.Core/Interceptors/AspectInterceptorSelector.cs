using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Core.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAtributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAtributes = type.GetMethod(method.Name)
                .GetCustomAttribute<MethodInterceptionBaseAttribute>(true);
            classAtributes.AddRange(methodAtributes);

            return classAtributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
