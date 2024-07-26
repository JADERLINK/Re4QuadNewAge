using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;

namespace Re4QuadExtremeEditor.src.Class.MyProperty
{
    [DefaultProperty(nameof(Version))]
    public class NoneProperty
    {
        //Info

        [CategoryAttribute("Info")]
        [DescriptionAttribute("")]
        [DisplayNameAttribute("Re4 Quad Extreme Editor [New Age]")]
        [DefaultValueAttribute(null)]
        [ReadOnlyAttribute(true)]
        [BrowsableAttribute(true)]
        public string Version { get => "Version: 1.2.1"; }

        [CategoryAttribute("Info")]
        [DescriptionAttribute("")]
        [DisplayNameAttribute("OpenGL")]
        [DefaultValueAttribute(null)]
        [ReadOnlyAttribute(true)]
        [BrowsableAttribute(true)]
        public string OpenGLVersion { get => "Version: " + Globals.OpenGLVersion; }

        //JADERLINK

        [CategoryAttribute("JADERLINK")]
        [DescriptionAttribute("")]
        [DisplayNameAttribute("Tool created by:")]
        [DefaultValueAttribute(null)]
        [ReadOnlyAttribute(true)]
        [BrowsableAttribute(true)]
        public string JADERLINK { get => "JADERLINK"; }

        [CategoryAttribute("JADERLINK")]
        [DescriptionAttribute("")]
        [DisplayNameAttribute("Youtube:")]
        [DefaultValueAttribute(null)]
        [ReadOnlyAttribute(true)]
        [BrowsableAttribute(true)]
        public string Youtube { get => "https://www.youtube.com/@JADERLINK"; }

        [CategoryAttribute("JADERLINK")]
        [DescriptionAttribute("")]
        [DisplayNameAttribute("GitHub:")]
        [DefaultValueAttribute(null)]
        [ReadOnlyAttribute(true)]
        [BrowsableAttribute(true)]
        public string GitHub { get => "https://github.com/JADERLINK"; }

        [CategoryAttribute("JADERLINK")]
        [DescriptionAttribute("")]
        [DisplayNameAttribute("Blog:")]
        [DefaultValueAttribute(null)]
        [ReadOnlyAttribute(true)]
        [BrowsableAttribute(true)]
        public string Blog { get => "https://jaderlink.blogspot.com/"; }

        [CategoryAttribute("JADERLINK")]
        [DescriptionAttribute("")]
        [DisplayNameAttribute("Donate:")]
        [DefaultValueAttribute(null)]
        [ReadOnlyAttribute(true)]
        [BrowsableAttribute(true)]
        public string Donate { get => "https://jaderlink.github.io/Donate/"; }

    }
}
