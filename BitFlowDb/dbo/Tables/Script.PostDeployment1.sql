/*
# This file contains SQL statements that will be executed after the build script.
*/

/*
## Insert into AspNetRoles
*/

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

/*
## Insert into TemplateType
*/

if not exists (select 1 from flowcheck.templatetype)
begin
    insert into flowcheck.templatetype ([name], imgurl, code, updatedate, createdate, createuser, updateuser)
    values 
    ( 'rompibles', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAAADElEQVQImWNgYGAAAAAEAAGjChXjAAAAAElFTkSuQmCC', 30, null, sysdatetime(), 'system', null),
    ( 'gases y estanqueidad', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAAADElEQVQImWNgYGAAAAAEAAGjChXjAAAAAElFTkSuQmCC', 10, null, sysdatetime(), 'system', null),
    ( 'verificación código de barras', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAAADElEQVQImWNgYGAAAAAEAAGjChXjAAAAAElFTkSuQmCC', 40, null, sysdatetime(), 'system', null),
    ( 'control de manipuladores', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAAADElEQVQImWNgYGAAAAAEAAGjChXjAAAAAElFTkSuQmCC', 20, null, sysdatetime(), 'system', null);
end

/*
## Insert into AddressType
*/

if not exists (select 1 from [Person].[AddressType])
begin
    insert into [Person].[AddressType] (Code, Name, SortKey, CreateDate, CreateUser, UpdateDate, UpdateUser)
    values
    ('H', 'Home', 1, sysdatetime(), 'system', null, null),
    ('W', 'Work', 2, sysdatetime(), 'system', null, null),
    ('O', 'Other', 3, sysdatetime(), 'system', null, null);
end

/*
## Insert into EmailType
*/

if not exists (select 1 from [Person].[EmailType])
begin
    insert into [Person].[EmailType] (Code, Name, SortKey, CreateDate, CreateUser, UpdateDate, UpdateUser)
    values
    ('P', 'Personal', 1, sysdatetime(), 'system', null, null),
    ('W', 'Work', 2, sysdatetime(), 'system', null, null),
    ('O', 'Other', 3, sysdatetime(), 'system', null, null);
end

/*
## Insert into PersonType
*/

if not exists (select 1 from [Person].[PersonType])
begin
    insert into [Person].[PersonType] (Code, Name, SortKey, CreateDate, CreateUser, UpdateDate, UpdateUser)
    values
    ('IN', 'Individual', 1, sysdatetime(), 'system', null, null),
    ('CO', 'Company', 2, sysdatetime(), 'system', null, null),
    ('OT', 'Other', 3, sysdatetime(), 'system', null, null);
end

/*
## Insert into PhoneType
*/

if not exists (select 1 from [Person].[PhoneType])
begin
    insert into [Person].[PhoneType] (Code, Name, SortKey, CreateDate, CreateUser, UpdateDate, UpdateUser)
    values
    ('H', 'Home', 1, sysdatetime(), 'system', null, null),
    ('M', 'Mobile', 2, sysdatetime(), 'system', null, null),
    ('W', 'Work', 3, sysdatetime(), 'system', null, null),
    ('O', 'Other', 4, sysdatetime(), 'system', null, null);
end

/*
## Insert admin user
*/

declare @existingAdminUserId uniqueidentifier;
select @existingAdminUserId = Id from [dbo].[AspNetUsers] where [Email] = 'admin@onasoft.ch';

if @existingAdminUserId is null
begin
    declare @adminuserid uniqueidentifier = newid();
    declare @personidTable_admin table (personid int);

    insert into [dbo].[AspNetUsers] 
        ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], 
         [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], 
         [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
    values
        (@adminuserid, 'admin@onasoft.ch', upper('admin@onasoft.ch'), 
         'admin@onasoft.ch', upper('admin@onasoft.ch'), 0, 
         'AQAAAAIAAYagAAAAENHA/mlrNFniXGdDw7M8P9mJp+DFeS4F0VjknawUa8WPT7e1+odG1nfYdDXNYoZD1g==', 
         '5D2CA2I7KB6KLHJECBWF6KPV266DWZ57', newid(), 
         666, 0, 0, NULL, 1, 0);

    insert into [FlowCheck].[FlowUser] 
        ([Id], [UpdateDate], [CreateDate], [CreateUser], [UpdateUser])
    values 
        (@adminuserid, NULL, sysdatetime(), 'system', NULL);

    insert into [Person].[Person] 
        ([PersonTypeId], [Culture], [Title], [LastName], [FirstName], 
         [VatNumber], [Remarks], [AnnualRevenue], [ApplicationUserId])
    output inserted.personid into @personidTable_admin
    values 
        (1,'fr-CH', 'Undefined', 'Ona', 'Admin', NULL, NULL, NULL, @adminuserid);

    declare @personid_admin int;
    select @personid_admin = personid from @personidTable_admin;

    insert into [Person].[Email] 
        ([PersonId], [EmailTypeId], [EmailAddress], [SortKey], [Remarks], [IsDefault], [UpdateDate], [CreateDate], [CreateUser], [UpdateUser])
    values 
        (@personid_admin, 1, 'admin@onasoft.ch', NULL, 'fake email', 1, NULL, sysdatetime(), 'system', NULL);

    set @existingAdminUserId = @adminuserid;
end

declare @adminRoleId uniqueidentifier;
select @adminRoleId = Id from [dbo].[AspNetRoles] where [Name] = 'admin';

if not exists (
    select 1 from [FlowCheck].[AspNetUserRoles] 
    where UserId  = @existingAdminUserId and RoleId = @adminRoleId
)
begin
    insert into [FlowCheck].[AspNetUserRoles] (UserId, RoleId)
    values (@existingAdminUserId, @adminRoleId);
end

/*
## Insert manager user
*/

declare @existingmanageruserid uniqueidentifier;
select @existingmanageruserid = Id from [dbo].[AspNetUsers] where [Email] = 'manager@onasoft.ch';

if @existingmanageruserid is null
begin
    declare @manageruserid uniqueidentifier = newid();
    declare @personidTable_manager table (personid int);

    insert into [dbo].[AspNetUsers] 
        ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], 
         [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], 
         [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
    values
        (@manageruserid, 'manager@onasoft.ch', upper('manager@onasoft.ch'), 
         'manager@onasoft.ch', upper('manager@onasoft.ch'), 0, 
         'AQAAAAIAAYagAAAAENHA/mlrNFniXGdDw7M8P9mJp+DFeS4F0VjknawUa8WPT7e1+odG1nfYdDXNYoZD1g==', 
         '5D2CA2I7KB6KLHJECBWF6KPV266DWZ57', newid(), 
         666, 0, 0, NULL, 1, 0);

    insert into [FlowCheck].[FlowUser] 
        ([Id], [UpdateDate], [CreateDate], [CreateUser], [UpdateUser])
    values 
        (@manageruserid, NULL, sysdatetime(), 'system', NULL);

    insert into [Person].[Person] 
        ([PersonTypeId], [Culture], [Title], [LastName], [FirstName], 
         [VatNumber], [Remarks], [AnnualRevenue], [ApplicationUserId])
    output inserted.personid into @personidTable_manager
    values 
        (1,'fr-CH', 'Undefined', 'Ona', 'Manager', NULL, NULL, NULL, @manageruserid);

    declare @personid_manager int;
    select @personid_manager = personid from @personidTable_manager;

    insert into [Person].[Email] 
        ([PersonId], [EmailTypeId], [EmailAddress], [SortKey], [Remarks], [IsDefault], [UpdateDate], [CreateDate], [CreateUser], [UpdateUser])
    values 
        (@personid_manager, 1, 'manager@onasoft.ch', NULL, 'fake email', 1, NULL, sysdatetime(), 'system', NULL);

    set @existingmanageruserid = @manageruserid;
end

declare @managerroleid uniqueidentifier;
select @managerroleid = Id from [dbo].[aspnetroles] where [Name] = 'manager';

if not exists (
    select 1 from [FlowCheck].[AspNetUserRoles] 
    where UserId  = @existingmanageruserid and RoleId = @managerroleid
)
begin
    insert into [FlowCheck].[AspNetUserRoles] (UserId, RoleId)
    values (@existingmanageruserid, @managerroleid);
end

/*
## Insert viewer user
*/

declare @existingvieweruserid uniqueidentifier;
select @existingvieweruserid = Id from [dbo].[AspNetUsers] where [Email] = 'viewer@onasoft.ch';

if @existingvieweruserid is null
begin
    declare @vieweruserid uniqueidentifier = newid();
    declare @personidTable_viewer table (personid int);

    insert into [dbo].[AspNetUsers] 
        ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], 
         [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], 
         [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
    values
        (@vieweruserid, 'viewer@onasoft.ch', upper('viewer@onasoft.ch'), 
         'viewer@onasoft.ch', upper('viewer@onasoft.ch'), 0, 
         'AQAAAAIAAYagAAAAENHA/mlrNFniXGdDw7M8P9mJp+DFeS4F0VjknawUa8WPT7e1+odG1nfYdDXNYoZD1g==', 
         '5D2CA2I7KB6KLHJECBWF6KPV266DWZ57', newid(), 
         666, 0, 0, NULL, 1, 0);

    insert into [FlowCheck].[FlowUser] 
        ([Id], [UpdateDate], [CreateDate], [CreateUser], [UpdateUser])
    values 
        (@vieweruserid, NULL, sysdatetime(), 'system', NULL);

    insert into [Person].[Person] 
        ([PersonTypeId], [Culture], [Title], [LastName], [FirstName], 
         [VatNumber], [Remarks], [AnnualRevenue], [ApplicationUserId])
    output inserted.personid into @personidTable_viewer
    values 
        (1,'fr-CH', 'Undefined', 'Ona', 'Viewer', NULL, NULL, NULL, @vieweruserid);

    declare @personid_viewer int;
    select @personid_viewer = personid from @personidTable_viewer;

    insert into [Person].[Email] 
        ([PersonId], [EmailTypeId], [EmailAddress], [SortKey], [Remarks], [IsDefault], [UpdateDate], [CreateDate], [CreateUser], [UpdateUser])
    values 
        (@personid_viewer, 1, 'viewer@onasoft.ch', NULL, 'fake email', 1, NULL, sysdatetime(), 'system', NULL);

    set @existingvieweruserid = @vieweruserid;
end

declare @viewerroleid uniqueidentifier;
select @viewerroleid = Id from [dbo].[aspnetroles] where [Name] = 'viewer';

if not exists (
    select 1 from [FlowCheck].[AspNetUserRoles] 
    where UserId  = @existingvieweruserid and RoleId = @viewerroleid
)
begin
    insert into [FlowCheck].[AspNetUserRoles] (UserId, RoleId)
    values (@existingvieweruserid, @viewerroleid);
end

/*
## Insert standard user
*/

declare @existingstandarduserid uniqueidentifier;
select @existingstandarduserid = Id from [dbo].[AspNetUsers] where [Email] = 'user@onasoft.ch';

if @existingstandarduserid is null
begin
    declare @standarduserid uniqueidentifier = newid();
    declare @personidTable_standard table (personid int);

    insert into [dbo].[AspNetUsers] 
        ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], 
         [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], 
         [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
    values
        (@standarduserid, 'user@onasoft.ch', upper('user@onasoft.ch'), 
         'user@onasoft.ch', upper('user@onasoft.ch'), 0, 
         'AQAAAAIAAYagAAAAENHA/mlrNFniXGdDw7M8P9mJp+DFeS4F0VjknawUa8WPT7e1+odG1nfYdDXNYoZD1g==', 
         '5D2CA2I7KB6KLHJECBWF6KPV266DWZ57', newid(), 
         666, 0, 0, NULL, 1, 0);

    insert into [FlowCheck].[FlowUser] 
        ([Id], [UpdateDate], [CreateDate], [CreateUser], [UpdateUser])
    values 
        (@standarduserid, NULL, sysdatetime(), 'system', NULL);

    insert into [Person].[Person] 
        ([PersonTypeId], [Culture], [Title], [LastName], [FirstName], 
         [VatNumber], [Remarks], [AnnualRevenue], [ApplicationUserId])
    output inserted.personid into @personidTable_standard
    values 
        (1,'fr-CH', 'Undefined', 'Ona', 'User', NULL, NULL, NULL, @standarduserid);

    declare @personid_standard int;
    select @personid_standard = personid from @personidTable_standard;

    insert into [Person].[Email] 
        ([PersonId], [EmailTypeId], [EmailAddress], [SortKey], [Remarks], [IsDefault], [UpdateDate], [CreateDate], [CreateUser], [UpdateUser])
    values 
        (@personid_standard, 1, 'user@onasoft.ch', NULL, 'fake email', 1, NULL, sysdatetime(), 'system', NULL);

    set @existingstandarduserid = @standarduserid;
end

declare @userroleid uniqueidentifier;
select @userroleid = Id from [dbo].[aspnetroles] where [Name] = 'user';

if not exists (
    select 1 from [FlowCheck].[AspNetUserRoles] 
    where UserId  = @existingstandarduserid and RoleId = @userroleid
)
begin
    insert into [FlowCheck].[AspNetUserRoles] (UserId, RoleId)
    values (@existingstandarduserid, @userroleid);
end