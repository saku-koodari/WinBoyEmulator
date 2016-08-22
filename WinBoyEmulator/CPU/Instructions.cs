using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.CPU
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
     ADC
     ADD
     AND
     BIT
     CALL
     CCF
     CPL
     CP
     DAA
     DEC
     DI
     EI
     HALT
     INC
     JP
     JR
     LD
     LDD
     LDH
     LDI
     LD
     NOP
     */
      
     /*
     OR
     POP
     PUSH
     RES
     RET
     RETI
     RLA
     RLCA
     RLC
     RL
     RRA
     RRCA
     RRC
     RR
     RST
     SBC
     SCF
     SET
     SLA
     SRA
     SRL
     STOP
     SUB
     SWAP
     XOR
     LDHL
     */
}