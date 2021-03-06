﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using SilverLightFileSystem.FileInfoWCFServiceReference;

namespace SilverLightFileSystem
{
	public partial class MainPage : UserControl
	{
		ObservableCollection<Folder> folders;			// 文件夹的相关信息
		FileInfoWCFServiceClient webClient;				// 调用数据库的“引用”
		ObservableCollection<string> extensions;		// 后缀名的集合

		int id = 0;										// 数据库中的Id

		public MainPage()
		{
			InitializeComponent();
			folders = new ObservableCollection<Folder>();
			lst_Folder.ItemsSource = folders;

			webClient = new FileInfoWCFServiceClient();

			webClient.Check_HasIdCompleted += new 
				EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(InitCompleted);
			webClient.Check_HasIdAsync();

			extensions = new ObservableCollection<string>();
			ddlst_Extension.ItemsSource = extensions;
			extensions.Add("ALL");
		}


		private void InitCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			webClient.GetStartIdCompleted += new 
				EventHandler<GetStartIdCompletedEventArgs>(GetStartId);
			webClient.GetStartIdAsync();
		}

		private void GetStartId(object sender, GetStartIdCompletedEventArgs e)
		{
			id = e.Result + 1;
		}


		private void btn_Browse_Click(object sender, RoutedEventArgs e)
		{
			lst_File.Items.Clear();
			folders.Clear();
			txt_Path.Text = "";
			txt_Path.Tag = null;

			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = false;

			bool? result = ofd.ShowDialog();

			if (result.HasValue && result.Value)
			{
				txt_Path.Text = ofd.File.DirectoryName;
				txt_Path.Tag = ofd.File;

				folders.Clear();
				folders.Add(new Folder(".", ofd.File.Directory));

				GetAllDir(ofd.File.Directory, 0);
				AddFileToDB(ofd.File.Directory, null);
				AddDirToDB(ofd.File.Directory, null);

				IEnumerable<FileInfo> files = ofd.File.Directory.EnumerateFiles();

				lst_File.Items.Clear();

				foreach (FileInfo i in files)
				{
					lst_File.Items.Add(i.Name);
					if (!extensions.Contains(i.Extension))
					{
						extensions.Add(i.Extension);
					}
				}

				id++;		// 为了分隔开
				webClient.SetStartIdAsync(id);
				SetEnabled(true);
			}
			else
			{
				MessageBox.Show("取消选择");
				SetEnabled(false);
			}
		}

		private void SetEnabled(bool b)
		{
			txt_Search.IsEnabled = b;
			btn_Search.IsEnabled = b;
			ddlst_Extension.IsEnabled = b;
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

		/// <summary>
		/// 获取dir目录下的所有子目录，并加入folders集合
		/// </summary>
		/// <param name="dir">“根”目录</param>
		/// <param name="level">深度</param>
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
				folders.Add(new Folder(str, di));
				GetAllDir(di, level + 1);
			}
		}

		/// <summary>
		/// 获取dir目录下的所有子目录，并加入数据库
		/// </summary>
		/// <param name="dir">“根”目录</param>
		/// <param name="pid">父节点的Id</param>
		private void AddDirToDB(DirectoryInfo dir, int? pid)
		{
			IEnumerable<DirectoryInfo> dirs = dir.EnumerateDirectories();

			foreach (DirectoryInfo di in dirs)
			{
				webClient.AddFileToDBAsync(id, pid, di.Name, GetDirSize(di), 
										   di.CreationTime, "dir");

				int tmp_id = id;
				id++;

				AddFileToDB(di, tmp_id);
				AddDirToDB(di, tmp_id);
			}
		}

		/// <summary>
		/// 获取dir目录下的所有文件，并加入数据库
		/// </summary>
		/// <param name="dir">“根”目录</param>
		/// <param name="pid">父节点的Id</param>
		private void AddFileToDB(DirectoryInfo dir, int? pid)
		{
			IEnumerable<FileInfo> files = dir.EnumerateFiles();

			foreach (FileInfo fi in files)
			{
				webClient.AddFileToDBAsync(id, pid, fi.Name, fi.Length, 
										   fi.CreationTime, fi.Extension);
				id++;
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
				extensions.Clear();
				extensions.Add("ALL");

				foreach (FileInfo i in files)
				{
					lst_File.Items.Add(i.Name);
					if (!extensions.Contains(i.Extension))
					{
						extensions.Add(i.Extension);
					}
				}
			}
		}
		
		/// <summary>
		/// 计算dir目录下（包括子目录）的所有文件的总大小
		/// </summary>
		/// <param name="dir"></param>
		/// <returns>大小</returns>
		private long GetDirSize(DirectoryInfo dir)
		{
			long size = 0;

			IEnumerable<FileInfo> files = dir.EnumerateFiles();
			foreach (var file in files)
			{
				size += file.Length;
			}

			IEnumerable<DirectoryInfo> dirs = dir.EnumerateDirectories();
			foreach (var di in dirs)
			{
				size += GetDirSize(di);
			}

			return size;
		}

		private void ddlst_Extension_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			DirectoryInfo dir = folders[lst_Folder.SelectedIndex < 0 ?
				0 : lst_Folder.SelectedIndex].DirInfo;
			IEnumerable<FileInfo> files = dir.EnumerateFiles();
			lst_File.Items.Clear();

			// 选择“ALL”
			if (ddlst_Extension.SelectedIndex == 0)
			{
				foreach (var i in files)
				{
					lst_File.Items.Add(i.Name);
				}
			}

			if (ddlst_Extension.SelectedIndex > 0)
			{
				string extension = ddlst_Extension.SelectedValue.ToString();

				foreach (var i in files)
				{
					if (i.Extension == extension)
					{
						lst_File.Items.Add(i.Name);
					}
				}
			}
		}

		private void btn_Search_Click(object sender, RoutedEventArgs e)
		{
			string keyword = txt_Search.Text.ToLower();		// 忽略大小写
			if (keyword != null)
			{
				DirectoryInfo dir = folders[lst_Folder.SelectedIndex < 0 ? 
					0 : lst_Folder.SelectedIndex].DirInfo;
				IEnumerable<FileInfo> files = dir.EnumerateFiles();
				lst_File.Items.Clear();

				foreach (var i in files)
				{
					if (i.Name.ToLower().Contains(keyword))
					{
						lst_File.Items.Add(i.Name);
					}
				}
			}
			else
			{
				ddlst_Extension.SelectedIndex = 0;
			}
		}

		private void txt_Search_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				btn_Search_Click(null, null);
			}
		}

		private void txt_Search_TextChanged(object sender, TextChangedEventArgs e)
		{
			btn_Search_Click(null, null);
		}
	}
}
