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
		private SequentialGuid _sequentialGuid;

		[GlobalSetup]
		public void Setup() =>
			_sequentialGuid = new SequentialGuid();


		[Benchmark]
		public List<Guid> SequentialTest()
		{
			var result = new List<Guid>(Count);
			for (var i = 0; i < Count; i++)
			{
				result.Add(_sequentialGuid.Next());
			}

			return result;
		}
	}
}
