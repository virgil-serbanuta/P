using Plang.Compiler.TypeChecker;
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
            return new List<CompiledFile> {};
        }
    }
}