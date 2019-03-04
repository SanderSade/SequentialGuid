using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Sander.SequentialGuid;

namespace PerformanceTests
{
	[MemoryDiagnoser]
	[ClrJob(true)]
	public class CharacterAtBenchmark
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
		public List<char> UsingString()
		{
			var result = new List<char>(Count);
			for (var i = 0; i < Count; i++)
			{
				result.Add(_guids[i].ToString()[9]);
			}

			return result;
		}


		[Benchmark]
		public List<char> UsingCharAt()
		{
			var result = new List<char>(Count);
			for (var i = 0; i < Count; i++)
			{
				result.Add(_guids[i].GetCharacterAt(9));
			}

			return result;
		}
	}
}