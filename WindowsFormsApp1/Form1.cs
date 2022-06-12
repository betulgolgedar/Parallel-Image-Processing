using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Bitmap newFile,cover;
        Bitmap[,] modimgArray;
        Bitmap[,] imgArray;
        Bitmap[,] trimgArray;
        ImageManupilation modifyRGB=new ImageManupilation();
        FileOperations getFile=new FileOperations();
        
        public Form1()
        {
            InitializeComponent();
            modifyRGB.ImageFinished += OnImageFinished;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void OnImageFinished(object sender, ImageEventArgs e)
        {
            DisplayImage(e.bmap, 2);
            //label1.Text = "Event Handler teslim alındı.";
        }
 
        private void ManipulateImg_Click(object sender, EventArgs e)
        {
            //Thread t1 = new Thread(()=>modifyRGB.Manipulate(newFile));
            //t1.Start();
            var timer = new Stopwatch();
            timer.Start();
           
            modifyRGB.Manipulate(newFile);

            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            string foo = "Geçen Süre:" + timeTaken.ToString(@"m\:ss\.fff");
            label1.Text = foo;
            

        }
        private void Flip_Click(object sender, EventArgs e)
        {
            var timer = new Stopwatch();
            timer.Start();

            modifyRGB.Flip(newFile);

            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            string foo = "Geçen Süre:" + timeTaken.ToString(@"m\:ss\.fff");
            label1.Text = foo;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void DisplayImage(Bitmap b, int window)
        {
            if(window == 1)
            {
                pictureBox1.Image = b;
            }
            else if(window == 2)
            {
                pictureBox2.Image = b;
            }
            else
            {
                pictureBox1.Image = b;
                pictureBox2.Image=b;
            }
            ManipulateImg.Enabled = true;
        }
        //Open Image File and Return BMP Ret: Bitmap Class->Form1
        private void OpenImg_Click(object sender, EventArgs e)
        {
            
            newFile=getFile.OpenFile();
            DisplayImage(newFile, 3);
            label4.Text="Genişlik: "+newFile.Width;
            label5.Text="Yükseklik: "+newFile.Height;
            label6.Text = "Piksel Sayısı:" + newFile.Width * newFile.Height;

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void Tiles_Click(object sender, EventArgs e)
        {
            imgArray = modifyRGB.MakeTiles(newFile);

            pictureBox3.Image = imgArray[0, 0];
            pictureBox4.Image = imgArray[0, 1];
            pictureBox5.Image = imgArray[1, 0];
            pictureBox6.Image = imgArray[1, 1];
            pictureBox7.Image = imgArray[2, 0];
            pictureBox8.Image = imgArray[2, 1];
            pictureBox9.Image = imgArray[3, 0];
            pictureBox10.Image = imgArray[3, 1];

        }

        private void Parallel_Click(object sender, EventArgs e)
        {
            var timer = new Stopwatch();
            timer.Start();
            modimgArray = modifyRGB.ParallelImageProcess(imgArray);
            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            string foo = "Geçen Süre:" + timeTaken.ToString(@"m\:ss\.fff");
            label2.Text = foo;

            pictureBox3.Image = modimgArray[0, 0];
            pictureBox4.Image = modimgArray[0, 1];
            pictureBox5.Image = modimgArray[1, 0];
            pictureBox6.Image = modimgArray[1, 1];
            pictureBox7.Image = modimgArray[2, 0];
            pictureBox8.Image = modimgArray[2, 1];
            pictureBox9.Image = modimgArray[3, 0];
            pictureBox10.Image = modimgArray[3, 1];
        }

        private void TransformedImg_Click(object sender, EventArgs e)
        {
            var timer = new Stopwatch();
            timer.Start();
            trimgArray = modifyRGB.TransformedImg(imgArray);
            TimeSpan timeTaken = timer.Elapsed;
            string foo = "Geçen Süre:" + timeTaken.ToString(@"m\:ss\.fff");
            label3.Text = foo;

            pictureBox3.Image = trimgArray[0, 1];
            pictureBox4.Image = trimgArray[0, 0];
            pictureBox5.Image = trimgArray[1, 1];
            pictureBox6.Image = trimgArray[1, 0];
            pictureBox7.Image = trimgArray[2, 1];
            pictureBox8.Image = trimgArray[2, 0];
            pictureBox9.Image = trimgArray[3, 1];
            pictureBox10.Image = trimgArray[3, 0];
        }

       
    }
}
