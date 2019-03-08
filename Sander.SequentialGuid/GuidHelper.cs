using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Sander.SequentialGuid.App;

namespace Sander.SequentialGuid
{
	/// <summary>
	///     Useful helper methods for GUID
	/// </summary>
	public static class GuidHelper
	{
		/// <summary>
		/// Opposite of Guid.Empty - returns ffffffff-ffff-ffff-ffff-ffffffffffff
		/// </summary>
		public static Guid MaxValue => new Guid("ffffffffffffffffffffffffffffffff");


		/// <summary>
		///     Get GUID from BigInteger.
		///     <para>Note that it is possible for BigInteger not to fit to GUID, ffffffff-ffff-ffff-ffff-ffffffffffff + 1 will overflow GUID and return 00000000-00...,
		/// ffffffff-ffff-ffff-ffff-ffffffffffff + 2 returns 00000001-00... and so forth</para>
		/// <para>
		/// Defaults to isPythonCompliant = true, as this is the more common use outside Microsoft/.NET.
		/// E.g. compatible with http://guid-convert.appspot.com/ and Java UUID.
		/// See also https://stackoverflow.com/questions/9195551/why-does-guid-tobytearray-order-the-bytes-the-way-it-does
		/// </para>
		/// </summary>
		public static Guid FromBigInteger(BigInteger integer, bool isPythonCompliant = true)
		{
			var bytes = integer.ToByteArray();

			//GUID can only be initialized from 16-byte array,
			//but BigInteger can be a single byte
			//or more than 16 bytes - see also comment on ToBigInteger()
			if (bytes.Length > 16)
				Array.Resize(ref bytes, 16);

			var byteArray = new byte[16];
			bytes.CopyTo(byteArray, 0);

			return isPythonCompliant ? FromCompliantByteArray(bytes) : new Guid(byteArray);
		}


		/// <summary>
		///     Convert decimal to Guid
		/// </summary>
		public static Guid FromDecimal(decimal dec) =>
			GuidConverter.DecimalToGuid(dec);


		/// <summary>
		///     Convert two longs to Guid
		/// <para>Note that two longs cannot hold GUID larger than ffffffff-ffff-7fff-ffff-ffffffffff7f, but that shouldn't be issue in most realistic use cases</para>
		/// </summary>
		public static Guid FromLongs(long first, long second)
		{
			return GuidConverter.GuidFromInt64(first, second);
		}

		/// <summary>
		/// Create GUID from byte array compatible with Python and Java.
		/// <para>See http://guid-convert.appspot.com/ and https://stackoverflow.com/questions/9195551/why-does-guid-tobytearray-order-the-bytes-the-way-it-does </para>
		/// </summary>
		public static Guid FromCompliantByteArray(byte[] bytes)
		{
			if (bytes.Length > 16)
				Array.Resize(ref bytes, 16);

			var byteArray = new byte[16];
			bytes.CopyTo(byteArray, 0);
			var byteList = new List<byte>(16)
			{
				byteArray[12],
				byteArray[13],
				byteArray[14],
				byteArray[15],
				byteArray[10],
				byteArray[11],
				byteArray[8],
				byteArray[9]
			};
			byteList.AddRange(byteArray.Take(8).Reverse());

			return new Guid(byteList.ToArray());
		}
	}
}
