using Infrastructure.Player;
using UnityEditor;

namespace Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear level data")]
        public static void ClearLevelData()
        {
            SaveLoad.Delete();
        }
    }
}
