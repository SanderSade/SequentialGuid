using System;
using System.Collections.Generic;
using System.Diagnostics;
using BenchmarkDotNet.Running;
using Sander.SequentialGuid;

namespace PerformanceTests
{
	public class Program
	{
		public static void Main()
		{
			//var v = RunSequentialGeneration();
			//Console.WriteLine(v.Count);
			//Console.ReadKey();
			//return;
			var summary1 = BenchmarkRunner.Run<SequentialGuidBenchmark>();
			//var summary2 = BenchmarkRunner.Run<CharacterAtBenchmark>();
			//var summary3 = BenchmarkRunner.Run<GetByteAtBenchmark>();
			Console.Write(summary1.AllRuntimes);
			//Console.Write(summary2.AllRuntimes);
			//Console.Write(summary3.AllRuntimes);
			Console.ReadKey();
		}


		private static List<Guid> RunSequentialGeneration()
		{
			var sequentialGuid = new SequentialGuid(1);
			const int count = 30_000_000;
			var result = new List<Guid>(count);
			var sw = Stopwatch.StartNew();
			for (var i = 0; i < count; i++)
				result.Add(sequentialGuid.Next());

			sw.Stop();
			Console.WriteLine($"{count} steps took {sw.Elapsed}");

			return result;
		}

	}
}
