using GameCreator.Editor.Installs;
using UnityEditor;

namespace GameCreator.Editor.Localization
{
    public static class UninstallLocalization
    {
        [MenuItem(
            itemName: "Game Creator/Uninstall/Localization",
            isValidateFunction: false,
            priority: UninstallManager.PRIORITY
        )]
        
        private static void Uninstall()
        {
            UninstallManager.Uninstall("Localization");
        }
    }
}