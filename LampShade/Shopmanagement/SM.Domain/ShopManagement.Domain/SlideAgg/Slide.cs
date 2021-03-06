using System.Diagnostics.SymbolStore;
using _0_Framework.Domain;

namespace ShopManagement.Domain.SlideAgg
{
    public class Slide : EntityBase
    {
        public string PictureUrl { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Heading { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string BtnText { get; private set; }
        public string Link { get; private set; }
        public bool IsRemoved { get; private set; }

        public Slide()
        {
        }

        public Slide(string pictureUrl, string pictureAlt, string pictureTitle,
            string heading, string title, string text, string btnText, string link)
        {
            PictureUrl = pictureUrl;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
            Link = link;
            IsRemoved = false;
        }

        public void Edit(string pictureUrl, string pictureAlt, string pictureTitle, string heading,
            string title, string text, string btnText, string link)
        {
            if (!string.IsNullOrWhiteSpace(pictureUrl))
            {
                PictureUrl = pictureUrl;
            }
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
            Link = link;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}