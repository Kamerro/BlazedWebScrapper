namespace BlazedWebScrapper.Data
{
    public class Client
    {
        public string ID { get => _id; set=> _id = value; }
        public string Name { get => _name; set=> _name = value; }
        private string _id;
        private string _name;   

    }
}
