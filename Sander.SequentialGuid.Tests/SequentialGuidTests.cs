﻿using System;
using System.Diagnostics;
using System.Linq;
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
			var guid = Guid.NewGuid();
			var sequential = new SequentialGuid(guid);
			Enumerable.Range(0, 1000000)
				.AsParallel()
				.WithDegreeOfParallelism(512)
				.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
				.ForAll(x => sequential.Next());

			var next = sequential.Next();
			var original = guid.ToBigInteger();
			var diff = next.ToBigInteger() - original;
			Trace.WriteLine($"{guid} - {next} = {diff}");

			Assert.AreEqual(1000001,diff);
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
		public void NegativeStepTest()
		{
			var guid = Guid.NewGuid();
			var original = guid.ToBigInteger();
			Trace.WriteLine(original);
			var sequential = new SequentialGuid(guid, -1);
			for (var i = 1; i < 100000; i++)
			{
				var next = sequential.Next();
				var bi = next.ToBigInteger();
				Trace.WriteLine($"{bi}:{next}");
				Assert.AreEqual(original - i, bi);
			}

		}
	}
}
