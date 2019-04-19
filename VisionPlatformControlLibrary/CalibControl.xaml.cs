using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using Framework.Vision;
using Framework.Infrastructure.Serialization;

namespace VisionPlatformControlLibrary
{
    /// <summary>
    /// CalibControl.xaml 的交互逻辑
    /// </summary>
    public partial class CalibControl : UserControl
    {
        #region 私有成员
        
        /// <summary>
        /// 标定点列表(与控件绑定)
        /// </summary>
        private ObservableCollection<CalibPointData> _CalibPointList;

        /// <summary>
        /// 标定参数
        /// </summary>
        private CalibParam _CalibParam;

        private string _FilePath = "";

        /// <summary>
        /// 获取标定矩阵字符串
        /// </summary>
        /// <param name="Matrix">矩阵</param>
        private void DisplayCalibMatrix(double[] Matrix)
        {
            try
            {
                if ((Matrix != null) && (Matrix.Length > 0))
                {
                    //显示标定结果
                    string MatrixString = "";

                    for (int i = 0; i < Matrix.Length - 1; i++)
                    {
                        MatrixString += Matrix[i].ToString() + ",";
                    }

                    MatrixString += Matrix[Matrix.Length - 1].ToString();

                    CalibMatrixTextBox.Text = MatrixString;
                }
                else
                {
                    CalibMatrixTextBox.Text = "Err";
                }
            }
            catch (Exception)
            {

            }

        }


        /// <summary>
        /// 禁止所有的控件
        /// </summary>
        private void DisableAllControl()
        {
            ImageXTextBox.IsEnabled = false;
            ImageYTextBox.IsEnabled = false;
            RobotXTextBox.IsEnabled = false;
            RobotYTextBox.IsEnabled = false;

            ClearButton.IsEnabled = false;
            GetVisionResultButton.IsEnabled = false;

            AddButton.IsEnabled = false;
            CoverButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            CalibButton.IsEnabled = false;

            CalibPointListView.IsEnabled = false;

            LoadFileButton.IsEnabled = true;
            CreateFileButton.IsEnabled = true;
            ResetButton.IsEnabled = true;

            CalibFileTextBox.Text = "";
            ImageXTextBox.Text = "";
            ImageYTextBox.Text = "";
            RobotXTextBox.Text = "";
            RobotYTextBox.Text = "";
            CalibMatrixTextBox.Text = "";

        }

        /// <summary>
        /// 使能所有的控件
        /// </summary>
        private void EnableAllControl()
        {
            ImageXTextBox.IsEnabled = true;
            ImageYTextBox.IsEnabled = true;
            RobotXTextBox.IsEnabled = true;
            RobotYTextBox.IsEnabled = true;

            ClearButton.IsEnabled = true;
            GetVisionResultButton.IsEnabled = true;

            AddButton.IsEnabled = true;
            CoverButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
            CalibButton.IsEnabled = true;

            CalibPointListView.IsEnabled = true;

            LoadFileButton.IsEnabled = false;
            CreateFileButton.IsEnabled = false;
            ResetButton.IsEnabled = true;

        }

        #region UI事件

        /// <summary>
        /// 控件加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _CalibParam = new CalibParam();
            
            _CalibPointList = new ObservableCollection<CalibPointData>();
            CalibPointListView.ItemsSource = _CalibPointList;

            //禁止所有的控件
            DisableAllControl();

        }

        /// <summary>
        /// 标定点列表控件尺寸改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalibPointListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ImageX.Width = (e.NewSize.Width - 8) / 4;
            ImageY.Width = (e.NewSize.Width - 8) / 4;
            RobotX.Width = (e.NewSize.Width - 8) / 4;
            RobotY.Width = (e.NewSize.Width - 8) / 4;

        }

        /// <summary>
        /// 复位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            UserControl_Loaded(null, null);
        }

        /// <summary>
        /// 加载文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ofd = new Microsoft.Win32.OpenFileDialog();

                ofd.DefaultExt = ".json";
                ofd.Filter = "json file|*.json";

                if (ofd.ShowDialog() == true)
                {
                    _FilePath = ofd.FileName;
                    CalibFileTextBox.Text = _FilePath;
                    _CalibParam = JsonSerialization.DeserializeObjectFromFile<CalibParam>(_FilePath);

                    if (_CalibParam == null)
                    {
                        MessageBox.Show("无效文件!");
                        return;
                    }
                    
                    _CalibPointList.Clear();

                    foreach (var item in _CalibParam.CalibPointList)
                    {
                        _CalibPointList.Add(item);
                    }

                    EnableAllControl();

                    DisplayCalibMatrix(_CalibParam.Matrix);
                    
                }
            }
            catch (Exception)
            {
                
            }

        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            //创建一个保存文件式的对话框  
            var sfd = new Microsoft.Win32.SaveFileDialog();

            //设置保存的文件的类型，注意过滤器的语法  
            sfd.Filter = "json file|*.json";
            sfd.FileName = "";

            //调用ShowDialog()方法显示该对话框，该方法的返回值代表用户是否点击了确定按钮  
            if (sfd.ShowDialog() == true)
            {
                _FilePath = sfd.FileName;
                CalibFileTextBox.Text = _FilePath;

                EnableAllControl();
            }
        }

        /// <summary>
        /// 清除按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ImageXTextBox.Text = "";
            ImageYTextBox.Text = "";
            RobotXTextBox.Text = "";
            RobotYTextBox.Text = "";
        }

        /// <summary>
        /// 获取视觉结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetVisionResultButton_Click(object sender, RoutedEventArgs e)
        {
            double imageX = 0;
            double imageY = 0;

            if (GetVisionResult(out imageX, out imageY))
            {
                ImageXTextBox.Text = imageX.ToString();
                ImageYTextBox.Text = imageY.ToString();
            }
            else
            {
                ImageXTextBox.Text = "-1";
                ImageYTextBox.Text = "-1";
            }

        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //获取控件值
                double ImageX, ImageY, RobotX, RobotY;

                if (!double.TryParse(ImageXTextBox.Text, out ImageX))
                {
                    ImageX = -1;
                }
                if (!double.TryParse(ImageYTextBox.Text, out ImageY))
                {
                    ImageY = -1;
                }
                if (!double.TryParse(RobotXTextBox.Text, out RobotX))
                {
                    RobotX = -1;
                }
                if (!double.TryParse(RobotYTextBox.Text, out RobotY))
                {
                    RobotY = -1;
                }

                //添加
                _CalibPointList.Add(new CalibPointData(ImageX, ImageY, RobotX, RobotY));

            }
            catch (Exception)
            {
                
            }

        }

        /// <summary>
        /// 覆盖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CoverButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //获取控件值
                double ImageX, ImageY, RobotX, RobotY;

                if (!double.TryParse(ImageXTextBox.Text, out ImageX))
                {
                    ImageX = -1;
                }
                if (!double.TryParse(ImageYTextBox.Text, out ImageY))
                {
                    ImageY = -1;
                }
                if (!double.TryParse(RobotXTextBox.Text, out RobotX))
                {
                    RobotX = -1;
                }
                if (!double.TryParse(RobotYTextBox.Text, out RobotY))
                {
                    RobotY = -1;
                }

                //覆盖原有数据
                if ((CalibPointListView.SelectedIndex >= 0) && (CalibPointListView.SelectedIndex < _CalibPointList.Count))
                {
                    _CalibPointList[CalibPointListView.SelectedIndex] = new CalibPointData(ImageX, ImageY, RobotX, RobotY);
                }
            }
            catch (Exception)
            {
                
            }

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //删除原有数据
                if ((CalibPointListView.SelectedIndex >= 0) && (CalibPointListView.SelectedIndex < _CalibPointList.Count))
                {
                    _CalibPointList.RemoveAt(CalibPointListView.SelectedIndex);
                }
            }
            catch (Exception)
            {
                
            }
        }

        /// <summary>
        /// 标定/保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalibButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                //拼接数据
                double[] Px = new double[_CalibPointList.Count];
                double[] Py = new double[_CalibPointList.Count];
                double[] Qx = new double[_CalibPointList.Count];
                double[] Qy = new double[_CalibPointList.Count];

                _CalibParam.CalibPointList.Clear();

                for (int i = 0; i < _CalibPointList.Count; i++)
                {
                    Px[i] = _CalibPointList[i].Px;
                    Py[i] = _CalibPointList[i].Py;
                    Qx[i] = _CalibPointList[i].Qx;
                    Qy[i] = _CalibPointList[i].Qy;

                    _CalibParam.CalibPointList.Add(_CalibPointList[i]);
                }

                //计算标定矩阵
                double[] maxtrix;
                var result = GetCalibMatrix(Px, Py, Qx, Qy, out maxtrix);
                _CalibParam.Matrix = maxtrix;
                _CalibParam.IsValid = result;

                //保存结果
                JsonSerialization.SerializeObjectToFile(_CalibParam, _FilePath);

                //显示标定矩阵
                DisplayCalibMatrix(maxtrix);

                if (result)
                {
                    MessageBox.Show("标定成功!");
                }
                else
                {
                    MessageBox.Show("标定失败!");
                }

            }
            catch (Exception)
            {
                
            }

        }

        /// <summary>
        /// CalibPointListView选择项改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalibPointListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((CalibPointListView.SelectedIndex >= 0) && (CalibPointListView.SelectedIndex < _CalibPointList.Count))
                {
                    ImageXTextBox.Text = _CalibPointList[CalibPointListView.SelectedIndex].Px.ToString();
                    ImageYTextBox.Text = _CalibPointList[CalibPointListView.SelectedIndex].Py.ToString();
                    RobotXTextBox.Text = _CalibPointList[CalibPointListView.SelectedIndex].Qx.ToString();
                    RobotYTextBox.Text = _CalibPointList[CalibPointListView.SelectedIndex].Qy.ToString();
                }
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #endregion

        #region 构造/析构接口

        /// <summary>
        /// 创建标定控件新实例
        /// </summary>
        public CalibControl()
        {
            InitializeComponent();
        }

        #endregion

        #region 公共接口

        /// <summary>
        /// 获取视觉结果
        /// </summary>
        /// <param name="imageX">图像X坐标</param>
        /// <param name="imageY">图像Y坐标</param>
        /// <returns>执行结果</returns>
        public virtual bool GetVisionResult(out double imageX, out double imageY)
        {
            imageX = -1;
            imageY = -1;

            return false;
        }

        /// <summary>
        /// 获取标定矩阵
        /// </summary>
        /// <param name="Px">原始X点位</param>
        /// <param name="Py">原始Y点位</param>
        /// <param name="Qx">转换X点位</param>
        /// <param name="Qy">转换Y点位</param>
        /// <param name="Matrix">转换矩阵</param>
        /// <returns>执行结果</returns>
        public virtual bool GetCalibMatrix(double[] Px, double[] Py, double[] Qx, double[] Qy, out double[] Matrix)
        {
            Matrix = new double[] { -1, -1, -1, -1, -1, -1, -1, -1 };


            return false;
        }



        #endregion

    }

    



}
