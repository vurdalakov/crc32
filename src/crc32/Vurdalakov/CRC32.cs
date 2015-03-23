// This software is part of the CRC32 application.
// CRC32 is a command-line program that calculates CRC32 file hashes.
// https://github.com/vurdalakov/crc32
//
// The MIT License (MIT)
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
    using System.Security.Cryptography;

    /// <summary>
    /// Computes the CRC32 hash for the input data.
    /// </summary>
    /// <remarks>
    ///     <para>The hash is used as a unique value of fixed size representing a large amount of data. Hashes of two sets of data should match if and only if the corresponding data also matches. Small changes to the data result in large unpredictable changes in the hash.</para>
    ///     <para>The hash size for the CRC-32 algorithm is 32 bits.</para>
    ///     <para>This is an abstract class. The only implementation of this class is <see cref="CRC32Managed"/>.</para>
    /// </remarks>
    public abstract class CRC32 : HashAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CRC32"/> class.
        /// </summary>
        public CRC32() : base()
        {
            this.HashSizeValue = 32;
        }

        /// <summary>
        /// Gets the value of the computed CRC-32 hash code as unsigned 32-bit integer.
        /// </summary>
        /// <value>The current value of the computed CRC-32 hash code.</value>
        public UInt32 CRC32Hash { get; protected set; }

        /// <summary>
        /// Creates an instance of the default implementation of <see cref="CRC32"/> using the managed library and standard polynomial (0x04C11DB7/0xEDB88320/0x82608EDB, used in Ethernet, Serial ATA, MPEG-2, PKZIP, Gzip, Bzip2, PNG, etc.).
        /// </summary>
        /// <returns>A new instance of <see cref="CRC32"/>.</returns>
        new public static CRC32 Create()
        {
            return new CRC32Managed();
        }

        /// <summary>
        /// Creates an instance of the default implementation of <see cref="CRC32"/> using the managed library and custom polynomial..
        /// </summary>
        /// <param name="polynomial">The polynomial to be used (reversed representation).</param>
        /// <returns>A new instance of <see cref="CRC32"/>.</returns>
        public static CRC32 Create(UInt32 polynomial)
        {
            return new CRC32Managed(polynomial);
        }

        /// <summary>
        ///     <para>Creates an instance of the specified implementation of the <see cref="CRC32"/>.</para>
        ///     <para>Calling this method always throws <see cref="NotImplementedException"/>.</para>
        /// </summary>
        /// <param name="hashName">The implementation of <see cref="CRC32"/> to create.</param>
        /// <returns>A new instance of CRC32 using the specified implementation.</returns>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        new public static CRC32 Create(String hashName)
        {
            throw new NotImplementedException();
        }
    }
}
