using AutoDoomFramework.Common.Tools;
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
        // Registry Id
        private string uId;
        [JsonPropertyName("UId")]
        public string UId { get; set; }

        // Registry Type
        private string type;
        [JsonPropertyName("Type")]
        public string Type { get; set; }

        // Registry name
        private string name;
        [JsonPropertyName("Name")]
        public string Name
        {
            get => name;
            set 
            {
                name = value;
                UId = MD5Encoder.Encode(name + Location);
            }
        }

        // Registry folder path
        private string location;
        [JsonPropertyName("Location")]
        public string Location
        {
            get => location;
            set
            {
                location = value;
                UId = MD5Encoder.Encode(Name + location);
            }
        }

        // Registry description
        private string description;
        [JsonPropertyName("Description")]
        public string Description
        {
            get => description;
            set => description = value;
        }

        public Registry() { }

        public Registry(string Name, string Location, string Type)
        {
            UId = MD5Encoder.Encode(Name + Location);
            this.Type = Type;
            this.Name = Name;
            this.Location = Location;
            this.Description = "";
        }

        public Registry(string Name, string Location, string Type, string Description)
        {
            UId = MD5Encoder.Encode(Name + Location);
            this.Type = Type;
            this.Name = Name;
            this.Location = Location;
            this.Description = Description;
        }


    }
}
