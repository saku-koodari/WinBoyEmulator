// This file is part of WinBoyEmulator.
// 
// WinBoyEmulator is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     WinBoyEmulator is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with WinBoyEmulator.  If not, see<http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SharpDX.Windows;
using Log4Any;
using WinBoyEmulator.Rendering.Utils;

namespace WinBoyEmulator.Rendering.App
{
    public abstract partial class BaseClass : IDisposable
    {
        private readonly Stopwatch _stopwatch;
        private LogWriter _logWriter;
        private bool _isDisposed;
        private bool _isFormRezing;
        private Form _form;
        private float _frameAccumulator;
        private float _frameCount;

        public BaseClass()
        {
            _logWriter = new LogWriter(GetType());
            _stopwatch = new Stopwatch();
        }

        /// <summary>Performs object finalization.</summary>
        ~BaseClass()
        {
            if (_isDisposed)
                return;

            Dispose(false);
            _isDisposed = true;
        }

        /// <summary>The display's handle</summary>
        protected IntPtr DisplayHandle => _form.Handle;

        /// <summary>Gets the size of the client area of the form.</summary>
        protected Size ClientSize => _form.ClientSize;

        /// <summary>Number of seconds since the last frame.</summary>
        public float FrameDelta { get; private set; }

        /// <summaryNumber of seconds since the last frame.</summary>
        public float FramePerSecond { get; private set; }


        #region // TODO: Delete inside this
        /// <summary>Just because I'm lazy to write this every time.</summary>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void Info() => _logWriter.InfoFormat("Virtual method {0}.", GetCurrentMethod());

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private string GetCurrentMethod() => (new System.Diagnostics.StackTrace()).GetFrame(2).GetMethod().Name;
        #endregion

        private void Render()
        {
            _frameAccumulator += FrameDelta;
            ++_frameCount;

            if(_frameAccumulator >= 1.0f)
            {
                FramePerSecond = _frameCount / _frameAccumulator;

                _form.Text = Configuration.Instance.Title + " - FPS: " + FramePerSecond + " ( App.BaseClass.Render() )";
                _frameAccumulator = 0.0f;
                _frameCount = 0;
            }

            BeginDraw();
            Draw(_stopwatch);
            EndDraw();
        }

        private void OnUpdate()
        {
            FrameDelta = (float)_stopwatch.Update();
            Update(_stopwatch);
        }

        protected abstract void Initialize();

        /// <summary>Disposes of object resources.</summary>
        /// <param name="disposeManagedResources">Dispose managed resources.</param>
        protected virtual void Dispose(bool disposeManagedResources)
        {
            if (disposeManagedResources)
                _form?.Dispose();
        }

        protected virtual void LoadContent() => Info();

        protected virtual void UnloadContent() => Info();

        protected virtual void Update(Stopwatch stopwatch) => Info();

        protected virtual void BeginRun() => Info();

        protected virtual void EndRun() => Info();

        protected virtual void Draw(Stopwatch stopwatch) => Info();

        protected virtual void BeginDraw() => Info();

        protected virtual void EndDraw() => Info();

        /// <summary>Close the form.</summary>
        public void Exit() => _form.Close();

        /// <summary>Run on a new form.</summary>
        public void Run()
        {
            Run(new RenderForm(Configuration.Instance.Title)
            {
                ClientSize = new Size(Configuration.Instance.Width, Configuration.Instance.Height)
            });
        }

        /// <summary>Run game on the target form.</summary>
        /// <param name="targetForm">The form where the game will be put.</param>
        public void Run(Form targetForm)
        {
            _form = targetForm;
            Initialize();

            bool isFormClosed = false;
            bool isFormResizing = false;

            _form.MouseClick += MouseClick;
            _form.KeyDown += KeyDown;
            _form.KeyUp += KeyUp;

            _form.ResizeBegin += ResizeBegin;
            _form.ResizeEnd += ResizeEnd;

            _form.Closed += Closed;

            LoadContent();

            _stopwatch.Start();

            BeginRun();

            RenderLoop.Run(_form, () =>
            {
                if (isFormClosed)
                    return;

                OnUpdate();

                if (!isFormResizing)
                    Render();
            });

            UnloadContent();
            EndRun();

            // Dispose explicity
            Dispose();
        }

        /// <summary>Disposes of object resources.</summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                Dispose(true);
                _isDisposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }
}
