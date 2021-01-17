using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace OmdbApi.Helpers
{
    public static class ReflectionHelper
    {
        public static string ReflectionReplace<T>(string template, T obj)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                var stringToReplace = typeof(T).Name + "." + property.Name;
                var value = property.GetValue(obj) ?? "";
                template = template.Replace(stringToReplace, value.ToString());
            }
            return template;
        }
    }
}
