using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TreeDemo
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow:Window
	{
		public MainWindow()
		{
			InitializeComponent();
			ShowTreeView();
		}

		private void Window_Loaded(object sender,RoutedEventArgs e)
		{
		}

		private void ShowTreeView()
		{
			List<PropertyNodeItem> itemList = new List<PropertyNodeItem>();
			PropertyNodeItem node1 = new PropertyNodeItem()
			{
				ID = 1,
				FatherID = 0,
				DisplayName = "Node No.1",
				Name = "This is the discription of Node1. This is a folder.",
				Check = false
			};

			PropertyNodeItem node1tag1 = new PropertyNodeItem()
			{
				ID = 2,
				FatherID = 1,
				DisplayName = "Tag No.1",
				Name = "This is the discription of Tag 1. This is a tag.",
				Check = false
			};
			node1.Children.Add(node1tag1);

			PropertyNodeItem node1tag2 = new PropertyNodeItem()
			{
				ID = 3,
				FatherID = 1,
				DisplayName = "Tag No.2",
				Name = "This is the discription of Tag 2. This is a tag.",
				Check = false
			};
			node1.Children.Add(node1tag2);
			itemList.Add(node1);

			PropertyNodeItem node2 = new PropertyNodeItem()
			{
				ID = 4,
				FatherID = 0,
				DisplayName = "Node No.2",
				Name = "This is the discription of Node 2. This is a folder.",
				Check = false
			};

			PropertyNodeItem node2tag3 = new PropertyNodeItem()
			{
				ID = 5,
				FatherID = 4,
				DisplayName = "Tag No.3",
				Name = "This is the discription of Tag 3. This is a tag.",
				Check = false
			};
			node2.Children.Add(node2tag3);

			PropertyNodeItem node2tag4 = new PropertyNodeItem()
			{
				ID = 6,
				FatherID = 4,
				DisplayName = "Tag No.4",
				Name = "This is the discription of Tag 4. This is a tag.",
				Check = false
			};
			node2.Children.Add(node2tag4);
			itemList.Add(node2);

			PropertyNodeItem node1tag1tag1 = new PropertyNodeItem()
			{
				ID = 7,
				FatherID = 2,
				DisplayName = "TagTag No.1",
				Name = "This is the discription of TagTag 1. This is a tag.",
				Check = false
			};
			node1tag1.Children.Add(node1tag1tag1);

			PropertyNodeItem node1tag1tag2 = new PropertyNodeItem()
			{
				ID = 8,
				FatherID = 2,
				DisplayName = "TagTag No.2",
				Name = "This is the discription of TagTag 2. This is a tag.",
				Check = false
			};
			node1tag1.Children.Add(node1tag1tag2);

			PropertyNodeItem node1tag1tag1tag1 = new PropertyNodeItem()
			{
				ID = 9,
				FatherID = 7,
				DisplayName = "TagTagTag No.1",
				Name = "This is the discription of TagTagTag 1. This is a tag.",
				Check = false
			};
			node1tag1tag1.Children.Add(node1tag1tag1tag1);

			PropertyNodeItem node1tag1tag1tag2 = new PropertyNodeItem()
			{
				ID = 10,
				FatherID = 7,
				DisplayName = "TagTagTag No.2",
				Name = "This is the discription of TagTagTag 2. This is a tag.",
				Check = false
			};
			node1tag1tag1.Children.Add(node1tag1tag1tag2);

			this.tvProperties.ItemsSource = itemList;
		}

		private void CheckBox_Click(object sender,RoutedEventArgs e)
		{
			CheckBox cb = sender as CheckBox;
			ContentPresenter cp = cb.TemplatedParent as ContentPresenter;
			TreeViewItem tvi = cp.TemplatedParent as TreeViewItem;
			PropertyNodeItem pni = tvi.DataContext as PropertyNodeItem;
			List<PropertyNodeItem> list = this.tvProperties.ItemsSource as List<PropertyNodeItem>;

			if(pni.FatherID == 0)
			{
				cb.IsChecked = !cb.IsChecked;
			}
			else
			{
				if(cb.IsChecked == true)
				{
					tvi.IsSelected = true;
					CheckClickID(list,pni.ID,pni.FatherID,true);
				}
				else
				{
					tvi.IsSelected = false;
					CheckClickID(list,pni.ID,pni.FatherID,false);
				}
			}
		}

		//测试用
		int realID = 0;
		PropertyNodeItem returnPNI;

		private void CheckClickID(List<PropertyNodeItem> list,int ID,int FatherID,bool IsSelected)
		{
			#region 旧坑 性能高 逻辑无比困难 未实现

			//List<int> returnID = new List<int>();
			//returnID.Add(FatherID);
			//returnID.Add(0);

			//foreach(PropertyNodeItem p in list)
			//{
			//	if(p.Children.Count != 0)
			//	{
			//		List<PropertyNodeItem> chlid = p.Children;
			//		returnID = CheckClickID(chlid,ID,FatherID,IsSelected);
			//		//保持有子节点已经勾中的中间层的钩的不变
			//		if(ID == p.ID && returnID[1] == -1)
			//		{
			//			p.Check = true;
			//			tvProperties.UpdateLayout();
			//		}
			//		//子节点没有勾中的中间层的钩子改变
			//		else if(ID == p.ID && returnID[1] == p.Children.Count)
			//		{
			//			p.Check = IsSelected;
			//			tvProperties.UpdateLayout();
			//		}
			//	}
			//	else
			//	{
			//		//保持有子节点已经勾中的中间层的钩的不变
			//		if(p.FatherID != returnID[1] && p.Check)
			//		{
			//			//有一个就行 所以直接return
			//			returnID[1] = -1;
			//		}
			//		//子节点没有勾中的中间层的钩子改变
			//		if(p.FatherID != returnID[1] && !p.Check)
			//		{
			//			//要循环完全部子节点才能判断是不是全部都没有勾中
			//			returnID[1]++;
			//		}
			//		//保持多个子节点勾中而取消一个勾中一个子节点时的父节点状态
			//		else if(p.ID != ID && p.FatherID == returnID[1] && p.Check)
			//		{
			//			returnID[1] = -2;
			//		}
			//	}

			//	//选中子节点同时选中父节点
			//	if(returnID[0] == p.ID)
			//	{
			//		p.Check = IsSelected;
			//		if(p.FatherID != 0)
			//		{
			//			returnID[0] = p.FatherID;
			//			return returnID;
			//		}
			//	}
			//}

			//return returnID;

			#endregion

			#region 新坑 性能低 多循环 已实现
			if(realID == 0)
			{
				realID = ID;
			}
			//选中子节点同时选中父节点
			PropertyNodeItem pni = SonClickFather(list,ID,FatherID,IsSelected);

			//保持有子节点已经勾中的中间层的钩的不变
			if(pni.Children.Count != 0)
			{
				pni.Check = MiddleClickLast(pni.Children,pni);
			}
			//保持父节点的兄弟节点选中是取消父节点而保持根节点的状态
			else if (!pni.Check)
			{
				MiddleClickNotLast(list,pni);
			}
			returnPNI = null;

			//保持多个子节点勾中而取消一个勾中一个子节点时的根节点状态
			KeepFather(list,pni);

			#endregion
		}

		private void KeepFather(List<PropertyNodeItem> list,PropertyNodeItem pni)
		{
			List<PropertyNodeItem> fulllist = this.tvProperties.ItemsSource as List<PropertyNodeItem>;

			foreach(PropertyNodeItem item in list)
			{
				if(item.ID != pni.ID && item.FatherID == pni.FatherID && item.Check)
				{
					SonClickFather(fulllist,pni.ID,pni.FatherID,true);
				}
				else
				{
					KeepFather(item.Children,pni);
				}
			}
		}

		private bool MiddleClickLast(List<PropertyNodeItem> child,PropertyNodeItem pni)
		{
			bool finalcheck = pni.Check;

			foreach(PropertyNodeItem self in child)
			{
				if(self.Children.Count != 0)
				{
					MiddleClickLast(self.Children,self);
				}
				else
				{
					if(self.Check)
					{
						List<PropertyNodeItem> list = this.tvProperties.ItemsSource as List<PropertyNodeItem>;
						SonClickFather(list,pni.ID,pni.FatherID,true);
						return true;
					}
					//else
					//{
					//	finalcheck = false;
					//}
				}
			}

			return finalcheck;
		}

		private int MiddleClickNotLast(List<PropertyNodeItem> list,PropertyNodeItem pni)
		{
			int brotherFID = 0;
			foreach(PropertyNodeItem all in list)
			{
				if(all.Children.Count != 0)
				{
					brotherFID = MiddleClickNotLast(all.Children,pni);
				}
				if(all.ID == pni.FatherID)
				{
					brotherFID = all.FatherID;
				}
				if(all.FatherID == brotherFID && all.Check)
				{
					List<PropertyNodeItem> fulllist = this.tvProperties.ItemsSource as List<PropertyNodeItem>;
					SonClickFather(fulllist,all.ID,all.FatherID,true);
				}
			}
			return brotherFID;
		}

		private PropertyNodeItem SonClickFather(List<PropertyNodeItem> list,int iD,int fatherID,bool isSelected)
		{

			List<PropertyNodeItem> fulllist = this.tvProperties.ItemsSource as List<PropertyNodeItem>;

			foreach(PropertyNodeItem all in list)
			{
				if(all.Children.Count != 0 && all.ID == fatherID)
				{
					all.Check = isSelected;
					SonClickFather(fulllist,all.ID,all.FatherID,isSelected);
				}
				else if(all.Children.Count != 0 && all.ID != fatherID)
				{
					SonClickFather(all.Children,iD,fatherID,isSelected);
				}
				if(all.ID == realID && returnPNI == null)
				{
					returnPNI = all;
					realID = 0;
				}
			}

			return returnPNI;
		}

		private void menuitem_Click(object sender,RoutedEventArgs e)
		{
			MenuItem menuitem = sender as MenuItem;
			bool click = Convert.ToBoolean(menuitem.Tag);
			if(click)
			{
				foreach(var item in tvProperties.Items)
				{
					TreeViewItem tvi = tvProperties.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
					tvi.ExpandSubtree();
					PropertyNodeItem pni = tvi.DataContext as PropertyNodeItem;
					TVIChoiced(pni,click);
				}
			}
			else
			{
				//foreach(var item in tvProperties.Items)
				//{
				//	TreeViewItem tvi = tvProperties.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
				//	tvi.IsExpanded = false;
				//	PropertyNodeItem pni = tvi.DataContext as PropertyNodeItem;
				//	TVIChoiced(pni,click);
				//}
				SetNodeExpandedState(tvProperties,click);
			}
		}

		private void TVIChoiced(PropertyNodeItem pni,bool click)
		{
			if(pni != null)
			{
				if(pni.Children.Count != 0)
				{
					pni.Check = click;

					foreach(PropertyNodeItem chlid in pni.Children)
					{
						TVIChoiced(chlid,click);
					}
				}
				else
				{
					pni.Check = click;
				}
			}
		}

		/// <summary>
		/// 节点收缩
		/// </summary>
		/// <param name="control">TreeView控件</param>
		/// <param name="expandNode">true:展开 false:收缩</param>
		private void SetNodeExpandedState(ItemsControl control,bool expandNode)
		{
			if(control != null)
			{
				foreach(object item in control.Items)
				{
					TreeViewItem treeItem = control.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
					PropertyNodeItem pni = treeItem.DataContext as PropertyNodeItem;
					TVIChoiced(pni,expandNode);
					if(treeItem != null && treeItem.HasItems)
					{
						treeItem.IsExpanded = expandNode;
						if(treeItem.ItemContainerGenerator.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
						{
							treeItem.UpdateLayout();
						}

						SetNodeExpandedState(treeItem as ItemsControl,expandNode);
					}
				}
			}
		}
	}
	internal class PropertyNodeItem:INotifyPropertyChanged
	{
		public int ID
		{
			get; set;
		}
		public int FatherID
		{
			get; set;
		}
		public string DisplayName
		{
			get; set;
		}
		private bool check;
		public string Name
		{
			get; set;
		}

		public bool Check
		{
			get
			{
				return check;
			}
			set
			{
				check = value;
				OnPropertyChanged("Check");
			}
		}

		public List<PropertyNodeItem> Children
		{
			get; set;
		}
		public PropertyNodeItem()
		{
			Children = new List<PropertyNodeItem>();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected internal virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
		}
	}

	//public static class CheckTreeViem
	//{
	//	// Using a DependencyProperty as the backing store for SearchValue.  This enables animation, styling, binding, etc...
	//	public static readonly DependencyProperty IsCheckTreeProperty =
	//		DependencyProperty.RegisterAttached("IsCheckTree",typeof(bool),typeof(CheckTreeViem),new UIPropertyMetadata(false));

	//	public static bool GetIsCheckTree(DependencyObject obj)
	//	{
	//		return (bool)obj.GetValue(IsCheckTreeProperty);
	//	}

	//	public static void SetIsCheckTree(DependencyObject obj,bool value)
	//	{
	//		obj.SetValue(IsCheckTreeProperty,value);
	//	}
	//}
}



