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
		/// <summary>
		/// 添加文件到数据库
		/// </summary>
		/// <param name="id"></param>
		/// <param name="pid"></param>
		/// <param name="fileName"></param>
		/// <param name="size"></param>
		/// <param name="createTime"></param>
		/// <param name="ext">后缀名</param>
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

		/// <summary>
		/// 从数据库中获取开始ID
		/// </summary>
		/// <returns>开始ID</returns>
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

		/// <summary>
		/// 设置新的开始ID
		/// </summary>
		/// <param name="newId">新的开始ID</param>
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
		/// 检查表中是否有“开始ID”，没有话插入一行
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
