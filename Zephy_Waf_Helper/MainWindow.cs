using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace Zephy_Waf_Helper
{
    public partial class MainWindow : Form
    {
        
        string filePath { get; set; }
        string fileName { get; set; }

        Color valid = new Color();
        Color invalid = new Color();
        ToolTip confidence = new ToolTip();

        public MainWindow(string path)
        {
            InitializeComponent();

            filePath = path;
            valid = Color.LightGreen;
            invalid = Color.Red;

            FlowLayoutPanel main = new FlowLayoutPanel();
            main.FlowDirection = FlowDirection.TopDown;
            main.AutoSize = true;
            this.Controls.Add(main);

            Label createPath = new Label();
            createPath.AutoSize = true;
            createPath.Text = "Creation Path:";
            main.Controls.Add(createPath);

            TextBox pathBox = new TextBox();
            pathBox.Name = "pathBox";
            pathBox.Size = new Size(this.ClientRectangle.Width - 15, pathBox.Size.Height);
            pathBox.Text = path;
            pathBox.Enabled = false;
            pathBox.TextChanged += PathBox_TextChanged;
            main.Controls.Add(pathBox);

            Label pathLabel = new Label();
            pathLabel.AutoSize = true;
            pathLabel.Text = "Name of File to Create:";

            main.Controls.Add(pathLabel);

            TextBox fileName = new TextBox();
            fileName.Size = new Size(this.ClientRectangle.Width - 15, pathBox.Size.Height);
            fileName.Text = "Insert File Name";
            fileName.TextChanged += FileName_TextChanged;
            main.Controls.Add(fileName);

            Button createFiles = new Button();
            createFiles.Name = "button";
            createFiles.Text = "create .h/cpp";
            createFiles.Click += CreateFiles_Click;
            main.Controls.Add(createFiles);

            TestValid();

        }

        private bool TestValid()
        {
            Button colory = this.Controls[0].Controls["button"] as Button;
            TextBox textboxy = this.Controls[0].Controls["pathBox"] as TextBox;


            if (!Path.IsPathRooted(filePath))
            {
                colory.BackColor = invalid;
                string desc = "Path is not rooted.";
                confidence.RemoveAll();
                confidence.SetToolTip(colory, desc);
                textboxy.Enabled = true;
                return false;
            }
            else
                confidence.RemoveAll();
            if (fileName == null)
            {
                colory.BackColor = invalid;
                string desc = "You must specify a file name.";
                confidence.RemoveAll();
                confidence.SetToolTip(colory, desc);
                return false;
            }
            
            //
            if (!filePath.EndsWith(@"\"))
                filePath += @"\\";

            string localPath = filePath + fileName;

            string cpp = localPath += @".cpp";
            string head = localPath += @".h";
            if (File.Exists(cpp) || File.Exists(head))
            {
                colory.BackColor = invalid;
                string desc = "Header or implementation at this path already exist.";
                confidence.SetToolTip(colory, desc);
                return false;
            }

            colory.BackColor = valid;
            confidence.RemoveAll();
            return true;
        }


        private void CreateFiles_Click(object sender, EventArgs e)
        {
            if (!TestValid())
                return;

            string cpp = filePath + fileName + ".cpp";
            string head = filePath + fileName + ".h";

            FileStream localStream = null;
            try {
                bool herp = true;
                localStream = File.Create(cpp);
                localStream.Close();
                localStream = File.Create(head);
                localStream.Close();
                
            }
            catch
            {
                MessageBox.Show("failed for some reason");
                
            }
            TestValid();
        }


        private void PathBox_TextChanged(object sender, EventArgs e)
        {
            TextBox sendy = sender as TextBox;
            filePath = sendy.Text;
            TestValid();
        }

        private void FileName_TextChanged(object sender, EventArgs e)
        {
            TextBox sendy = sender as TextBox;
            fileName = sendy.Text;
            TestValid();
        }



    }
}
