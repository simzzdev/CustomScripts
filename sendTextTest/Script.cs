using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomScripts
{
    public class ScriptSettings
    {
        public List<Script> Scripts;
        public ScriptSettings() { }
        public static ScriptSettings InitTest()
        {
            return new ScriptSettings()
            {
                Scripts = new List<Script>()
                {
                    new Script("test", "script1"),
                    new Script("test2", "words2"),
                    new Script("name1", "name of the stuff"),
                    new Script("sample data", "all this sample... yawn")
                }
            };
        }
    }

    public class Script
    {
        public string Name;
        public string Text;

        public Script() { }
        public Script(string name, string text)
        {
            this.Name = name;
            this.Text = text;
        }

        public override string ToString()
        {
            return this.Name + ": " + this.Text.Substring(0, 20) + "...";
        }
        public bool Search(string searchStr)
        {
            searchStr = searchStr.ToLower();
            return (this.Name.ToLower().Contains(searchStr) || this.Text.ToLower().Contains(searchStr));
        }
    }
}
