using System;

namespace WebApp.Services.Assets.Models
{
    public class Asset
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Broker { get; set; }

        public string Category { get; set; }

        public string Currency { get; set; }
    }
}
