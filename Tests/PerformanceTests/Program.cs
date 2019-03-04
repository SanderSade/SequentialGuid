using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Sander.SequentialGuid;

namespace PerformanceTests
{
	public class Program
	{
		public static void Main()
		{
			//var sqb = new SequentialGuidBenchmark();
			//sqb.Setup();
			//var sw = Stopwatch.StartNew();
			//var result = sqb.SequentialTest();
			//sw.Stop();
			//Console.WriteLine($"{SequentialGuidBenchmark.Count} steps took {sw.Elapsed}");

			//var guid = Guid.NewGuid();
			//var c = guid.GetCharacterAt(2);
			//Console.ReadKey();
			//return;

		//	var summary = BenchmarkRunner.Run<SequentialGuidBenchmark>();
			var summary = BenchmarkRunner.Run<CharacterAtBenchmark>();
			Console.Write(summary.AllRuntimes);
			Console.ReadKey();
		}

		[MemoryDiagnoser]
		[ClrJob(true)]
		public class SequentialGuidBenchmark
		{
			internal const int Count = 10000000;
			private SequentialGuid _sequentialGuid;

			[GlobalSetup]
			public void Setup() =>
				_sequentialGuid = new SequentialGuid();


			[Benchmark]
			public List<Guid> SequentialTest()
			{
				var result = new List<Guid>(Count);
				for (int i = 0; i < Count; i++)
				{
					result.Add(_sequentialGuid.Next());
				}

				return result;
			}
		}



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
					result.Add(_guids[i].ToString()[0]);
				}

				return result;
			}


			[Benchmark]
			public List<char> UsingCharAt()
			{
				var result = new List<char>(Count);
				for (var i = 0; i < Count; i++)
				{
					result.Add(_guids[i].GetCharacterAt(0));
				}

				return result;
			}
		}
	}
}
