using Mimp.SeeSharper.Async;
using Mimp.SeeSharper.Async.Abstraction;
using System;
using System.Threading;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public static class StreamParentInfoExtensions
    {


        #region Create


        public static bool CanCreate(this IStreamParentInfo parent)
        {
            if (parent is null)
                throw new ArgumentNullException(nameof(parent));

            return parent is ICreateStreamParentInfo || parent is IAsyncCreateStreamParentInfo;
        }


        public static bool Create(this IStreamParentInfo parent)
        {
            if (parent is null)
                throw new ArgumentNullException(nameof(parent));

            return parent switch
            {
                ICreateStreamParentInfo create => create.Create(),
                IAsyncCreateStreamParentInfo create => create.CreateAsync().Await(),
                _ => throw ProvideIOException.GetNoCreateStreamParentException(parent)
            };
        }

        public static async IAwaitable<bool> CreateAsync(this IStreamParentInfo parent, CancellationToken cancellationToken)
        {
            if (parent is null)
                throw new ArgumentNullException(nameof(parent));

            return parent switch
            {
                IAsyncCreateStreamParentInfo create => await create.CreateAsync(cancellationToken),
                ICreateStreamParentInfo create => create.Create(),
                _ => throw ProvideIOException.GetNoCreateStreamParentException(parent)
            };
        }

        public static IAwaitable<bool> CreateAsync(this IStreamParentInfo parent) =>
            parent.CreateAsync(CancellationToken.None);



        #endregion Create


        #region Delete


        public static bool CanDelete(this IStreamParentInfo parent)
        {
            if (parent is null)
                throw new ArgumentNullException(nameof(parent));

            return parent is IDeleteStreamParentInfo || parent is IAsyncDeleteStreamParentInfo;
        }


        public static bool Delete(this IStreamParentInfo parent)
        {
            if (parent is null)
                throw new ArgumentNullException(nameof(parent));

            return parent switch
            {
                IDeleteStreamParentInfo create => create.Delete(),
                IAsyncDeleteStreamParentInfo create => create.DeleteAsync().Await(),
                _ => throw ProvideIOException.GetNoDeleteStreamParentException(parent)
            };
        }

        public static async IAwaitable<bool> DeleteAsync(this IStreamParentInfo parent, CancellationToken cancellationToken)
        {
            if (parent is null)
                throw new ArgumentNullException(nameof(parent));

            return parent switch
            {
                IAsyncDeleteStreamParentInfo create => await create.DeleteAsync(cancellationToken),
                IDeleteStreamParentInfo create => create.Delete(),
                _ => throw ProvideIOException.GetNoDeleteStreamParentException(parent)
            };
        }

        public static IAwaitable<bool> DeleteAsync(this IStreamParentInfo parent) =>
            parent.DeleteAsync(CancellationToken.None);


        #endregion Delete


    }
}
