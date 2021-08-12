using Kernel.Domain.Model.Dtos;
using Kernel.Domain.Model.Entities;
using Kernel.Domain.Model.Enums;
using Kernel.Domain.Model.Providers;
using Kernel.Domain.Model.Validation;
using Kernel.Domain.Model.ValueObjects;
using Kernel.Domain.Repositories;
using Kernel.Domain.Validation;
using System.Threading.Tasks;

namespace Kernel.Domain.Services
{
    public class CrudService<T> : QueryService<T> where T : class, IAggregateRoot, new()
    {
        protected IUserProvider UserProvider { get; set; }
        protected Validator<T> Validator { get; set; }       

        public CrudService(
            ISessionFactory sessionFactory, 
            IUserProvider userProvider,
            Validator<T> validator) : base(sessionFactory)
        {
            UserProvider = userProvider;
            Validator = validator;
        }

        protected async Task<Token> GetToken() => await UserProvider.GetToken();

        public virtual async Task<ValidatorResult> Validate(T entity, ValidationType validationType, string userName)
        {
            var result = await Validator.Validate(entity, validationType, userName);
            if (result.HasErrors)
                throw new ValidatorException(result.Errors);
            return result;
        }

        public virtual async Task Insert(T entity)
        {
            var token = await GetToken();
            await Validator.Validate(entity, ValidationType.Insert, token.Email);

            using var session = SessionFactory.OpenSession();
            var repo = session.GetRepository<T>();

            if (entity is ITrackable trackeableEntity)
                trackeableEntity.TrackableInfo = new TrackableInfo(token.Email);

            if (entity is IActivable activableEntity)
                activableEntity.ActiveInfo = new ActiveInfo();

            if (entity is EntityKeySeq entityKeySeq)
                entityKeySeq.Key = await GetNextSequence(session);

            await repo.Insert(entity);
            session.SaveChanges();
        }

        public virtual async Task Update(T entity)
        {
            var token = await GetToken();
            await Validator.Validate(entity, ValidationType.Update, token.Email);

            using var session = SessionFactory.OpenSession();
            var repo = session.GetRepository<T>();

            if (entity is ITrackable trackeableEntity)
                trackeableEntity.TrackableInfo.Changed(token.Email);

            repo.Update(entity);
            session.SaveChanges();
        }

        public virtual async Task Delete(T entity)
        {
            var token = await GetToken();
            var result = await Validator.Validate(entity, ValidationType.Delete, token.Email);

            using var session = SessionFactory.OpenSession();
            var repo = session.GetRepository<T>();

            if (entity is IActivable activableEntity && !result.HasWarnings)
            {
                activableEntity.ActiveInfo.Deactivate();
                repo.Update(entity);
            }
            else
                repo.Delete(entity);

            if (entity is ITrackable trackeableEntity)
                trackeableEntity.TrackableInfo.Changed(token.Email);

            session.SaveChanges();
        }

        protected async Task<long> GetNextSequence(ISessionRepository session)
        {
            var entityName = typeof(T).Name;
            var repo = session.GetRepository<Sequence>();
            var seq = await repo.Get(entityName);
            if (seq == null)
            {
                seq = new Sequence { Key = entityName, NextId = 1 };
                await repo.Insert(seq);
            }
            var nextKey = seq.NextId;
            seq.NextId++;
            return nextKey;
        }
    }
}
