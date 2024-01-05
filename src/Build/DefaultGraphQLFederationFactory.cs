using PipServices3.Commons.Refer;
using PipServices3.Components.Build;
using PipServices3.GraphQL.Federation.Services;

namespace PipServices3.GraphQL.Federation.Build
{
    /// <summary>
    /// Creates GraphQL components by their descriptors.
    /// </summary>
    public class DefaultGraphQLFederationFactory : Factory
    {
        public static Descriptor Descriptor = new Descriptor("pip-services", "factory", "fedgraphql", "default", "1.0");
        public static Descriptor Descriptor3 = new Descriptor("pip-services3", "factory", "fedgraphql", "default", "1.0");

        /// <summary>
        /// Create a new instance of the factory.
        /// </summary>
        public DefaultGraphQLFederationFactory()
        {
        }
    }
}
