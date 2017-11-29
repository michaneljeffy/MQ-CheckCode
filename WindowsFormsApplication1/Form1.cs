using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.Net;
using System.IO;
using Gma.QrCodeNet;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public delegate void MyInvoke();
        public Form1()
        {
            InitializeComponent();
        }
        int i,j,k = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.timer2.Enabled = true;

            this.timer2.Enabled = true;
            this.label1.Text = DateTime.Now.ToString();

        }

        public DateTime startTime;

        private void Form1_Load(object sender, EventArgs e)
        {
            //var timer1 = new System.Timers.Timer(1000) ;
            //timer1.Elapsed += Timer1_Elapsed;
            //timer1.Start();

            //var timer2 = new System.Timers.Timer(3000);
            //timer2.Elapsed += Timer2_Elapsed;
            //timer2.Start();

            //var timer3 = new System.Timers.Timer(9000);
            //timer3.Elapsed += Timer3_Elapsed;
            //timer3.Start();

            //var timer = new System.Timers.Timer();
            //timer.Interval = 1000;
            //timer.Elapsed += Timer_Elapsed;
            //timer.Start();
            //Label  labletest = new Label();
            //labletest.Top = 100;
            //labletest.Left = 100;
            //labletest.Width = 100;
            //labletest.Height = 100;
            //labletest.Text = "Test";
            //labletest.Visible = true;
            //labletest.Parent = this;
            // string url = "http://static.in66.co/images/20170621/5949c5f7d670d.jpg";

            var wc = new WebClient();
            wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
            wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
            wc.DownloadFileAsync(new Uri("http://inimg01.jiuyan.info//in/2017/08/23/505C5AA9-20E8-18A8-BCE2-5F2FDC45FAF3.jpg?imageMogr2/thumbnail/875x"),"1.jpg");
            startTime = DateTime.Now;

            
            Bitmap QRCode = GenerateQRCode("http://www.baidu.com", System.Drawing.Color.Black , System.Drawing.Color.White );

            //pictureBox1.Image =new Bitmap(QRCode, new Size (150,150));
        }

        private Bitmap GenerateQRCode(string text, System.Drawing.Color DarkColor, System.Drawing.Color LightColor)
        {
            Gma.QrCodeNet.Encoding.QrEncoder Encoder = new Gma.QrCodeNet.Encoding.QrEncoder(Gma.QrCodeNet.Encoding.ErrorCorrectionLevel.H);
            Gma.QrCodeNet.Encoding.QrCode Code = Encoder.Encode(text);
            Bitmap TempBMP = new Bitmap(Code.Matrix.Width, Code.Matrix.Height);
            for (int X = 0; X <= Code.Matrix.Width - 1; X++)
            {
                for (int Y = 0; Y <= Code.Matrix.Height - 1; Y++)
                {
                    if (Code.Matrix.InternalArray[X, Y])
                        TempBMP.SetPixel(X, Y, DarkColor);
                    else
                        TempBMP.SetPixel(X, Y, LightColor);
                }
            }
            return TempBMP;
        }

        private void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.button1.Text = e.Error.InnerException.Message;
            }
            
        }

        private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var totalLength = e.TotalBytesToReceive;
            var receivedLength = e.BytesReceived;

            this.button1.Text = "下载进度" +string .Format("{0}/{1}", receivedLength , totalLength);

            float progress = receivedLength / totalLength;
            this.button2.Text = "下载进度" + progress ;

            this.button3.Text = "开始下载时间" + startTime.ToString("HHmmss:fff");

            this.button4.Text = "下载时间" + (DateTime.Now - startTime).TotalSeconds;
        }

        public int count = 0;
        public  Image  GetImage(string url)
        {
            if (count>3)
            {
                count = 0;
                return null;
            }

            WebClient wc = new WebClient();
            Image image = null;
            try
            {          
                Stream stream = wc.OpenRead(url);
                Stream copyedStream= new MemoryStream();
                //image = Image.FromStream(stream);
                CopyStream(stream, copyedStream);
                //save to temp folder
               // image.Save("./1.jpg");

                //FileStream fs = File.Open("./1.jpg",FileMode.Open);
               // long bytesLength = fs.Length;
                long dataLength;
                long.TryParse(wc.ResponseHeaders.Get("Content-Length"), out dataLength);
                long bytesLength = copyedStream.Length;
                if (dataLength == bytesLength)
               {
                    image = Bitmap.FromStream(copyedStream);
                  //  image = Bitmap.FromStream(stream);
                   // fs.Close();             
                //   File.Delete("./1.jpg");//删除缓存文件
                   // return image;                
              }
             else
              {
               //     fs.Close();
                  //  File.Delete("./1.jpg");//删除缓存文件
                   // count++;
                  //  GetImage(url);//重新下载
                }          
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return image;
        }
        private static byte[] GetStreamLength(Stream source)
        {
          //  long originalPosition = source.Position;
            //source.Position = 0;

            try
            {
                byte[] readBuffer = new byte[4096];
                int totalBytesRead = 0;
                int bytesRead;
                while ((bytesRead = source.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;
                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = source.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                //source.Position = originalPosition;
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public  void writeImageToDisk(byte[] img, String fileName)
        {
            try
            {
                FileStream fs = new FileStream(fileName ,FileMode.Create);
                fs.Write(img,0,4096);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.button1.Invoke(new MyInvoke(() =>
            {
                this.button1.Text = DateTime.Now.ToString();
            }));
        }

        private void Timer3_Elapsed(object sender, ElapsedEventArgs e)
        {
            k++;
           this.Invoke(new MyInvoke(() => this.label3.Text = k.ToString()));
            //this.label3.Text = k.ToString();
        }

        private void Timer2_Elapsed(object sender, ElapsedEventArgs e)
        {
            j++;
            // this.label2.Text = j.ToString();
            Thread.Sleep(3000);
            this.Invoke (new MyInvoke(() => this.label2.Text = j.ToString()));
        }

        private void Timer1_Elapsed(object sender, ElapsedEventArgs e)
        {

            i++;
           this.Invoke(new MyInvoke(() => this.label1.Text = i.ToString()));
       

        }

        private void add(int i)
        {
            i++;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string code =  CreateCheckCodeImage.GenerateCheckCode();
            MemoryStream ms = CreateCheckCodeImage.Production(code);
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.timer3.Enabled = true;
            this.label2.Text = DateTime.Now.ToString();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            
            this.label3.Text = DateTime.Now.ToString();
        }

         
    }
}
