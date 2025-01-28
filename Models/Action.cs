using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models
{
    public class Action
    {
        public static int Insert { get { return 1; } }
        public static int Update { get { return 2; } }
        public static int Delete { get { return 3; } }
        public static int Select { get { return 4; } }
    }
}