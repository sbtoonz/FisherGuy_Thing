using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using ServerSync;
using UnityEngine;

namespace FisherGuy
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class FisherGuyMod : BaseUnityPlugin
    {
        private const string ModName = "Fisher Guy Mod";
        internal const string ModVersion = "1.0";
        private const string ModGUID = "com.zarboz.FisherGuyMod";
        private static Harmony harmony = null!;

        #region  ConfigSync
        ConfigSync configSync = new(ModGUID) 
            { DisplayName = ModName, CurrentVersion = ModVersion, MinimumRequiredVersion = ModVersion};
        internal static ConfigEntry<bool> ServerConfigLocked = null!;
        ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description, bool synchronizedSetting = true)
        {
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = configSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }
        ConfigEntry<T> config<T>(string group, string name, T value, string description, bool synchronizedSetting = true) => config(group, name, value, new ConfigDescription(description), synchronizedSetting);
        #endregion

        private static AssetBundle? _bundle;
        internal static GameObject? Njord;
        public void Awake()
        {
            _bundle = Utilities.LoadAssetBundle("fisherguy");
            if (_bundle == null) return;
            Njord = _bundle.LoadAsset<GameObject>("Njord");
            var assembly = Assembly.GetExecutingAssembly();
            harmony = new(ModGUID);
            harmony.PatchAll(assembly);
            ServerConfigLocked = config("1 - General", "Lock Configuration", true,
                "If on, the configuration is locked and can be changed by server admins only.");
            configSync.AddLockingConfigEntry(ServerConfigLocked);
            _bundle.Unload(false);
        }
    }
}