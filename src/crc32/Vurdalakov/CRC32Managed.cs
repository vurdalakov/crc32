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
    /// Computes the <see cref="CRC32"/> hash for the input data using the managed library.
    /// </summary>
    /// <remarks>
    ///     <para>The hash is used as a unique value of fixed size representing a large amount of data. Hashes of two sets of data should match if and only if the corresponding data also matches. Small changes to the data result in large unpredictable changes in the hash.</para>
    ///     <para>This is a purely managed implementation of <see cref="CRC32"/> that does not wrap CAPI.</para>
    ///     <para>The hash size for the CRC32Managed algorithm is 32 bits.</para>
    /// </remarks>
    /// <example>
    /// The following example computes the CRC32Managed hash for data and stores it in result. This example assumes that there is a predefined constant DATA_SIZE.
    /// <code>
    /// byte[] data = new byte[DATA_SIZE];
    /// CRC32 crc32 = new CRC32Managed(); 
    /// byte[] result = crc32.ComputeHash(data);
    /// </code>
    /// </example>
    public class CRC32Managed : CRC32
    {
        private UInt32[] crc32Table = new UInt32[256];
        private UInt32 crc32Result;

        /// <summary>
        /// Initializes a new instance of <see cref="CRC32Managed"/> class using the managed library and standard polynomial (0x04C11DB7/0xEDB88320/0x82608EDB, used in Ethernet, Serial ATA, MPEG-2, PKZIP, Gzip, Bzip2, PNG, etc.).
        /// </summary>
        public CRC32Managed() : this(0xEDB88320)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CRC32Managed"/> class using the managed library and custom polynomial.
        /// </summary>
        /// <param name="polynomial">The polynomial to be used (reversed representation).</param>
        public CRC32Managed(UInt32 polynomial) : base()
        {
            for (UInt32 i = 0; i < 256; i++)
            {
                UInt32 crc32 = i;
                for (int j = 8; j > 0; j--)
                {
                    if ((crc32 & 1) == 1)
                    {
                        crc32 = (crc32 >> 1) ^ polynomial;
                    }
                    else
                    {
                        crc32 >>= 1;
                    }
                }
                crc32Table[i] = crc32;
            }

            Initialize();
        }

        /// <summary>
        /// Gets a value indicating whether the current transform can be reused.
        /// </summary>
        /// <value>Always <c>true</c>.</value>
        public override bool CanReuseTransform { get { return true; } }

        /// <summary>
        /// Gets a value indicating whether multiple blocks can be transformed.
        /// </summary>
        /// <value>Always <c>true</c>.</value>
        public override bool CanTransformMultipleBlocks { get { return true; } }

        /// <summary>
        /// Initializes an instance of <see cref="CRC32Managed"/>.
        /// </summary>
        public override void Initialize()
        {
            this.crc32Result = 0xFFFFFFFF;
        }

        /// <summary>
        /// Routes data written to the object into the <see cref="CRC32"/> hash algorithm for computing the hash.
        /// </summary>
        /// <param name="array">The input to compute the hash code for.</param>
        /// <param name="offset">The offset into the byte array from which to begin using data.</param>
        /// <param name="count">The number of bytes in the byte array to use as data.</param>
        /// <remarks>
        ///     <para>This method is not called by application code.</para>
        ///     <para>This method performs the hash computation. Every write to the cryptographic stream object passes the data through this method. For each block of data, this method updates the state of the hash object so a correct hash value is returned at the end of the data stream.</para>
        /// </remarks>
        protected override void HashCore(Byte[] array, int start, int size)
        {
            int end = start + size;
            for (int i = start; i < end; i++)
            {
                this.crc32Result = (this.crc32Result >> 8) ^ this.crc32Table[array[i] ^ (this.crc32Result & 0x000000FF)];
            }
        }

        /// <summary>
        /// Finalizes the hash computation after the last data is processed by the cryptographic stream object.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        /// <remarks>
        ///     <para>This method is not called by application code.</para>
        ///     <para>This method finalizes any partial computation and returns the correct hash value of the data stream.</para>
        /// </remarks>
        protected override Byte[] HashFinal()
        {
            this.crc32Result = ~this.crc32Result;

            this.CRC32Hash = this.crc32Result;

            this.HashValue = BitConverter.GetBytes(this.crc32Result);

            return this.HashValue;
        }
    }
}
