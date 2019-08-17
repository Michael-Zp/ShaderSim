using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace ShaderRenderer
{
    public class ShaderProgram : Disposable
    {
        /// <summary>The shader ids used for linking</summary>
        private List<int> shaderIDs = new List<int>();

        /// <summary>
        /// Gets a value indicating whether this instance is linked.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is linked; otherwise, <c>false</c>.
        /// </value>
        public bool IsLinked { get; private set; }

        /// <summary>Gets the last log.</summary>
        /// <value>The last log.</value>
        public string LastLog { get; private set; }

        /// <summary>Gets the program identifier.</summary>
        /// <value>The program identifier.</value>
        public int ProgramID { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Zenseless.OpenGL.ShaderP" /> class.
        /// </summary>
        public ShaderProgram()
        {
            this.ProgramID = GL.CreateProgram();
        }

        public ShaderProgram(string vertex, string fragment)
        {
            ProgramID = GL.CreateProgram();

            Compile(vertex, ShaderType.VertexShader);
            Compile(fragment, ShaderType.FragmentShader);

            Link();
        }

        /// <summary>Compiles the specified s shader.</summary>
        /// <param name="sShader">The s shader.</param>
        /// <param name="type">The type.</param>
        /// <exception cref="T:Zenseless.HLGL.ShaderCompileException">
        /// Could not create " + type.ToString() + " object
        /// or
        /// Error compiling  " + type.ToString()
        /// </exception>
        public void Compile(string sShader, ShaderType type)
        {
            this.IsLinked = false;
            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, sShader);
            GL.CompileShader(shader);
            int @params;
            GL.GetShader(shader, ShaderParameter.CompileStatus, out @params);
            this.LastLog = GL.GetShaderInfoLog(shader);
            if (1 != @params)
            {
                GL.DeleteShader(shader);
            }
            GL.AttachShader(this.ProgramID, shader);
            this.shaderIDs.Add(shader);
        }

        /// <summary>Begins this shader use.</summary>
        public void Activate()
        {
            GL.UseProgram(this.ProgramID);
        }

        /// <summary>Ends this shader use.</summary>
        public void Deactivate()
        {
            GL.UseProgram(0);
        }

        /// <summary>Gets the resource location.</summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Unknown ShaderResourceType</exception>
        public int GetResourceLocation(ShaderResourceType resourceType, string name)
        {
            switch (resourceType)
            {
                case ShaderResourceType.Uniform:
                    return this.GetResourceIndex(name, ProgramInterface.Uniform);
                case ShaderResourceType.Attribute:
                    return GL.GetAttribLocation(this.ProgramID, name);
                case ShaderResourceType.UniformBuffer:
                    return this.GetResourceIndex(name, ProgramInterface.UniformBlock);
                case ShaderResourceType.RWBuffer:
                    return this.GetResourceIndex(name, ProgramInterface.ShaderStorageBlock);
                default:
                    throw new ArgumentOutOfRangeException("Unknown ShaderResourceType");
            }
        }

        /// <summary>
        /// Links all compiled shaders to a shader program and deletes them.
        /// </summary>
        /// <exception cref="T:Zenseless.HLGL.ShaderException">
        /// Unknown Link error!
        /// or
        /// Error linking shader
        /// </exception>
        public void Link()
        {
            GL.LinkProgram(this.ProgramID);
            this.IsLinked = true;
            this.RemoveShaders();
        }

        /// <summary>Will be called from the default Dispose method.</summary>
        protected override void DisposeResources()
        {
            if (this.ProgramID == 0)
                return;
            GL.DeleteProgram(this.ProgramID);
        }

        /// <summary>Gets the index of the resource.</summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private int GetResourceIndex(string name, ProgramInterface type)
        {
            return GL.GetProgramResourceIndex(this.ProgramID, type, name);
        }

        /// <summary>Removes the shaders.</summary>
        private void RemoveShaders()
        {
            foreach (int shaderId in this.shaderIDs)
            {
                GL.DetachShader(this.ProgramID, shaderId);
                GL.DeleteShader(shaderId);
            }
            this.shaderIDs.Clear();
        }
    }
}
