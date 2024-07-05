using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//https://stackoverflow.com/questions/4580263/how-to-open-in-default-browser-in-c-sharp


namespace Re4QuadExtremeEditor.src.Forms
{
    public partial class CreditsForm : Form
    {
        public CreditsForm()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void CreditsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.F1)
            {
                Close();
            }
        }

        private void buttonCLOSE_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void To(string url)
        {
            try { System.Diagnostics.Process.Start("explorer.exe", url); } catch (Exception) { }
        }

        #region main tab

        private void linkLabelProjectGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/Re4QuadNewAge");
        }

        private void linkLabelDonate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://jaderlink.github.io/Donate/");
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

        private void linkLabelGuad64Project_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/DavidSM64/Quad64");
        }

        private void linkLabelLicenceQuad64_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/DavidSM64/Quad64/blob/master/LICENSE");
        }

        private void linkLabelTgaGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/ALEXGREENALEX/TGASharpLib");
        }

        private void linkLabelTgaGitLab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://gitlab.com/Alex_Green/TGASharpLib");
        }

        private void linkLabelLicenseTGA_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/ALEXGREENALEX/TGASharpLib/blob/master/LICENSE");
        }

        private void linkLabelSiteJsonNET_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.newtonsoft.com/json");
        }

        private void linkLabelLicenseJsonNET_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md");
        }

        private void linkLabelSiteOpenTK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://opentk.net/");
        }

        private void linkLabelNugetOpenTK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.nuget.org/packages/OpenTK/");
        }

        private void linkLabelNugetGLControl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.nuget.org/packages/OpenTK.GLControl/");
        }

        private void linkLabelLicenseOpenTK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/opentk/opentk/blob/master/LICENSE.md");
        }

        private void linkLabelLicenseCodeProject_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.codeproject.com/info/cpol10.aspx");
        }

        private void linkLabelGridComboBox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.codeproject.com/Articles/23242/Property-Grid-Dynamic-List-ComboBox-Validation-and");
        }

        private void linkLabelMultiselectTreeView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.codeproject.com/Articles/20581/Multiselect-Treeview-Implementation");
        }

        private void linkLabelDynamicProperties_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.codeproject.com/Articles/189521/Dynamic-Properties-for-PropertyGrid");
        }

        private void linkLabelCustomizedDisplay_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.codeproject.com/Articles/4448/Customized-Display-of-Collection-Data-in-a-Propert");
        }

        private void linkLabelDdsGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/ALEXGREENALEX/DDSReaderSharp");
        }

        private void linkLabelDdsGitLab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://gitlab.com/Alex_Green/DDSReaderSharp");
        }

        private void linkLabelLicenseDDS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/ALEXGREENALEX/DDSReaderSharp/blob/master/LICENSE");
        }
        #endregion

        #region thanks tab

        private void linkLabelSonOfPercia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://residentevilmodding.boards.net/thread/9780/2018-re4uhd-toolset-persia-released");
        }

        private void linkLabelMrCurious_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://docs.google.com/document/d/1XWXl8naf8NZhV0k56cdAPYTOobgOcmVtHN1QYQnC7mA/edit");
        }

        private void linkLabelJaderLinkBlog2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://jaderlink.blogspot.com/");
        }

        private void linkLabelJaderLinkGitHub2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK");
        }

        private void linkLabelYoutubeJaderLink2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.youtube.com/@JADERLINK");
        }

        private void linkLabelLordVincBlog_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://lordvinc.blogspot.com/");
        }

        private void linkLabelLordVincGitHub_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/Lordvinc");
        }

        private void linkLabelYoutubeLordvinc_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.youtube.com/@Lordvinc");
        }

        private void linkLabelYoutubeCurious_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.youtube.com/@MrCuriousModding");
        }

        private void linkLabelRemodCurious_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://residentevilmodding.boards.net/user/5592");
        }

        private void linkLabelRemodPercia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://residentevilmodding.boards.net/user/987");
        }

        private void linkLabelRemodMario_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://residentevilmodding.boards.net/user/473");
        }

        private void linkLabelYoutubeMario_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.youtube.com/@mariokart64n");
        }

        private void linkLabelRemodHardRain_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://residentevilmodding.boards.net/user/26610");
        }

        private void linkLabelYoutubeHardRain_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.youtube.com/@HardRainModder");
        }

        private void linkLabelRemodZatarita_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://residentevilmodding.boards.net/user/29688");
        }

        private void linkLabelRemodDarthxvoid_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://residentevilmodding.boards.net/user/32724");
        }

        private void linkLabelYoutubeDarthxVoid_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://www.youtube.com/@DarthxVoid");
        }

        #endregion

        #region Jaderlink Tools tab

        private void linkLabel_JADERLINK_MODEL_VIEWER_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/JADERLINK_MODEL_VIEWER");
        }

        private void linkLabel_RE4_2007_MODEL_VIEWER_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/JADERLINK_MODEL_VIEWER");
        }

        private void linkLabel_RE4_PS2_MODEL_VIEWER_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/JADERLINK_MODEL_VIEWER");
        }

        private void linkLabel_RE4_UHD_MODEL_VIEWER_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/JADERLINK_MODEL_VIEWER");
        }

        private void linkLabel_RE4_2007_SCENARIO_SMD_TOOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/RE4-2007-SCENARIO-SMD-TOOL");
        }

        private void linkLabel_RE4_2007_PMD_TOOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/RE4-2007-PMD-TOOL");
        }

        private void linkLabel_RE4_PS2_SCENARIO_SMD_TOOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/RE4-PS2-SCENARIO-SMD-TOOL");
        }

        private void linkLabel_RE4_PS2_BIN_TOOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/RE4-PS2-BIN-TOOL");
        }

        private void linkLabel_RE4_PS2_TPL_TOOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/RE4-PS2-TPL-TOOL");
        }

        private void linkLabel_RE4_UHD_SCENARIO_SMD_TOOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/RE4-UHD-SCENARIO-SMD-TOOL");
        }

        private void linkLabel_RE4_UHD_BIN_TOOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/RE4-UHD-BIN-TOOL");
        }

        private void linkLabel_RE4_UHD_PACKYZ2_TOOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/RE4-UHD-PACKYZ2-TOOL");
        }

        private void linkLabel_RE4_SMX_TOOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/RE4-SMX-TOOL");
        }

        private void linkLabel_RE4_SAT_EAT_TOOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/RE4-SAT-EAT-TOOL");
        }

        private void linkLabel_RE4_RTP_TOOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/RE4-RTP-TOOL");
        }

        private void linkLabel_RE4_ETM_TOOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/RE4-ETM-TOOL");
        }

        private void linkLabel_RE4_ITM_TOOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/RE4-ITM-TOOL");
        }

        private void linkLabel_JADERLINK_DATUDAS_TOOL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/JADERLINK_DATUDAS_TOOL");
        }

        private void linkLabel_DATUDAS_IDX_STANDARDIZE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            To("https://github.com/JADERLINK/DATUDAS_IDX_STANDARDIZE");
        }


        #endregion

    }
}
