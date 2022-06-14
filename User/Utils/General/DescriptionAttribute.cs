using System;

namespace User.Utils.General
{
    public class DescriptionAttribute : Attribute
    {
        private string description;
        private string delevoper;
        private string title;

        public DescriptionAttribute(string Delevoper, string Title, string Description)
        {
            this.delevoper = Delevoper;
            this.title = Title;
            this.description = Description;
        }

        public virtual string Description
        {
            get { return description; }
        }

        public virtual string Delevoper
        {
            get { return delevoper; }
        }

        public virtual string Title
        {
            get { return title; }
        }
    }
}