using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sander.SequentialGuid.Tests
{
	[TestClass]
	public class ConversionTests
	{
		[TestMethod]
		public void BigIntegerTest()
		{
			var guid = Guid.NewGuid();
			var bigInt = guid.ToBigInteger();
			Trace.WriteLine($"{guid}: {bigInt}");
			var revert = GuidHelper.FromBigInteger(bigInt);
			Assert.AreEqual(guid, revert);
		}

		[TestMethod]
		public void DecimalTest()
		{
			var guid = Guid.NewGuid();
			var dec = guid.ToDecimal();
			Trace.WriteLine($"{guid}: {dec}");
			var revert = GuidHelper.FromDecimal(dec);
			Assert.AreEqual(guid, revert);
		}

		[TestMethod]
		public void Int64Test()
		{
			var guid = Guid.NewGuid();
			var longs = guid.ToLongs();
			Trace.WriteLine($"{guid}: {longs.Item1}, {longs.Item2}");
			var revert = GuidHelper.FromLongs(longs.Item1, longs.Item2);
			Assert.AreEqual(guid, revert);
		}
	}
}
