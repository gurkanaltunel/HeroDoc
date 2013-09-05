using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace DocumentService.Interceptors
{
    public class AspectInterceptor:IInterceptor
    {

        public void Intercept(IInvocation invocation)
        {
            var attributes = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(AspectAttributeBase), true).Cast<AspectAttributeBase>();
            foreach (var attribute in attributes)
            {
                attribute.ExecuteAttribute(invocation);
            }
            invocation.Proceed();
        }
    }
}
