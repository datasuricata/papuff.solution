using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Services.Base {
    public interface IServiceBase {
        Task Commit();
        Task CommitForce();
    }
}
