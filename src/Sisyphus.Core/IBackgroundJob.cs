using System.Threading.Tasks;

namespace Sisyphus.Core
{
    public interface IBackgroundJob
    {
        Task RunAsync();
    }
}