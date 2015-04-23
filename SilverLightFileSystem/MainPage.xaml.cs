using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SilverLightFileSystem.FileInfoWCFServiceReference;

namespace SilverLightFileSystem
{
	public partial class MainPage : UserControl
	{
		ObservableCollection<Folder> folders;
		FileInfoWCFServiceClient webClient;
		int id = 1;

		public MainPage()
		{
			InitializeComponent();
			folders = new ObservableCollection<Folder>();
			lst_Folder.ItemsSource = folders;

			webClient = new FileInfoWCFServiceClient();
		}

		private void btn_Browse_Click(object sender, RoutedEventArgs e)
		{
			lst_File.Items.Clear();
			folders.Clear();
			txt_PathTextBox.Text = "";
			txt_PathTextBox.Tag = null;

			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = false;

			bool? result = ofd.ShowDialog();

			if (result.HasValue && result.Value)
			{
				txt_PathTextBox.Text = ofd.File.DirectoryName;
				txt_PathTextBox.Tag = ofd.File;


				folders.Clear();
				folders.Add(new Folder(".", ofd.File.Directory));

				GetAllDir(ofd.File.Directory, 0);
				AddDirToDB(ofd.File.Directory,null);


				IEnumerable<FileInfo> files = ofd.File.Directory.EnumerateFiles();

				lst_File.Items.Clear();

				foreach (FileInfo i in files)
				{
					lst_File.Items.Add(i.Name);
				}

				//webClient.TestCompleted += new EventHandler<TestCompletedEventArgs>(test);

			}
			else
			{
				MessageBox.Show("取消选择");
			}
		}

		/*
		private void btn_Clear_Click(object sender, RoutedEventArgs e)
		{
			txt_PathTextBox.Text = "";
		}

		private void btn_Enter_Click(object sender, RoutedEventArgs e)
		{
			string path = txt_PathTextBox.Text;

			try
			{
				FileInfo fi = (FileInfo) txt_PathTextBox.Tag;

				//lst_Folder.Items.Clear();
				//lst_Folder.Items.Add(".");

				folders.Clear();
				folders.Add(new Folder(".", fi.Directory));

				GetAllDir(fi.Directory, 0);

				IEnumerable<FileInfo> files = fi.Directory.EnumerateFiles();

				lst_File.Items.Clear();

				foreach (FileInfo i in files)
				{
					lst_File.Items.Add(i.Name);
				}
				lst_Folder.ItemsSource = folders;
			}
			catch
			{
				MessageBox.Show("请先选择文件");
			}
		}
		*/

		private void GetAllDir(DirectoryInfo dir, int level)
		{
			IEnumerable<DirectoryInfo> dirs = dir.EnumerateDirectories();
			string str = "";

			for (int i = 0; i < level; i++)
			{
				str += "/";
			}

			foreach (DirectoryInfo di in dirs)
			{
				folders.Add(new Folder(str,di));
				GetAllDir(di, level + 1);
				//webClient.AddDirToDBAsync(id, pid, di.Name, di.CreationTime);
			}

		}

		private void AddDirToDB(DirectoryInfo dir, int? pid)
		{
			IEnumerable<DirectoryInfo> dirs = dir.EnumerateDirectories();

			foreach (DirectoryInfo di in dirs)
			{
				webClient.AddDirToDBAsync(id, pid, di.Name, di.CreationTime);
				int tmp_pid = id;
				id++;
				AddDirToDB(di, tmp_pid);
			}
		}


		private void lst_Folder_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = lst_Folder.SelectedIndex;

			if (index >= 0)
			{
				DirectoryInfo di = folders[index].DirInfo;
				IEnumerable<FileInfo> files = di.EnumerateFiles();

				lst_File.Items.Clear();
				foreach (FileInfo i in files)
				{
					lst_File.Items.Add(i.Name);
				}
			}
		}

		private void test(object sender, TestCompletedEventArgs e)
		{
			MessageBox.Show(e.Result.ToString());
		}

	}
}
