using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoDoomFramework.Models
{
    class Registry
    {
        // Registry name
        
        private string name;
        [JsonPropertyName("Name")]
        public string Name
        {
            get => name;
            set => name = value;
        }

        // Registry folder path
        private string location;
        [JsonPropertyName("Location")]
        public string Location
        {
            get => location;
            set => location = value;
        }

        // Registry description
        private string description;
        [JsonPropertyName("Description")]
        public string Description
        {
            get => description;
            set => description = value;
        }

        
        public Registry(string Name, string Location)
        {
            name = Name;
            location = Location;
            description = "";
        }
        
        [JsonConstructor]
        public Registry(string Name, string Location, string Description)
        {
            this.Name = Name;
            this.Location = Location;
            this.Description = Description;
        }


    }
}
