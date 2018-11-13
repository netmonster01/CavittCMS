using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using cavitt.net.Data;
using cavitt.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using cavitt.net.Interfaces;
using static cavitt.net.CustomEnums;

namespace cavitt.net.Repositories
{
    public class LoggerRepository : ILoggerRepository
    {
        private IServiceProvider _serviceProvider;
        private ApplicationDbContext _applicationDbContext;
        private UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _accessor;

        public LoggerRepository(IServiceProvider serviceProvider, IHttpContextAccessor accessor)
        {
            _serviceProvider = serviceProvider;
            _applicationDbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            _userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _accessor = accessor;
        }
        public void Write(Log log)
        {
            log.UserId = _accessor.HttpContext.User?.Identity?.Name;
            _applicationDbContext.Logs.Add(log);
            _applicationDbContext.SaveChangesAsync();
        }


        public void Write(Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log log = new Log
            {
                DateCreated = DateTime.Now,
                UserId = _accessor.HttpContext.User?.Identity?.Name,
                Source = string.Format("Source:{0} LineNumber:{1}", sourceFilePath, sourceLineNumber),
                LogType = CustomEnums.LogType.Error,
                Message = ex.Message
            };

            _applicationDbContext.Logs.Add(log);
            _applicationDbContext.SaveChangesAsync();
        }

        public void Write(LogType type, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Log log = new Log
            {
                DateCreated = DateTime.Now,
                UserId = _accessor.HttpContext.User?.Identity?.Name,
                Source = string.Format("Source:{0} LineNumber:{1}", sourceFilePath, sourceLineNumber),
                LogType = type,
                Message = message
            };

            _applicationDbContext.Logs.Add(log);
            _applicationDbContext.SaveChangesAsync();
        }

        public List<Log> GetLogs()
        {
            List<Log> logs = new List<Log>();
            try
            {
                List<Log> uncheckedList = _applicationDbContext.Logs.Where(l => l.Checked == false).ToList();
                logs = uncheckedList.ToList();
                uncheckedList = uncheckedList.Where(x => (x.Checked = false) == true).ToList();
                _applicationDbContext.Logs.UpdateRange(uncheckedList.ToArray());
                _applicationDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Write(ex);
            }

            return logs;
        }

        public int ErrorLogCount()
        {
            return _applicationDbContext.Logs.Where(l => l.LogType == LogType.Error && l.Checked == false).Count();
        }
    }
}
