using Newtonsoft.Json;
using System;
using System.IO;
using static Main_namespace.Game;
using static Main_namespace.MainClass;

namespace Main_namespace
{
    class Saves
    {
        public static void LoadGameSaves()
        {
            if (!ChekIntegritySaves())
            {
                RevairGameSaves();
                return;
            }
            StreamReader r = new(inventorypath);
            string EJinventory;
            EJinventory = r?.ReadLine() ?? "";
            r?.Dispose();
            string Jinventory = EJinventory.DecryptStringFromBytes_Aes(EKey, EIv);
            StreamReader r1 = new(settingspath);
            string EJsettings;
            EJsettings = r1?.ReadLine() ?? "";
            r1?.Dispose();
            string Jsettings = EJsettings.DecryptStringFromBytes_Aes(EKey, EIv);
            settings = (JsonConvert.DeserializeObject<Settings>(Jsettings) ?? settings);
            language = settings?.Language ?? "";
            if (((int)settings.ConsoleColor) <= 15) Console.ForegroundColor = settings.ConsoleColor;
            if (Console.ForegroundColor == 0) Console.ForegroundColor = ConsoleColor.White;
            inventory = (JsonConvert.DeserializeObject<Inventory>(Jinventory) ?? inventory);
            gold = inventory?.Gold ?? 0;
            stones = inventory?.Stones ?? 0;
        }

        public static void SaveGameSaves()
        {
            if (!ChekIntegritySaves())
            {
                RevairGameSaves();
                return;
            }
            inventory.Gold = gold;
            inventory.Stones = stones;
            settings.ConsoleColor = Console.ForegroundColor;
            settings.Language = language;
            string Jinventory = JsonConvert.SerializeObject(inventory);
            string Jsettings = JsonConvert.SerializeObject(settings);
            byte[] Einventory = Jinventory.EncryptStringToBytes_Aes(EKey, EIv);
            using (StreamWriter w = new(inventorypath)) w.WriteLine(Convert.ToBase64String(Einventory));
            byte[] Esettings = Jsettings.EncryptStringToBytes_Aes(EKey, EIv);
            using (StreamWriter w = new(settingspath)) w.WriteLine(Convert.ToBase64String(Esettings));
        }

        private static void RevairGameSaves()
        {
            if (!Directory.Exists(gamefloaderpath))
            {
                Directory.CreateDirectory(gamefloaderpath);
                File.Create(settingspath).Dispose();
                File.Create(inventorypath).Dispose();
            }
            else
            {
                Directory.Delete(gamefloaderpath, true);
                Directory.CreateDirectory(gamefloaderpath);
                File.Create(settingspath).Dispose();
                File.Create(inventorypath).Dispose();
            }
            gold = 0;
            stones = 0;
            Console.ForegroundColor = ConsoleColor.White;
            inventory.Stones = 0;
            inventory.Gold = 0;
            settings.ConsoleColor = ConsoleColor.White;
            SaveGameSaves();
        }

        public static bool ChekIntegritySaves()
        {
            if (Directory.Exists(gamefloaderpath) && File.Exists(inventorypath) && File.Exists(settingspath)) return true;
            return false;
        }
    }
}
