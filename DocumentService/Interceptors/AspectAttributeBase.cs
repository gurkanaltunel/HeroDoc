using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace DocumentService.Interceptors
{
    public abstract class AspectAttributeBase:Attribute
    {
        public void ExecuteAttribute(IInvocation invocation)
        {
            var allowExecution = BeforeExecution(invocation);
            if (!allowExecution)
            {
                return;
            }
            invocation.Proceed();
            AfterMethodExecution(invocation);
        }

        protected virtual void AfterMethodExecution(IInvocation invocation)
        {
           
        }

        protected virtual bool BeforeExecution(IInvocation invocation)
        {
            return true;
        }
    }
}
