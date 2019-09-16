using papuff.domain.Arguments.Companies;
using papuff.domain.Arguments.Users;
using papuff.domain.Core.Companies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Services.Core {
    public interface IServiceCompany {

        Task<Company> GetById(string id);
        Task<IEnumerable<Company>> GetByUser(string logged);
        Task<IEnumerable<Company>> GetCompanies();

        Task Register(CompanyRequest request);
        Task Address(AddressRequest request);
    }
}
