using static Main_namespace.Game;
using static Main_namespace.DevTools;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Main_namespace
{
    class Scenes
    {
        /// <summary>
        /// массив сцен
        /// </summary>
        public static readonly string[] SCm = { "", "", ""};
        /// <summary>
        /// массив вторичных строк
        /// </summary>
        public static readonly string[] SLm = { "", "", "", "", ""};

        public static void СhangeLanguage(string language)
        {
            switch (language)
            {
                case "ru":
                    SCm[(int)SC.Main] = $"{GetNewLine(10)}{GetTabs(9)}привет, ты попал в тестовою ASCII игру{GetNewLine(2)}{GetTabs(8)}    управлние: S - настройки, пробел - начать игру\n\n{GetTabs(7)}       управление в игре: пробел - 1 ход, I - инвентарь, S - магазин \n\n\n{GetTabs(8)}что бы вернутся (к примеру с настроек) нужно нажать BackSpace";
                    SCm[(int)SC.Settings] = $"{GetNewLine(9)}{GetTabs(9)}вот доступные: цвета белый, феолетовый, красный (выбор по числам){GetNewLine(2)}{GetTabs(9)}на L можно выбрать язык, 1 - ru, 2 - en";
                    SCm[(int)SC.Shop] = $"у вас есть: камни: {stones} , золото: {gold} " + (sword == true ? " у вас есть меч, " : "") + $"\n\t (для покупки товаров нажимайте на указаные цифры)\n{GetTabs(2)}   меч - 1";
                    SLm[(int)SL.stonefound] = "вы нашли камушек";
                    SLm[(int)SL.inventory] = $" камушки {stones}, золото {gold}  " + (sword == true ? "у вас есть меч" : "");
                    SLm[(int)SL.revairingstart] = "Востановление сохранения...";
                    SLm[(int)SL.revairingcompleted] = "Востановление завершено... (нажми Enter для продолжения)";
                    break;
                default:
                case "en":
                    SCm[(int)SC.Main] = $"{GetNewLine(10)}{GetTabs(9)}hello, you are in a test ASCII game{GetNewLine(2)}{GetTabs(9)}control: S - setings, SpaceBar - start game\n\n{GetTabs(9)}control in game: spacebar - 1 move, I - inventory, S - shop\n{GetTabs(9)}(to go back (for example from settings) you need to press BackSpace)";
                    SCm[(int)SC.Settings] = $"{GetNewLine(9)}{GetTabs(9)}Here are the available colors: white, purple, red (choice by number){GetNewLine(2)}{GetTabs(9)}on L you can select language, 1 - ru, 2 - en";
                    SCm[(int)SC.Shop] = $"you have: stones: {stones} , gold: {gold} " + (sword == true ? " you have sword, " : "");
                    SLm[(int)SL.stonefound] = "you found stone";
                    SLm[(int)SL.inventory] = $" stones: {stones}, gold {gold}  " + (sword == true ? "you have a sword" : "");
                    SLm[(int)SL.revairingstart] = "Revairing game data...";
                    SLm[(int)SL.revairingcompleted] = "Revairing completed... (press enter to continue)";
                    break;
            }
        }

        public static async void Reload()
        {
            await Task.Run(() => 
            {
                switch (language)
                {
                    case "ru":
                        SCm[(int)SC.Shop] = $" у вас есть: камни: {stones} , золото: {gold} " + (sword == true ? " у вас есть меч, " : "") + $"\n\t (для покупки товаров нажимайте на указаные цифры)\n{GetTabs(2)}   меч - 1";
                        SLm[(int)SL.inventory] = $" камушки {stones}, золото {gold}  " + (sword == true ? "у вас есть меч" : "");
                        break;
                    case "en":
                    default:
                        SCm[(int)SC.Shop] = $" you have: stones: {stones} , gold: {gold} " + (sword == true ? " you have sword, " : "") + $"\n\t (to purchase goods, press on the indicated numbers)\n{GetTabs(2)}   sword - 1"; ;
                        SLm[(int)SL.inventory] = $" stones {stones}, gold {gold}  " + (sword == true ? "you have a sword" : "");
                        break;
                }
            });
        }

        public static string Constructor(int item, int currency, int costs)
        {
            string ret;
            if (language == "ru") ret = " вы действительно хотите купить? для покупки нажмите Y ( ";
            else ret = " do you really want to buy? for buy press Y ( ";
            switch (language)
            {
                case "ru":
                    switch (item)
                    {
                        case (int)Items.Sword:
                            switch (currency)
                            {
                                case (int)Currency.Stones:
                                    ret += $"меч стоит {costs} камней";
                                    break;
                                case (int)Currency.Gold:
                                    ret += $"меч стоит {costs} золота";
                                    break;
                                default:
                                    ret += $"err";
                                    break;
                            }
                            break;
                        default:
                            ret += "err";
                            break;
                    }
                    break;
                default:
                case "en":
                    switch (item)
                    {
                        case (int)Items.Sword:
                            switch (currency)
                            {
                                case (int)Currency.Stones:
                                    ret += $"sword costs {costs} stones";
                                    break;
                                case (int)Currency.Gold:
                                    ret += $"sword costs {costs} golds";
                                    break;
                                default:
                                    ret += $"err";
                                    break;
                            }
                            break;
                        default:
                            ret += "err";
                            break;
                    }
                    break;
            }
            return ret + " )";
        }

    }    
}
