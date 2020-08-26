using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIGE
{
    public struct Bulletin
    {
        public string textA;
        public string highlight;
        public string textB;
        public int highlightColor;
        public bool shown;
        public DateTime showTime;

        public Bulletin(string text)
        {
            textA = text;
            highlight = "";
            textB = "";
            highlightColor = 15;
            shown = false;
            showTime = DateTime.Now;
        }

        public Bulletin(string textA, string highlight, string textB, int highlightColor)
        {
            this.textA = textA;
            this.highlight = highlight;
            this.textB = textB;
            this.highlightColor = highlightColor;
            shown = false;
            showTime = DateTime.Now;
        }

        public void Show()
        {
            shown = true;
            showTime = DateTime.Now;
        }
    }
}
