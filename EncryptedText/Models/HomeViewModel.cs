using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncryptedText.Models
{
    public class HomeViewModel
    {
        public string EncryptedText { get; set; }
        public string EntropyText { get; set; }
        public string DecryptedText { get; set; }
    }
}