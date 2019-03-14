using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using Sander.SequentialGuid;

namespace PerformanceTests
{
	[ClrJob(true)]
	public class SequentialGuidBenchmark
	{
		internal const int Count = 25_000_000;


		[Benchmark]
		public List<Guid> SequentialTest()
		{
			var sequentialGuid = new SequentialGuid(1);
			var result = new List<Guid>(Count);
			for (var i = 0; i < Count; i++)
				result.Add(sequentialGuid.Next());

			return result;
		}

		[DllImport("rpcrt4.dll", SetLastError = true)]
		private static extern int UuidCreateSequential(out Guid guid);

		[Benchmark]
		public List<Guid> NativeSequentialTest()
		{
			var result = new List<Guid>(Count);
			for (var i = 0; i < Count; i++)
			{
				if (UuidCreateSequential(out var guid) != 0)
					throw new ApplicationException ("UuidCreateSequential failed: " + UuidCreateSequential(out var g2));
				result.Add(guid);
			}

			return result;
		}
	}
}
