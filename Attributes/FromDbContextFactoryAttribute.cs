using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Agile.BaseLib.Extensions;
using Agile.BaseLib.Options;

namespace Agile.BaseLib.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class FromDbContextFactoryAttribute : Attribute
    {
        public string DbContextTagName { get; set; }

        public FromDbContextFactoryAttribute(string tagName)
        {
            DbContextTagName = tagName;
        }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class FromDbOptionAttribute : Attribute
    {
        public string TagName { get; set; }

        public FromDbOptionAttribute(string tagName)
        {
            TagName = tagName;
        }
    }

}
