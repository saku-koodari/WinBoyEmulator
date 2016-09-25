using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinBoyEmulator.Rendering.App
{
    /// <summary>
    /// Event partial of class App
    /// </summary>
    public partial class BaseClass
    {
        private bool _isFormClosed;
        private bool _isFormResizing;

        /// <summary>
        /// Window resize event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Resize(object sender, EventArgs e) { throw new NotImplementedException(); }

        /// <summary>
        /// Event that launches when window resizing starts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResizeBegin(object sender, EventArgs e) => _isFormResizing = true;
        
        /// <summary>
        /// Event that launchs when windows resizing ends.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResizeEnd(object sender, EventArgs e)
        {
            _isFormResizing = false;
            Resize(sender, e);
        }

        /// <summary>
        /// Event that launchs when windows should close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Closed(object sender, EventArgs e) => _isFormClosed = true;

        /// <summary>
        /// Mouse click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void MouseClick(object sender, MouseEventArgs e) { throw new NotImplementedException(); }

        /// <summary>
        /// Key down event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void KeyDown(object sender, KeyEventArgs e) { throw new NotImplementedException(); }

        /// <summary>
        /// Key up event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void KeyUp(object sender, KeyEventArgs e) { throw new NotImplementedException(); }
    }
}
