using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIM.Policies.Sample;

internal class ClassWithMethodsToIgnore
{
    public void ShouldBeIgnoredBecauseItHasNoReturnType(IProxyRequestContext context)
    {
    }
}
