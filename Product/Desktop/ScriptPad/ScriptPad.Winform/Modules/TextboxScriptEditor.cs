using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptPad.Components;

namespace ScriptPad.Modules
{
    public class TextboxScriptEditor : TextBox, IScriptEditor
    {
        public string GetCode()
        {
            return Text;
        }

        public object Run()
        {
            throw new NotImplementedException();
        }

        public T Run<T>()
        {
            try
            {
                return (T) Run();
            }
            catch
            {
                return default (T);
            }
        }
    }
    
}
