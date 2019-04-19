using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Vision
{
    /// <summary>
    /// 标定参数
    /// </summary>
    public class CalibParam
    {
        /// <summary>
        /// 标定点列表
        /// </summary>
        public List<CalibPointData> CalibPointList { get; set; }

        /// <summary>
        /// 标定矩阵
        /// </summary>
        public double[] Matrix { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 创建标定参数新实例
        /// </summary>
        public CalibParam()
        {
            CalibPointList = new List<CalibPointData>();
            Matrix = new double[0];
            IsValid = false;
        }

    }
}
