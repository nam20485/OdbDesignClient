namespace Odb.Client.Viewer
{
    public abstract class GlHandleObj : IDisposable
    {
        public int Handle { get; protected set; }

        private bool _disposed;

        protected abstract void Free();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // do nothing (free managed resources)
                }

                Free();
                Handle = -1;
                _disposed = true;
            }
        }

        ~GlHandleObj()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
