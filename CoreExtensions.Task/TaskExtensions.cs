using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace CoreExtensions
{
    /// <summary>
    ///     LINQ extension methods for the TPL-'Task' monad.
    /// </summary>
    /// <remarks>
    ///     A C# 4.0 implementation of http://blogs.msdn.com/b/pfxteam/archive/2013/04/03/tasks-monads-and-linq.aspx
    /// </remarks>
    public static class TaskExtensions
    {
        private static void AddAsyncStackTrace(Exception ex, string callerMemberName, string callerFilePath,
                            int callerLineNumber = 0)
        {
            if (!(ex.Data["_AsyncStackTrace"] is IList<string> trace))
                trace = new List<string>();
            trace.Add(string.Format("@{0}, in '{1}', line {2}", callerMemberName, callerFilePath, callerLineNumber));
            ex.Data["_AsyncStackTrace"] = trace;
        }

        public static async Task AsyncTrace(
                            this Task task,
                            [CallerMemberName] string callerMemberName = null,
                            [CallerFilePath] string callerFilePath = null,
                            [CallerLineNumber] int callerLineNumber = 0)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                AddAsyncStackTrace(ex, callerMemberName, callerFilePath, callerLineNumber);
                throw;
            }
        }

        public static async Task<T> AsyncTrace<T>(
                            this Task<T> task,
                            [CallerMemberName] string callerMemberName = null,
                            [CallerFilePath] string callerFilePath = null,
                            [CallerLineNumber] int callerLineNumber = 0)
        {
            try
            {
                return await task;
            }
            catch (Exception ex)
            {
                AddAsyncStackTrace(ex, callerMemberName, callerFilePath, callerLineNumber);
                throw;
            }
        }

        public static Task<V> GroupJoin<T, U, K, V>(
                            this Task<T> source, Task<U> inner,
                            Func<T, K> outerKeySelector, Func<U, K> innerKeySelector,
                            Func<T, Task<U>, V> resultSelector)
        {
            return source.TaskBind(t =>
            {
                return resultSelector(
                    t,
                    inner.Where(u =>
                        EqualityComparer<K>.Default.Equals(
                            outerKeySelector(t),
                            innerKeySelector(u)
                            )
                        )
                    ).TaskUnit();
            }
                );
        }

        public static Task<V> Join<T, U, K, V>(
                            this Task<T> source, Task<U> inner,
                            Func<T, K> outerKeySelector, Func<U, K> innerKeySelector,
                            Func<T, U, V> resultSelector)
        {
            Task.WaitAll(source, inner);

            return source.TaskBind(t =>
            {
                return inner.TaskBind(u =>
                {
                    if (!EqualityComparer<K>.Default.Equals(outerKeySelector(t), innerKeySelector(u)))
                        throw new OperationCanceledException();

                    return resultSelector(t, u).TaskUnit();
                }
                    );
            }
                );
        }

        public static Task<U> Select<T, U>(
                            this Task<T> source, Func<T, U> selector)
        {
            return source.TaskBind(t => selector(t).TaskUnit());
        }

        public static Task<C> SelectMany<A, B, C>(
                            this Task<A> monad,
                            Func<A, Task<B>> function,
                            Func<A, B, C> projection)
        {

            return monad.TaskBind(
                outer => function(outer).TaskBind(
                    inner => projection(outer, inner).TaskUnit()));
        }

        public static Task<V> TaskBind<U, V>(
                            this Task<U> m, Func<U, Task<V>> k)
        {
            return m.ContinueWith(m_ => k(m_.Result)).Unwrap();
        }

        public static Task<T> TaskUnit<T>(this T value)
        {
            return Task.Factory.StartNew(() => value);
        }

        public static bool WaitCancellationRequested(
                            this CancellationToken token,
                            TimeSpan timeout)
        {
            return token.WaitHandle.WaitOne(timeout);
        }

        public static Task<T> Where<T>(
                            this Task<T> source, Func<T, bool> predicate)
        {
            return source.TaskBind(t =>
            {
                if (!predicate(t))
                    throw new OperationCanceledException();

                return t.TaskUnit();
            }
                );
        }
    }
}
