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
		private readonly int _step;
		private readonly object _lock = new object();
		private BigInteger _guidInteger;

		/// <summary>
		///     Creates a sequential GUID based on random GUID, optionally defining step
		/// <para>Step can be negative, and defaults to 1</para>
		/// </summary>
		public SequentialGuid(int step = 1) : this(Guid.NewGuid(), step)
		{
		}

		/// <summary>
		///     Create sequential GUID from existing GUID, optionally defining step
		/// <para>Step can be negative, and defaults to 1</para>
		/// </summary>
		public SequentialGuid(Guid originalGuid, int step = 1)
		{
			_step = step;
			_guidInteger = originalGuid.ToBigInteger();
		}

		/// <summary>
		///     Return next sequential value of GUID
		/// </summary>
		public Guid Next()
		{
			lock (_lock)
			{
				_guidInteger += _step;
				return GuidHelper.FromBigInteger(_guidInteger);
			}
		}

		/// <summary>
		/// Get the current value of the sequential GUID
		/// </summary>
		public Guid Current
		{
			get
			{
				lock (_lock)
				{
					return GuidHelper.FromBigInteger(_guidInteger);
				}
			}
		}
	}
}
