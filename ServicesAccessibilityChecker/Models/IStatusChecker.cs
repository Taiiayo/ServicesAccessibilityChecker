using System.Threading.Tasks;

namespace ServicesAccessibilityChecker.Models
{
    public interface IStatusChecker
    {
        Task<string> SendRequestAsync(int serviceId);
    }
}
