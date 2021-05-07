using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Infrastructure.Player
{
    public static class SaveLoad
    {
        private static readonly string Path = Application.persistentDataPath + "/progress.data";
        
        public static void Save(ProgressData data)
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(Path, FileMode.Create);
            
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static ProgressData Load()
        {
            if (File.Exists(Path) == false)
                return null;
            
            var formatter = new BinaryFormatter();
            var stream = new FileStream(Path, FileMode.Open);

            var savedData = (ProgressData) formatter.Deserialize(stream);
            stream.Close();
            
            return savedData;
        }

        public static void Delete()
        {
            if (File.Exists(Path) == false)
                return;
            
            File.Delete(Path);
        }
    }
}