using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;
using SharpGL;
using System.Runtime.InteropServices;

namespace CelShadingSample
{
    public class TrefoilKnot
    {
        Vertex EvaluateTrefoil(float s, float t)
        {
            float TwoPi = (float)Math.PI * 2;
            float a = 0.5f;
            float b = 0.3f;
            float c = 0.5f;
            float d = 0.1f;
            float u = (1 - s) * 2 * TwoPi;
            float v = t * TwoPi;
            float r = (float)(a + b * Math.Cos(1.5f * u));
            float x = (float)(r * Math.Cos(u));
            float y = (float)(r * Math.Sin(u));
            float z = (float)(c * Math.Sin(1.5f * u));

            Vertex dv;
            dv.X = (float)(-1.5f * b * Math.Sin(1.5f * u) * Math.Cos(u) -
                    (a + b * Math.Cos(1.5f * u)) * Math.Sin(u));
            dv.Y = (float)(-1.5f * b * Math.Sin(1.5f * u) * Math.Sin(u) +
                    (a + b * Math.Cos(1.5f * u)) * Math.Cos(u));
            dv.Z = (float)(1.5f * c * Math.Cos(1.5f * u));

            Vertex q = new Vertex(dv);
            q.Normalize();
            Vertex qvn = new Vertex(q.Y, -q.X, 0);
            qvn.Normalize();
            Vertex ww = q.VectorProduct(qvn);

            Vertex range;
            range.X = (float)(x + d * (qvn.X * Math.Cos(v) + ww.X * Math.Sin(v)));
            range.Y = (float)(y + d * (qvn.Y * Math.Cos(v) + ww.Y * Math.Sin(v)));
            range.Z = (float)(z + d * ww.Z * Math.Sin(v));
            return range;
        }

        public uint CreateVertexNormalBuffer(OpenGL gl)
        {
            Vertex[] verts = new Vertex[VertexCount * 2];
            int count = 0;

            float ds = 1.0f / Slices;
            float dt = 1.0f / Stacks;

            // The upper bounds in these loops are tweaked to reduce the
            // chance of precision error causing an incorrect # of iterations.

            for (float s = 0; s < 1 - ds / 2; s += ds)
            {
                for (float t = 0; t < 1 - dt / 2; t += dt)
                {
                    const float E = 0.01f;
                    Vertex p = EvaluateTrefoil(s, t);
                    Vertex u = EvaluateTrefoil(s + E, t) - p;
                    Vertex v = EvaluateTrefoil(s, t + E) - p;
                    Vertex n = u.VectorProduct(v);
                    n.Normalize();
                    verts[count++] = p;
                    verts[count++] = n;
                }
            }
            
            //  Pin the data.
            GCHandle vertsHandle = GCHandle.Alloc(verts, GCHandleType.Pinned);
            IntPtr vertsPtr = vertsHandle.AddrOfPinnedObject();
            var size = Marshal.SizeOf(typeof(Vertex)) * VertexCount;

            uint[] buffers = new uint[1];
            gl.GenBuffers(1, buffers);
            uint handle = buffers[0];
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, handle);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, size, vertsPtr, OpenGL.GL_STATIC_DRAW);

            //  Free the data.
            vertsHandle.Free();

            return handle;
        }

        public uint CreateIndexBuffer(OpenGL gl)
        {
            ushort[] inds = new ushort[IndexCount];
            int count = 0;

            ushort n = 0;
            for (ushort i = 0; i < Slices; i++)
            {
                for (ushort j = 0; j < Stacks; j++)
                {
                    inds[count++] = (ushort)(n + j);
                    inds[count++] = (ushort)(n + (j + 1) % Stacks);
                    inds[count++] = (ushort)((n + j + Stacks) % VertexCount);

                    inds[count++] = (ushort)((n + j + Stacks) % VertexCount);
                    inds[count++] = (ushort)((n + (j + 1) % Stacks) % VertexCount);
                    inds[count++] = (ushort)((n + (j + 1) % Stacks + Stacks) % VertexCount);
                }

                n += (ushort)Stacks;
            }
            
            //  Pin the data.
            GCHandle indsHandle = GCHandle.Alloc(inds, GCHandleType.Pinned);
            IntPtr indsPtr = indsHandle.AddrOfPinnedObject();
            var size = Marshal.SizeOf(typeof(ushort)) * VertexCount;

            uint[] buffers = new uint[1];
            gl.GenBuffers(1, buffers);
            uint handle = buffers[0];
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, handle);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, size, indsPtr, OpenGL.GL_STATIC_DRAW);

            //  Free the data.
            indsHandle.Free();

            return handle;
        }

        public int Slices
        {
            get { return 128; }
        }
        public int Stacks
        {
            get { return 32;}
        }
        public int VertexCount
        {
            get {return Slices * Stacks;}
        }
        public int IndexCount
        {
            get { return VertexCount * 6; }
        }
    }
}
