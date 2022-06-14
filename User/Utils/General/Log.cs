using System;

namespace User.Utils.General
{
    internal class Log
    {
        public long LogId { get; set; }
        public string UserId { get; set; }
        public string Ip { get; set; }
        public string HostName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ActionDescription { get; set; }
        public string HttpMethod { get; set; }
        public string Url { get; set; }
        public string Developer { get; set; }
        public string DescriptionTitle { get; set; }
        public string Description { get; set; }
        public bool BError { get; set; }
        public string FormContent { get; set; }
        public string Exception { get; set; }
        public DateTime InsertedDate { get; set; }

    }
}