﻿namespace ShopManagement.Application.Contract.A.Slide
{
    public class SlideViewModel
    {
        public long Id { get; set; }
        public string PictureUrl { get; set; }
        public string Heading { get; set; }
        public string Title { get; set; }
        public string CreationDate { get; set; }
        public bool IsRemoved { get; set; }

    }
}