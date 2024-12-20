-- this file contains sql statements that will be executed after the build script.

if not exists (select 1
from [dbo].[aspnetroles])
begin
    -- insert usual roles into aspnetroles after deployment

    insert into [dbo].[aspnetroles]
        (id, [name], normalizedname, concurrencystamp)
    values
        (newid(), 'admin', upper('admin'), newid()),
        (newid(), 'user', upper('user'), newid()),
        (newid(), 'manager', upper('manager'), newid()),
        (newid(), 'viewer', upper('viewer'), newid());
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