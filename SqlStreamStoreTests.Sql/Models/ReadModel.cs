using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlStreamStoreTests.Sql.Models
{
    public class ReadModel
    {
        public int Id { get; private set; }
        public string Value { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime LastModifiedOn { get; private set; }

        private ReadModel() { }

        public ReadModel(string value, DateTime createdOn)
        {
            Value = value;
            CreatedOn = createdOn;
            LastModifiedOn = createdOn;
        }

        public void UpdateValue(string newValue, DateTime modifiedOn)
        {
            Value = newValue;
            LastModifiedOn = modifiedOn;
        }
    }
}
