using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.IO;

namespace CodeCamp.WP7.Tools
{
    public static class IsoStore
    {
        public static void Save<T>(object o, string fileName)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            using (IsolatedStorageFileStream fs = new IsolatedStorageFileStream(fileName, FileMode.Create, isf))
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                ser.Serialize(fs, o);
                fs.Close();
            }
        }

        public static T Load<T>(string fileName)
        {
            T result = default(T);

            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                if (isf.FileExists(fileName))
                    using (IsolatedStorageFileStream fs = new IsolatedStorageFileStream(fileName, FileMode.Open, isf))
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(T));
                        result = (T)ser.Deserialize(fs);
                        fs.Close();
                    }

            return result;
        }

        public static void Delete(string fileName)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                if (isf.FileExists(fileName)) isf.DeleteFile(fileName);
        }

        public static bool FileExists(string fileName)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                return isf.FileExists(fileName);
        }
    }
}