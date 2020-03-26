declare @startDate date = '2020-01-01';
declare @endDate date = '2021-01-01';

declare @typesCount int = 2;
declare @statusesCount int = 3;

declare @i int = 0;

while @i < 1000
begin
    insert into Tasks
     values (null,
      null,
      concat('Task', cast(@i as varchar(max))),
      floor(rand()* (@typesCount + 1)),
      floor(rand()* (@statusesCount + 1)),
      floor(rand() * 101),
      concat('Descr', cast(@i as varchar(max))),
      floor(rand() * 4),
      dateAdd(Day, Rand() * DateDiff(Day, @startDate, @endDate), @startDate),
      dateAdd(Day, Rand() * DateDiff(Day, @startDate, @endDate) + floor(rand() * 10), @startDate))

    set @i = @i + 1;
end;