using System;
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
		///     Opposite of Guid.Empty - returns ffffffff-ffff-ffff-ffff-ffffffffffff
		/// </summary>
		public static Guid MaxValue => new Guid("ffffffffffffffffffffffffffffffff");


		/// <summary>
		///     Get GUID from BigInteger.
		///     <para>
		///         Note that it is possible for BigInteger not to fit to GUID, ffffffff-ffff-ffff-ffff-ffffffffffff + 1 will
		///         overflow GUID and return 00000000-00..., ffffffff-ffff-ffff-ffff-ffffffffffff + 2 returns 00000001-00...
		///         and so forth
		///     </para>
		///     <para>
		///         Defaults to isPythonCompliant = true, as this is the more common use outside Microsoft/.NET.
		///         E.g. compatible with http://guid-convert.appspot.com and Java UUID.
		///         See also https://stackoverflow.com/questions/9195551/why-does-guid-tobytearray-order-the-bytes-the-way-it-does
		///     </para>
		/// </summary>
		public static Guid FromBigInteger(BigInteger integer, bool isPythonCompliant = true)
		{
			var bytes = integer.ToByteArray();

			//GUID can only be initialized from 16-byte array,
			//but BigInteger can be a single byte
			//or more than 16 bytes - see also comment on ToBigInteger()
			if (bytes.Length != 16)
				Array.Resize(ref bytes, 16);

			return isPythonCompliant ? FromCompliantByteArray(bytes) : new Guid(bytes);
		}


		/// <summary>
		///     Convert decimal to Guid
		/// </summary>
		public static Guid FromDecimal(decimal dec) =>
			GuidConverter.DecimalToGuid(dec);


		/// <summary>
		///     Convert two longs to Guid
		///     <para>
		///         Note that two longs cannot hold GUID larger than ffffffff-ffff-7fff-ffff-ffffffffff7f, but that shouldn't be
		///         issue in most realistic use cases
		///     </para>
		/// </summary>
		public static Guid FromLongs(long first, long second) =>
			GuidConverter.GuidFromInt64(first, second);

		/// <summary>
		///     Create GUID from byte array compatible with Python and Java.
		///     <para>
		///         See http://guid-convert.appspot.com and
		///         https://stackoverflow.com/questions/9195551/why-does-guid-tobytearray-order-the-bytes-the-way-it-does
		///     </para>
		/// </summary>
		public static Guid FromCompliantByteArray(byte[] bytes)
		{
			if (bytes?.Length != 16)
				throw new ArgumentOutOfRangeException(nameof(bytes), "Byte array must contain 16 bytes!");

			var byteArray = new byte[16];

			byteArray[0] = bytes[12];
			byteArray[1] = bytes[13];
			byteArray[2] = bytes[14];
			byteArray[3] = bytes[15];
			byteArray[4] = bytes[10];
			byteArray[5] = bytes[11];
			byteArray[6] = bytes[8];
			byteArray[7] = bytes[9];
			byteArray[8] = bytes[7];
			byteArray[9] = bytes[6];
			byteArray[10] = bytes[5];
			byteArray[11] = bytes[4];
			byteArray[12] = bytes[3];
			byteArray[13] = bytes[2];
			byteArray[14] = bytes[1];
			byteArray[15] = bytes[0];


			return new Guid(byteArray);
		}
	}
}
