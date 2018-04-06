using System;
using System.Security.Cryptography;
using System.Text;

namespace WebCrawler
{
    /// <summary>
    /// The crawl extension.
    /// </summary>
    public static class CrawlExtension
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get unique identifier.
        /// </summary>
        /// <param name="urlInfo">
        /// The url info.
        /// </param>
        /// <returns>
        /// The <see cref="ulong"/>.
        /// </returns>
        public static ulong GetUniqueIdentifier(this UrlInfo urlInfo)
        {
            byte[] bytes = Encoding.Default.GetBytes(urlInfo.UrlString);

            var service = new MD5CryptoServiceProvider();
            byte[] hashValue = service.ComputeHash(bytes);

            return BitConverter.ToUInt64(hashValue, 0);
        }

        #endregion
    }
}
