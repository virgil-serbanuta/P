using Plang.Compiler.TypeChecker.AST;
using Plang.Compiler.TypeChecker.AST.Declarations;
using Plang.Compiler.TypeChecker.Types;
using System.Collections.Generic;

namespace Plang.Compiler.Backend.Rvm
{
    internal class RvmNameManager : NameManagerBase
    {
        public RvmNameManager(string namePrefix) : base(namePrefix)
        {
        }

        protected override string ComputeNameForDecl(IPDecl decl)
        {
            string name = decl.Name;

            switch (decl)
            {
                case PEvent pEvent:
                    if (pEvent.IsNullEvent)
                    {
                        name = "DefaultEvent";
                    }

                    if (pEvent.IsHaltEvent)
                    {
                        name = "PHalt";
                    }

                    return name;

                case Interface _:
                    return "I_" + name;
                default:
                  name = string.IsNullOrEmpty(name) ? "Anon" : name;
                  if (name.StartsWith("$"))
                  {
                      name = "TMP_" + name.Substring(1);
                  }

                  return UniquifyName(name);
            }
        }

        public string GetMachineImplName(Machine m)
        {
            return $"Impl_{GetNameForDecl(m)}";
        }

        public string GetHandlerName(PEvent e)
        {
            return $"handle_{GetNameForDecl(e)}";
        }
    }
}