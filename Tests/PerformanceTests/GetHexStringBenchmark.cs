using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Sander.SequentialGuid;

namespace PerformanceTests
{
	[MemoryDiagnoser]
	[ClrJob(true)]
	public class GetHexStringBenchmark
	{
		private List<Guid> _guids;
		internal const int Count = 1000000;


		[GlobalSetup]
		public void Setup()
		{
			_guids = new List<Guid>(Count);
			for (var i = 0; i < Count; i++)
				_guids.Add(Guid.NewGuid());
		}



		[Benchmark]
		public List<string> WithToString()
		{
			var result = new List<string>(Count);
			for (var i = 0; i < Count; i++)
			{
				result.Add(_guids[i].ToString("N"));
			}

			return result;
		}


		[Benchmark]
		public List<string> WithToHexString()
		{
			var result = new List<string>(Count);
			for (var i = 0; i < Count; i++)
			{
				result.Add(_guids[i].ToHexString());
			}

			return result;
		}
	}
}
