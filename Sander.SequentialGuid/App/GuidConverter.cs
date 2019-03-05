using System;
using System.Runtime.InteropServices;

namespace Sander.SequentialGuid.App
{
	/// <summary>
	/// Based on https://stackoverflow.com/a/3563872/3248515
	/// </summary>
	internal static class GuidConverter
	{
		private static GuidConverterStruct _converter;

		internal static Guid DecimalToGuid(decimal dec)
		{
			_converter.Decimal = dec;
			return _converter.Guid;
		}

		internal static decimal GuidToDecimal(Guid guid)
		{
			_converter.Guid = guid;
			return _converter.Decimal;
		}

		internal static (long, long) GuidToLongs(Guid guid)
		{
			_converter.Guid = guid;
			return (_converter.Long1, _converter.Long2);
		}

		internal static Guid LongsToGuid(long a, long b)
		{
			_converter.Long1 = a;
			_converter.Long2 = b;
			return _converter.Guid;
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct GuidConverterStruct
		{
			[FieldOffset(0)]
			internal decimal Decimal;

			[FieldOffset(0)]
			internal Guid Guid;

			[FieldOffset(0)]
			internal long Long1;

			[FieldOffset(8)]
			internal long Long2;
		}
	}
}
