# Zack.EFCore.Batch
ʹ�����������, Entity Framework Core �û�����ʹ��LINQ���ɾ�����߸��¶������ݿ��¼������ִֻ��һ��SQL��䲢�Ҳ���Ҫ���Ȱ�ʵ�������ص��ڴ��С� 
���������֧�� Entity Framework Core 5.0�Լ����߰档  

����˵��:  
��һ��:
```
 Install-Package Zack.EFCore.Batch
```
�ڶ���:
�����´�����ӵ����DbContext���OnConfiguring�����У�
```
 optionsBuilder.UserBatchEF();
```
������:
ʹ��DbContext����չ����DeleteRangeAsync()��ɾ��һ������.
DeleteRangeAsync()�Ĳ������ǹ���������lambda���ʽ��
���Ӵ���:
```
await ctx.DeleteRangeAsync<Book>(b => b.Price > n || b.AuthorName == "zack yang"); 
```
DeleteRange()������DeleteRangeAsync()��ͬ�������汾��

ʹ��DbContext����չ����BatchUpdate()������һ��BatchUpdateBuilder����
BatchUpdateBuilder���������ĸ�������
* Set()�������ڸ�һ�����Ը�ֵ�������ĵ�һ�����������Ե�lambda���ʽ,�ڶ���������ֵ��lambda���ʽ��
* Where() �ǹ�������
* ExecuteAsync()ʹ������ִ��BatchUpdateBuilder���첽����,Execute()��ExecuteAsync()��ͬ�������汾��

 ���Ӵ���:
 ```
await ctx.BatchUpdate<Book>()
    .Set(b => b.Price, b => b.Price + 3)
    .Set(b => b.Title, b => s)
    .Set(b=>b.AuthorName,b=>b.Title.Substring(3,2)+b.AuthorName.ToUpper())
    .Set(b => b.PubTime, b => DateTime.Now)
    .Where(b => b.Id > n || b.AuthorName.StartsWith("Zack"))
    .ExecuteAsync();
```