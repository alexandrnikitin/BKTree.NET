using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BKTree.NET.Sandbox.Utils
{
    public static class ResourcesUtils
    {
        public static IEnumerable<string> Get(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }
    }
}