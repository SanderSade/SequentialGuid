using System;
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
			var next = sequential.Next();
			Trace.WriteLine($"{guid}:{next}");
			var bi = next.ToBigInteger();
			Assert.AreEqual(1, bi);
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
	}
}
