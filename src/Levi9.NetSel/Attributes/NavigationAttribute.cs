using System;

namespace Levi9.NetSel.Attributes
{
    /// <summary>
    /// Page factory navigation attribute implementation.
    /// </summary>
    public sealed class NavigationAttribute : Attribute
    {
        /// <summary>
        /// Property BaseUrl.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Property Path.
        /// </summary>
        public string Path { get; set; }
    }
}