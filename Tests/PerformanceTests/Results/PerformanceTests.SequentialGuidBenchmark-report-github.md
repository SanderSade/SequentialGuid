``` ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17763.316 (1809/October2018Update/Redstone5)
Intel Core i5-4690K CPU 3.50GHz (Haswell), 1 CPU, 4 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|         Method |    Mean |    Error |   StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------- |--------:|---------:|---------:|------:|------------:|------------:|------------:|--------------------:|
| SequentialTest | 2.212 s | 0.0044 s | 0.0041 s |  1.00 | 267000.0000 |           - |           - |           953.68 MB |