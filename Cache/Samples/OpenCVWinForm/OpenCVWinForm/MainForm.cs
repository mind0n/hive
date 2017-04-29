using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using OpenCvSharp;

namespace OpenCVWinForm
{
    public partial class MainForm : Form
    {
        private Thread _cameraThread;

        public MainForm()
        {
            InitializeComponent();
        }

        #region Camera Thread
        private void CaptureCamera()
        {
            _cameraThread = new Thread(new ThreadStart(CaptureCameraCallback));
            _cameraThread.Start();
        }

        private void CaptureCameraCallback()
        {
            const double ScaleFactor = 2.5;
            const int MinNeighbors = 1;
            CvSize MinSize = new CvSize(30, 30);

            CvCapture cap = CvCapture.FromCamera(1);
            CvHaarClassifierCascade cascade = CvHaarClassifierCascade.FromFile("haarcascade_eye.xml");
            while (true)
            {
                IplImage img = cap.QueryFrame();
                //IplImage.FromBitmap()
                //CvSeq<CvAvgComp> eyes = Cv.HaarDetectObjects(img, cascade, Cv.CreateMemStorage(), ScaleFactor, MinNeighbors, HaarDetectionType.DoCannyPruning, MinSize);
                
                //foreach (CvAvgComp eye in eyes.AsParallel())
                //{
                //    img.DrawRect(eye.Rect, CvColor.Red);

                //    if (eye.Rect.Left > pctCvWindow.Width / 2)
                //    {
                //        try
                //        {
                //            IplImage rightEyeImg1 = img.Clone();
                //            Cv.SetImageROI(rightEyeImg1, eye.Rect);
                //            IplImage rightEyeImg2 = Cv.CreateImage(eye.Rect.Size, rightEyeImg1.Depth, rightEyeImg1.NChannels);
                //            Cv.Copy(rightEyeImg1, rightEyeImg2, null);
                //            Cv.ResetImageROI(rightEyeImg1);

                            
                //            Bitmap rightEyeBm = BitmapConverter.ToBitmap(rightEyeImg2);
                //            pctRightEye.Image = rightEyeBm;
                //        }
                //        catch { }
                //    }
                //    else
                //    {
                //        try
                //        {
                //            IplImage leftEyeImg1 = img.Clone();
                //            Cv.SetImageROI(leftEyeImg1, eye.Rect);
                //            IplImage leftEyeImg2 = Cv.CreateImage(eye.Rect.Size, leftEyeImg1.Depth, leftEyeImg1.NChannels);
                //            Cv.Copy(leftEyeImg1, leftEyeImg2, null);
                //            Cv.ResetImageROI(leftEyeImg1);

                //            Bitmap leftEyeBm = BitmapConverter.ToBitmap(leftEyeImg2);
                //            pctLeftEye.Image = leftEyeBm;
                //        }catch{}
                //    }
                //}

                Bitmap bm = BitmapConverter.ToBitmap(img);
                bm.SetResolution(pctCvWindow.Width, pctCvWindow.Height);
                //pctCvWindow.Image = bm;
                pb.Image = bm;
                img = null;
                bm = null;
                Thread.Sleep(100);
            }
        }
        #endregion

        #region Form Handlers
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_cameraThread != null && _cameraThread.IsAlive)
            {
                _cameraThread.Abort();
            }
        }
        #endregion

        #region Button Handlers
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text.Equals("Start"))
            {
                CaptureCamera();
                btnStart.Text = "Stop";
            }
            else
            {
                _cameraThread.Abort();
                btnStart.Text = "Start";
            }
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "opencv-image.jpg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String imageFileName = saveFileDialog1.FileName;
                pctCvWindow.Image.Save(imageFileName);
            }
        }
        #endregion
    }
}
