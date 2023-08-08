using System.Threading.Tasks;

namespace ts.ProjectName.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommonWebRuntime
    {
        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task Navigate(string url);

        /// <summary>
        /// 从资源文件中读取配置
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Config ReadFromResource(string fileName);

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task SetCookie(string url, string domain = null);

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <returns></returns>
        Task Close();

        /// <summary>
        /// 获取网页显示内容
        /// </summary>
        /// <returns></returns>
        object GetWebContent();

        /// <summary>
        /// 打开调试窗口
        /// </summary>
        void OpenDevelopTools();
    }
}
