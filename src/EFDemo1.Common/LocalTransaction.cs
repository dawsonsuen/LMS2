using NEvilES;
using NEvilES.Pipeline;

namespace EFDemo1.Common
{
    public class LocalTransaction : CommandContext.TransactionBase
    {
        public LocalTransaction()
        {
            Id = CombGuid.NewGuid();
        }
    }
}