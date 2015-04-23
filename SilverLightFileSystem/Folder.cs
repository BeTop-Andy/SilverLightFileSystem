using System.IO;

namespace SilverLightFileSystem
{
	public class Folder
	{
		public DirectoryInfo DirInfo
		{
			get;
			set;
		}
		public string Prefix
		{
			get;
			set;
		}

		public Folder(string pre, DirectoryInfo di)
		{
			DirInfo = di;
			Prefix = pre;
		}

		public override string ToString()
		{
			return Prefix == "." ? "." : Prefix + DirInfo.Name;
		}
	}
}
