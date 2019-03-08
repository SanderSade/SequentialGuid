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
		private readonly bool _isPythonCompliant;
		private readonly object _lock = new object();
		private readonly int _step;
		private BigInteger _guidInteger;

		/// <summary>
		///     Creates a sequential GUID based on random GUID, optionally defining step
		///     <para>Step can be negative, and defaults to 1</para>
		///     <para>
		///         Defaults to isPythonCompliant = true, as this is the more common use outside Microsoft/.NET.
		///         E.g. compatible with http://guid-convert.appspot.com/ and Java UUID.
		///         See also https://stackoverflow.com/questions/9195551/why-does-guid-tobytearray-order-the-bytes-the-way-it-does
		///     </para>
		/// </summary>
		public SequentialGuid(int step = 1, bool isPythonCompliant = true) : this(Guid.NewGuid(), step,
			isPythonCompliant)
		{
		}

		/// <summary>
		///     Create sequential GUID from existing GUID, optionally defining step
		///     <para>Step can be negative, and defaults to 1</para>
		///     <para>
		///         Defaults to isPythonCompliant = true, as this is the more common use outside Microsoft/.NET.
		///         E.g. compatible with http://guid-convert.appspot.com/ and Java UUID.
		///         See also https://stackoverflow.com/questions/9195551/why-does-guid-tobytearray-order-the-bytes-the-way-it-does
		///     </para>
		/// </summary>
		public SequentialGuid(Guid originalGuid, int step = 1, bool isPythonCompliant = true)
		{
			_step = step;
			_isPythonCompliant = isPythonCompliant;
			_guidInteger = originalGuid.ToBigInteger(_isPythonCompliant);
		}

		/// <summary>
		///     Get the current value of the sequential GUID
		/// </summary>
		public Guid Current
		{
			get
			{
				lock (_lock)
				{
					return GuidHelper.FromBigInteger(_guidInteger, _isPythonCompliant);
				}
			}
		}

		/// <summary>
		///     Return next sequential value of GUID
		/// </summary>
		public Guid Next()
		{
			lock (_lock)
			{
				_guidInteger += _step;
				return GuidHelper.FromBigInteger(_guidInteger, _isPythonCompliant);
			}
		}
	}
}
