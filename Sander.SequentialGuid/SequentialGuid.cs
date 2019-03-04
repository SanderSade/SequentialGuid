using System;
using System.Numerics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Sander.SequentialGuid.Tests")]

namespace Sander.SequentialGuid
{
	/// <summary>
	///     Sequential GUID
	///     <para>Thread-safe</para>
	/// </summary>
	public class SequentialGuid
	{
		private readonly object _lock = new object();
		private BigInteger _guidInteger;

		/// <summary>
		///     Creates a sequential GUID based on random GUID
		/// </summary>
		public SequentialGuid() : this(Guid.NewGuid())
		{
		}

		/// <summary>
		///     Create sequential GUID from existing GUID
		/// </summary>
		public SequentialGuid(Guid originalGuid) =>
			_guidInteger = originalGuid.AsBigInteger();

		/// <summary>
		///     Return next sequential value of GUID
		/// </summary>
		/// <returns></returns>
		public Guid Next()
		{
			lock (_lock)
			{
				_guidInteger = _guidInteger + 1;
				return GuidHelper.FromBigInteger(_guidInteger);
			}
		}
	}
}
