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
		public void BigIntegerEmptyTest()
		{
			var guid = Guid.Empty;
			var bigInt = guid.ToBigInteger();
			Trace.WriteLine($"{guid}: {bigInt}");
			var revert = GuidHelper.FromBigInteger(bigInt);
			Assert.AreEqual(guid, revert);
		}


		[TestMethod]
		public void BigIntegerOverflowTest()
		{
			var guid = new Guid("ffffffffffffffffffffffffffffffff");
			var bigInt = guid.ToBigInteger();
			bigInt++;
			var revert = GuidHelper.FromBigInteger(bigInt);
			Trace.WriteLine($"{guid}: {revert}");
			Assert.AreEqual(Guid.Empty, revert);
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
		public void DecimalEmptyTest()
		{
			var guid = Guid.Empty;
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

		[TestMethod]
		public void Int64MaxTest()
		{
			var guid = GuidHelper.FromLongs(long.MaxValue, long.MaxValue);
			Trace.WriteLine(guid);
		}


		[TestMethod]
		public void Int64OverflowTest()
		{
			var guid = new Guid("ffffffffffffffffffffffffffffffff");
			var longs = guid.ToLongs();

			Trace.WriteLine($"{longs.Item1}.{longs.Item2} {guid}");
		}

		[TestMethod]
		public void GetCharacterTest()
		{
			var guid = Guid.NewGuid();
			var guidString = guid.ToString("N");
			Trace.WriteLine(guid);
			Trace.WriteLine(guidString);
			for (var i = 0; i < 32; i++)
			{
				var c = guid.GetCharacterAt(i);
				var s = guidString[i];
				Trace.Write($"{s}:{c}.");
				Assert.AreEqual(s, c);
			}
		}


		[TestMethod]
		public void GetByteTest()
		{
			var guid = Guid.NewGuid();
			var byteArray = guid.ToByteArray();
			Trace.WriteLine(guid);
			for (var i = 0; i < 16; i++)
			{
				var a = guid.GetByteAt(i);
				var s = byteArray[i];
				Trace.Write($"{s}:{a}.");
				Assert.AreEqual(s, a);
			}
		}
	}
}
