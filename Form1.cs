using System.Windows.Forms;
using NAudio.Wave;

namespace Wincat
{

    public struct User
    {
        public String Path;
    }
    public partial class Form1 : Form
    {
        User u;

       
        private AudioFileReader? audioreader;
        private WaveOutEvent? OutputDevice;

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialogmenu = new OpenFileDialog();
            dialogmenu.Filter = "MP3 Files|*.mp3;*.mp4";
            if (dialogmenu.ShowDialog() == DialogResult.OK)
            {
                u.Path = dialogmenu.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(u.Path))
            {
                MessageBox.Show("No file was selected.");
                return;
            }
            else
            {
                string ext = System.IO.Path.GetExtension(u.Path).ToLower();

                if (ext == ".mp3")
                {

                    axWindowsMediaPlayer1.Visible = false;
                    axWindowsMediaPlayer1.URL = "";

                    label1.Text = System.IO.Path.GetFileName(u.Path);
                    OutputDevice?.Stop();
                    OutputDevice?.Dispose();
                    audioreader?.Dispose();

                    audioreader = new AudioFileReader(u.Path);
                    OutputDevice = new WaveOutEvent();

                    OutputDevice.Init(audioreader);
                    OutputDevice.Play();
                }

                else if (ext == ".mp4")
                {
                    OutputDevice?.Stop();

                    axWindowsMediaPlayer1.Visible = true;
                    axWindowsMediaPlayer1.URL = u.Path;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(u.Path))
            {
                MessageBox.Show("File was not selected");
                return;
            } else
            {
                string ext = System.IO.Path.GetExtension(u.Path).ToLower();

                if (ext == ".mp3")
                {
                    OutputDevice?.Stop();
                    OutputDevice?.Dispose();
                    audioreader?.Dispose();
                }
                else
                {
                    if (axWindowsMediaPlayer1 != null)
                    {
                        axWindowsMediaPlayer1.Ctlcontrols.stop();
                    }
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (audioreader != null) audioreader.Volume = trackBar1.Value / 100f;
            if (axWindowsMediaPlayer1 != null) axWindowsMediaPlayer1.settings.volume = trackBar1.Value;

        }
    }
}
