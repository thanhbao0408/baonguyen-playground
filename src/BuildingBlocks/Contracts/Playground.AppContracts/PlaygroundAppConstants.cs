using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.AppContracts
{
    public class PlaygroundAppConstants
    {
        #region Blog Resources
        public const string BlogApiResourceName = "blogApi";
        public const string BlogApiResourceDisplayName = "Blog API";
        public const string BlogApiResourceDescription = "Allow the application to access Blog API on your behalf";

        public const string BlogApiScopeReadName = "blogApi.read";
        public const string BlogApiScopeReadDisplayName = "Read Access to Blog API";

        public const string BlogApiScopeWriteName = "blogApi.write";
        public const string BlogApiScopeWriteDisplayName = "Write Access to Blog API";
        #endregion
    }
}
