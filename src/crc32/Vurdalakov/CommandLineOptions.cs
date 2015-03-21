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

    public class CommandLineOptions
    {
        private Dictionary<String, List<String>> options = new Dictionary<string,List<string>>();

        internal CommandLineOptions()
        {
        }

        internal void Add(String name, String value)
        {
            name = name.ToLower();

            if (options.ContainsKey(name))
            {
                this.options[name].Add(value);
            }
            else
            {
                List<String> values = new List<String>();
                values.Add(value);

                this.options.Add(name, values);
            }
        }

        public Boolean Exists(String name)
        {
            return this.options.ContainsKey(name.ToLower());
        }

        public String GetValue(String name, String defaultValue)
        {
            if (!Exists(name))
            {
                return defaultValue;
            }

            List<String> optionValues = this.options[name.ToLower()];

            if (0 == optionValues.Count)
            {
                return defaultValue;
            }

            return optionValues[0];
        }

        public Int32 GetValue(String name, Int32 defaultValue)
        {
            String value = GetValue(name, null);

            if (null == value)
            {
                return defaultValue;
            }

            return Convert.ToInt32(value);
        }

        public String[] GetValues(String name)
        {
            if (!Exists(name))
            {
                return new String[0];
            }

            List<String> optionValues = this.options[name.ToLower()];

            return optionValues.ToArray();
        }
    }
}
