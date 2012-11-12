// This software is part of the CRC32 application.
// CRC32 is a command-line program that calculates CRC32 hash of a file.
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

namespace Vurdalakov.Crc32
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    class Program
    {
        static void Main(string[] args)
        {
            CommandLineParser commandLineParser = new CommandLineParser();

            Boolean dec = commandLineParser.Options.Exists("dec");
            Boolean uhex = commandLineParser.Options.Exists("hex");
            Boolean lhex = commandLineParser.Options.Exists("lowerhex");

            if (String.IsNullOrEmpty(commandLineParser.FileName) || (dec && uhex) || (dec && lhex) || (uhex && lhex))
            {
                AssemblyInfo assemblyInfo = AssemblyInfo.Create();

                Console.WriteLine("{0} v{1} - {2}\nCalculates CRC32 hash of a file\n\nUsage:\n\tcrc32 [-dec | -hex | -lowerhex] <file name>",
                    assemblyInfo.ProductName, assemblyInfo.FileVersion, assemblyInfo.Description);

                return;
            }

            UInt32 hash;
            using (Stream stream = new FileStream(commandLineParser.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                CRC32Managed crc32Managed = new CRC32Managed();
                crc32Managed.ComputeHash(stream);

                hash = crc32Managed.CRC32Hash;
            }

            if (dec)
            {
                Console.WriteLine("{0}", hash);
            }
            else if (uhex)
            {
                Console.WriteLine("{0:X}", hash);
            }
            else if (lhex)
            {
                Console.WriteLine("{0:x}", hash);
            }
            else
            {
                Console.WriteLine("{0}\nCRC32 dec: {1}\nCRC32 hex: {1:X}", commandLineParser.FileName, hash);
            }
        }
    }
}
