using Plang.Compiler.TypeChecker;
using System.Collections.Generic;

namespace Plang.Compiler.Backend.Rvm
{
    public class RvmCodeGenerator : ICodeGenerator
    {
        public IEnumerable<CompiledFile> GenerateCode(ICompilationJob job, Scope globalScope)
        {
            return new List<CompiledFile> { };
        }
    }
}