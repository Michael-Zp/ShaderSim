using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using ShaderRenderer;
using ShaderUtils;

namespace ShaderTranslator
{
    class TranslatorVAO : Disposable, IVertexArrayObject
    {
        private Dictionary<int, BufferObject> boundBuffers = new Dictionary<int, BufferObject>();
        /// <summary>The identifier vao</summary>
        private int idVAO;
        /// <summary>The identifier buffer binding</summary>
        private const int idBufferBinding = 2147483647;
        private int lastAttributeLength;

        public int IDLength { get; private set; }

        private RenderTranslator _wrapper;

        public TranslatorVAO(RenderTranslator wrapper)
        {
            _wrapper = wrapper;

            idVAO = GL.GenVertexArray();
        }

        public void SetIndex(IEnumerable<uint> data)
        {
            if (data == null || data.Count() == 0)
                return;
            this.Activate();
            BufferObject bufferObject = this.RequestBuffer(int.MaxValue, BufferTarget.ElementArrayBuffer);
            bufferObject.Set<uint>(data.ToArray(), BufferUsageHint.StaticDraw);
            bufferObject.Activate();
            this.Deactivate();
            bufferObject.Deactivate();
            this.IDLength = data.Count();
        }

        public void SetAttribute<T>(string name, Tuple<VertexShader, FragmentShader> shader, IEnumerable<T> data, bool perInstance = false) where T : struct
        {
            int bindingID = _wrapper.GetAttributeBindingID(shader, name);

            if (-1 == bindingID)
                return;
            this.Activate();
            BufferObject bufferObject = this.RequestBuffer(bindingID, BufferTarget.ArrayBuffer);
            bufferObject.Set<T>(data.ToArray(), BufferUsageHint.StaticDraw);
            bufferObject.Activate();
            int stride = Marshal.SizeOf(typeof(T));
            int elementSize = stride / Marshal.SizeOf(typeof(float));
            GL.VertexAttribPointer(bindingID, elementSize, VertexAttribPointerType.Float, false, stride, 0);
            GL.EnableVertexAttribArray(bindingID);
            if (perInstance)
                GL.VertexAttribDivisor(bindingID, 1);
            else
                this.lastAttributeLength = data.Count();
            this.Deactivate();
            bufferObject.Deactivate();
            GL.DisableVertexAttribArray(bindingID);
        }

        protected override void DisposeResources()
        {
            foreach (Disposable disposable in this.boundBuffers.Values)
                disposable.Dispose();
            this.boundBuffers.Clear();
            GL.DeleteVertexArray(this.idVAO);
            this.idVAO = 0;
        }

        private void Activate()
        {
            GL.BindVertexArray(this.idVAO);
        }

        private void Deactivate()
        {
            GL.BindVertexArray(0);
        }

        public void Draw(int instanceCount = 1)
        {
            this.Activate();
            if (this.IDLength == 0)
                GL.DrawArraysInstanced(PrimitiveType.Triangles, 0, this.lastAttributeLength, instanceCount);
            else
                GL.DrawElementsInstanced(PrimitiveType.Triangles, this.IDLength, DrawElementsType.UnsignedInt, (IntPtr)0, instanceCount);
            this.Deactivate();
        }

        private BufferObject RequestBuffer(int bindingID, BufferTarget bufferTarget)
        {
            if (!this.boundBuffers.TryGetValue(bindingID, out var bufferObject))
            {
                bufferObject = new BufferObject(bufferTarget);
                this.boundBuffers[bindingID] = bufferObject;
            }
            return bufferObject;
        }
    }
}
