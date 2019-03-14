## Sequential GUID generation, 25 million steps

``` ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17763.316 (1809/October2018Update/Redstone5)
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 4 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3362.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3362.0

Job=Clr  Runtime=Clr  

```
|               Method |     Mean |     Error |    StdDev | Ratio |
|--------------------- |---------:|----------:|----------:|------:|
|       SequentialTest | 794.8 ms | 14.659 ms | 12.241 ms |  1.00 |
|                      |          |           |           |       |
| NativeSequentialTest | 881.6 ms |  7.803 ms |  6.516 ms |  1.00 |


