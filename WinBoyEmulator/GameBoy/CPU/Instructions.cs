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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.GameBoy.CPU
{
    public interface IInstructions
    {
        #region Commands
        /*
         ADC A, n
         ADD A, n
         ADD HL, n
         ADD SP, n
         AND n
         BIT b, r
         CALL cc, nn
         CALL nn
         CCF
         CPL
         CP n
         DAA
         DEC n
         DEC nn
         DI
         EI
         HALT
         INC n
         INC nn
         JP (HL)
         JP cc,nn
         JP nn
         JR cc, n
         JR n
         LD (C), A
         LD (HL+), A
         // ...
         LDD (HL), A
         LDD A, (HL)
         LDH (n), A
         LDH
         ...
         */
        #endregion
    }

    /*
     keywords:
     ADC    ADD     AND
     BIT
     CALL   CCF     CPL     CP
     DAA    DEC     DI
     EI
     HALT
     INC
     JP     JR
     LD     LDD     LDH    LDI      LD
     NOP
     OR
     POP    PUSH
     RES    RET     RETI    RLA     RLCA
     RLC    RL      RRA     RRCA    RRC
     RR     RST
     SBC    SCF     SET     SLA     SRA
     SRL    STOP    SUB     SWAP
     XOR
     LDHL
     */
}