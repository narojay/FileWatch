# FileWatch

## 功能

.net core 监听文件夹变化， FileSystemWatcher 封装

## 问题1

实际运行发现一个bug，那就是第一次往文件夹里面Copy一个新文件能正常运行，但是Copy第二个文件的时候就报一个文件正被其他线程占用无法打开的异常：
FileSystemWatcher有个问题，就是当新文件到达了以后，**Watcher太灵敏，文件到达了，IO拷贝还没有完成，其Created事件就已经触发了**。
也就是说，FileSystemWatcher的Created事件不是在新文件到达拷贝完成的时候触发的，而是这个事件在文件一创建还没有IO拷贝完成的时候就触发了。
这就造成两个线程在读取同一个文件的异常了。

## 如何解决?

在实例化文件流之前睡眠一会

` Thread.Sleep(100);`

`using (var file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))`

## 问题2
一次性复制很多到文件夹结果只有一个事件被捕获
Windows 操作系统会将FileSystemWatcher创建的缓冲区中的文件更改通知您的组件。如果短时间内有很多变化，缓冲区可能会溢出。这会导致组件无法跟踪目录中的更改，并且只会提供全面通知。使用InternalBufferSize属性增加缓冲区的大小是昂贵的，因为它来自无法换出到磁盘的非分页内存，因此请保持缓冲区足够小但足够大，以免错过任何文件更改事件。为避免缓冲区溢出，，以便您可以过滤掉不需要的更改通知。

## 如何解决?

请使用NotifyFilter和IncludeSubdirectories属性



## 文档

[文档](https://docs.microsoft.com/en-us/dotnet/api/system.io.filesystemwatcher?redirectedfrom=MSDN&view=net-5.0)
