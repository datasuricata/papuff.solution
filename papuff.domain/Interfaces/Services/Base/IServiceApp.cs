using FluentValidation;
using papuff.domain.Core.Base;
using System.Collections.Generic;

namespace papuff.domain.Interfaces.Services.Base {
    public interface IServiceApp<T> where T : EntityBase {
        void ValidUpdate<V>(T obj) where V : AbstractValidator<T>;
        void ValidManyRegisters<V>(List<T> entities) where V : AbstractValidator<T>;
        void ValidRegister<V>(T obj) where V : AbstractValidator<T>;
        void ValidEntity<V>(T obj) where V : AbstractValidator<T>;
    }
}
