using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Sander.SequentialGuid;

namespace PerformanceTests
{
	[MemoryDiagnoser]
	[ClrJob(true)]
	public class GetByteAtBenchmark
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
		public List<byte> WithToByteArray()
		{
			var result = new List<byte>(Count);
			for (var i = 0; i < Count; i++)
			{
				result.Add(_guids[i].ToByteArray()[9]);
			}

			return result;
		}


		[Benchmark]
		public List<byte> WithGetByteAt()
		{
			var result = new List<byte>(Count);
			for (var i = 0; i < Count; i++)
			{
				result.Add(_guids[i].GetByteAt(9));
			}

			return result;
		}
	}
}
