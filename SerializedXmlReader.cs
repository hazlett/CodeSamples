using System;
using System.IO;
using System.Xml.Serialization;

namespace GTMY.IO
{
    public class SerializedXmlReader : IReader
    {
        protected string path;  

        public SerializedXmlReader(string path)
        {
            this.path = path;
        }
        public SerializedXmlReader(string path, string defaultPath) : this(path)
        {
            if (!File.Exists(path))
            {
                CreateDefault(defaultPath);
            }
        }

        public T Read<T>() where T : new()
        {
            try
            {
                if (!File.Exists(path))
                {
                    return new T();                
                }

                return ReadData<T>();
            }
            catch
            {
                return new T();
            }
        }

        protected virtual T ReadData<T>()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return (T)serializer.Deserialize(stream);
            }
        }

        private void CreateDefault(string defaultPath)
        {
            string directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.Copy(defaultPath, path);
        }

        public void Dispose()
        {
            //Nothing to dispose
        }
    }
}
