SELECT mrr.ACCOUNT, mrt.Id, mrt.RequestedId, mrt.TypeId, mrt.RespondedDocuments, mrt.RequestedDate, mrt.RequestedUserID, mrt.RespondedUserID, mrt.RespondedDate, mrt.MediaDownloaded, mrt.MediaUploaded, mrt.LastUpdatedDate, mrt.LastUpdatedBy, mrt.RequestStatusId, mrt.ReSubmittedDate, mrt.ReSubmittedBy, mrt.TypeConstraints
FROM MSI_MediaRequestTypes mrt INNER JOIN MSI_MediaRequestResponse mrr ON mrr.Id = mrt.RequestedId
WHERE (mrr.ACCOUNT = '5000026067');

--Downloadable OR Status Fullfillment
SELECT mrr.AgencyId, mrr.ACCOUNT, mrt.Id, mrt.RequestedId, mrt.TypeId, mrt.RespondedDocuments, mrt.RequestedDate, mrt.RequestedUserID, mrt.RespondedUserID, mrt.RespondedDate, mrt.MediaDownloaded, mrt.MediaUploaded, mrt.LastUpdatedDate, mrt.LastUpdatedBy, mrt.RequestStatusId, mrt.ReSubmittedDate, mrt.ReSubmittedBy, mrt.TypeConstraints
FROM MSI_MediaRequestTypes mrt INNER JOIN MSI_MediaRequestResponse mrr ON mrr.Id = mrt.RequestedId
WHERE mrt.MediaDownloaded = 0
select * from MSI_MediaRequestStatus

SELECT r.RoleId, u.UserId, u.UserName, mt.Name, mrs.Name, mrr.*,mrt.* FROM MSI_MediaRequestResponse mrr 
INNER JOIN MSI_MediaRequestTypes mrt ON mrr.Id = mrt.RequestedId
INNER JOIN MSI_MediaTypes mt ON mt.ID = mrt.TypeId
INNER JOIN MSI_MediaRequestStatus mrs ON mrs.ID = mrt.RequestStatusId
INNER JOIN aspnet_Users u ON mrr.RequestedByUserId = u.UserId
INNER JOIN aspnet_UsersInRoles uir ON uir.UserId = u.UserId
INNER JOIN aspnet_Roles r ON r.RoleId = uir.RoleId
ORDER BY mrr.RequestedDate DESC;

SELECT mt.Name, mtr.* FROM MSI_MediaTracker mtr INNER JOIN MSI_MediaTypes mt ON mt.ID = mtr.MEdiaTypeId
ORDER BY mtr.CreatedDate DESC

select u.UserName, mn.* from MSI_MessageNotification as MN 
INNER JOIN aspnet_Users u ON mn.RecipientUserId = u.UserId;

--delete from MSI_MessageNotification;
--delete from MSI_MediaRequestTypes;
--delete from MSI_MediaRequestResponse;
delete from MSI_MediaTracker