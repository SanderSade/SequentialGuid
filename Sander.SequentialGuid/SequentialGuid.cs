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
			_guidInteger = originalGuid.ToBigInteger();

		/// <summary>
		///     Return next sequential value of GUID
		/// </summary>
		public Guid Next()
		{
			lock (_lock)
			{
				_guidInteger++;
				return GuidHelper.FromBigInteger(_guidInteger);
			}
		}

		/// <summary>
		/// Get the current value of the sequential GUID
		/// </summary>
		public Guid Current()
		{
			lock (_lock)
			{
				return GuidHelper.FromBigInteger(_guidInteger);
			}
		}
	}
}
