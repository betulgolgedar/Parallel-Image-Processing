using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace WindowsFormsApp1
{
    public class ImageEventArgs : EventArgs
    {
        public Bitmap bmap { get; set; }
    }
    internal class ImageManupilation
    {
      
        public void Manipulate(Bitmap bmap)
        {

            Color theColor = new Color();

            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    
                    theColor = bmap.GetPixel(i, j);
                    int avg = (theColor.R + theColor.G + theColor.B) / 3;
                    Color newColor = Color.FromArgb(avg, avg, avg);
                    
                    bmap.SetPixel(i, j, newColor);
                    
                }
            }
           
            OnImageFinished(bmap);
        }

        public void Flip(Bitmap bmap){

            Bitmap tp = new Bitmap(bmap.Width, bmap.Height);
            
            Color pix;
            
            
            for (int i = 0; i < bmap.Width; i++) {
            

                for (int j = 0; j < bmap.Height; j++)
                {
                    pix = bmap.GetPixel(i, j);
                    tp.SetPixel(i,bmap.Height-1-j, pix);   
                    
                }            
            }
            OnImageFinished(tp);
        }
        
       

        public event EventHandler<ImageEventArgs> ImageFinished;

        protected virtual void OnImageFinished(Bitmap bmap)
        {
            ImageFinished?.Invoke(this, new ImageEventArgs() { bmap = bmap });
        }

        public Bitmap[,] MakeTiles(Bitmap bmap) {
            Size tilesize=new Size(bmap.Width/4,bmap.Height/2);
            Bitmap[,] bmaparray = new Bitmap[4, 2];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Rectangle movingTileFrames = new Rectangle(
                        i * tilesize.Width, j * tilesize.Height, tilesize.Width, tilesize.Height);

                    bmaparray[i,j]=new Bitmap(tilesize.Width, tilesize.Height);

                    using(Graphics canvas = Graphics.FromImage(bmaparray[i, j]))
                    {
                        canvas.DrawImage(bmap, new Rectangle(0,0,tilesize.Width,tilesize.Height),
                            movingTileFrames,GraphicsUnit.Pixel);
                    }
                }
            }

            return bmaparray;
        }
        public Bitmap[,] ParallelImageProcess(Bitmap[,] bmap) {

            Parallel.For(0, 4, x =>
            {
                for (int y = 0; y < 2; y++)
                {
                    int width = bmap[x, y].Width;
                    int height = bmap[x, y].Height;

                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            Color oldPixel = bmap[x, y].GetPixel(i, j);
                            int avg=(oldPixel.R+oldPixel.G+oldPixel.B)/3;
                            Color newPixel=Color.FromArgb(avg, avg, avg);
                            bmap[x, y].SetPixel(i, j, newPixel);
                        }
                    }
                }
            });
            return bmap;
        }
        public Bitmap[,] TransformedImg(Bitmap[,] bmap)
        {
            
            Color pix;
            
            Bitmap[,] bmaparray = new Bitmap[4, 2];

            Parallel.For(0, 4, x =>
            {
                for (int y = 0; y < 2; y++)
                {       

                    int width = bmap[x, y].Width;
                    int height = bmap[x, y].Height;

                    bmaparray[x, y] = new Bitmap(width, height);

                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            pix = bmap[x,y].GetPixel(i, j);
                            bmaparray[x,y].SetPixel(i, height-1-j, pix);
                        }
                    }
                }
            });

            return bmaparray;
        }

       
    }
}
