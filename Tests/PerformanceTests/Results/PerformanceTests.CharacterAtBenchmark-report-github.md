## Get character at the specified position, 25M operations
``` ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17763.316 (1809/October2018Update/Redstone5)
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 4 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3362.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3362.0

Job=Clr  Runtime=Clr  

```
|      Method |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------ |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
| UsingString | 5,249.2 ms | 66.014 ms | 61.750 ms |  1.00 | 524000.0000 |           - |           - |           2145.8 MB |
|             |            |           |           |       |             |             |             |                     |
| UsingCharAt |   583.7 ms |  6.453 ms |  5.388 ms |  1.00 |           - |           - |           - |            47.69 MB |

