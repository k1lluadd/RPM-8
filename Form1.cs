using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace NotepadLab8
{
    public partial class Form1 : Form
    {
        private string currentFile = null;

        public Form1()
        {
            InitializeComponent();
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            lblStatus.Text = $"Символов: {rtb.TextLength}  |  Файл: {(currentFile ?? "(новый)")}";
        }

        private void rtb_TextChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void miNew_Click(object s, EventArgs e) { rtb.Clear(); currentFile = null; UpdateStatus(); }

        private void miOpen_Click(object s, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                rtb.Text = File.ReadAllText(ofd.FileName);
                currentFile = ofd.FileName; UpdateStatus();
            }
        }

        private void miSave_Click(object s, EventArgs e)
        {
            if (currentFile == null) { miSaveAs_Click(s, e); return; }
            File.WriteAllText(currentFile, rtb.Text);
        }

        private void miSaveAs_Click(object s, EventArgs e)
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                currentFile = sfd.FileName;
                File.WriteAllText(currentFile, rtb.Text);
                UpdateStatus();
            }
        }

        private void miExit_Click(object s, EventArgs e) => Close();
        private void miCut_Click(object s, EventArgs e) => rtb.Cut();
        private void miCopy_Click(object s, EventArgs e) => rtb.Copy();
        private void miPaste_Click(object s, EventArgs e) => rtb.Paste();
        private void miSelectAll_Click(object s, EventArgs e) => rtb.SelectAll();

        private void miFont_Click(object s, EventArgs e)
        {
            if (fd.ShowDialog() == DialogResult.OK) rtb.Font = fd.Font;
        }

        private void miWordWrap_Click(object s, EventArgs e)
        {
            rtb.WordWrap = miWordWrap.Checked;
        }

        private void miAbout_Click(object s, EventArgs e)
            => MessageBox.Show("Блокнот v1.0\nВариант 5\nГаврилов А.В. гр. ИС24", "О программе");
    }
}