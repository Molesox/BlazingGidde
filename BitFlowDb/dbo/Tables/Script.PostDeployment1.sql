-- This file contains SQL statements that will be executed after the build script.

if not exists (select 1 from [dbo].[aspnetroles])
begin
    declare @adminid    uniqueidentifier = newid();
    declare @userid     uniqueidentifier = newid();
    declare @managerid  uniqueidentifier = newid();
    declare @viewerid   uniqueidentifier = newid();

    insert into [dbo].[aspnetroles] (id, [name], normalizedname, concurrencystamp)
    values
        (@adminid,   'admin',   upper('admin'),   newid()),
        (@userid,    'user',    upper('user'),    newid()),
        (@managerid, 'manager', upper('manager'), newid()),
        (@viewerid,  'viewer',  upper('viewer'),  newid());

    insert into [FlowCheck].[flowRole] (Id)
    values
        (@adminid),
        (@userid),
        (@managerid),
        (@viewerid);
end
go

if not exists (select 1 from flowcheck.templatetype)
begin
    insert into flowcheck.templatetype ([name], imgurl, code, updatedate, createdate, createuser, updateuser)
    values 
    ( 'rompibles', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAAADElEQVQImWNgYGAAAAAEAAGjChXjAAAAAElFTkSuQmCC', 30, null, sysdatetime(), 'system', null),
    ( 'gases y estanqueidad', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAAADElEQVQImWNgYGAAAAAEAAGjChXjAAAAAElFTkSuQmCC', 10, null, sysdatetime(), 'system', null),
    ( 'verificación código de barras', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAAADElEQVQImWNgYGAAAAAEAAGjChXjAAAAAElFTkSuQmCC', 40, null, sysdatetime(), 'system', null),
    ( 'control de manipuladores', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAAADElEQVQImWNgYGAAAAAEAAGjChXjAAAAAElFTkSuQmCC', 20, null, sysdatetime(), 'system', null);
end
go

-- Insert into AddressType
if not exists (select 1 from [Person].[AddressType])
begin
    insert into [Person].[AddressType] (Code, Name, SortKey, CreateDate, CreateUser, UpdateDate, UpdateUser)
    values
    ('H', 'Home', 1, sysdatetime(), 'system', null, null),
    ('W', 'Work', 2, sysdatetime(), 'system', null, null),
    ('O', 'Other', 3, sysdatetime(), 'system', null, null);
end
go

-- Insert into EmailType
if not exists (select 1 from [Person].[EmailType])
begin
    insert into [Person].[EmailType] (Code, Name, SortKey, CreateDate, CreateUser, UpdateDate, UpdateUser)
    values
    ('P', 'Personal', 1, sysdatetime(), 'system', null, null),
    ('W', 'Work', 2, sysdatetime(), 'system', null, null),
    ('O', 'Other', 3, sysdatetime(), 'system', null, null);
end
go

-- Insert into PersonType
if not exists (select 1 from [Person].[PersonType])
begin
    insert into [Person].[PersonType] (Code, Name, SortKey, CreateDate, CreateUser, UpdateDate, UpdateUser)
    values
    ('IN', 'Individual', 1, sysdatetime(), 'system', null, null),
    ('CO', 'Company', 2, sysdatetime(), 'system', null, null),
    ('OT', 'Other', 3, sysdatetime(), 'system', null, null);
end
go

-- Insert into PhoneType
if not exists (select 1 from [Person].[PhoneType])
begin
    insert into [Person].[PhoneType] (Code, Name, SortKey, CreateDate, CreateUser, UpdateDate, UpdateUser)
    values
    ('H', 'Home', 1, sysdatetime(), 'system', null, null),
    ('M', 'Mobile', 2, sysdatetime(), 'system', null, null),
    ('W', 'Work', 3, sysdatetime(), 'system', null, null),
    ('O', 'Other', 4, sysdatetime(), 'system', null, null);
end
go