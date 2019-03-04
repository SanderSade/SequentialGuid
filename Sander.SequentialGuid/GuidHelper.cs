using System;
using System.Numerics;
using Sander.SequentialGuid.App;

namespace Sander.SequentialGuid
{
	/// <summary>
	///     Useful extension and helper methods for GUID
	/// </summary>
	public static class GuidHelper
	{
		/// <summary>
		///     Convert GUID to BigInteger
		/// </summary>
		public static BigInteger AsBigInteger(this Guid guid) =>
			new BigInteger(guid.ToByteArray());


		/// <summary>
		///     Get GUID from BigInteger.
		///     <para>Note that it is possible for BigInteger not to fit to GUID</para>
		/// </summary>
		public static Guid FromBigInteger(BigInteger integer) =>
			new Guid(integer.ToByteArray());


		/// <summary>
		///     Convert GUID to decimal
		/// </summary>
		public static decimal AsDecimal(this Guid guid) =>
			GuidToDecimalConverter.GuidToDecimal(guid);


		/// <summary>
		///     Convert decimal to Guid
		/// </summary>
		public static Guid FromDecimal(decimal dec) =>
			GuidToDecimalConverter.DecimalToGuid(dec);


		/// <summary>
		///     Convert GUID to pair of longs
		/// </summary>
		public static (long, long) AsLongs(this Guid guid) =>
			GuidToLongConverter.GuidToLongs(guid);


		/// <summary>
		///     Convert two longs to Guid
		/// </summary>
		public static Guid FromLongs(long first, long second) =>
			GuidToLongConverter.LongsToGuid(first, second);


		/// <summary>
		/// Get the hex character at the specified position (0..31).
		/// Character is returned in lowercase
		/// <para>Far faster and uses less memory than using guid-to-string</para>
		/// </summary>
		public static char GetCharacterAt(this Guid guid, int position)
		{		
			if (position < 0 || position > 31)
				throw new ArgumentOutOfRangeException(nameof(position), position,
					$"Position must be between 0 and 31, but received {position}");

			//remap position to internal byte order, as characters 0..13 do not match the byte order
			var remap = position >> 1;
			switch (remap)
			{
				case 0: remap = 3;
					break;
				case 1:
					remap = 2;
					break;
				case 2:
					remap = 1;
					break;
				case 3:
					remap = 0;
					break;
				case 4:
					remap = 5;
					break;
				case 5:
					remap = 4;
					break;
				case 6:
					remap = 7;
					break;
				case 7:
					remap = 6;
					break;				
			}

			var guidByte = PrivateFieldProvider.GetByte(guid, remap);

			//logic similar to https://github.com/dotnet/corefx/blob/7622efd2dbd363a632e00b6b95be4d990ea125de/src/Common/src/CoreLib/System/Guid.cs#L989,
			//but we're using nibble and not full byte
			var nibbleByte = (position % 2 == 0 ? (guidByte & 0xF0) >> 4 : guidByte & 0x0F) & 0xf;			
			return (char)(nibbleByte > 9 ? nibbleByte + 107 : nibbleByte + 48);
		}

		/// <summary>
		/// Get byte from GUID without converting GUID to byte array. Position is the native position of the byte in GUID structure (0..15)
		/// <para>This is very slightly faster than using Guid.ToByteArray(), but uses far less memory</para>
		/// </summary>		
		public static byte GetByteAt(this Guid guid, int position)
		{
			if (position < 0 || position > 15)
				throw new ArgumentOutOfRangeException(nameof(position), position,
					$"Position must be between 0 and 15, but received {position}");

			return PrivateFieldProvider.GetByte(guid, position);
		}
	}
}
