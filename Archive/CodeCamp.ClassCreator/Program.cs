using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeCamp.ClassCreator
{
    class Program
    {
        /// <summary>
        /// This console application creates POCO classes out of edxml file
        /// 
        /// Use CodeCamp.ClassCreator i=full file and path name of edmx file o=directory where to put the created class files
        /// n=Namespace to add to the classes
        /// </summary>
        /// <param name="args">i=full file and path name of edmx file o=directory where to put the created class files</param>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Use CodeCamp.ClassCreator i=<full file and path name of edmx file> o=<full path name of folder where to put classes> n=<namespace name>");
                return;
            }
            Dictionary<string, string> _Data = args.ToDictionary(x => x.Split('=')[0], y => y.Substring(2));
            if (!_Data.ContainsKey("i")) throw new Exception("i switch mandatory, use CodeCamp.ClassCreator without arguments for help");
            if (!_Data.ContainsKey("o")) throw new Exception("o switch mandatory, use CodeCamp.ClassCreator without arguments for help");
            if (!_Data.ContainsKey("n")) throw new Exception("n switch mandatory, use CodeCamp.ClassCreator without arguments for help");
            EDMXtoClasses.CreateClasses(_Data["i"], _Data["o"], _Data["n"]);
        }
    }
}
