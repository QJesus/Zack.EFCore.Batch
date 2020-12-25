# Zack.EFCore.Batch
ʹ�����������, Entity Framework Core �û�����ʹ��LINQ���ɾ�����߸��¶������ݿ��¼������ִֻ��һ��SQL��䲢�Ҳ���Ҫ���Ȱ�ʵ�������ص��ڴ��С� 
���������֧�� Entity Framework Core 5.0�Լ����߰档  

����˵��:  
��һ������װNuGet��:
Postgresql��ʹ��Npgsql.EntityFrameworkCore.PostgreSQL���û�����ʹ��Install-Package Zack.EFCore.Batch.Npgsql

MS SQLServer�û�����ʹ��Install-Package Zack.EFCore.Batch.MSSQL

MySQL��ʹ��Pomelo.EntityFrameworkCore.MySql���û�����ʹ��Install-Package Zack.EFCore.Batch.MySQL.Pomelo

Sqlite�û�����ʹ��Install-Package Zack.EFCore.Batch.Sqlite

�ڶ���:
���ݲ�ͬ�����ݿ⣬��ֱ�����´�����ӵ����DbContext���OnConfiguring�����У�
```csharp
optionsBuilder.UseBatchEF_MSSQL();// MSSQL Server �û������
optionsBuilder.UseBatchEF_Npgsql();//Postgresql �û������
optionsBuilder.UseBatchEF_MySQLPomelo();//MySQL �û������
optionsBuilder.UseBatchEF_Sqlite();//Sqlite �û������
```

������:
ʹ��DbContext����չ����DeleteRangeAsync()��ɾ��һ������.
DeleteRangeAsync()�Ĳ������ǹ���������lambda���ʽ��
���Ӵ���:
```csharp
await ctx.DeleteRangeAsync<Book>(b => b.Price > n || b.AuthorName == "zack yang"); 
```

����Ĵ��뽫�������ݿ���ִ������SQL��䣺
```SQL
Delete FROM [T_Books] WHERE ([Price] > @__p_0) OR ([AuthorName] = @__s_1)
```

DeleteRange()������DeleteRangeAsync()��ͬ�������汾��

ʹ��DbContext����չ����BatchUpdate()������һ��BatchUpdateBuilder����
BatchUpdateBuilder���������ĸ�������
* Set()�������ڸ�һ�����Ը�ֵ�������ĵ�һ�����������Ե�lambda���ʽ,�ڶ���������ֵ��lambda���ʽ��
* Where() �ǹ�������
* ExecuteAsync()ʹ������ִ��BatchUpdateBuilder���첽����,Execute()��ExecuteAsync()��ͬ�������汾��

 ���Ӵ���:
```csharp
await ctx.BatchUpdate<Book>()
    .Set(b => b.Price, b => b.Price + 3)
    .Set(b => b.Title, b => s)
    .Set(b=>b.AuthorName,b=>b.Title.Substring(3,2)+b.AuthorName.ToUpper())
    .Set(b => b.PubTime, b => DateTime.Now)
    .Where(b => b.Id > n || b.AuthorName.StartsWith("Zack"))
    .ExecuteAsync();
```

����Ĵ��뽫�������ݿ���ִ������SQL��䣺
```SQL
Update [T_Books] SET [Price] = [Price] + 3.0E0, [Title] = @__s_1, [AuthorName] = COALESCE(SUBSTRING([Title], 3 + 1, 2), N'') + COALESCE(UPPER([AuthorName]), N''), [PubTime] = GETDATE()
WHERE ([Id] > @__p_0) OR ([AuthorName] IS NOT NULL AND ([AuthorName] LIKE N'Zack%'))
```

���������ʹ��EF Coreʵ�ֵ�lambda���ʽ��SQL���ķ��룬���Լ�������EF Core֧�ֵ�lambda���ʽд������֧�֡�

�������ݿ��Ѿ������ԣ����Ա�Zack.EFCore.Batch֧��: MS SQLServer(Microsoft.EntityFrameworkCore.SqlServer), MySQL(Pomelo.EntityFrameworkCore.MySql), PostgreSQL(Npgsql.EntityFrameworkCore.PostgreSQL)��

��������˵��ֻҪһ�����ݿ��ж�Ӧ��EF Core 5��Provider����ôZack.EFCore.Batch�Ϳ���֧��������ݿ⡣�����ʹ�õ����ݿ�Ŀǰ���ڱ�֧�ֵķ�Χ�ڣ����ύIssue����һ�������һ���������ڿ�����ɡ�


[���������Ŀ������棨Bվ��](https://www.bilibili.com/read/cv8545714)  

[���������Ŀ������棨����ͷ����](https://www.toutiao.com/i6899423396355293708/)  
