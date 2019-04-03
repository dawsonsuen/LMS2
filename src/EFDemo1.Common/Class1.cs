using System;

namespace EFDemo1.Common
{
    public class LocalEventStore : IAsyncRepository
    {
        private readonly IEventTypeLookupStrategy _eventTypeLookupStrategy;
        private readonly CommandContext _commandContext;
        public LocalEventStore(
            IEventTypeLookupStrategy eventTypeLookupStrategy,
            CommandContext commandContext
            )
        {
            _eventTypeLookupStrategy = eventTypeLookupStrategy;
            _commandContext = commandContext;
        }
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Populate,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto,
            Converters = new JsonConverter[] { new StringEnumConverter() }
        };
        public async Task<TAggregate> GetAsync<TAggregate>(Guid id) where TAggregate : IAggregate
        {
            IAggregate aggregate = await GetAsync(typeof(TAggregate), id);
            return (TAggregate)aggregate;
        }
        public Task<IAggregate> GetAsync(Type type, Guid id) => GetAsync(type, id, null);

        public async Task<IAggregate> GetAsync(Type type, Guid id, Int64? version)
        {

        }
        Task<IAggregate> GetStatelessAsync(Type type, Guid id);
        Task<AggregateCommit> SaveAsync(IAggregate aggregate);
    }
}
