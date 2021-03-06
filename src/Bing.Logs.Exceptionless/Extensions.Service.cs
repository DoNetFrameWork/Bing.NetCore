﻿using System;
using System.Collections.Generic;
using System.Text;
using Bing.Logs.Abstractions;
using Bing.Logs.Core;
using Exceptionless;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Logs.Exceptionless
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 注册Exceptionless日志操作
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configAction">配置操作</param>
        /// <param name="name">服务名称</param>
        public static void AddExceptionless(this IServiceCollection services,
            Action<ExceptionlessConfiguration> configAction, string name = null)
        {
            services.AddScoped<ILogProviderFactory, Bing.Logs.Exceptionless.LogProviderFactory>();
            services.AddSingleton(typeof(ILogFormat), t => NullLogFormat.Instance);
            services.AddScoped<ILogContext, Bing.Logs.Exceptionless.LogContext>();
            services.AddScoped<ILog, Log>();

            configAction?.Invoke(ExceptionlessClient.Default.Configuration);
        }
    }
}
