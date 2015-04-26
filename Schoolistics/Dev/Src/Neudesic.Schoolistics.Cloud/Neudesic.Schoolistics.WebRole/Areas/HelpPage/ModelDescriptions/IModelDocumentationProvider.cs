using System;
using System.Reflection;

namespace Neudesic.Schoolistics.WebRole.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}