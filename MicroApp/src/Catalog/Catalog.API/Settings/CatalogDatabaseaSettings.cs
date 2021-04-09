﻿namespace Catalog.API.Settings
{
    public class CatalogDatabaseaSettings : ICatalogDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
