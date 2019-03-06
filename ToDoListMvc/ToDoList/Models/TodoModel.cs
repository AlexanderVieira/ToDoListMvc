using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace ToDoList.Models
{
    public class TodoModel : TableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool Completed { get; set; }

        public TodoModel()
        {
            
        }

        public TodoModel(int id, string name)
        {
            this.RowKey = id.ToString();
            this.PartitionKey = name;
        }
    }
}
