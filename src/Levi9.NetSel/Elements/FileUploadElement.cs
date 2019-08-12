using System.IO;
using Levi9.NetSel.Proxies;

namespace Levi9.NetSel.Elements
{
    /// <summary>
    /// Contains methods for interaction with file upload elements.
    /// </summary>
    public class FileUploadElement : NetSelElement
    {
        /// <summary>
        /// Initializes a new instance of the FileUploadElement class.
        /// </summary>
        public FileUploadElement(NetSelElementProxy proxy) : base(proxy) { }

        /// <summary>
        /// Uploads a file.
        /// </summary>
        /// <param name="filePath">Document path.</param>
        public void UploadFile(string filePath)
        {
            var fullPath = Path.GetFullPath(filePath);
            WebElement.SendKeys(fullPath);
        }
	}
}