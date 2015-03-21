// This software is part of the VurdalakovDotNet class library.
//
// Copyright (c) 2012 Vurdalakov
// http://www.vurdalakov.net
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

namespace Vurdalakov
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    // [command] [/switch] [/option:value] [filename]

    // note: not case

    public class CommandLineParser
    {
        public String Command { get; private set; }

        public String FileName { get { return 1 == this.FileNames.Length ? this.FileNames[0] : null; } }

        public String[] FileNames { get; private set; }

        public CommandLineOptions Options { get; private set; }

        public CommandLineParser(Boolean commandRequired = false)
            : this(Environment.GetCommandLineArgs(), commandRequired)
        {
        }

        public CommandLineParser(String[] args, Boolean commandRequired = false)
        {
            // command

            if (commandRequired)
            {
                if ((args.Length > 1) && !IsOptionOrSwitch(args[1]))
                {
                    Command = args[1].ToLower();
                }
                else
                {
                    throw new ArgumentException("Command is missing");
                }
            }

            // switches and options

            List<String> fileNames = new List<String>();
            Options = new CommandLineOptions();

            int first = String.IsNullOrEmpty(Command) ? 1 : 2;
            for (int i = first ; i < args.Length; i++)
            {
                String arg = args[i];

                if (IsOptionOrSwitch(arg))
                {
                    arg = arg.Substring(1);

                    if (arg.IndexOf(':') > 0)
                    {
                        String[] option = arg.Split(new char[] { ':' });

                        String name = option[0].ToLower();
                        String value = option[1];

                        if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(value))
                        {
                            throw new ArgumentException("Invalid option: '" + arg + "'");
                        }

                        this.Options.Add(name, value);
                    }
                    else
                    {
                        this.Options.Add(arg, "");
                    }
                }
                else
                {
                    fileNames.Add(arg);
                }
            }

            FileNames = fileNames.ToArray();
        }

        private bool IsOptionOrSwitch(String name)
        {
            return ('-' == name[0]) || ('/' == name[0]);
        }
    }
}
