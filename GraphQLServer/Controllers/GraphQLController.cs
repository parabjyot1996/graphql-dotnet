using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQL.Validation.Complexity;
using GraphQLServer.Contracts;
using GraphQLServer.Entities.Context;
using GraphQLServer.GraphQL.Queries;
using GraphQLServer.GraphQL.Schemas;
using GraphQLServer.GraphQL.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLServer.Controllers
{
    [ApiController]
    [Route("graphql")]
    public class GraphQLController : ControllerBase
    {
        private readonly IDependencyResolver _resolver;
        private readonly IDocumentExecuter _documentExecuter;
        private readonly DataLoaderDocumentListener _documentListener;

        public GraphQLController(IDependencyResolver resolver,
                                    IDocumentExecuter documentExecuter,
                                    DataLoaderDocumentListener documentListener)
        {
            _resolver = resolver;
            _documentExecuter = documentExecuter;
            _documentListener = documentListener;
        }

        [Authorize]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            if (query == null)
            { 
                return BadRequest();
            }

            var inputs = query.Variables.ToInputs();
            var schema = new AppSchema(_resolver);

            var options = new ExecutionOptions
            {
                Schema = schema,
                Query = query.Query,
                Inputs = inputs,
                UserContext = User,
                ComplexityConfiguration = new ComplexityConfiguration { FieldImpact = 2, MaxComplexity = 30, MaxDepth = 15 }
            };

            options.Listeners.Add(_documentListener);

            var result = await _documentExecuter.ExecuteAsync(options).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }
    }
}