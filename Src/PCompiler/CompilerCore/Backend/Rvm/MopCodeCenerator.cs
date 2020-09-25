using Plang.Compiler.TypeChecker;
using Plang.Compiler.TypeChecker.AST;
using Plang.Compiler.TypeChecker.AST.Declarations;
using System;
using System.Collections.Generic;
using System.IO;

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

        private CompiledFile WriteMonitor(Machine machine) {
            CompiledFile source = new CompiledFile(Context.MopFileName(machine.Name));

            WriteSourcePrologue(source.Stream);

            WriteSpec(source.Stream, machine);

            return source;
        }

        private void WriteSourcePrologue(StringWriter output) {
            Context.WriteLine(output, "package mop;");
            Context.WriteLine(output);
            Context.WriteLine(output, "import java.io.*;");
            Context.WriteLine(output, "import java.util.*;");
            Context.WriteLine(output, "<add your own imports>");
            Context.WriteLine(output);
        }

        private void WriteSpec(StringWriter output, Machine machine)
        {
            string declName = Context.Names.GetNameForDecl(machine);
            Context.WriteLine(output, $"{declName}(<add your args>) {{");
            string implName = Context.Names.GetMachineImplName(machine);
            Context.WriteLine(output, $"{implName} impl = new {implName}();");

            // TODO: Is this the right event set to iterate? Does it contain all events that have anything at all to do with the current machine?
            foreach (PEvent e in machine.Observes.Events)
            {
                Context.WriteLine(output, "");
                WriteSpecEvent(output, e);
            }

            Context.WriteLine(output, $"}}");
        }

        private void WriteSpecEvent(StringWriter output, PEvent e)
        {
            Context.WriteLine(output, $"event {Context.Names.GetNameForDecl(e)} <after/before> (<add-args>):");
            Context.WriteLine(output, $"execution(<method>)");
            Context.WriteLine(output, $"&& target(<name>)");
            Context.WriteLine(output, $"&& args(<names>) {{");
            Context.WriteLine(output, $"<add code>");
            Context.WriteLine(output, $"impl.{Context.Names.GetHandlerName(e)}()");
            Context.WriteLine(output, $"<add code>");
            Context.WriteLine(output, $"}}");
        }

        CompiledFile WriteMachine(Machine machine) {
            throw new NotImplementedException();
        }
    }
}