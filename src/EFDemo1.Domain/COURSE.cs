using System;
using NEvilES;

namespace EFDemo1.Domain
{

    public abstract class COURSE
    {
        public class NewCourseRequest : CourseCreated, ICommand { }

        public class CourseCreated : Event
        {
            public string Name { get; set; }
            public int MaxNumber { get; set; }
            public int Credit { get; set; }
            public string CourseCode { get; set; }
            public string CourseDetail { get; set; }
        }

        public class Aggregate : AggregateBase,
            IHandleAggregateCommand<NewCourseRequest, ICOURSERulesValidator>
        {
            public void Handle(NewCourseRequest command, ICOURSERulesValidator validator)
            {

                if (validator.Exists()) throw new DomainAggregateException(this, "Course is already existed.");

                Raise<CourseCreated>(command);
            }

            private string Name;
            private int Credit;
            private void Apply(CourseCreated e)
            {
                Id = e.StreamId;
                Name = e.Name;
                Credit = e.Credit;
            }
        }
    }
}
