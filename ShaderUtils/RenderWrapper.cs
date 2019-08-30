﻿using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace ShaderUtils
{
    public abstract class RenderWrapper
    {
        public abstract bool DepthEnabled { get; set; }

        protected IVertexArrayObject ActiveVAO;

        protected int Width;
        protected int Height;
        protected Bitmap Bmp;

        public void SetRenderSize(int width, int height)
        {
            Width = width;
            Height = height;
            Bmp = new Bitmap(Width, Height);
            OnResize(width, height);
        }

        public virtual void OnResize(int width, int height)
        {

        }

        public abstract void SetUniform<T>(string name, T value) where T : struct;

        public abstract void ActivateShader(VertexShader vertex, FragmentShader fragment);

        public abstract void DeactivateShader();

        public void ActivateVAO(IVertexArrayObject vao)
        {
            ActiveVAO = vao;
        }

        public void DeactivateVAO()
        {
            ActiveVAO = null;
        }
    }
}
