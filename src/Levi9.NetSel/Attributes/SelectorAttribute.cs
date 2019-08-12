using System;

namespace Levi9.NetSel.Attributes
{
    /// <summary>
    /// Page factory selector attribute implementation.
    /// </summary>
    public sealed class SelectorAttribute : Attribute
    {
        /// <summary>
        /// Property Type.
        /// </summary>
        public SelectorType Type { get; set; }

        /// <summary>
        /// Property Value.
        /// </summary>
        public string Value { get; set; }
    }
}