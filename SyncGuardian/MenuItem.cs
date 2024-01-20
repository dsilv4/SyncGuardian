using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncGuardian
{   /// <summary>
    /// Menu entry
    /// </summary>
    public class MenuItem(string description, Action execute, int position)
    {
        public string Description { get; set; } = description;
        public Action Execute { get; set; } = execute;
        public int Position { get; set; } = position;
    }
}
