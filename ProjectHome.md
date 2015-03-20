### Overview ###

**crc32** is a command line program that calculates CRC32 file hashes.

It uses a [C# implementation of CRC32 hash algorithm](http://code.google.com/p/vurdalakovdotnet/wiki/crc32) that is inherited from [System.Security.Cryptography.HashAlgorithm](http://msdn.microsoft.com/en-us/library/system.security.cryptography.hashalgorithm.aspx) class.

This tool is part of [`dostools` collection](https://github.com/vurdalakov/dostools).

### Command line syntax ###

```
    crc32 [-dec | -hex | -lowerhex] <file name>
```

Without options specified application shows CRC32 hash in both decimal and hexadecimal formats.

The following options are supported:
<ul>
<li><b><code>/dec</code></b> - display CRC32 hash in decimal format (3281181382)</li>
<li><b><code>/hex</code></b> - display CRC32 hash in uppercase hexadecimal format (C392DAC6)</li>
<li><b><code>/lowerhex</code></b> - display CRC32 hash in lowercase hexadecimal format (c392dac6)</li>
</ul>

Examples:

```
    crc32 filename.bin
    crc32 -dec filename.bin
    crc32 -hex filename.bin
    crc32 -lowerhex filename.bin
```