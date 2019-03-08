using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Sander.SequentialGuid;

namespace PerformanceTests
{
	[MemoryDiagnoser]
	[ClrJob(true)]
	public class SequentialGuidBenchmark
	{
		internal const int Count = 10000;



		[Benchmark]
		public List<Guid> SequentialCompliantTest()
		{
			var sequentialGuid = new SequentialGuid();
			var result = new List<Guid>(Count);
			for (var i = 0; i < Count; i++)
			{
				result.Add(sequentialGuid.Next());
			}

			return result;
		}


		[Benchmark]
		public List<Guid> SequentialTest()
		{
			var sequentialGuid = new SequentialGuid(1, false);
			var result = new List<Guid>(Count);
			for (var i = 0; i < Count; i++)
			{
				result.Add(sequentialGuid.Next());
			}

			return result;
		}


	}
}
