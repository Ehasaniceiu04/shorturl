
namespace Ehasan.ShortUrl.Business.Command
{
    public interface IUrlCommandFactory
    {
        IUrlCommand CreateCommand(string serviceName);
    }
}
