# TODO

```c#
        public string Checksum()
        {
            using var md5 = MD5.Create();
            var inputData = JsonSerializer.SerializeToUtf8Bytes(this);
            var hash = md5.ComputeHash(inputData);
            return BitConverter.ToString(hash).Replace("-", "");
        }
```
