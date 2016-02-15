namespace HealthySystem.Web
{
    using Glimpse.AspNet.Extensions;
    using Glimpse.Core.Extensibility;

    public class GlimpseSecurityPolicy : IRuntimePolicy
    {
        /// <summary>
        /// The RuntimeEvent.ExecuteResource is only needed in case you create a security policy
        /// Have a look at http://blog.getglimpse.com/2013/12/09/protect-glimpse-axd-with-your-custom-runtime-policy/ for more details
        /// </summary>
        public RuntimeEvent ExecuteOn => RuntimeEvent.EndRequest | RuntimeEvent.ExecuteResource;

        /// <summary>
        /// You can perform a check like the one below to control Glimpse's permissions within your application.
        /// More information about RuntimePolicies can be found at http://getglimpse.com/Help/Custom-Runtime-Policy
        /// </summary>
        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
            var httpContext = policyContext.GetHttpContext();
            return !httpContext.User.IsInRole("Administrator") ? RuntimePolicy.Off : RuntimePolicy.On;
        }
    }
}