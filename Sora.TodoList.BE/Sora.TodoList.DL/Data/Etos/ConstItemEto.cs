using System.Collections.Generic;

namespace Sora.TodoList.DL.Data.Etos
{
    public class ConstItemEto
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string ExtraPropertiesStr { get; set; }

        public Dictionary<string, string> ExtraProperties { get; set; } = new Dictionary<string, string>();
    }
}