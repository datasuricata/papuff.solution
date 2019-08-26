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
    public class ServiceEnterprise : ServiceApp<Company>, IServiceEnterprise {

        private readonly IRepository<Address> _repositoryAddress;

        public ServiceEnterprise(IServiceProvider provider, IRepository<Address> repositoryAddress) : base(provider) {
            _repositoryAddress = repositoryAddress;
        }

        public async Task Register(CompanyRequest request) {
            var company = new Company(request.Name, request.Email, request.SiteUri, request.CNPJ,
                request.Tell, request.Registration, request.OpeningDate, request.UserId);

            ValidEntity<CompanyValidator>(company);

            if (Notifier.IsValid)
                await _repository.RegisterAsync(company);
        }

        public async Task Address(AddressRequest request) {

            var current = _repositoryAddress.GetById(false, request.Id);

            if (current is null) {
                var address = new Address(request.Building, request.Number, request.Complement,
                    request.AddressLine, request.District, request.City, request.StateProvince,
                    request.Country, request.PostalCode, request.CompanyId, true);

                new AddressValidator().Validate(address);
                await _repositoryAddress.RegisterAsync(address);

            } else {
                current.Update(request.Building, request.Number, request.Complement,
                    request.AddressLine, request.District, request.City, request.StateProvince,
                    request.Country, request.PostalCode);

                _repositoryAddress.Update(current);
            }
        }

        public async Task<Company> GetById(string id) {
            return await _repository.GetByIdAsync(true, id);
        }

        public async Task<IEnumerable<Company>> GetByUser(string logged) {
            return await _repository.ListByAsync(true, c => c.UserId == logged);
        }

        public async Task<IEnumerable<Company>> GetCompanies() {
            return await _repository.ListByAsync(true, c => !c.IsDeleted);
        }
    }
}
