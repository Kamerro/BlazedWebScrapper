namespace BlazedWebScrapper.Data.Classes.Extension_methods
{
    public static class ListExtension
    {
        public static List<string> LeaveOnlyAuthorName(this List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = list[i].Replace("\n", "").Trim();
            }
            return list;
        }
    }
}
