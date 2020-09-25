using Plang.Compiler.TypeChecker;
using Plang.Compiler.TypeChecker.AST;
using Plang.Compiler.TypeChecker.AST.Declarations;
using System;
using System.Collections.Generic;

namespace Plang.Compiler.Backend.Rvm
{
    internal class MopCodeGenerator
    {
        private CompilationContext Context {get;}

        internal MopCodeGenerator(CompilationContext context)
        {
            Context = context;
        }

        public IEnumerable<CompiledFile> GenerateSources(Scope globalScope)
        {
            List<CompiledFile> sources = new List<CompiledFile>();

            foreach (IPDecl decl in globalScope.AllDecls)
            {
                switch (decl)
                {
                    case Machine machine:
                        if (machine.IsSpec)
                        {
                            sources.Add(WriteMonitor(machine));
                        }
                        else
                        {
                            sources.Add(WriteMachine(machine));
                        }

                        break;
                    default:
                        break;
                }
            }

            return sources;
        }

        CompiledFile WriteMonitor(Machine machine) {
            CompiledFile source = new CompiledFile(Context.MopFileName(machine.Name));
            return source;
        }

        CompiledFile WriteMachine(Machine machine) {
            throw new NotImplementedException();
        }
    }
}