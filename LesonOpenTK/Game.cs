using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesonOpenTK
{
    public class Game : GameWindow
    {
        float[] vertices =
        {
            -0.5f, -0.5f, 0.0f,  // Button left-vertic
            0.5f, -0.5f, 0.0f,   // Button right-vertic
            0.0f, 0.5f, 0.0f     // Top-vertice
        };
        Shader shader;
        int VertexBufferObject;
        int VertexArrayObject;
        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) { }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            shader = new Shader("shader.vert", "shader.frag");
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            shader.Use();
            // 3. now draw the object
            // someOpenGLFunctionThatDrawsOurTriangle();
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            shader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            SwapBuffers();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            shader.Dispose();
        }

    }
}
