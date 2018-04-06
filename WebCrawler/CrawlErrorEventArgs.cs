using System;

namespace WebCrawler
{
    /// <summary>
    /// The crawl error event handler.
    /// </summary>
    /// <param name="args">
    /// The args.
    /// </param>
    public delegate void CrawlErrorEventHandler(CrawlErrorEventArgs args);

    /// <summary>
    /// The crawl error event args.
    /// </summary>
    public class CrawlErrorEventArgs : EventArgs
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        #endregion
    }
}
