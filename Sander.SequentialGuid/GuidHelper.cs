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
		/// Get character at the specified position (0..32).
		/// Character is returned in lowercase
		/// <para>Far faster and user less memory than using guid-to-string</para>
		/// </summary>
		public static char GetCharacterAt(this Guid guid, int position)
		{
			//remap position to internal byte order
			int Remap()
			{
				var remap = position >> 1;
				switch (remap)
				{
					case 0: return 3;
					case 1: return 2;
					case 2: return 1;
					case 3: return 0;
					case 4: return 5;
					case 5: return 4;
					case 6: return 7;
					case 7: return 6;
					default:
						return remap;
				}
			}

			//after https://github.com/dotnet/corefx/blob/7622efd2dbd363a632e00b6b95be4d990ea125de/src/Common/src/CoreLib/System/Guid.cs#L989
			char HexToChar(int aq)
			{
				aq = aq & 0xf;
				return (char)(aq > 9 ? aq - 10 + 0x61 : aq + 0x30);
			}

			if (position < 0 || position > 31)
				throw new ArgumentOutOfRangeException(nameof(position), position,
					$"Position must be between 0 and 31, but received {position}");

			var guidByte = PrivateFieldProvider.GetByte(guid, Remap());

			return HexToChar(position % 2 == 0 ? (guidByte & 0xF0) >> 4 : guidByte & 0x0F);
		}

		/// <summary>
		/// Get byte from GUID without converting GUID to byte array
		/// <para>This is very slightly faster than using Guid.ToByteArray(), but uses far less memory</para>
		/// </summary>		
		public static byte GetByteAt(this Guid guid, int position)
		{

			if (position < 0 || position > 31)
				throw new ArgumentOutOfRangeException(nameof(position), position,
					$"Position must be between 0 and 31, but received {position}");

			return PrivateFieldProvider.GetByte(guid, position);
		}
	}
}
