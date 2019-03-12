using System;
using System.Runtime.CompilerServices;
using Sander.SequentialGuid.App;

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
		private readonly byte _step;
		private GuidBytes _guidBytes;


		/// <summary>
		///     Creates a sequential GUID based on random GUID, optionally defining step
		///     <para>Step defaults to 1</para>
		/// </summary>
		public SequentialGuid(byte step = 1) : this(Guid.NewGuid(), step)
		{
		}

		/// <summary>
		///     Create sequential GUID from existing GUID, optionally defining step
		///     <para>Step defaults to 1</para>
		/// </summary>
		public SequentialGuid(Guid originalGuid, byte step = 1)
		{
			if (step == 0x00)
				throw new ArgumentOutOfRangeException(nameof(step), "Step cannot be 0!");

			Original = originalGuid;
			_step = step;
			_guidBytes.Guid = originalGuid;
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
					return _guidBytes.Guid;
				}
			}
		}

		/// <summary>
		/// Return original GUID (first in sequence)
		/// </summary>
		public Guid Original { get; }

		/// <summary>
		///     Return next sequential value of GUID
		/// </summary>
		public Guid Next()
		{
			lock (_lock)
			{
				//this is really non-elegant, rethink this!
				if (!StepByte(ref _guidBytes.B15, _step) && !StepByte(ref _guidBytes.B14) &&
					!StepByte(ref _guidBytes.B13) &&
					!StepByte(ref _guidBytes.B12) && !StepByte(ref _guidBytes.B11) && !StepByte(ref _guidBytes.B10) &&
					!StepByte(ref _guidBytes.B9) && !StepByte(ref _guidBytes.B8) && !StepByte(ref _guidBytes.B7) &&
					!StepByte(ref _guidBytes.B6) && !StepByte(ref _guidBytes.B5) && !StepByte(ref _guidBytes.B4) &&
					!StepByte(ref _guidBytes.B3) && !StepByte(ref _guidBytes.B2) && !StepByte(ref _guidBytes.B1))
					StepByte(ref _guidBytes.B0);

				return _guidBytes.Guid;
			}
		}

		/// <summary>
		///     Try to add _step to rightmost byte, and step up others in case of 0xFF
		///     Return false in case of overflow (next byte from right needs to be incremented by 1)
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool StepByte(ref byte currentByte, byte step = 1)
		{
			var result = currentByte + step > 0xff;
			currentByte = (byte)(currentByte + step - (result ? 256 : 0));
			return !result;
		}
	}
}
