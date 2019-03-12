``` ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17763.316 (1809/October2018Update/Redstone5)
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 4 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3362.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3362.0

Job=Clr  Runtime=Clr  

```
|               Method |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|       SequentialTest | 3.833 ms | 0.0763 ms | 0.1396 ms |  1.00 |    332.0313 |    332.0313 |    332.0313 |             1.53 MB |
|                      |          |           |           |       |             |             |             |                     |
| NativeSequentialTest | 3.427 ms | 0.0193 ms | 0.0171 ms |  1.00 |    328.1250 |    328.1250 |    328.1250 |             1.53 MB |
