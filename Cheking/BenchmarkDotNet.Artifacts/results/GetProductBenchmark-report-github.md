```

BenchmarkDotNet v0.15.8, Windows 10 (10.0.19045.6466/22H2/2022Update)
Intel Core i5-7200U CPU 2.50GHz (Max: 2.71GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.311
  [Host]     : .NET 9.0.13 (9.0.13, 9.0.1326.6317), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.13 (9.0.13, 9.0.1326.6317), X64 RyuJIT x86-64-v3


```
| Method      | Mean     | Error    | StdDev   | Median   | Gen0   | Allocated |
|------------ |---------:|---------:|---------:|---------:|-------:|----------:|
| GetProducts | 260.9 ns | 28.36 ns | 82.73 ns | 225.6 ns | 0.2141 |     336 B |
