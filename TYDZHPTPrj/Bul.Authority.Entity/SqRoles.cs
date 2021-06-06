﻿using System;

namespace Bul.Authority.Entity
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class SqRoles
    {
        /// <summary>
        /// 
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JSBM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JSMC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SYZT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SFSC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long SSGSID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? Creater { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? Updater { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
