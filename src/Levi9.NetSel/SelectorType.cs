namespace Levi9.NetSel
{
    /// <summary>
    /// Element selector types.
    /// </summary>
    public enum SelectorType
    {
        /// <summary>
        /// Select element using Id.
        /// </summary>
        Id = 0,
        /// <summary>
        /// Select element using Name.
        /// </summary>
        Name = 1,
        /// <summary>
        /// Select element using TagName.
        /// </summary>
        TagName = 2,
        /// <summary>
        /// Select element using ClassName.
        /// </summary>
        ClassName = 3,
        /// <summary>
        /// Select element using CssSelector.
        /// </summary>
        CssSelector = 4,
        /// <summary>
        /// Select element using LinkText.
        /// </summary>
        LinkText = 5,
        /// <summary>
        /// Select element using PartialLinkText.
        /// </summary>
        PartialLinkText = 6,
        /// <summary>
        /// Select element using XPath.
        /// </summary>
        XPath = 7
    }
}