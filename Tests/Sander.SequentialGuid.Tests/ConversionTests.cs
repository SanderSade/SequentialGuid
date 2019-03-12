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
			for (var i = 0; i < 10000; i++)
			{
				var guid = Guid.NewGuid();
				var bigInt = guid.ToBigInteger();
				Trace.WriteLine($"{guid}: {bigInt}");
				var revert = GuidHelper.FromBigInteger(bigInt);
				Assert.AreEqual(guid, revert);
			}
		}

		[TestMethod]
		public void BigIntegerNonPythonTest()
		{
			for (var i = 0; i < 10000; i++)
			{
				var guid = Guid.NewGuid();
				var bigInt = guid.ToBigInteger(false);
				Trace.WriteLine($"{guid}: {bigInt}");
				var revert = GuidHelper.FromBigInteger(bigInt, false);
				Assert.AreEqual(guid, revert);
			}
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
		public void BigIntegerPythonTest()
		{
			var guid = new Guid("ff8aa059-fcce-4757-99fc-dffd2d8d2599");
			var bigInt = guid.ToBigInteger();
			Assert.AreEqual("339672928206713999937804465197131048345", bigInt.ToString());
			var reverse = GuidHelper.FromBigInteger(bigInt);
			Assert.AreEqual(guid, reverse);
		}

		[TestMethod]
		public void BigIntegerShortTest()
		{
			for (var i = 0; i < 10000; i++)
			{
				var bigInt = i;
				var guid = GuidHelper.FromBigInteger(bigInt);
				Trace.WriteLine($"{guid}: {bigInt}");
				var reverse = guid.ToBigInteger();
				Assert.AreEqual(bigInt, reverse);
			}
		}

		[TestMethod]
		public void BigIntegerShortNonPythonTest()
		{
			for (var i = 0; i < 10000; i++)
			{
				var bigInt = i;
				var guid = GuidHelper.FromBigInteger(bigInt, false);
				Trace.WriteLine($"{guid}: {bigInt}");
				var reverse = guid.ToBigInteger(false);
				Assert.AreEqual(bigInt, reverse);
			}
		}

		[TestMethod]
		public void BigIntegerOverflowTest()
		{
			var guid = GuidHelper.MaxValue;
			var bigInt = guid.ToBigInteger();
			bigInt++;
			var revert = GuidHelper.FromBigInteger(bigInt);
			Trace.WriteLine($"{guid}: {revert}");
			Assert.AreEqual(Guid.Empty, revert);
		}



		[TestMethod]
		public void DecimalMaxTest()
		{
			var dec = decimal.MaxValue;
			var guid = GuidHelper.FromDecimal(dec);
			Trace.WriteLine($"{guid}: {dec}");
		}



		[TestMethod]
		public void Int64Test()
		{
			for (var j = 0; j < 1000; j++)
			{
				var guid = Guid.NewGuid();
				var longs = guid.ToLongs();
				Trace.WriteLine($"{guid}: {longs.Item1}, {longs.Item2}");
				var revert = GuidHelper.FromLongs(longs.Item1, longs.Item2);
				Assert.AreEqual(guid, revert);
			}
		}


		[TestMethod]
		public void GetCharacterTest()
		{
			for (var j = 0; j < 1000; j++)
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
		}


		[TestMethod]
		public void GetByteTest()
		{
			for (var j = 0; j < 1000; j++)
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


		[TestMethod]
		public void CompliantByteArrayTest()
		{
			for (var i = 0; i < 1000; i++)
			{
				var guid = Guid.NewGuid();
				var array = guid.ToCompliantByteArray();
				var rev = new Guid(array);

				Assert.AreNotEqual(guid, rev);
				var byteRev = GuidHelper.FromCompliantByteArray(array);
				Assert.AreEqual(guid, byteRev);
			}
		}

	}
}
