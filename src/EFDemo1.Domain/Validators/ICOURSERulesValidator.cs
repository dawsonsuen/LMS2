namespace EFDemo1.Domain
{
    public interface ICOURSERulesValidator
    {
        bool Exists();
    }
    public class COURSERulesValidator : ICOURSERulesValidator
    {
        public bool Exists() => false;

    }
}