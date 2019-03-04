using System;
using System.Runtime.InteropServices;

namespace Sander.SequentialGuid.App
{
	/// <summary>
	///     From https://stackoverflow.com/a/49372627/3248515
	/// </summary>
	internal sealed class GuidToLongConverter
	{
		private static GuidConverter _converter;

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
		private struct GuidConverter
		{
			//[FieldOffset(0)]
			//internal decimal Decimal;
			[FieldOffset(0)]
			internal Guid Guid;

			[FieldOffset(0)]
			internal long Long1;

			[FieldOffset(8)]
			internal long Long2;
		}
	}
}
