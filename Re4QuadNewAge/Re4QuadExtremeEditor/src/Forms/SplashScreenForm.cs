using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Re4QuadExtremeEditor.src.Forms
{
    public partial class SplashScreenForm : Form
    {
        private SplashScreenConteiner conteiner;

        private bool BlockClose = true;

        public SplashScreenForm(SplashScreenConteiner conteiner)
        {
            conteiner.Close = CloseForm;
            conteiner.ReleasedToClose = ReleasedToClose;
            this.conteiner = conteiner;
            InitializeComponent();
        }

        private void ReleasedToClose() 
        {
            if (conteiner.FormIsClosed == false)
            {
                this.Invoke(new Action(InvokedReleasedToClose));
            }
        }

        private void InvokedReleasedToClose() 
        {
            BlockClose = false;
        }

        private void CloseForm() 
        {
            if (conteiner.FormIsClosed == false)
            {
                this.Invoke(new Action(InvokedCloseForm));
            }
        }

        private void InvokedCloseForm() 
        {
            BlockClose = false;
            Close();
        }

        private void SplashScreenForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            conteiner.FormIsClosed = true;
        }

        private void SplashScreenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BlockClose)
            {
                e.Cancel = true;
            }
        }


        //----------------------
        private void To(string url)
        {
            try { System.Diagnostics.Process.Start("explorer.exe", url); } catch (Exception) { }
        }

        private void linkLabelJaderLinkBlog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://jaderlink.blogspot.com/");
        }

        private void linkLabelJaderLinkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK");
        }

        private void linkLabelYoutubeJaderLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.youtube.com/@JADERLINK");
        }

        private void linkLabelDonate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://jaderlink.github.io/Donate/");
        }
    }
}
