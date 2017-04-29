using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace EdgeDetect
{
    class EyeDetect
    {
        public EyeDetect()
        {
            CvColor[] colors = new CvColor[]{
                new CvColor(0,0,255),
                new CvColor(0,128,255),
                new CvColor(0,255,255),
                new CvColor(0,255,0),
                new CvColor(255,128,0),
                new CvColor(255,255,0),
                new CvColor(255,0,0),
                new CvColor(255,0,255),
            };

            const double Scale = 1.25;
            const double ScaleFactor = 2.5;
            const int MinNeighbors = 2;

            using (CvCapture cap = CvCapture.FromCamera(1))
            using (CvWindow w = new CvWindow("Eye Tracker"))
            {
                while (CvWindow.WaitKey(10) < 0)
                {
                    using (IplImage img = cap.QueryFrame())
                    using (IplImage smallImg = new IplImage(new CvSize(Cv.Round(img.Width / Scale), Cv.Round(img.Height / Scale)), BitDepth.U8, 1))
                    {

                        using (IplImage gray = new IplImage(img.Size, BitDepth.U8, 1))
                        {
                            Cv.CvtColor(img, gray, ColorConversion.BgrToGray);
                            Cv.Resize(gray, smallImg, Interpolation.Linear);
                            Cv.EqualizeHist(smallImg, smallImg);
                        }

                        using (CvHaarClassifierCascade cascade = CvHaarClassifierCascade.FromFile("C:\\Program Files\\OpenCV\\data\\haarcascades\\haarcascade_eye.xml"))
                        using (CvMemStorage storage = new CvMemStorage())
                        {
                            storage.Clear();

                            Stopwatch watch = Stopwatch.StartNew();
                            CvSeq<CvAvgComp> eyes = Cv.HaarDetectObjects(smallImg, cascade, storage, ScaleFactor, MinNeighbors, 0, new CvSize(30, 30));
                            watch.Stop();
                            //Console.WriteLine("detection time = {0}msn", watch.ElapsedMilliseconds);

                            for (int i = 0; i < eyes.Total; i++)
                            {
                                CvRect r = eyes[i].Value.Rect;
                                CvPoint center = new CvPoint
                                {
                                    X = Cv.Round((r.X + r.Width * 0.5) * Scale),
                                    Y = Cv.Round((r.Y + r.Height * 0.5) * Scale)
                                };
                                int radius = Cv.Round((r.Width + r.Height) * 0.25 * Scale);
                                img.Circle(center, radius, colors[i % 8], 3, LineType.AntiAlias, 0);
                            }
                        }

                        w.Image = img;
                    }
                }
            }
        }
    }
}