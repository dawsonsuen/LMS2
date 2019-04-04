using System;
using Xunit;
using NEvilES;
using NEvilES.Pipeline;
using static NEvilES.Pipeline.CommandContext;
using NEvilES.Extensions.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using LiteDB;
using JsonFlatFileDataStore;

namespace EFDemo1.Common.Tests
{

    public class ReadModelService : IReadFromReadModel, IWriteReadModel
    {
        private Dictionary<Guid, IHaveIdentity> Models = new Dictionary<Guid, IHaveIdentity>();

        public T Get<T>(Guid id) where T :  class, IHaveIdentity
        {
            // if (Models.TryGetValue(id, out IHaveIdentity item))
            // {
                var modalName = typeof(T).Name;

                using (DataStore ds = new DataStore($"{modalName}.json"))
                {
                    var collection = ds.GetCollection<T>();

                    var course = new CourseModel
                    {
                        Name = item.,
                        Credit = message.Credit,
                    };

                    await collection.InsertOneAsync(course);

                }

                return (T)item;
            // }


            return default(T);
        }

        public IEnumerable<T> Query<T>(Func<T, bool> p)
        {
            throw new NotImplementedException();
        }

        public void Save<T>(T item) where T : class, IHaveIdentity
        {
            Models[item.Id] = item;
        }

        void IWriteReadModel.Delete<T>(T item)
        {
            throw new NotImplementedException();
        }

        void IWriteReadModel.Insert<T>(T item)
        {
            throw new NotImplementedException();
        }

        void IWriteReadModel.Update<T>(T item)
        {
            throw new NotImplementedException();
        }
    }

    public class UnitTest1
    {

        public class Transaction : IHaveIdentity
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Credit { get; set; }
            public class Projector :
                IProjectAsync<Test.TransactionAdded>,
                IProjectAsync<Test.CreditChanged>
            {
                // public static int x = 0;
                private readonly IReadFromReadModel read;
                private readonly IWriteReadModel write;
                // private int _fun;
                // private Guid _id;

                public Projector(IReadFromReadModel read, IWriteReadModel write)
                {
                    // x++;
                    // _fun = x;
                    // _id = Guid.NewGuid();
                    this.read = read;
                    this.write = write;
                }

                public int GetFun()
                {
                    return 0;
                }

                public Task ProjectAsync(Test.CreditChanged message, ProjectorData data)
                {
                    var transaction = read.Get<Transaction>(message.StreamId);

                    transaction.Credit = message.Credit;

                    write.Save(transaction);

                    var p = GetFun();
                    return Task.CompletedTask;
                }

                public Task ProjectAsync(Test.TransactionAdded message, ProjectorData data)
                {
                    var newTran = new Transaction
                    {
                        Id = message.StreamId,
                        Name = message.Name,
                        Credit = message.Credit
                    };

                    write.Save(newTran);

                    return Task.CompletedTask;
                    // throw new NotImplementedException();
                }
            }
        }

        public class Test
        {
            public class AddTransaction : TransactionAdded, ICommand { }
            public class TransactionAdded : Event
            {
                public string Name { get; set; }
                public int Credit { get; set; }
            }

            public class ChangeAmount : CreditChanged, ICommand { }

            public class CreditChanged : Event
            {
                public int Credit { get; set; }
            }




            public class Aggregate : AggregateBase,
                IHandleAggregateCommand<AddTransaction>,
                IHandleAggregateCommand<ChangeAmount>
            {
                public void Handle(AddTransaction command)
                {
                    Raise<TransactionAdded>(command);
                }

                public void Handle(ChangeAmount command)
                {
                    Raise<CreditChanged>(command);
                }





                private string name;
                private int credit;
                private int maxNumber;
                private void Apply(TransactionAdded e)
                {
                    Id = e.StreamId;
                    name = e.Name;
                    credit = e.Credit;
                }

                private void Apply(CreditChanged e)
                {
                    credit = e.Credit;
                }



            }

        }


        [Fact]
        public async Task ServiceCollection_GetDefaultImplementationAsync()
        {

            IServiceCollection services = new ServiceCollection();

            services.AddEventStore<LocalEventStore, LocalTransaction>(opts =>
            {
                opts.DomainAssemblyTypes = new[]
                {
                    typeof(EFDemo1.Domain.COURSE.CourseCreated),

                            };
                opts.ReadModelAssemblyTypes = new[]
                {
                   typeof(ReadModel.Course)
                            };

                opts.GetUserContext = (s => new CommandContext.User(Guid.NewGuid()));
            });


            services.AddScoped<ReadModelService>();
            services.AddScoped<IReadFromReadModel>(s => s.GetRequiredService<ReadModelService>());
            services.AddScoped<IWriteReadModel>(s => s.GetRequiredService<ReadModelService>());
            services.AddScoped<LiteDatabase>(s => new LiteDatabase(@"MyData.db"));
            services.AddScoped<DataStore>(s => new DataStore("data.json"));
            services.AddScoped<Domain.ICOURSERulesValidator, Domain.COURSERulesValidator>();
            var fS = services.BuildServiceProvider();

            // var num1 = fS.GetService<IProjectAsync<TestAggregate.AddTransaction>>();
            // var num2 = fS.GetService<IProjectAsync<TestAggregate.AmountChanged>>();
            // var id = CombGuid.NewGuid();
            var id = Guid.Parse("53a62c22-4dde-4854-a4ee-aa23018659ff");
            var commandProcessor = fS.GetRequiredService<IAsyncCommandProcessor>();
            // var result = await commandProcessor.ProcessAsync(new Test.AddTransaction
            // {
            //     StreamId = id,
            //     Credit = 20,
            //     Name = "Computing Fundamentals"
            // });

            // var result1 = await commandProcessor.ProcessAsync(new Test.ChangeAmount
            // {
            //     StreamId = id,
            //     Credit = 70
            // });

            var result3 = await commandProcessor.ProcessAsync(new Domain.COURSE.NewCourseRequest
            {
                StreamId = id,
                Name = "Web Development",
                Credit = 50,
                MaxNumber = 100
            });

            var reader = fS.GetRequiredService<IReadFromReadModel>();
            var transaction = reader.Get<Transaction>(id);

        }

    }
}
