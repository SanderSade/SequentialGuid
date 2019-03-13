using System;
using System.Runtime.InteropServices;

namespace Sander.SequentialGuid.App
{
	/// <summary>
	///     Map GUID to individual bytes. May not be the most elegant approach, but is fast...
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	internal struct GuidBytes
	{
		[FieldOffset(0)]
		internal Guid Guid;

		[FieldOffset(0)]
		internal byte B0;

		[FieldOffset(1)]
		internal byte B1;

		[FieldOffset(2)]
		internal byte B2;

		[FieldOffset(3)]
		internal byte B3;

		[FieldOffset(4)]
		internal byte B4;

		[FieldOffset(5)]
		internal byte B5;

		[FieldOffset(6)]
		internal byte B6;

		[FieldOffset(7)]
		internal byte B7;

		[FieldOffset(8)]
		internal byte B8;

		[FieldOffset(9)]
		internal byte B9;

		[FieldOffset(10)]
		internal byte B10;

		[FieldOffset(11)]
		internal byte B11;

		[FieldOffset(12)]
		internal byte B12;

		[FieldOffset(13)]
		internal byte B13;

		[FieldOffset(14)]
		internal byte B14;

		[FieldOffset(15)]
		internal byte B15;

		/// <summary>
		/// Get "Microsoft" byte position
		/// </summary>
		internal byte GetByteAt(int position)
		{
			switch (position)
			{
				case 0: return B0;
				case 1: return B1;
				case 2: return B2;
				case 3: return B3;
				case 4: return B4;
				case 5: return B5;
				case 6: return B6;
				case 7: return B7;
				case 8: return B8;
				case 9: return B9;
				case 10: return B10;
				case 11: return B11;
				case 12: return B12;
				case 13: return B13;
				case 14: return B14;
				case 15: return B15;
				default:
					throw new IndexOutOfRangeException($"Position must be between 0 and 15, received {position}");
			}
		}
	}
}
