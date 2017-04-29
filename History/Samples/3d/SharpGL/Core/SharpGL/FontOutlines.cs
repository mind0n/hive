using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL
{
    /// <summary>
    /// A FontBitmap entry contains the details of a font face.
    /// </summary>
    internal class FontBitmapEntry
    {
        public IntPtr HDC
        {
            get;
            set;
        }

        public IntPtr HRC
        {
            get;
            set;
        }

        public string FaceName
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public uint ListBase
        {
            get;
            set;
        }

        public uint ListCount
        {
            get;
            set;
        }
    }

    /// <summary>
    /// This class wraps the functionality of the wglUseFontBitmaps function to
    /// allow straightforward rendering of text.
    /// </summary>
    public class FontBitmaps
    {
        private FontBitmapEntry CreateFontBitmapEntry(OpenGL gl, string faceName, int height)
        {
            //  Make the OpenGL instance current.
            gl.MakeCurrent();

            //  Create the font based on the face name.
            IntPtr hFont = Win32.CreateFont(height, 0, 0, 0, Win32.FW_DONTCARE, 0, 0, 0, Win32.DEFAULT_CHARSET, 
                Win32.OUT_OUTLINE_PRECIS, Win32.CLIP_DEFAULT_PRECIS, Win32.CLEARTYPE_QUALITY, Win32.VARIABLE_PITCH, faceName);
            
            //  Select the font handle.
            IntPtr hOldObject = Win32.SelectObject(gl.RenderContextProvider.DeviceContextHandle, hFont);

            //  Create the font bitmaps.
            bool result = Win32.wglUseFontBitmaps(gl.RenderContextProvider.DeviceContextHandle, 0, 255, nextListBase);

            //  Reselect the old font.
            Win32.SelectObject(gl.RenderContextProvider.DeviceContextHandle, hOldObject);

            //  Free the font.
            Win32.DeleteObject(hFont);

            //  Create the font bitmap entry.
            FontBitmapEntry fbe = new FontBitmapEntry()
            {
                HDC = gl.RenderContextProvider.DeviceContextHandle,
                HRC = gl.RenderContextProvider.RenderContextHandle,
                FaceName = faceName,
                Height = height,
                ListBase = nextListBase,
                ListCount = 255
            };

            //  Add the font bitmap entry to the internal list.
            fontBitmapEntries.Add(fbe);
            
            //  Shift the list base.
            nextListBase += 1000;

            return fbe;
        }

        /// <summary>
        /// Draws the text.
        /// </summary>
        /// <param name="gl">The gl.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        /// <param name="faceName">Name of the face.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <param name="text">The text.</param>
        public void DrawText(OpenGL gl, int x, int y, float r, float g, float b, string faceName, float fontSize, string text)
        {
            //  Get the font size in pixels.
            int fontHeight = (int)(fontSize * (16.0f / 12.0f));

            //  Do we have a font bitmap entry for this OpenGL instance and face name?
            var result = from fbe in fontBitmapEntries
                         where fbe.HDC == gl.RenderContextProvider.DeviceContextHandle
                         && fbe.HRC == gl.RenderContextProvider.RenderContextHandle
                         && string.Compare(fbe.FaceName, faceName, true) == 0
                         && fbe.Height == fontHeight
                         select fbe;

            //  Get the FBE or null.
            FontBitmapEntry fontBitmapEntry = result == null || result.Count() == 0 ? null : result.First();

            //  If we don't have the FBE, we must create it.
            if (fontBitmapEntry == null)
                fontBitmapEntry = CreateFontBitmapEntry(gl, faceName, fontHeight);

            double width = gl.RenderContextProvider.Width;
            double height = gl.RenderContextProvider.Height;

            double aspect_ratio = width / height;
            
            //  Create the appropriate projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.PushMatrix();
            gl.LoadIdentity();
            
            int[] viewport = new int[4];
            gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);
            //gl.Ortho2D(viewport[0], viewport[2], viewport[1], viewport[3]);
            gl.Ortho(0, width, 0, height, -1, 1);

            //  Create the appropriate modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PushMatrix();
            gl.LoadIdentity();
            //gl.Translate(0, 0, -5);
            //gl.RasterPos(x, y);
            gl.Color(r, g, b);
            gl.RasterPos(x, y);

            gl.PushAttrib(OpenGL.GL_LIST_BIT | OpenGL.GL_CURRENT_BIT |
                OpenGL.GL_ENABLE_BIT | OpenGL.GL_TRANSFORM_BIT);
            gl.Color(r, g, b);
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Disable(OpenGL.GL_TEXTURE_2D);
            gl.Disable(OpenGL.GL_DEPTH_TEST);
            gl.RasterPos(x, y);
           /** gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA); */

            //  Set the list base.
            gl.ListBase(fontBitmapEntry.ListBase);

            //  Create an array of lists for the glyphs.
            List<byte> lists = new List<byte>();
            foreach(char c in text)
                lists.Add((byte)c);            

            //  Call the lists for the string.
            gl.CallLists(lists.Count, lists.ToArray());
            gl.Flush();
            
            //  Reset the list bit.
            gl.PopAttrib();

            //  Pop the modelview.
            gl.PopMatrix();

            //  back to the projection and pop it, then back to the model view.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.PopMatrix();
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private List<FontBitmapEntry> fontBitmapEntries = new List<FontBitmapEntry>();

        private uint nextListBase = 1000;
    }
}
