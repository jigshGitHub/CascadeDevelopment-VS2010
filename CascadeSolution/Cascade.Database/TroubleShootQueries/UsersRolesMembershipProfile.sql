Select r.RoleName,p.PropertyValuesString,m.Email, u.* From aspnet_Users u
 Left Join aspnet_Membership m On m.UserId = u.UserId
 Left Join aspnet_UsersInRoles uir On uir.UserId = u.UserId
 Left Join aspnet_Roles r On r.RoleId = uir.RoleId 
 Left Join aspnet_Profile p On p.UserId = u.UserId