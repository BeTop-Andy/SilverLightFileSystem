using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.IO;

using FilesContext;
using FilesEntities;

namespace SilverLightFileSystem.Web
{
	[ServiceContract(Namespace = "")]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class FileInfoWCFService
	{
		[OperationContract]
		public void DoWork()
		{
			// 在此处添加操作实现
			return;
		}

		// 在此处添加更多操作并使用 [OperationContract] 标记它们
		[OperationContract]
		public int Test(FileInfo fi)
		{
			return (int) fi.Length;
		}

		[OperationContract]
		public void AddFileToDB(int id,int? pid, string fileName, long size, DateTime createTime)
		{
			FolderModelDataContext dc = new FolderModelDataContext();

			dc.Files.InsertOnSubmit(new Files
			{
				Id = id,
				PID = pid,
				Name = fileName,
				Size = size,
				CreateTime = createTime,
				Type = "file"
			});

			dc.SubmitChanges();
		}

		[OperationContract]
		public void AddDirToDB(int id, int? pid, string dirName, long size, DateTime createTime)
		{
			FolderModelDataContext dc = new FolderModelDataContext();

			dc.Files.InsertOnSubmit(new Files
			{
				Id = id,
				PID = pid,
				Name = dirName,
				Size = size,
				CreateTime = createTime,
				Type = "dir"
			});

			dc.SubmitChanges();
		}

		[OperationContract]
		public int GetStartId()
		{
			FolderModelDataContext dc = new FolderModelDataContext();

			var id = dc.Files.SingleOrDefault<Files>(i => i.Type=="id");

			if (id == null)
			{
				return -100;
			}

			return id.Id;
		}

		[OperationContract]
		public void SetStartId(int newId)
		{
			FolderModelDataContext dc = new FolderModelDataContext();

			var id = dc.Files.SingleOrDefault<Files>(i => i.Type == "id");

			if (id == null)
			{
				return;
			}

			id.Id = newId;

			dc.SubmitChanges();
		}

		/// <summary>
		/// 检查是否有ID这一行，没有的话就插入
		/// </summary>
		[OperationContract]
		public void Check_HasId()
		{
			FolderModelDataContext dc = new FolderModelDataContext();

			var id = dc.Files.SingleOrDefault<Files>(i => i.Type == "id");

			if (id == null)
			{
				dc.Files.InsertOnSubmit(new Files
				{
					Id = 0,
					PID = null,
					Name = "id",
					Size = 0,
					CreateTime = DateTime.Now,
					Type = "id"
				});

				dc.SubmitChanges();
			}
		}
	}
}
