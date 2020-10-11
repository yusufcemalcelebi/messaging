using System;
namespace Messaging.Core
{
    public class BasePaginationModel
    {
        public BasePaginationModel()
        {
            Size = 50;
            Page = 0;
        }

        public int Size { get; set; }
        public int Page { get; set; }
    }
}
