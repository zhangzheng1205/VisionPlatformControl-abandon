using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;
using Framework.Camera;

namespace VisionPlatformControl
{
    /// <summary>
    /// CameraSelectControl.xaml 的交互逻辑
    /// </summary>
    public partial class CameraSelectControl : UserControl
    {
        #region 私有成员

        /// <summary>
        /// 列表内容
        /// </summary>
        private class ListContent
        {
            /// <summary>
            /// 名字
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 内容
            /// </summary>
            public object Value { get; set; }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="name"></param>
            /// <param name="Value"></param>
            public ListContent(string name, object value)
            {
                Name = name;
                Value = value;
            }

        }

        /// <summary>
        /// 设备信息列表
        /// </summary>
        private ObservableCollection<ListContent> _deviceInforList;
        private ObservableCollection<ListContent> _cameraList;

        /// <summary>
        /// 显示相机信息
        /// </summary>
        /// <param name="Device">设备信息</param>
        private void DisplayCameraInfo(DeviceInfo devInfo)
        {
            _deviceInforList.Clear();

            _deviceInforList.Add(new ListContent("MAC地址", devInfo?.MACAddress ?? "----"));
            _deviceInforList.Add(new ListContent("IP地址", devInfo?.IPAddress ?? "----"));
            _deviceInforList.Add(new ListContent("子网掩码", devInfo?.SubnetMask ?? "----"));
            _deviceInforList.Add(new ListContent("默认网关", devInfo?.GatewayAddress ?? "----"));
            _deviceInforList.Add(new ListContent("模块名", devInfo?.ModelName ?? "----"));
            _deviceInforList.Add(new ListContent("生产商", devInfo?.Manufacturer ?? "----"));
            _deviceInforList.Add(new ListContent("序列号", devInfo?.SerialNumber ?? "----"));
            _deviceInforList.Add(new ListContent("自定义名", devInfo?.UserName ?? "----"));
        }

        /// <summary>
        /// 搜寻所有相机
        /// </summary>
        private void SearchAllCamera()
        {
            // 更新控件
            Dispatcher.BeginInvoke(new Action(() =>
            {
                DevMessageLabel.Content = "可用设备(搜寻中...)";
                _cameraList.Clear();
            }));

            var newThread = new Thread(delegate ()
            {
                // 开始搜索
                var cameraList = Camera?.GetDeviceList();

                // 更新控件
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (cameraList != null)
                    {
                        _cameraList.Clear();

                        foreach (var item in cameraList)
                        {
                            _cameraList.Add(new ListContent(item.ToString(), item));
                        }
                    }

                    DevMessageLabel.Content = "可用设备";
                }));

            });

            newThread.Start();

        }

        #region UI事件

        /// <summary>
        /// 界面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DeviceInfoListView.ItemsSource = _deviceInforList;
            AvailableDevicesListView.ItemsSource = _cameraList;
            AvailableDevicesListView.SelectedIndex = -1;

            DisplayCameraInfo(null);
            SearchAllCamera();

        }

        /// <summary>
        /// 控件尺寸改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                var width = DeviceInfoListView.ActualWidth - (DeviceInfoName.ActualWidth + 10);

                if (width > 0)
                {
                    DeviceInfoValue.Width = width;
                }
            }
            catch (Exception)
            {
                
            }
        }

        /// <summary>
        /// 点击"更新"按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchAllCamera();
            }
            catch (Exception)
            {
                
            }
        }

        /// <summary>
        /// 选择项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AvailableDevicesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems?.Count > 0)
                {
                    DisplayCameraInfo((DeviceInfo)(e.AddedItems[0] as ListContent)?.Value);
                }
                else
                {
                    DisplayCameraInfo(null);
                }
            }
            catch (Exception)
            {
                
            }
        }

        /// <summary>
        /// 鼠标双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AvailableDevicesListView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((e.OriginalSource is System.Windows.Controls.TextBlock) && ((e.Source as ListView)?.SelectedItem != null) && ((e.Source as ListView)?.SelectedIndex >= 0))
            {
                CameraSelectedEvent?.Invoke(this, new CameraSelectedEventArgs((DeviceInfo)(((e.Source as ListView)?.SelectedItem as ListContent)?.Value)));
            }

        }

        #endregion

        #endregion

        #region 构造/析构接口

        /// <summary>
        /// 构造函数
        /// </summary>
        public CameraSelectControl() : this(null)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="camera">相机接口</param>
        public CameraSelectControl(ICamera camera)
        {
            InitializeComponent();

            _deviceInforList = new ObservableCollection<ListContent>();
            _cameraList = new ObservableCollection<ListContent>();

            Camera = camera;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 相机控制接口
        /// </summary>
        public ICamera Camera { get; set; }

        #endregion

        #region 事件

        /// <summary>
        /// 相机选择事件
        /// </summary>
        event EventHandler<CameraSelectedEventArgs> CameraSelectedEvent;

        #endregion

        #region 公共方法


        #endregion

    }

    /// <summary>
    /// 相机选择事件参数
    /// </summary>
    public class CameraSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Device">设备</param>
        public CameraSelectedEventArgs(DeviceInfo device)
        {
            Device = device;

        }

        /// <summary>
        /// 设备连接状态
        /// </summary>
        public DeviceInfo Device { get; private set; }

    }

}
