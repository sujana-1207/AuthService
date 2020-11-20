using System;
using System.Collections.Generic;

#nullable disable

namespace AuthService
{
    public partial class TestResultsTab
    {
        public int TestId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Result { get; set; }
    }
}
