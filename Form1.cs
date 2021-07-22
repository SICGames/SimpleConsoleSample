using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace ProcessConsoleTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void runProcess()
        {

            Process proc = new Process();
            proc.StartInfo.FileName = "ping";
            proc.StartInfo.Arguments = "www.youtube.com";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.EnableRaisingEvents = true;
            proc.Start();
            proc.OutputDataReceived += Proc_OutputDataReceived;
            
            proc.BeginOutputReadLine();
            
            //proc.WaitForExit();
        }
        
        private void updateConsole(string data)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                textBox1.Text += $"{data}{Environment.NewLine}";
            });

        }
        
        private void Proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                string data = e.Data;
                updateConsole(data);
            }
            else 
              updateConsole("Complete.");
        }

        private  void button1_Click(object sender, EventArgs e)
        {
            runProcess();
        }
    }
}
