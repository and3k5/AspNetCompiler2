using System;
using Microsoft.Build.Tasks;

namespace AspNetCompiler2
{
    // ReSharper disable once UnusedMember.Global
    public class AspNetCompiler2 : AspNetCompiler
    {
        public string Exclude
        {
            get => (string) Bag[nameof(Exclude)];
            set => Bag[nameof(Exclude)] = value;
        }

        public string ExcludeSeparator
        {
            get => (string) Bag[nameof(ExcludeSeparator)];
            set => Bag[nameof(ExcludeSeparator)] = value;
        }

        protected string[] GetExcluded()
        {
            if (string.IsNullOrWhiteSpace(Exclude))
                return new string[0];

            return !string.IsNullOrEmpty(ExcludeSeparator)
                ? Exclude.Split(new[] {ExcludeSeparator}, StringSplitOptions.RemoveEmptyEntries)
                : new[] {Exclude};
        }

        protected override void AddCommandLineCommands(CommandLineBuilderExtension commandLine)
        {
            base.AddCommandLineCommands(commandLine);

            foreach (var path in GetExcluded())
                commandLine.AppendSwitchIfNotNull("-x ", path);
        }
    }
}