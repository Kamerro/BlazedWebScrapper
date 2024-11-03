using System.Drawing;

namespace BlazedWebScrapper.Data.Classes.Configuration
{
    public class TabConfigurator
    {
        public string TileColor
        {
            get => _tileColor.Name;
            set
            {
                _tileColor = Color.FromName(value);
            }
        }
        private Color _tileColor;
    }
}
