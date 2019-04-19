using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Vision
{
    /// <summary>
    /// 标定定位数据
    /// </summary>
    public class CalibPointData
    {
        /// <summary>
        /// 原始X点位
        /// </summary>
        public double Px { get; set; }

        /// <summary>
        /// 原始Y点位
        /// </summary>
        public double Py { get; set; }

        /// <summary>
        /// 转换X点位
        /// </summary>
        public double Qx { get; set; }

        /// <summary>
        /// 转换Y点位
        /// </summary>
        public double Qy { get; set; }

        /// <summary>
        /// 创建CalibData新实例
        /// </summary>
        /// <param name="px">原始X点位</param>
        /// <param name="py">原始Y点位</param>
        /// <param name="qx">转换X点位</param>
        /// <param name="qy">转换Y点位</param>
        public CalibPointData(double px, double py, double qx, double qy)
        {
            Px = px;
            Py = py;
            Qx = qx;
            Qy = qy;

        }

    }
}
