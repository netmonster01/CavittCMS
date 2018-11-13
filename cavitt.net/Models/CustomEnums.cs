using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net
{
    public class CustomEnums
    {
        public enum ContentType
        {
            Blog,
            Article,
            Project

        }

        public enum LogType
        {
            Error,
            Warning,
            Debug,
            Info,
            Pass,
            Fail
        }

        public enum RoleType
        {
            Admin,
            Reader,
            Publisher,
            Family
        }


        public enum ProjectType
        {
            Personal,
            Professional
        }
    }
}
