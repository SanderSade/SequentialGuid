``` ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17763.316 (1809/October2018Update/Redstone5)
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 4 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3362.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3362.0

Job=Clr  Runtime=Clr  

```
|          Method |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
| WithToByteArray | 15.62 ms | 0.1652 ms | 0.1380 ms |  1.00 |   6656.2500 |           - |           - |         28320.56 KB |
|                 |          |           |           |       |             |             |             |                     |
|   WithGetByteAt | 15.30 ms | 0.0842 ms | 0.0703 ms |  1.00 |     15.6250 |     15.6250 |     15.6250 |           976.72 KB |
