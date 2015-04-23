using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;

using FilesContext;
using FilesEntities;

namespace SilverLightFileSystem.Web
{
	[ServiceContract(Namespace = "")]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class FileInfoWCFService
	{
		[OperationContract]
		public void AddFileToDB(int id,int? pid, string fileName, long size, DateTime createTime, string ext)
		{
			FolderModelDataContext dc = new FolderModelDataContext();

			dc.Files.InsertOnSubmit(new Files
			{
				Id = id,
				PID = pid,
				Name = fileName,
				Size = size,
				CreateTime = createTime,
				Type = ext
			});

			dc.SubmitChanges();
		}

		[OperationContract]
		public int GetStartId()
		{
			FolderModelDataContext dc = new FolderModelDataContext();

			var id = dc.IDTable.SingleOrDefault();

			if (id == null)
			{
				return -100;
			}

			return id.StartId;
		}

		[OperationContract]
		public void SetStartId(int newId)
		{
			FolderModelDataContext dc = new FolderModelDataContext();

			var id = dc.IDTable.SingleOrDefault();

			if (id == null)
			{
				return;
			}
			id.StartId = newId;
			
			dc.SubmitChanges();
		}

		/// <summary>
		/// 检查表中是否有“ID”，没有话插入一行
		/// </summary>
		[OperationContract]
		public void Check_HasId()
		{
			FolderModelDataContext dc = new FolderModelDataContext();

			var idRow = dc.IDTable.SingleOrDefault();

			if (idRow == null)
			{
				dc.IDTable.InsertOnSubmit(new IDTable
				{
					StartId = 0
				});

				dc.SubmitChanges();
			}
		}
	}
}
