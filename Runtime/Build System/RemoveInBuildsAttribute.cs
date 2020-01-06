using System;

namespace Hairibar.EngineExtensions
{
    /// <summary>
    /// Any component with this attribute will be removed at build time.
    /// Components must not have any extra serialized properties when UNITY_EDITOR is enabled. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RemoveInBuildsAttribute : Attribute
    {

    }
}