-- this file contains sql statements that will be executed after the build script.

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
    ( 'rompibles', '/Images/rompibleCYE.png', 30, null, sysdatetime(), 'system', null),
    ( 'gases y estanqueidad', '/GasEst.png', 10, null, sysdatetime(), 'system', null),
    ( 'verificación código de barras', '/BAR.png', 40, null, sysdatetime(), 'system', null),
    ( 'control de manipuladores', '/controlmanipuladores.png', 20, null, sysdatetime(), 'system', null);
end

go