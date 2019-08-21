﻿using FluentValidation;
using papuff.domain.Core.Base;
using papuff.domain.Interfaces.Repositories;
using papuff.domain.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.services.Services.Base {
    public class ServiceApp<T> : ServiceBase, IServiceApp<T> where T : EntityBase {

        #region [ attributes ]

        protected readonly IRepository<T> repository;

        public ServiceApp(IServiceProvider provider) : base(provider) {
            this.repository = (IRepository<T>)provider.GetService(typeof(IRepository<T>));
        }

        #endregion

        #region [ persistance ]

        public void ValidUpdate<V>(T obj) where V : AbstractValidator<T> {
            Validate(obj, Activator.CreateInstance<V>());
            repository.Update(obj);
        }

        public void ValidManyRegisters<V>(List<T> entities) where V : AbstractValidator<T> {
            ValidateList(entities, Activator.CreateInstance<V>());
            repository.RegisterList(entities);
        }

        public void ValidRegister<V>(T obj) where V : AbstractValidator<T> {
            Validate(obj, Activator.CreateInstance<V>());
            repository.Register(obj);
        }

        public void ValidEntity<V>(T obj) where V : AbstractValidator<T> {
            Validate(obj, Activator.CreateInstance<V>());
        }

        #endregion

        #region [ validator ]

        private void Validate(T obj, AbstractValidator<T> validator) {
            if (obj == null) {
                Notifier.Add<ServiceBase>("Não validado.");
                return;
            }

            validator.ValidateAndThrow(obj);
        }

        private void ValidateList(List<T> obj, AbstractValidator<T> validator) {
            if (obj == null) {
                Notifier.Add<ServiceBase>("Não validado.");
                return;
            }

            foreach (var x in obj)
                validator.ValidateAndThrow(x);
        }

        #endregion
    }
}