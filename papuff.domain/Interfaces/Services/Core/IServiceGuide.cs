using papuff.domain.Arguments.Generals;
using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Services.Core {
    public interface IServiceGuide {
        Task Create(GuideRequest request);
    }
}
