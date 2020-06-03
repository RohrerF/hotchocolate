using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace HotChocolate.Execution.Utilities
{
    /// <summary>
    ///  The task queue stores <see cref="ResolverTask"/> in a queue. 
    /// </summary>
    internal interface ITaskQueue
    {        
        /// <summary>
        /// Try to dequeue a element from the queue
        /// </summary>
        /// <param name="task">
        /// The dequeued task. Is not null when the method returns<c>true</c>
        /// </param>
        /// <returns>Return <c>true</c> if there was an element to dequeue</returns>
        bool TryDequeue([NotNullWhen(true)] out ResolverTask? task);

        /// <summary>
        /// Initializes a <see cref="ResolverTask"/> and enqueues it.
        /// </summary>
        void Enqueue(
            IOperationContext operationContext,
            IPreparedSelection selection,
            int responseIndex,
            ResultMap resultMap,
            object? parent,
            Path path,
            IImmutableDictionary<string, object?> scopedContextData);

        /// <summary>
        /// Clears the queue and returns all the <see cref="ResolverTask"/> instances to the pool.
        /// </summary>
        void Clear();
    }
}
