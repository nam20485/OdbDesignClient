

namespace Odb.Client.Viewer
{
    public abstract class GlHandleObj : IDisposable
    {
        public int Handle { get; protected set; }

        private bool _disposed;       

        protected GlHandleObj()            
        {           
        }        

        protected virtual void FreeManagedResources()
        {
        }

        protected virtual void FreeUnmanagedResources()
        {
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    FreeManagedResources();
                }

                FreeUnmanagedResources();
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
