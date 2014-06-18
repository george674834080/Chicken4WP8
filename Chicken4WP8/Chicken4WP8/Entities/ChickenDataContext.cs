﻿using System.Data.Linq;

namespace Chicken4WP8.Entities
{
    public class ChickenDataContext : DataContext
    {
        private const string connectionString = @"isostore:/ChickenDataContext.sdf";

        public ChickenDataContext()
            : base(connectionString)
        { }

        public Table<Setting> Settings
        {
            get
            {
                return this.GetTable<Setting>();
            }
        }
    }
}