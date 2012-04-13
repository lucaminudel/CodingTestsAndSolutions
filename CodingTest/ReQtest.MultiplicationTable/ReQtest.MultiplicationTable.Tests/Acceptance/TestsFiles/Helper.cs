using System;
using System.IO;
using System.Reflection;

namespace ReQtest.MultiplicationTable.Tests.Acceptance.TestsFiles
{
    public class Helper
    {
        private class ResourceCleaner : IDisposable
        {
            private readonly string _filename;
            public ResourceCleaner(string filename)
            {
                _filename = filename;
            }

            public void Dispose()
            {
                File.Delete(_filename);
            }
        }

        public void DeleteFileIfExixts(string filename)
        {
            File.Delete(filename);
        }

        public bool FilesAreEqual(string expectedFilename, string actualFilename)
        {

            string expectedFileContent = File.ReadAllText(expectedFilename);

            string actualFileContent = File.ReadAllText(actualFilename);

            return 0 == String.Compare(expectedFileContent, actualFileContent, true);

        }

        public IDisposable ExtractResource(string resourceName)
        {
            string destination = resourceName;
            
            resourceName = GetType().Namespace + "." + resourceName;
            using (Stream embeddedResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                WriteResourceToFile(embeddedResourceStream, destination);
            }

            return new ResourceCleaner(destination);
        }

        public bool FileContains(string filename, string value)
        {
            string fileContent = File.ReadAllText(filename);

            return fileContent.Contains(value);
        }

        private static void WriteResourceToFile(Stream stream, string destination)
        {
            using (StreamWriter writer = File.CreateText(destination))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    writer.Write(reader.ReadToEnd());
                }
            }
        }
    }
}
