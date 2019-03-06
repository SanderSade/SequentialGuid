using System;

namespace Sander.SequentialGuid.App
{
	/// <summary>
	/// </summary>
	internal static class GuidConverter
	{
		internal static unsafe Guid DecimalToGuid(decimal dec)
		{
			return *(Guid*)(void*)&dec;
		}

		internal static unsafe decimal GuidToDecimal(Guid guid)
		{
			return *(decimal*)(void*)&guid;
		}

		/// <summary>
		///     From https://stackoverflow.com/a/49380620/3248515
		/// </summary>
		internal static unsafe void GuidToInt64(Guid value, out long x, out long y)
		{
			var ptr = (long*)&value;
			x = *ptr++;
			y = *ptr;
		}

		/// <summary>
		///     From https://stackoverflow.com/a/49380620/3248515
		/// </summary>
		internal static unsafe Guid GuidFromInt64(long x, long y)
		{
			var ptr = stackalloc long[2];
			ptr[0] = x;
			ptr[1] = y;
			return *(Guid*)ptr;
		}
	}
}
