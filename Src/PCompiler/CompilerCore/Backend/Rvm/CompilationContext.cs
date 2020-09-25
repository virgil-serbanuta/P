using Plang.Compiler.TypeChecker.AST.Declarations;
using Plang.Compiler.TypeChecker.Types;
using System.Collections.Generic;

namespace Plang.Compiler.Backend.Rvm
{
    internal class CompilationContext : CompilationContextBase
    {
        public CompilationContext(ICompilationJob job)
            : base(job)
        {
            Names = new RvmNameManager("PGEN_");

            ProjectDependencies = job.ProjectDependencies.Count == 0? new List<string>() { ProjectName } : job.ProjectDependencies;
            GlobalFunctionClassName = "GlobalFunctions";
        }

        public RvmNameManager Names { get; }

        public string GlobalFunctionClassName { get; }

        public string MopFileName(string name)
        {
            return $"{ProjectName}-{name}.mop";
        }

        public IReadOnlyList<string> ProjectDependencies { get; }

        public string GetStaticMethodQualifiedName(Function function)
        {
            return $"{GlobalFunctionClassName}.{Names.GetNameForDecl(function)}";
        }
    }
}