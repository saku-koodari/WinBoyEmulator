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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Third party
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.DXGI;
using SharpDX.Windows;

// Self-written
using log4Any;
using WinBoyEmulator.GameBoy;

// Aliasses
using D3D11 = SharpDX.Direct3D11;

namespace WinBoyEmulator.SharpDX4GB
{
    public class Game : IDisposable
    {
        private const int FPS = 120;
        private const int Width = GameBoy.Configuration.Screen.Width;
        private const int Height = GameBoy.Configuration.Screen.Height;
        private const string VertexShaderFile = "SharpDX4GB/vertexShader.hlsl";
        private const string PixelShaderFile = "SharpDX4GB/pixelShader.hlsl";

        private LogWriter _logWriter;
        private Emulator _emulator;

        private ModeDescription _backBufferDescription;
        private RenderForm _renderForm;
        private SwapChain _swapChain;
        private SwapChainDescription _swapChainDescription;
        private Vector3[] _vertices;
        private Viewport _viewport;
        private D3D11.Buffer _triangleVertexBuffer;
        private D3D11.Device _d3dDevice;
        private D3D11.DeviceContext _d3dDeviceContext;
        private D3D11.InputElement[] _inputElements;
        private D3D11.PixelShader _pixelShader;
        private D3D11.RenderTargetView _renderTargetView;
        private D3D11.VertexShader _vertexShader;

        /// <summary>Path of the game.</summary>
        public string GamePath
        {
            get
            {
                return _emulator.GamePath;
            }
            set
            {
                _emulator.GamePath = value;
            }
        }

        public Game() : this("WinBoyEmulator") { }

        public Game(string text)
        {
            _logWriter = new LogWriter( GetType() );
            _emulator = new Emulator();

            _renderForm = new RenderForm(text);
            _renderForm.ClientSize = new Size(Width, Height);
            // You may need to modify rendering if you set this true.
            _renderForm.AllowUserResizing = false;

            _initializeDeviceResources();
            _initializeShaders();
            _initializeTriangle();

            _inputElements = new D3D11.InputElement[]
            {
                new D3D11.InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0)
            };
        }

        #region Initialize methods
        private void _initializeDeviceResources()
        {
            _backBufferDescription = new ModeDescription(Width, Height, new Rational(FPS, 1), Format.R8G8B8A8_UNorm);
            _swapChainDescription = new SwapChainDescription
            {
                ModeDescription     = _backBufferDescription,
                SampleDescription   = new SampleDescription(1, 0),
                Usage               = Usage.RenderTargetOutput,
                BufferCount         = 1,
                OutputHandle        = _renderForm.Handle,
                IsWindowed          = true
            };

            var hw = DriverType.Hardware;
            var flags = D3D11.DeviceCreationFlags.None;

            D3D11.Device.CreateWithSwapChain(hw, flags, _swapChainDescription, out _d3dDevice, out _swapChain);
            _d3dDeviceContext = _d3dDevice.ImmediateContext;

            using (D3D11.Texture2D backbuffer = _swapChain.GetBackBuffer<D3D11.Texture2D>(0))
            {
                _renderTargetView = new D3D11.RenderTargetView(_d3dDevice, backbuffer);
            }
        }

        private void _initializeShaders()
        {
            using (var vertexShaderByteCode = ShaderBytecode.CompileFromFile(VertexShaderFile, "main", "vs_4_0", ShaderFlags.Debug))
            {
                _vertexShader = new D3D11.VertexShader(_d3dDevice, vertexShaderByteCode);
            }

            using (var pixelShaderByteCode = ShaderBytecode.CompileFromFile(PixelShaderFile, "main", "ps_4_0", ShaderFlags.Debug))
            {
                _pixelShader = new D3D11.PixelShader(_d3dDevice, pixelShaderByteCode);
            }

            _d3dDeviceContext.VertexShader.Set(_vertexShader);
            _d3dDeviceContext.PixelShader.Set(_pixelShader);

            _d3dDeviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
        }

        private void _initializeTriangle()
        {
            _vertices = new Vector3[]
            {
                new Vector3(-0.5f, 0.5f, 0.0f),
                new Vector3(0.5f, 0.5f, 0.0f),
                new Vector3(0.0f, -0.5f, 0.0f)
            };
            _triangleVertexBuffer = D3D11.Buffer.Create(_d3dDevice, D3D11.BindFlags.VertexBuffer, _vertices);
        }
        #endregion

        private void _draw()
        {
            _d3dDeviceContext.OutputMerger.SetRenderTargets(_renderTargetView);
            _d3dDeviceContext.ClearRenderTargetView(_renderTargetView, new SharpDX.Color(255, 255, 255));
            _swapChain.Present(1, PresentFlags.None);
        }

        private void _renderCallBack() => _draw();

        /// <summary>
        /// Check if object is disposed. If it's not, then dispose the object. Else do nothing.
        /// </summary>
        /// <param name="disposableObject">object that inherit's SharpDX.DisposeBase</param>
        private void _disposeIfNotDisposed(DisposeBase disposableObject, string nameofObject = null)
        {
            if (string.IsNullOrEmpty(nameofObject))
                nameofObject = nameof(disposableObject);

            if (!disposableObject.IsDisposed)
            {
                disposableObject.Dispose();
                // Would it better to use trace? This might put too much data to output.
                _logWriter.Debug($"Object: {nameofObject} disposed.");
            }
            else
            {
                var stackTrace = new StackTrace();
                var stackFrame = stackTrace.GetFrame(1);
                var method = stackFrame.GetMethod();
                var message = $"Object: {nameofObject} is already disposed.\nSmall stack trace:\n";

                for (var i = 0; i < 3; i++)
                {
                    message += $"i:{i}\tmethod{stackTrace.GetFrame(i).GetMethod()}\n";
                }

                _logWriter.Info(message);
            }
        }

        /// <summary>Runs a game.</summary>
        public void Run()
        {
            if (string.IsNullOrEmpty(GamePath))
                throw new InvalidOperationException("Missing property GamePath");

            _emulator.StartEmulation();

            RenderLoop.Run(_renderForm, _renderCallBack);
        }

        /// <summary>Dispose objects.</summary>
        public void Dispose()
        {
            _emulator.StopEmulation();

            _disposeIfNotDisposed(_triangleVertexBuffer, nameof(_triangleVertexBuffer));
            _disposeIfNotDisposed(_vertexShader, nameof(_vertexShader));
            _disposeIfNotDisposed(_pixelShader, nameof(_pixelShader));
            _disposeIfNotDisposed(_renderTargetView, nameof(_renderTargetView));
            _disposeIfNotDisposed(_swapChain, nameof(_renderTargetView));
            _disposeIfNotDisposed(_d3dDevice, nameof(_d3dDevice));
            _disposeIfNotDisposed(_d3dDeviceContext, nameof(_d3dDeviceContext));

            if( !_renderForm.IsDisposed )
                _renderForm.Dispose();
        }
    }
}
