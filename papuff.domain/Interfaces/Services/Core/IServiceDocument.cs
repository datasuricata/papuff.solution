using papuff.domain.Arguments.Users;
using papuff.domain.Core.Generals;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Services.Core {
    public interface IServiceDocument {
        Task<Document> GetById(string id);
        Task<IEnumerable<Document>> GetByUser(string logged);
        Task Create(DocumentRequest request);
        Task Update(DocumentRequest request);
        Task PadLock(string id);
        Task Delete(string id);
    }
}
