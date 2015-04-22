using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;

namespace SilverLightFileSystem
{
	public class Folder
	{
		public DirectoryInfo Di
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
			Di = di;
			Prefix = pre;
		}

		public override string ToString()
		{
			return Prefix + Di.Name;
		}
	}
}
