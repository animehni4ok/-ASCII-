namespace Main_namespace
{
    class DevTools
    {
        /// <summary>
        /// метод для получение определенного колличеста табуляций
        /// </summary>
        /// <param name="tabcount">количество строк</param>
        /// <returns>указаное количество табуляций</returns>
        public static string GetTabs(int tabcount)
        {
            string returnedstring = "";
            for (int i = 0; i < tabcount; i++) returnedstring += "\t";
            return returnedstring;
        }

        /// <summary>
        /// метод для получения новых строк
        /// </summary>
        /// <param name="newlinecount">количество новых сток которые хочем получить</param>
        /// <returns>указаное количество новых строк</returns>
        public static string GetNewLine(int newlinecount)
        {
            string returnedstring = "";
            for (int i = 0; i < newlinecount; i++) returnedstring += "\n";
            return returnedstring;
        }
    }
}