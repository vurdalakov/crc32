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
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class AssemblyInfo
    {
        private AssemblyInfo()
        {
        }

        public String CompanyName { get; private set; }

        public Int32 CompilationRelaxations { get; private set; }

        public Boolean ComVisible { get; private set; }
        
        public String Configuration { get; private set; }
        
        public String Copyright { get; private set; }
        
        public String Culture { get; private set; }

        public DebuggableAttribute Debuggable { get; private set; }

        public String Description { get; private set; }

        public String FileVersion { get; private set; }

        public String Guid { get; private set; }

        public String ProductName { get; private set; }
        
        public String Title { get; private set; }
        
        public String Trademark { get; private set; }
        
        public Version Version { get; private set; }

        public Boolean WrapNonExceptionThrows { get; private set; }

        static public AssemblyInfo Create()
        {
            return AssemblyInfo.Create(Assembly.GetEntryAssembly());
        }

        static public AssemblyInfo Create(Assembly assembly)
        {
            AssemblyInfo assemblyInfo = new AssemblyInfo();

            foreach (Attribute attribute in assembly.GetCustomAttributes(true))
            {
                String name = attribute.TypeId.ToString();

                int dot = name.LastIndexOf('.');
                if (dot > 0)
                {
                    name = name.Remove(0, dot + 1);
                }

                const String attributeName = "Attribute";
                if (name.EndsWith(attributeName))
                {
                    name = name.Remove(name.Length - attributeName.Length);
                }

                // System.Reflection
                if (attribute is AssemblyCompanyAttribute)
                {
                    assemblyInfo.CompanyName = (attribute as AssemblyCompanyAttribute).Company;
                }
                else if (attribute is AssemblyConfigurationAttribute)
                {
                    assemblyInfo.Configuration = (attribute as AssemblyConfigurationAttribute).Configuration;
                }
                else if (attribute is AssemblyCopyrightAttribute)
                {
                    assemblyInfo.Copyright = (attribute as AssemblyCopyrightAttribute).Copyright;
                }
                else if (attribute is AssemblyCultureAttribute)
                {
                    assemblyInfo.Culture = (attribute as AssemblyCultureAttribute).Culture;
                }
                else if (attribute is AssemblyDescriptionAttribute)
                {
                    assemblyInfo.Description = (attribute as AssemblyDescriptionAttribute).Description;
                }
                else if (attribute is AssemblyFileVersionAttribute)
                {
                    assemblyInfo.FileVersion = (attribute as AssemblyFileVersionAttribute).Version;
                }
                else if (attribute is AssemblyProductAttribute)
                {
                    assemblyInfo.ProductName = (attribute as AssemblyProductAttribute).Product;
                }
                else if (attribute is AssemblyTitleAttribute)
                {
                    assemblyInfo.Title = (attribute as AssemblyTitleAttribute).Title;
                }
                else if (attribute is AssemblyTrademarkAttribute)
                {
                    assemblyInfo.Trademark = (attribute as AssemblyTrademarkAttribute).Trademark;
                }
                // System.Runtime.InteropServices
                else if (attribute is GuidAttribute)
                {
                    assemblyInfo.Guid = (attribute as GuidAttribute).Value;
                }
                else if (attribute is ComVisibleAttribute)
                {
                    assemblyInfo.ComVisible = (attribute as ComVisibleAttribute).Value;
                }
                // System.Diagnostics
                else if (attribute is DebuggableAttribute)
                {
                    assemblyInfo.Debuggable = attribute as DebuggableAttribute;
                }
                // System.Runtime.CompilerServices
                else if (attribute is CompilationRelaxationsAttribute)
                {
                    assemblyInfo.CompilationRelaxations = (attribute as CompilationRelaxationsAttribute).CompilationRelaxations;
                }
                else if (attribute is RuntimeCompatibilityAttribute)
                {
                    assemblyInfo.WrapNonExceptionThrows = (attribute as RuntimeCompatibilityAttribute).WrapNonExceptionThrows;
                }
                // other
                else
                {
                    Console.WriteLine("Unknown: '{0}'", name);
                }
            }

            assemblyInfo.Version = assembly.GetName().Version;

            return assemblyInfo;
        }
    }
}
