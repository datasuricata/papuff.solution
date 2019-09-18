using papuff.domain.Arguments.Users;
using papuff.domain.Core.Users;
using papuff.domain.Interfaces.Repositories;
using papuff.domain.Interfaces.Services.Core;
using papuff.services.Services.Base;
using papuff.services.Validators.Core.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.services.Services.Core {
    public class ServiceDocument : ServiceBase, IServiceDocument {

        private readonly IRepository<Document> _repoDocument;

        public ServiceDocument(IServiceProvider provider, IRepository<Document> repoDocument) : base(provider) {
            _repoDocument = repoDocument;
        }

        public async Task<Document> GetById(string id) {
            return await _repoDocument.ById(true, id);
        }

        public async Task<IEnumerable<Document>> GetByUser(string logged) {
            return await _repoDocument.ListBy(true, a => a.UserId == logged && !a.IsDeleted);
        }

        public async Task Create(DocumentRequest request) {
            var doc = new Document(request.Value, request.ImageUri, request.Type, request.UserId);
            _notify.When<ServiceDocument>(_repoDocument.Exist(d => d.Value == request.Value),
                "Já existe um documento com os mesmos dados");

            _notify.Validate(doc, new DocumentValidator());
            await _repoDocument.Register(doc);
        }

        public async Task Update(DocumentRequest request) {
            var doc = await _repoDocument.By(false, u => u.Id == request.Id);
            _notify.When<ServiceDocument>(doc == null, 
                "Documento não encontrado.");

            doc.Update(request.Value, request.ImageUri, request.Type);
            _repoDocument.Update(doc);
        }

        public async Task PadLock(string id) {
            var doc = await _repoDocument.ById(false, id);
            doc.PadLock();
            _repoDocument.Update(doc);
        }

        public async Task Delete(string id) {
            var doc = await _repoDocument.ById(false, id);
            doc.IsDeleted = true;
            _repoDocument.Update(doc);
        }
    }
}