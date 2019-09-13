using papuff.domain.Arguments.Generals;
using papuff.domain.Core.Generals;
using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Services.Core {
    public interface IServiceGeneral {
        Task<General> GetById(string id);
        Task<General> GetByUser(string logged);
        Task General(GeneralRequest request);
    }
}
