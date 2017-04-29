using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joy.Core;
using Roslyn.Scripting.CSharp;

namespace ScriptPad.Components
{
    public class ScriptExecutor
    {
        protected virtual string exectemplate
        {
            get
            {
                return @"
                    public {0} Method() 
                    {{ 
                        {1}
                        return rlt;
                    }}
                ";
            }
        }

        protected IScriptEditor edt;
        public ScriptExecutor(IScriptEditor editor)
        {
            edt = editor;
            
        }

        public virtual ExecutionResult Run()
        {
            
            var rlt = new ExecutionResult();
            try
            {
                string codes = edt.GetCode();
                string src = string.Format(exectemplate, "object", codes);
                var engine = new ScriptEngine();

                var session = engine.CreateSession();
                session.AddReference("System");
                session.AddReference("System.Core");

                //MessageBox.Show("ok");
                session.Execute(src);

                object r = session.Execute<object>("Method()");
                rlt.Result = r;
            }
            catch (Exception ex)
            {
                rlt.LastError = ex;
            }
            return rlt;
        }

        public T Run<T>()
        {
            try
            {
                var r = Run();
                if (r.IsNoException)
                {
                    return (T)r.Result;
                }
                return default (T);
            }
            catch (Exception error)
            {
                return default (T);
            }
        }   
    }

    public class ExecutionResult : ResultBase
    {
        
    }
}
