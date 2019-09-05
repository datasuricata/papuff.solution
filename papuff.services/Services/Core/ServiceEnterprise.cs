using papuff.domain.Arguments.Companies;
using papuff.domain.Arguments.Users;
using papuff.domain.Core.Companies;
using papuff.domain.Core.Users;
using papuff.domain.Interfaces.Repositories;
using papuff.domain.Interfaces.Services.Core;
using papuff.services.Services.Base;
using papuff.services.Validators.Core.Companies;
using papuff.services.Validators.Core.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.services.Services.Core {
    public class ServiceEnterprise : ServiceBase, IServiceEnterprise {

        private readonly IRepository<Company> _repoCompany;
        private readonly IRepository<Address> _repoAddress;

        public ServiceEnterprise(IServiceProvider provider, 
            IRepository<Address> repoAddress,
            IRepository<Company> repoCompany) : base(provider) {
            _repoAddress = repoAddress;
            _repoCompany = repoCompany;
        }

        public async Task Register(CompanyRequest request) {
            var company = new Company(request.Name, request.Email, request.SiteUri, request.CNPJ,
                request.Tell, request.Registration, request.OpeningDate, request.UserId);

            _notify.Validate(company, new CompanyValidator());

            if (_notify.IsValid)
                await _repoCompany.Register(company);
        }

        public async Task Address(AddressRequest request) {

            var current = await _repoAddress.ById(false, request.Id);

            if (current is null) {
                var address = new Address(request.Building, request.Number, request.Complement,
                    request.AddressLine, request.District, request.City, request.StateProvince,
                    request.Country, request.PostalCode, request.CompanyId, true);

                new AddressValidator().Validate(address);
                await _repoAddress.Register(address);

            } else {
                current.Update(request.Building, request.Number, request.Complement,
                    request.AddressLine, request.District, request.City, request.StateProvince,
                    request.Country, request.PostalCode);

                _repoAddress.Update(current);
            }
        }

        public async Task<Company> GetById(string id) {
            return await _repoCompany.ById(true, id);
        }

        public async Task<IEnumerable<Company>> GetByUser(string logged) {
            return await _repoCompany.ListBy(true, c => c.UserId == logged);
        }

        public async Task<IEnumerable<Company>> GetCompanies() {
            return await _repoCompany.ListBy(true, c => !c.IsDeleted);
        }
    }
}