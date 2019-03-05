using System;
using BenchmarkDotNet.Running;

namespace PerformanceTests
{
	public class Program
	{
		public static void Main()
		{
			var summary1 = BenchmarkRunner.Run<SequentialGuidBenchmark>();
			//var summary2 = BenchmarkRunner.Run<CharacterAtBenchmark>();
			//var summary3 = BenchmarkRunner.Run<GetByteAtBenchmark>();
			Console.Write(summary1.AllRuntimes);
			//Console.Write(summary2.AllRuntimes);
			//Console.Write(summary3.AllRuntimes);
			Console.ReadKey();
		}
	}
}
