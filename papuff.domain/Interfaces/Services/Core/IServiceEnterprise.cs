using papuff.domain.Arguments.Companies;
using papuff.domain.Core.Companies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Services.Core {
    public interface IServiceEnterprise {

        Task Register(CompanyRequest request);
        Task<Company> GetById(string id);
        Task<IEnumerable<Company>> GetByUser(string logged);
    }
}
