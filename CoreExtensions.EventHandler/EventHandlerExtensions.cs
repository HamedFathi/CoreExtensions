using System;

namespace CoreExtensions
{
    public static class EventHandlerExtensions
    {
        /// <summary>
        ///     An EventHandler extension method that raises.
        /// </summary>
        /// <param name="handler">The handler to act on.</param>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event information.</param>
        public static void Raise(this EventHandler handler, object sender, EventArgs e)
        {
            handler?.Invoke(sender, e);
        }

        /// <summary>
        ///     Method for invoking/raising events.
        /// </summary>
        /// <param name="handler">Required. The EventHandler to Invoke.</param>
        /// <param name="sender">Required.</param>
        public static void Raise(this EventHandler handler, object sender)
        {
            handler.Raise(sender, EventArgs.Empty);
        }

        /// <summary>
        ///     Method for invoking/raising events.
        /// </summary>
        /// <param name="handler">Required. The EventHandler to Invoke.</param>
        /// <param name="sender">Required.</param>
        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> handler, object sender)
                    where TEventArgs : EventArgs
        {
            handler.Raise(sender, Activator.CreateInstance<TEventArgs>());
        }

        /// <summary>
        ///     Method for invoking/raising events.
        /// </summary>
        /// <param name="handler">Required. The EventHandler to Invoke.</param>
        /// <param name="sender">Required.</param>
        /// <param name="e">Required.</param>
        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e)
                    where TEventArgs : EventArgs
        {
            handler?.Invoke(sender, e);
        }

        /// <summary>
        ///     An EventHandler extension method that raises the event event.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="sender">Source of the event.</param>
        public static void RaiseEvent(this EventHandler @this, object sender)
        {
            @this?.Invoke(sender, null);
        }

        /// <summary>
        ///     An EventHandler&lt;TEventArgs&gt; extension method that raises the event event.
        /// </summary>
        /// <typeparam name="TEventArgs">Type of the event arguments.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="sender">Source of the event.</param>
        public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> @this, object sender) where TEventArgs : EventArgs
        {
            @this?.Invoke(sender, Activator.CreateInstance<TEventArgs>());
        }

        /// <summary>
        ///     An EventHandler&lt;TEventArgs&gt; extension method that raises the event event.
        /// </summary>
        /// <typeparam name="TEventArgs">Type of the event arguments.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event information to send to registered event handlers.</param>
        public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> @this, object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            @this?.Invoke(sender, e);
        }
    }
}
