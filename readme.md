[![GitHub license](https://img.shields.io/badge/licence-MPL%202.0-brightgreen.svg)](https://github.com/SanderSade/SequentialGuid/blob/master/LICENSE)
[![NetStandard 2.0](https://img.shields.io/badge/-.NET%20Standard%202.0-green.svg)](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md)

## Introduction

By default, [GUIDs](https://en.wikipedia.org/wiki/Universally_unique_identifier) are not alphanumerically continuous or sortable in a meaningful way.

A very common use for GUID is a primary key in the database - but with non-sequential GUIDs, it is not optimal to have the primary key as a [clustered index](https://docs.microsoft.com/en-us/sql/relational-databases/indexes/clustered-and-nonclustered-indexes-described?view=sql-server-2017). Using clustered index with nonsequential GUIDs can cause fragmentation and general performance issues.

To resolve this, SQL Server has [NEWSEQUENTIALID](https://docs.microsoft.com/en-us/sql/t-sql/functions/newsequentialid-transact-sql?view=sql-server-ver15), which creates alphanumerically sortable, sequential GUIDs. The downside ot this approach is that the application will have to wait the SQL Server to create the primary key before the entry becomes usable - and of course, there are other database engines that do not have similar functionality.

Windows has a native [UuidCreateSequential](https://docs.microsoft.com/en-us/windows/desktop/api/rpcdce/nf-rpcdce-uuidcreatesequential) function, which is not available on other .NET platforms.

SequentialGuid library is implemented as a .NET Standard 2.0 package, allowing creation of sortable GUIDs prior storing data in the database on any compatible platform. In addition, there are useful helper functions to convert to/from GUID or get specific chartacter/byte.

SequentialGuid is aimed for high-performance applications, as other such libraries are often very underperforming or do not have comparable functionality. SequentialGuid performance is similar to the native UuidCreateSequential, see [benchmarks](https://github.com/SanderSade/SequentialGuid/blob/master/Tests/PerformanceTests/Results/PerformanceTests.SequentialGuidBenchmark-report-github.md).


### Features
* Flexible - define the starting GUID and step, or use the default values
* Fast and memory-efficient - even on a laptop, SequentialGuid handles well over 25 million calls per second. This is comparable or better than the UuidCreateSequential performance
* Thread-safe - create a single SequentialGuid instance for your web application and use it to generate sequential IDs
* .NET Standard 2.0
* Useful helper and extension methods, see below.

### Using SequentialGuid
Create one instance of SequentialGuid per sequence (most likely database table), sharing the instance among all the components which need that.


```C#
public MyClass()
{
...
	var lastId = _myDatabaseProvider.GetLastId(); //get last primary key value (GUID) from the database
	_sequentialGuid = new SequentialGuid(lastId, 32); //initialize SequentialGuid with lastId as base value and step 32
	Trace.WriteLine(_sequentialGuid.Original); //trace the value that we used
}

public async Task<Guid> InsertEntity(ParentObject parent, ChildObject child)
{
	var nextId = _sequentialGuid.Next(); //get next sequential GUID, i.e. last + 32
	parent.Id = nextId; //set the primary key for DB
	child.ParentId = nextId; //set up the child-parent relation without having to insert parent object first and wait for the result
	var task1 = _myDatabaseProvider.InsertAsync(parent); 
	var task2 = _otherDatabaseProvider.InsertChildAsync(child);
	await Task.WhenAll(task1, task2); //wait for both insertions to complete
	return nextId;
}

```


#### Extension methods
Extension methods are implemented in class GuidExtensions.

* **`GetCharacterAt(int position)`**- get a hexadecimal character at the specified position in GUID (0..31). As this does not rely on GUID being cast to string, it is much faster and uses less memory than default option - see the [benchmark](https://github.com/SanderSade/SequentialGuid/blob/master/Tests/PerformanceTests/Results/PerformanceTests.CharacterAtBenchmark-report-github.md).
*  **`GetByteAt(int position)`** - get byte at the specified position (0..15). Somewhat faster than ``ToByteArray()[position]``, and uses ~30x less memory, see the [benchmark](https://github.com/SanderSade/SequentialGuid/blob/master/Tests/PerformanceTests/Results/PerformanceTests.GetByteAtBenchmark-report-github.md). Note that this uses "Microsoft" byte order (see next method), to be compliant with ToByteArray() output.
* **`ToCompliantByteArray()`** - .NET and Microsoft use [different byte order](https://stackoverflow.com/questions/9195551/why-does-guid-tobytearray-order-the-bytes-the-way-it-does) inside GUID structure than other platforms (Java, Python and more). This returns byte array in the same order as Java/Python, and is suitable to be put to the data stores without native GUID/UUID implementation. Use `GuidHelper.FromCompliantByteArray()` to reverse the operation, as the [`new Guid(byte[] b)`](https://docs.microsoft.com/en-us/dotnet/api/system.guid.-ctor?view=netframework-4.7.2#System_Guid__ctor_System_Byte___) constructor expects ToByteArray()/Microsoft byte order.
* **`ToBigInteger(bool isCompliant)`** - convert GUID to [BigInteger](https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2). isCompliant = true is the default, and uses the same approach as ToCompliantByteArray() above, creating integer compatible with other systems and websites (e.g. http://guid-convert.appspot.com).
* **`ToLongs()`** - convert GUID to (long, long) C# 7 tuple. Generally this should not be needed, but some Javascript libraries have used two integers to represent GUID, which doesn't exist as a native data type in JS.

#### Helper methods
Helper methods are in static class GuidHelper.

* **`MaxValue`** - opposite of `Guid.Empty`, returns the maximum GUID value (ffffffff-ffff-ffff-ffff-ffffffffffff). Useful mainly for testing.
* **`FromBigInteger(BigInteger integer, bool isCompliant)`** - creates GUID from BigInteger. If the BigInteger is too large (over 16 bytes), excess bytes are trimmed. Defaults to isCompliant = true, using Java/Python-compliant byte ordering.
* **`FromDecimal(decimal dec)`** - create GUID from .NET decimal number
* **`FromLongs(long first, long second)`** - create GUID from two Int64/long numbers. Generally this should not be needed, but some Javascript libraries have used two integers to represent GUID, which doesn't exist as a native data type in JS.
* **`FromCompliantByteArray(byte[] bytes)`** - see `ToCompliantByteArray()` above.

### Dependencies
* System.Runtime.Numerics 4.3.0
* System.ValueTuple 4.5.0