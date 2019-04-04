using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using NEvilES;
using NEvilES.Pipeline;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EFDemo1.Common
{
    public class LocalEventStore : IAsyncRepository
    {
        private readonly LiteDatabase _db;
        private readonly IEventTypeLookupStrategy _eventTypeLookupStrategy;
        private readonly CommandContext _commandContext;

        public LocalEventStore(
            LiteDatabase db,
            IEventTypeLookupStrategy eventTypeLookupStrategy,
            CommandContext commandContext
            )
        {
            this._db = db;
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
            var events = new List<LocalEventTable>();
            if (version.HasValue && version.Value > 0)
            {
                events = _db.GetCollection<LocalEventTable>().Find(x => x.StreamId == id && x.Version <= version.Value).ToList();
            }
            else
            {
                events = _db.GetCollection<LocalEventTable>().Find(x => x.StreamId == id && x.Version >= 0).ToList();
            }

            if (events.Count == 0)
            {
                var emptyAggregate = (IAggregate)Activator.CreateInstance(type, true);
                ((AggregateBase)emptyAggregate).SetState(id);
                return emptyAggregate;
            }

            var aggregate = (IAggregate)Activator.CreateInstance(_eventTypeLookupStrategy.Resolve(events.FirstOrDefault().Category));


            foreach (var eventDb in events.OrderBy(x => x.Version))
            {
                var message =
                    (IEvent)
                    JsonConvert.DeserializeObject(eventDb.Body, _eventTypeLookupStrategy.Resolve(eventDb.BodyType), SerializerSettings);
                message.StreamId = eventDb.StreamId;
                aggregate.ApplyEvent(message);
            }
            ((AggregateBase)aggregate).SetState(id);

            return aggregate;

        }
        Task<IAggregate> GetStatelessAsync(Type type, Guid id)
        {
            return null;
        }

        public class CourseLog
        {
            public Guid StreamId { get; set; }
            public string Name { get; set; }
            public int MaxNumber { get; set; }
            public int Credit { get; set; }
            public string CourseCode { get; set; }
            public string CourseDetail { get; set; }
        }

        public async Task<AggregateCommit> SaveAsync(IAggregate aggregate)
        {
            if (aggregate.Id == Guid.Empty)
            {
                throw new Exception(
                    $"The aggregate {aggregate.GetType().FullName} has tried to be saved with an empty id");
            }

            var uncommittedEvents = aggregate.GetUncommittedEvents().Cast<IEventData>().ToArray();
            var count = 0;

            var metadata = string.Empty;
            try
            {


                    var col = _db.GetCollection<LocalEventTable>();
                    col.EnsureIndex(x => x.StreamId, false);

                    col.InsertBulk(uncommittedEvents.Select(x => new LocalEventTable {
                            StreamId = aggregate.Id,
                            Version = x.Version,
                            TransactionId = _commandContext.Transaction.Id,
                            AppVersion = _commandContext.AppVersion,
                            When = x.TimeStamp,
                            Body = JsonConvert.SerializeObject(x.Event, SerializerSettings),
                            Category = aggregate.GetType().FullName,
                            Who = _commandContext.ImpersonatorBy?.GuidId??_commandContext.By.GuidId,
                            BodyType = x.Type.FullName
                    }));




            }
            catch (Exception e)
            {
                throw new Exception(
                    $"The aggregate {aggregate.GetType().FullName} has tried to save events to an old version of an aggregate");
            }

            aggregate.ClearUncommittedEvents();
            return new AggregateCommit(aggregate.Id, _commandContext.By.GuidId, metadata, uncommittedEvents);
        }

        Task<IAggregate> IAsyncRepository.GetStatelessAsync(Type type, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
