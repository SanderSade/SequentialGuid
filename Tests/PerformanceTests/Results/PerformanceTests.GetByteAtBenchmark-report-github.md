## Get byte at the specified position, 25M operations
``` ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17763.316 (1809/October2018Update/Redstone5)
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 4 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3362.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3362.0

Job=Clr  Runtime=Clr  

```
|          Method |     Mean |    Error |   StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------- |---------:|---------:|---------:|------:|------------:|------------:|------------:|--------------------:|
| WithToByteArray | 433.4 ms | 8.177 ms | 7.249 ms |  1.00 | 166000.0000 |           - |           - |           691.42 MB |
|                 |          |          |          |       |             |             |             |                     |
|   WithGetByteAt | 266.7 ms | 2.186 ms | 2.045 ms |  1.00 |           - |           - |           - |            23.85 MB |



