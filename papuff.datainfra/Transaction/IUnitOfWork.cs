using System.Threading.Tasks;

namespace papuff.datainfra.Transaction {
    public interface IUnitOfWork {
        Task Commit();
    }
}
