using System;
using System.Threading;

namespace Caliburn.Micro
{
    internal class ContentLoaderBaseAsyncResult : IAsyncResult
    {
        public ContentLoaderBaseAsyncResult(object asyncState, LoaderBase loader, AsyncCallback callback)
        {
            AsyncState = asyncState;
            Loader = loader;
            Lock = new object();
            Callback = callback;
            AsyncWaitHandle = new AutoResetEvent(false);
        }

        #region IAsyncResult Members

        public object AsyncState { get; private set; }

        public WaitHandle AsyncWaitHandle { get; private set; }

        public bool CompletedSynchronously { get; private set; }

        public bool IsCompleted { get; private set; }

        #endregion

        internal bool BeginLoadCompleted { get; set; }
        internal AsyncCallback Callback { get; private set; }
        internal Exception Error { get; set; }
        internal LoaderBase Loader { get; private set; }
        internal object Lock { get; private set; }
        internal object Page { get; set; }
        internal Uri RedirectUri { get; set; }

        public void Complete()
        {
            CompletedSynchronously = !BeginLoadCompleted;
            IsCompleted = true;
            if(AsyncWaitHandle is AutoResetEvent)
                (AsyncWaitHandle as AutoResetEvent).Set();
        }
    }
}
