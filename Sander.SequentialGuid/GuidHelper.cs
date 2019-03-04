using System;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
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
		/// <para>Faster than using guid-to-string</para>
		/// </summary>
		public static char GetCharacterAt(this Guid guid, int position)
		{
			//remap position to internal byte order
			int Remap()
			{
				var remap = position / 2;
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

			/*
			 *    // Returns an unsigned byte array containing the GUID.
		public byte[] ToByteArray()
		{
			byte[] g = new byte[16];

			g[0] = (byte)(_a);
			g[1] = (byte)(_a >> 8);
			g[2] = (byte)(_a >> 16);
			g[3] = (byte)(_a >> 24);
			g[4] = (byte)(_b);
			g[5] = (byte)(_b >> 8);
			g[6] = (byte)(_c);
			g[7] = (byte)(_c >> 8);
			g[8] = _d;
			g[9] = _e;
			g[10] = _f;
			g[11] = _g;
			g[12] = _h;
			g[13] = _i;
			g[14] = _j;
			g[15] = _k;

			return g;
			 */

			//after https://github.com/dotnet/corefx/blob/7622efd2dbd363a632e00b6b95be4d990ea125de/src/Common/src/CoreLib/System/Guid.cs#L989
			char HexToChar(int aq)
			{
				aq = aq & 0xf;
				return (char)(aq > 9 ? aq - 10 + 0x61 : aq + 0x30);
			}

			if (position < 0 || position > 31)
				throw new ArgumentOutOfRangeException(nameof(position), position, $"Position must be between 0 and 31, but received {position}");

			var guidByte = guid.ToByteArray()[Remap()];

			return HexToChar(position % 2 == 0 ? (guidByte & 0xF0) >> 4 : guidByte & 0x0F);
		}

		//     From https://stackoverflow.com/a/16222886/3248515, modified
		private static Func<TS, T> CreateGetter<TS, T>(string fieldName)
		{
			var field = typeof(Guid).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
			Debug.Assert(field != null, nameof(field) + " != null");
			Debug.Assert(field.ReflectedType != null, "field.ReflectedType != null");
			var setterMethod = new DynamicMethod($"{field.ReflectedType.FullName}.get_{field.Name}",
				typeof(T), new[]
					{ typeof(TS) }, true);
			var gen = setterMethod.GetILGenerator();
			gen.Emit(OpCodes.Ldarg_0);
			gen.Emit(OpCodes.Ldfld, field);
			gen.Emit(OpCodes.Ret);
			return (Func<TS, T>)setterMethod.CreateDelegate(typeof(Func<TS, T>));
		}
	}
}
