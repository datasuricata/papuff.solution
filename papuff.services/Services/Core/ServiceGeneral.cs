using papuff.domain.Arguments.Generals;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Generals;
using papuff.domain.Interfaces.Repositories;
using papuff.domain.Interfaces.Services.Core;
using papuff.services.Services.Base;
using papuff.services.Validators.Core.Generals;
using System;
using System.Threading.Tasks;

namespace papuff.services.Services.Core {
    public class ServiceGeneral : ServiceBase, IServiceGeneral {

        private readonly IRepository<General> _repoGeneral;

        public ServiceGeneral(IRepository<General> repoGeneral, IServiceProvider provider) : base(provider) {
            _repoGeneral = repoGeneral;
        }

        public async Task<General> GetById(string id) {
            return await _repoGeneral.ById(true, id);
        }

        public async Task<General> GetByUser(string id) {
            return await _repoGeneral.By(true, u => u.UserId == id);
        }

        public async Task General(GeneralRequest request) {

            var current = await _repoGeneral.By(false, u => u.UserId == request.UserId);

            if (current is null) {
                var general = new General(request.BirthDate, request.Name,
                    request.Description, CurrentStage.Pending, request.UserId);

                new GeneralValidator().Validate(general);
                await _repoGeneral.Register(general);
            } else {
                current.Update(request.BirthDate, request.Name,
                    request.Description);
                _repoGeneral.Update(current);
            }
        }
    }
}