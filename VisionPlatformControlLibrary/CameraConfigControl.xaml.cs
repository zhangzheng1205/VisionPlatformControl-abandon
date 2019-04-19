using Framework.Camera;
using System;
using System.Collections.Generic;
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

namespace VisionPlatformControlLibrary
{
    /// <summary>
    /// CameraConfigControl.xaml 的交互逻辑
    /// </summary>
    public partial class CameraConfigControl : UserControl
    {
        #region 私有成员

        /// <summary>
        /// 读取相机参数
        /// </summary>
        public void ReadCameraParam()
        {
            if (Camera != null)
            {
                PixelFormatComboBox.Items.Clear();
                TriggerModeComboBox.Items.Clear();
                TriggerSourceComboBox.Items.Clear();
                TriggerActivationComboBox.Items.Clear();

                //像素类型
                if (Camera.PixelFormatTypeEnum != null)
                {
                    foreach (var item in Camera.PixelFormatTypeEnum)
                    {
                        PixelFormatComboBox.Items.Add(item.ToString());
                    }

                    PixelFormatComboBox.SelectedItem = Camera.PixelFormat.ToString();
                }

                //触发模式
                if (Camera.TriggerModeEnum != null)
                {
                    foreach (var item in Camera.TriggerModeEnum)
                    {
                        TriggerModeComboBox.Items.Add(item.ToString());
                    }

                    TriggerModeComboBox.SelectedItem = Camera.TriggerMode.ToString();
                }

                //触发源
                if (Camera.TriggerSourceEnum != null)
                {
                    foreach (var item in Camera.TriggerSourceEnum)
                    {
                        TriggerSourceComboBox.Items.Add(item.ToString());
                    }

                    if (Camera.TriggerSource == ETriggerSource.Unknown)
                    {
                        Camera.TriggerSource = ETriggerSource.Software;
                    }

                    TriggerSourceComboBox.SelectedItem = Camera.TriggerSource.ToString();

                }

                //硬件触发
                if (Camera.TriggerActivationEnum != null)
                {
                    foreach (var item in Camera.TriggerActivationEnum)
                    {
                        TriggerActivationComboBox.Items.Add(item.ToString());
                    }

                    if (Camera.TriggerActivation == ETriggerActivation.Unknown)
                    {
                        Camera.TriggerActivation = ETriggerActivation.RisingEdge;
                    }

                    TriggerActivationComboBox.SelectedItem = Camera.TriggerActivation.ToString();
                }
                
                //宽度
                WidthTextBox.Text = Camera.Width.ToString();

                //高度
                HeihtTextBox.Text = Camera.Heiht.ToString();
                
                //曝光值
                ExposureTimeTextBox.Text = Camera.ExposureTime.ToString();

                //增益值
                GainTextBox.Text = Camera.Gain.ToString();

            }
        }

        #region 控件事件

        /// <summary>
        /// 用户控件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PixelFormatComboBox.IsEnabled = false;
            WidthTextBox.IsEnabled = false;
            HeihtTextBox.IsEnabled = false;

            TriggerModeComboBox.IsEnabled = false;
            TriggerSourceComboBox.IsEnabled = false;
            TriggerActivationComboBox.IsEnabled = false;
            ExposureTimeTextBox.IsEnabled = false;
            GainTextBox.IsEnabled = false;

        }

        /// <summary>
        /// 像素格式选择控件点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PixelFormatCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (PixelFormatCheckBox.IsChecked == true)
            {
                PixelFormatComboBox.IsEnabled = true;
            }
            else
            {
                PixelFormatComboBox.IsEnabled = false;
            }

        }

        /// <summary>
        /// 宽度选择控件点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WidthCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (WidthCheckBox.IsChecked == true)
            {
                WidthTextBox.IsEnabled = true;
            }
            else
            {
                WidthTextBox.IsEnabled = false;
            }
        }

        /// <summary>
        /// 高度选择控件点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeihtCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (HeihtCheckBox.IsChecked == true)
            {
                HeihtTextBox.IsEnabled = true;
            }
            else
            {
                HeihtTextBox.IsEnabled = false;
            }
        }

        /// <summary>
        /// 选择控件点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriggerModeCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (TriggerModeCheckBox.IsChecked == true)
            {
                TriggerModeComboBox.IsEnabled = true;
            }
            else
            {
                TriggerModeComboBox.IsEnabled = false;
            }
        }

        /// <summary>
        /// 触发源选择控件点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriggerSourceCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (TriggerSourceCheckBox.IsChecked == true)
            {
                TriggerSourceComboBox.IsEnabled = true;
            }
            else
            {
                TriggerSourceComboBox.IsEnabled = false;
            }
        }

        /// <summary>
        /// 硬件有效触发信号选择控件点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriggerActivationCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (TriggerActivationCheckBox.IsChecked == true)
            {
                TriggerActivationComboBox.IsEnabled = true;
            }
            else
            {
                TriggerActivationComboBox.IsEnabled = false;
            }
        }

        /// <summary>
        /// 曝光时间选择控件点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExposureTimeCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (ExposureTimeCheckBox.IsChecked == true)
            {
                ExposureTimeTextBox.IsEnabled = true;
            }
            else
            {
                ExposureTimeTextBox.IsEnabled = false;
            }
        }

        /// <summary>
        /// 增益值选择控件点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GainCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (GainCheckBox.IsChecked == true)
            {
                GainTextBox.IsEnabled = true;
            }
            else
            {
                GainTextBox.IsEnabled = false;
            }
        }

        /// <summary>
        /// 宽度参数改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WidthTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    //写入数据
                    Camera.Width = int.Parse(WidthTextBox.Text);
                }
                catch (Exception)
                {

                }

                //回读数据
                WidthTextBox.Text = Camera.Width.ToString();

                //刷新参数
                ReadCameraParam();
            }
        }

        /// <summary>
        /// 高度参数改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeihtTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    //写入数据
                    Camera.Heiht = int.Parse(HeihtTextBox.Text);
                }
                catch (Exception)
                {

                }

                //回读数据
                HeihtTextBox.Text = Camera.Heiht.ToString();

                //刷新参数
                ReadCameraParam();
            }
        }

        /// <summary>
        /// 曝光值参数改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExposureTimeTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    //写入数据
                    Camera.ExposureTime = int.Parse(ExposureTimeTextBox.Text);
                }
                catch (Exception)
                {

                }

                //回读数据
                ExposureTimeTextBox.Text = Camera.ExposureTime.ToString();

                //刷新参数
                ReadCameraParam();
            }
        }

        /// <summary>
        /// 增益值参数改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GainTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    //写入数据
                    Camera.Gain = int.Parse(GainTextBox.Text);
                }
                catch (Exception)
                {

                }

                //回读数据
                GainTextBox.Text = Camera.Gain.ToString();

                //刷新参数
                ReadCameraParam();
            }
        }

        /// <summary>
        /// 像素格式参数改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PixelFormatComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //写入数据
            Camera.PixelFormat = (EPixelFormatType)Enum.Parse(typeof(EPixelFormatType), PixelFormatComboBox.SelectedItem.ToString());

            //回读数据
            PixelFormatComboBox.SelectedItem = Camera.PixelFormat.ToString();

            //刷新参数
            ReadCameraParam();
        }

        /// <summary>
        /// 触发模式参数改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriggerModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //写入数据
            Camera.TriggerMode = (ETriggerModeStatus)Enum.Parse(typeof(ETriggerModeStatus), TriggerModeComboBox.SelectedItem.ToString());

            //回读数据
            TriggerModeComboBox.SelectedItem = Camera.TriggerMode.ToString();

            //刷新参数
            ReadCameraParam();
        }

        /// <summary>
        /// 触发源参数改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriggerSourceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //写入数据
            Camera.TriggerSource = (ETriggerSource)Enum.Parse(typeof(ETriggerSource), TriggerSourceComboBox.SelectedItem.ToString());

            //回读数据
            TriggerSourceComboBox.SelectedItem = Camera.TriggerSource.ToString();

            //刷新参数
            ReadCameraParam();
        }

        /// <summary>
        /// 有效触发信号参数改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriggerActivationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //写入数据
            Camera.TriggerActivation = (ETriggerActivation)Enum.Parse(typeof(ETriggerActivation), TriggerActivationComboBox.SelectedItem.ToString());

            //回读数据
            TriggerActivationComboBox.SelectedItem = Camera.TriggerActivation.ToString();

            //刷新参数
            ReadCameraParam();
        }

        #endregion

        #endregion

        #region 构造/析构接口

        /// <summary>
        /// 创建相机配置控件新实例
        /// </summary>
        public CameraConfigControl() : this(null)
        {
            InitializeComponent();
        }

        /// <summary>
        /// 创建相机配置控件新实例
        /// </summary>
        /// <param name="camera">相机句柄</param>
        public CameraConfigControl(ICamera camera)
        {
            Camera = camera;

        }

        #endregion

        #region 属性

        /// <summary>
        /// 相机控制接口
        /// </summary>
        public ICamera Camera { get; set; }

        #endregion

        #region 公共事件


        #endregion

    }
}
