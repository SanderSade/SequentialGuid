using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sander.SequentialGuid.Tests
{
	[TestClass]
	public sealed class SequentialGuidTests
	{
		[TestMethod]
		public void EmptyGuidTest()
		{
			var guid = Guid.Empty;
			Trace.WriteLine(guid.ToBigInteger());
			var sequential = new SequentialGuid(guid);
			for (var i = 1; i < 100000; i++)
			{
				var next = sequential.Next();
				var bi = next.ToBigInteger();
				Trace.WriteLine($"{bi}:{next}");
				Assert.AreEqual(i, bi);
			}

		}

		[TestMethod]
		public void MultiThreadTest()
		{
			for (var i = 10_000; i < 12_000; i++)
			{
				var guid = Guid.NewGuid();
				var sequential = new SequentialGuid(guid);
				Enumerable.Range(0, i)
					.AsParallel()
					.WithDegreeOfParallelism(512)
					.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
					.ForAll(_ => sequential.Next());

				var next = sequential.Next();
				var original = guid.ToBigInteger();
				var diff = next.ToBigInteger() - original;
				Trace.WriteLine($"{guid} - {next} = {diff}");

				Assert.AreEqual(i + 1, diff);
			}
		}

		[TestMethod]
		public void CurrentTest()
		{
			var guid = Guid.NewGuid();
			var sequential = new SequentialGuid(guid);
			Assert.AreEqual(guid, sequential.Current);
			sequential.Next();
			Assert.AreNotEqual(guid, sequential.Current);
		}

		[TestMethod]
		public void OriginalTest()
		{
			var guid = Guid.NewGuid();
			var sequential = new SequentialGuid(guid);
			Assert.AreEqual(guid, sequential.Original);
			sequential.Next();
			Assert.AreNotEqual(sequential.Original, sequential.Current);
		}

		[TestMethod]
		public void MultiInstanceTest()
		{

			Enumerable.Range(1, 255)
				.AsParallel()
				.WithDegreeOfParallelism(100)
				.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
				.ForAll(x =>
				{
					var guid = Guid.NewGuid();
					var original = guid.ToBigInteger();
					var sequential = new SequentialGuid(guid, (byte)x);

					for (var i = 1; i < 100000; i++)
					{
						var next = sequential.Next();
						var bi = BigInteger.Add(original, i * x);
						var expected = GuidHelper.FromBigInteger(bi);
						if (expected != next)
						{
							Trace.WriteLine($"{x}({i}): Original = {guid}. Next: {next} vs {expected}");
							Trace.Flush();
						}

						Assert.AreEqual(expected, next);
					}
				});
		}


		[TestMethod]
		[ExpectedException(typeof(OverflowException))]
		public void OverFlowTest()
		{
			var guid = GuidHelper.MaxValue;
			var sequential = new SequentialGuid(guid);
			sequential.Next();
		}


		[TestMethod]
		public void AlmostOverFlowTest()
		{
			var guid = GuidHelper.MaxValue;
			var bi = guid.ToBigInteger() - 1;
			var almostMax = GuidHelper.FromBigInteger(bi);
			var sequential = new SequentialGuid(almostMax);
			var next = sequential.Next();
			Trace.WriteLine($"{almostMax}: {next}");
			Assert.AreEqual(guid, next);
		}


		[TestMethod]
		public void CheckSortingTest()
		{
			for (byte i = 1; i < 0xFF; i++)
			{
				var guid = Guid.NewGuid();
				var sequential = new SequentialGuid(guid, i);
				var count = 10_000;
				var guids = new List<Guid>(count);
				var strings = new List<string>(count);

				for (var j = 0; j < count; j++)
				{
					var s = sequential.Next();
					guids.Add(s);
					strings.Add(s.ToString("N"));
				}

				strings.Sort();

				for (var j = 0; j < count; j++)
				{
					var g = new Guid(strings[j]);
					if (g != guids[j])
						Trace.WriteLine($"Expected {g}, got {guids[j]}");

					Assert.AreEqual(g, guids[j]);
				}

			}
		}



		[TestMethod]
		public void MultithreadDuplicateTest()
		{
			for (var i = 10_000; i < 12_000; i++)
			{
				var bag = new ConcurrentBag<Guid>();
				var guid = Guid.NewGuid();
				var sequential = new SequentialGuid(guid);
				Enumerable.Range(0, i)
					.AsParallel()
					.WithDegreeOfParallelism(512)
					.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
					.ForAll(_ => bag.Add(sequential.Next()));

				var distinct = bag.Distinct().ToList();

				Assert.AreEqual(distinct.Count, bag.Count);
			}
		}

	}
}
