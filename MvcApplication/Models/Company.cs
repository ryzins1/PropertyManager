﻿namespace MvcApplication.Models
{
    public class Company : Entity
    {
        public string Url { get; set; }
        public string BuildingsUrl { get; set; }
        public string LeasesUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}