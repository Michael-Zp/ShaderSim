using System;

namespace ShaderRenderer
{
    public abstract class Disposable : IDisposable
    {
        private bool disposed;

        /// <summary>
        /// Will be called from the default Dispose method.
        /// Implementers should dispose all their resources her.
        /// </summary>
        protected abstract void DisposeResources();

        /// <summary>Dispose status of the instance.</summary>
        public bool Disposed
        {
            get
            {
                return this.disposed;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed || !disposing)
                return;
            this.DisposeResources();
            this.disposed = true;
        }
    }
}
