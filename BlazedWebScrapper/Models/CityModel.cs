using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper
{
	public class CityModel
	{
        public CityModel(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Code { get; set; }
        public string Name { get; set; }
    }
}
