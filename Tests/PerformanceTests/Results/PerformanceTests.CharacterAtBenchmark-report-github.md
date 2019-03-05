``` ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17763.316 (1809/October2018Update/Redstone5)
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 4 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|      Method |      Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------ |----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
| UsingString | 189.12 ms | 3.7498 ms | 8.2309 ms |  1.00 |  20666.6667 |           - |           - |            85.83 MB |
|             |           |           |           |       |             |             |             |                     |
| UsingCharAt |  21.10 ms | 0.4065 ms | 0.4681 ms |  1.00 |           - |           - |           - |             1.91 MB |
