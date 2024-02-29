using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace AutoUpdate
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
        }

        /// <summary>
        /// Var
        /// </summary>
        string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(4);
        string exeName = AppDomain.CurrentDomain.FriendlyName;
        string exePath = Assembly.GetEntryAssembly().Location;

        /// <summary>
        /// Load
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            /// <summary>
            /// Current
            /// </summary>
            lblcurrent.Text = "Current version: " + currentVersion;

            /// <summary>
            /// Global
            /// </summary>
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        }



        /// <summary>
        /// Method command console
        /// </summary>
        private void Cmd(string line)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c {line}",
                WindowStyle = ProcessWindowStyle.Hidden,
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (WebClient webClient = new WebClient())
            {
                /// <summary>
                /// Internet check
                /// </summary>
                if (Internet.OK())
                {
                    /// <summary>
                    /// Check verison
                    /// </summary>
                    string readVersion = webClient.DownloadString("URL"); // NEW VERSION APP 
                    if (currentVersion == readVersion) 
                    {
                        MessageBox.Show("Last version");
                    }
                    else
                    {
                        DialogResult = MessageBox.Show("Old version!\nUpdate version?", "", MessageBoxButtons.YesNo);
                        if (DialogResult == DialogResult.Yes)
                        {
                            webClient.DownloadFile("URL", "version2.exe"); // DOWNLOAD 
                            Cmd($"taskkill /f /im \"{exeName}\" && timeout /t 1 && del \"{exePath}\" && ren version2.exe \"{exeName}\" && \"{exePath}\"");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Connecting lost");
                }
            }
        }
    }
}
