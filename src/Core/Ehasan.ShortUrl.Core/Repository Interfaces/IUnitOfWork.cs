﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace BUZ.Authorization.Core.Repository_Interfaces
{
    public interface IUnitOfWork : IReadOnlyUnitOfWork
    {
        /// <summary>
        /// Saves the changes for each registered context.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Saves the changes for each registered context.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Async task</returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
