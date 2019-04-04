using System;
using System.Threading.Tasks;
using EFDemo1.Domain;
using JsonFlatFileDataStore;
using NEvilES.Pipeline;

namespace EFDemo1.ReadModel
{
    public class Course : IHaveIdentity
    {
        // This is CourseID
        public Guid Id { get; set; }
        //
        public string Name { get; set; }
        public int MaxNumber { get; set; }
        public int Credit { get; set; }
        // public string CourseCode { get; set; }
        // public string CourseDetail { get; set; }

        public class Projector : IProjectAsync<Domain.COURSE.CourseCreated>
        {
            private readonly DataStore _ds;

            public Projector(DataStore ds)
            {
                this._ds = ds;
            }

            public async Task ProjectAsync(Domain.COURSE.CourseCreated message, ProjectorData data)
            {

                var collection = _ds.GetCollection<CourseModel>();

                var item = new CourseModel
                {
                    Id = message.StreamId.ToString(),
                    Name = message.Name,
                    Credit = message.Credit,
                };

                await collection.InsertOneAsync(item);

                // throw new NotImplementedException();
            }

            public class CourseModel{
                public string Id { get;set;}
                public string Name {get;set;}
                public int Credit {get;set;}
            }
        }
    }
}
