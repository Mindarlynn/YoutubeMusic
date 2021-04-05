using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeMusic.ViewModel
{
    class WebView2ViewModel
    {
        public string Url { get; set; }

        public WebView2ViewModel()
        {
            Url = "https://music.youtube.com";
        }
    }
}
