using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    #region Using Directives
    using CommandLine;
    using CommandLine.Text;
    #endregion

    internal class Options
    {
        [Option('c', "command", Required = true, HelpText = "sql script or path to file with sql script")]
        public string Command { get; set; }

        [Option('l', "delimiter", HelpText = "columns delimeter", DefaultValue = "\t")]
        public string Delimiter { get; set; }

        [Option('s', "source", HelpText = "DataBase server, e.g. (local)", DefaultValue = "(local)")]
        public string Source { get; set; }

        [Option('d', "database", Required = true, HelpText = "DataBase name")]
        public string Database { get; set; }

        [Option('u', "user", HelpText = "user", DefaultValue = "")]
        public string User { get; set; }

        [Option('p', "password", HelpText = "password", DefaultValue="")]
        public string Password { get; set; }

        [Option('v', "verbose", HelpText = "verbose")]
        public bool Verbose { get; set; }

        [Option('o', "output", HelpText = "output file path")]
        public string OutputFilePath { get; set; }

        [Option('e', "encoding", HelpText = "Code page, see Encoding .Net class doc for the list of available values", DefaultValue=1251)]
        public int Encoding { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
