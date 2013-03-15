select * from aspnet_users where username = 'jshah'
select * from aspnet_membership where userid = '2BD31691-0C4B-4280-AB20-D1BA89A993EC'

--Set default password as pa$$word
update aspnet_membership set [password] = 'DE5aG4DJKaWNAC6qtb9Ex1mw0YQ=' where userid = '2BD31691-0C4B-4280-AB20-D1BA89A993EC'

--Unlock user
update aspnet_membership set islockedout = 0 where userid = '534E1E2E-926A-4EC5-8CC3-C1E9E5409B7B'

-- This will force user to change password.
update aspnet_membership set LastPasswordChangedDate = CreateDate where  userid = '2BD31691-0C4B-4280-AB20-D1BA89A993EC'

delete from aspnet_Membership where UserId in (select UserId from aspnet_Users where UserName like 'testfn%');
delete from aspnet_Profile where UserId in (select UserId from aspnet_Users where UserName like 'testfn%');
delete from aspnet_Users where UserName like 'testfn%'