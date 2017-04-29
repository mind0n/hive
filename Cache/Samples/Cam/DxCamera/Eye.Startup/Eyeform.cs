using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DirectX.Capture;
using System.Threading;
using Eyes;
using OpenCvSharp;

namespace Eye.Startup
{
    public partial class Eyeform : Form
    {
        protected PictureBox pb;
        protected Filters filter = new Filters();
        protected Capture cap;
        protected bool isStopping;
        public Eyeform()
        {
            InitializeComponent();
            Load += Eyeform_Load;
            FormClosing += Eyeform_FormClosing;
        }

        void Eyeform_FormClosing(object sender, FormClosingEventArgs e)
        {
            isStopping = true;
        }

        void Eyeform_Load(object sender, EventArgs e)
        {
            cap = new Capture(filter.VideoInputDevices[0], filter.AudioInputDevices[0]);
            cap.PreviewWindow = pnpreview;
            cap.FrameRate = 60;
            cap.FrameCaptureComplete += cap_FrameCaptureComplete;
            KeyPreview = true;
        }


        //void bb_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("ok");
        //}

        private void bCapture_Click(object sender, EventArgs e)
        {
            //cap.Filename = "c:\\temp.txt";
            var th = new Thread(delegate()
            {
                while (!isStopping)
                {
                    if (this.IsDisposed)
                    {
                        break;
                    }
                    try
                    {
                        Invoke((MethodInvoker) delegate
                        {
                            cap.CaptureFrame();
                        });
                    }
                    catch
                    {
                    }
                    Thread.Sleep(200);
                }
            });
            th.Start();
        }
        private bool isEyeEnabled;
        private void Eyeform_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                isEyeEnabled = isEyeEnabled ? false : true;
                if (isEyeEnabled)
                {
                    p = Point.Empty;
                }
            }
        }

        private void Eyeform_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Up)
            //{
            //    isEyeEnabled = true;
            //}
        }

        private Point p = Point.Empty;
        void cap_FrameCaptureComplete(PictureBox Frame)
        {
            if (isEyeEnabled)
            {
                var rcs = Recognize(Frame, pn);
                var e = rcs.Pos(30);
                if (p != Point.Empty)
                {
                    var d = new Point(e.X - p.X, e.Y - p.Y);
                    var c = Cursor.Position;
                    Cursor.Position = new Point(c.X + d.X, c.Y + d.Y);
                }
                else
                {
                    p = e;
                }
            }
            //if (pn.Controls.Count < 1)
            //{
            //    pb = Frame;
            //    pn.Controls.Add(Frame);
            //    Frame.Dock = DockStyle.Fill;
            //}
            //else
            //{
            //    pb.Image = Frame.Image;
            //}
        }
        EyeRects Recognize(PictureBox bb, Panel container)
        {
            const double ScaleFactor = 2.5;
            const int MinNeighbors = 1;
            CvSize MinSize = new CvSize(30, 30);

            //CvCapture cap = CvCapture.FromCamera(1);
            CvHaarClassifierCascade cascade = CvHaarClassifierCascade.FromFile("haarcascade_eye.xml");
            //IplImage img = cap.QueryFrame();
            IplImage img = IplImage.FromBitmap(new Bitmap(bb.Image));
            CvSeq<CvAvgComp> eyes = Cv.HaarDetectObjects(img, cascade, Cv.CreateMemStorage(), ScaleFactor, MinNeighbors, HaarDetectionType.DoCannyPruning, MinSize);
            img.DrawRect(new CvRect(30, 30, bb.Image.Width - 30, bb.Image.Height - 60), CvColor.Yellow);
            var rcs = new EyeRects();
            foreach (CvAvgComp eye in eyes.AsParallel())
            {
                rcs.AddRect(eye.Rect);
                img.DrawRect(eye.Rect, CvColor.Yellow);

                if (eye.Rect.Left > pn.Width / 2)
                {
                    try
                    {
                        IplImage rightEyeImg1 = img.Clone();
                        Cv.SetImageROI(rightEyeImg1, eye.Rect);
                        IplImage rightEyeImg2 = Cv.CreateImage(eye.Rect.Size, rightEyeImg1.Depth, rightEyeImg1.NChannels);
                        Cv.Copy(rightEyeImg1, rightEyeImg2, null);
                        Cv.ResetImageROI(rightEyeImg1);

                        //Bitmap rightEyeBm = BitmapConverter.ToBitmap(rightEyeImg2);
                        //spMain.Panel2.Image = rightEyeBm;
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        IplImage leftEyeImg1 = img.Clone();
                        Cv.SetImageROI(leftEyeImg1, eye.Rect);
                        IplImage leftEyeImg2 = Cv.CreateImage(eye.Rect.Size, leftEyeImg1.Depth, leftEyeImg1.NChannels);
                        Cv.Copy(leftEyeImg1, leftEyeImg2, null);
                        Cv.ResetImageROI(leftEyeImg1);

                        //Bitmap leftEyeBm = BitmapConverter.ToBitmap(leftEyeImg2);
                        //pctLeftEye.Image = leftEyeBm;
                    }
                    catch { }
                }
            }
            Bitmap bm = BitmapConverter.ToBitmap(img);

            //bm.SetResolution(1500, 1500);
            //pctCvWindow.Image = bm;
            //PictureBox pb = new PictureBox();
            //pb.Image = bm;
            //pb.Image = bm;
            bb.Image = bm;

            //spMain.Panel2.Controls.Clear();
            if (pn.Controls.Count < 1)
            {
                pn.Controls.Add(bb);
                //bb.Click += bb_Click;
            }
            bb.Dock = DockStyle.Fill;
            //pb.Image = bm;
            img = null;
            bm = null;
            return rcs;
        }

    }
}
