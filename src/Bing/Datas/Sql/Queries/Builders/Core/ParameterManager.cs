﻿using System.Collections.Generic;
using Bing.Datas.Sql.Queries.Builders.Abstractions;
using Bing.Utils;
using Bing.Utils.Extensions;

namespace Bing.Datas.Sql.Queries.Builders.Core
{
    /// <summary>
    /// 参数管理器
    /// </summary>
    public class ParameterManager:IParameterManager
    {
        /// <summary>
        /// 参数集合
        /// </summary>
        private readonly IDictionary<string, object> _params;

        /// <summary>
        /// 初始化一个<see cref="ParameterManager"/>类型的实例
        /// </summary>
        public ParameterManager()
        {
            _params = new Dictionary<string, object>();
        }

        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> GetParams()
        {
            return _params;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="operator">运算符</param>
        public void Add(string name, object value, Operator @operator)
        {
            _params.Add(name, GetValue(value, @operator));
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="value">参数值</param>
        /// <param name="operator">运算符</param>
        /// <returns></returns>
        private object GetValue(object value, Operator @operator)
        {
            if (string.IsNullOrWhiteSpace(value.SafeString()))
            {
                return value;
            }

            switch (@operator)
            {
                case Operator.Contains:
                    return $"%{value}%";
                case Operator.Starts:
                    return $"{value}%";
                case Operator.Ends:
                    return $"%{value}";
                default:
                    return value;
            }
        }
    }
}
