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

namespace WinBoyEmulator
{
    /// <summary>
    /// An interface which purpose is to keep a different
    /// video renderers compatible with WinBoyEmulator.
    /// </summary>
    public interface IVideoRenderer
    {
        // TODO: Consider updating just a Data (as buffer) instead of a whole screen.

        /// <summary>Screen that will be displayed in the form.</summary>
        Screen Screen { get; set; }

        /// <summary>Actions that is done during the loop.</summary>
        Action Loop { get; set; }

        /// <summary>
        /// Method that starts the Game.
        /// </summary>
        /// <param name="targetForm">The form where the game is drawn.</param>
        void Run(Form targetForm);

        /// <summary>
        /// Updates buffer.
        /// </summary>
        /// <param name="screen">
        ///     You can use with this argument or without
        ///     (the you must use Property <see cref="Screen"/>).
        /// </param>
        void Update(Screen screen = null);

        /// <summary>Draws buffer to the target form.</summary>
        void Draw();
    }
}
