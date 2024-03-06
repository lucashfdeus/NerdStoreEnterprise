using System;
using System.Collections.Generic;

namespace NSE.WebApp.MVC.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class ResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErroMessages Erros { get; set; }
    }

    public class ResponseErroMessages
    {
        public List<string> Mensagens { get; set; }
    }
}
