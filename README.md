# crc32

#### Overview

**crc32** is a command line program that calculates CRC32 file hashes.

This tool is part of [`dostools` collection](https://github.com/vurdalakov/dostools). 

Distributed under the [MIT license](http://opensource.org/licenses/MIT).

This project has been automatically exported from [Google Code](http://code.google.com/p/crc32).

#### Command line syntax

```
crc32 [-dec | -hex | -lowerhex] <file name>
```

Without options specified application shows CRC32 hash in both decimal and hexadecimal formats.

The following options are supported:

`/dec` - display CRC32 hash in decimal format (3281181382)

`/hex` - display CRC32 hash in uppercase hexadecimal format (C392DAC6)

`/lowerhex` - display CRC32 hash in lowercase hexadecimal format (c392dac6)

Examples:

```
crc32 filename.bin
crc32 -dec filename.bin
crc32 -hex filename.bin
crc32 -lowerhex filename.bin
```

#### Points of interest

This program uses a [C# implementation of CRC32 hash algorithm](https://github.com/vurdalakov/crc32/wiki/CRC32) that is inherited from [System.Security.Cryptography.HashAlgorithm](https://msdn.microsoft.com/en-us/library/system.security.cryptography.hashalgorithm.aspx) class.
