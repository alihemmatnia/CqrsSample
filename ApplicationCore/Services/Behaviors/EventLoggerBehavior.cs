using System.Text;
using Infrastructure.Entities;
using Infrastructure.RedisDb;
using MediatR;
using Newtonsoft.Json;

namespace ApplicationCore.Services.Behaviors
{
    public class EventLoggerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        readonly IRedisManager _eventStoreDbContext;

        public EventLoggerBehavior(IRedisManager eventStoreDbContext)
        {
            _eventStoreDbContext = eventStoreDbContext;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response = await next();

            string requestName = request.ToString();

            // Commands convention
            if (requestName.EndsWith("Command"))
            {
                Type requestType = request.GetType();
                string commandName = requestType.Name;

                var data = new Dictionary<string, object>
                {
                    {
                        "request", request
                    },
                    {
                        "response", response
                    }
                };

                string jsonData = JsonConvert.SerializeObject(data);

                var rr=new EventData()
                {
                    DateTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Response = jsonData
                };
               
                await _eventStoreDbContext.CreateEventSource(rr);
            }

            return response;
        }


        
    }

}
