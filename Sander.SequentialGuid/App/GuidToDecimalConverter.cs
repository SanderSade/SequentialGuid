using System;
using System.Runtime.InteropServices;

namespace Sander.SequentialGuid.App
{
	/// <summary>
	/// Based on https://stackoverflow.com/a/3563872/3248515
	/// </summary>
	internal static class GuidToDecimalConverter
	{
		private static DecimalGuidConverter _converter;

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

		[StructLayout(LayoutKind.Explicit)]
		private struct DecimalGuidConverter
		{
			[FieldOffset(0)]
			internal decimal Decimal;

			[FieldOffset(0)]
			internal Guid Guid;
		}
	}
}
