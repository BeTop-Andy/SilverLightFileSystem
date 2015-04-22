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
		public void AddDirToDB(int id, int? pid, string dirName, DateTime createTime)
		{
			FolderModelDataContext dc = new FolderModelDataContext();

			dc.Files.InsertOnSubmit(new Files
			{
				Id = id,
				PID = pid,
				Name = dirName,
				Size = 0,
				CreateTime = createTime,
				Type = "dir"
			});

			dc.SubmitChanges();
		}
	}
}
