using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// 
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// ID của user
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Chức vụ
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// Phân quyền
        /// </summary>
        public string GroupName { get; set; }
    }
}
