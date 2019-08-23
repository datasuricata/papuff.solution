using papuff.domain.Arguments.Companies;
using papuff.domain.Core.Companies;
using papuff.domain.Interfaces.Services.Core;
using papuff.services.Services.Base;
using papuff.services.Validators.Core.Companies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.services.Services.Core {
    public class ServiceEnterprise : ServiceApp<Company>, IServiceEnterprise {

        public ServiceEnterprise(IServiceProvider provider) : base(provider) {
        }

        public async Task Register(CompanyRequest request) {
            var company = new Company(request.Name, request.Email, request.SiteUri, request.CNPJ,
                request.Tell, request.Registration, request.OpeningDate, request.UserId);

            ValidEntity<CompanyValidator>(company);

            if (Notifier.IsValid)
                await repository.RegisterAsync(company);
        }

        public async Task<Company> GetById(string id) {
            return await repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Company>> GetByUser(string logged) {
            return await repository.ListAsync(c => c.UserId == logged);
        }
    }
}
