namespace PipServices3.GraphQL.Federation.Services
{
	public class FacadeGraphQLService : GraphQLFederationService
    {
        public FacadeGraphQLService() : 
            base("schema-federation.graphql")
        {
		}
	}
}