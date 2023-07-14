using System;
using System.Threading;
using static Main_namespace.Scenes;
using static Main_namespace.Saves;
using static Main_namespace.Game;
using System.Threading.Tasks;

namespace Main_namespace
{
    class MainClass
    {
        public static byte[] EKey = Convert.FromBase64String("9wXV4S05iIZI2QFuk9LjczO3zo/60KFj7n3dJiz6Y0c=");
        public static byte[] EIv = Convert.FromBase64String("cJtORFrIZVAbP9tbG0rlfQ==");

        static void Main()
        {
            Start();
            Update();
            while (true)
            {
                Console.SetBufferSize(250, 150);
                Console.SetWindowSize(200, 50);
                Thread.Sleep(1000);
                Reload();
            }
        }

        public static void Start()
        {
            СhangeLanguage(language);
            LoadGameSaves();
            if (language == "" || language == null)
            {
                ConsoleKey key;
                bool whilebool = true;
                while (whilebool)
                {
                    Console.WriteLine("select language, 1 - RU, 2 - ENG");
                    key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            language = "ru";
                            whilebool = false;
                            break;
                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            language = "en";
                            whilebool = false;
                            break;
                        default:
                            Console.WriteLine("you didn't choose a language");
                            break;
                    }
                }
            }
            SaveGameSaves();
            СhangeLanguage(language ?? "");
            Console.Clear();
            Console.WriteLine(SCm[(int)SC.Main]);
        }

        public static async void Update()
        {
            await Task.Run(() =>
            {
                ConsoleKey key;
                while (true)
                {
                    key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.S:
                            Settings();
                            break;
                        case ConsoleKey.Spacebar:
                            StartGame();
                            break;
                    }
                }
            });
        }
    }

    class Game
    {
        public static readonly string gamefloaderpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ASCII Game";
        public static readonly string settingspath = gamefloaderpath + @"\settings.ASGame";
        public static readonly string inventorypath = gamefloaderpath + @"\inventory.ASGame";
        public static long gold;
        public static long stones;
        public static string? language;
        public static bool sword;
        public static Settings settings = new();
        public static Inventory inventory = new();

        public static void Settings()
        {
            Console.Clear();
            Console.WriteLine(SCm[(int)SC.Settings]);
            ConsoleKey key;
            bool whilebool = true;
            while (whilebool)
            {
                key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.L:
                        bool whilelanguagebool = true;
                        while (whilelanguagebool)
                        {
                            key = Console.ReadKey().Key;
                            switch (key)
                            {
                                case ConsoleKey.D1:
                                case ConsoleKey.NumPad1:
                                    language = "ru";
                                    break;
                                case ConsoleKey.D2:
                                case ConsoleKey.NumPad2:
                                    language = "en";
                                    break;
                                case ConsoleKey.Backspace:
                                    whilelanguagebool = false;
                                    break;
                            }
                        }
                        SaveGameSaves();
                        СhangeLanguage(language ?? "");
                        break;
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case ConsoleKey.Backspace:
                        whilebool = false;
                        break;
                }
            }
            Console.Clear();
            SaveGameSaves();
            Console.WriteLine(SCm[(int)SC.Main]);
        }

        public static void Shop()
        {
            Reload();
            Console.WriteLine(SCm[(int)SC.Shop]);
            ConsoleKey key;
            bool whilebool = true;
            while (whilebool)
            {
                key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Backspace:
                        whilebool = false;
                        break;
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        if (!sword)
                        {
                            if (stones != 25)
                            {
                                Console.WriteLine(Constructor((int)Items.Sword, (int)Currency.Stones, 25));
                                key = Console.ReadKey().Key;
                                if (key == ConsoleKey.Y)
                                {
                                    stones -= 25;
                                    sword = true;
                                    switch (language)
                                    {
                                        case "ru":
                                            Console.WriteLine(" вы купили меч");
                                            break;
                                        default:
                                        case "en":
                                            Console.WriteLine(" you buy sword");
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                switch (language)
                                {
                                    case "ru":
                                        Console.WriteLine(" у вас не хватает камней");
                                        break;
                                    default:
                                    case "en":
                                        Console.WriteLine(" you don't have enough stones");
                                        break;
                                }
                            }
                        }
                        else
                        {
                            switch (language)
                            {
                                case "ru":
                                    Console.WriteLine(" у вас уже куплен меч");
                                    break;
                                default:
                                case "en":
                                    Console.WriteLine(" you already bought a sword");
                                    break;
                            }
                        }
                        break;
                }
            }
            SaveGameSaves();
            Console.Clear();
            Console.WriteLine(SCm[(int)SC.Main]);
        }

        public static void StartGame()
        {
            ConsoleKey key;
            bool whilebool = true;
            Random random = new();
            while (whilebool)
            {
                key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Spacebar:
                        if (random.Next(0, 100) <= 2)
                        {
                            Console.WriteLine(SLm[((int)SL.stonefound)]);
                            stones++;
                            SaveGameSaves();
                        }
                        break;
                    case ConsoleKey.Backspace:
                        whilebool = false;
                        break;
                    case ConsoleKey.S:
                        Shop();
                        break;
                    case ConsoleKey.I:
                        Console.WriteLine(SLm[(int)SL.inventory]);
                        break;
                }
            }
        }
    }
}
