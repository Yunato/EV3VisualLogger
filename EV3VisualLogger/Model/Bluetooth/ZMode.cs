using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV3VisualLogger.Model.Bluetooth {
    class ZMode {

        public void ZPutBin(ref byte[] zv, int i, byte b)
        {
            switch (b)
            {
                case 0x0D: // CR
                case 0x8D: // CR | 0x80
                    /* if (zv->CtlEsc ||
                       ((zv->LastSent & 0x7f) == '@')) */
                    //  {
                    Array.Resize(ref zv, zv.Length + 1);
                    zv[i] = 0x18;
                    (i)++;
                    b = (byte)(b ^ 0x40);
                    //  }
                    break;
                case 0x0A: // LF
                case 0x10: // DLE
                case 0x11: // XON
                case 0x13: // XOFF
                case 0x1d: // GS
                case 0x18: // CAN(0x18)
                case 0x8A: // LF | 0x80
                case 0x90: // DLE | 0x80
                case 0x91: // XON | 0x80
                case 0x93: // XOFF | 0x80
                case 0x9d: // GS | 0x80
                    Array.Resize(ref zv, zv.Length + 1);
                    zv[i] = 0x18;
                    (i)++;
                    b = (byte)(b ^ 0x40);
                    break;
            }
            Array.Resize(ref zv, zv.Length + 1);
            zv[i] = b;
        }

        public void ZStoHdr(ref byte[] zv, long Pos){
            zv[0] = ((byte)(((((uint)(((Pos)) & 0xffff)))) & 0xff));
	        zv[1] = ((byte)((((((uint)(((Pos)) & 0xffff)))) >> 8) & 0xff));
            zv[2] = ((byte)(((((uint)((((Pos)) >> 16) & 0xffff)))) & 0xff));
            zv[3] = ((byte)((((((uint)((((Pos)) >> 16) & 0xffff)))) >> 8) & 0xff));
        }

        public void ZPutHex(ref byte[] zv, int i, byte b){
            if (b <= 0x9f){
                Array.Resize(ref zv, zv.Length + 1);
                zv[i] = (byte)((b >> 4) + 0x30);
            }else{
                Array.Resize(ref zv, zv.Length + 1);
                zv[i] = (byte)((b >> 4) + 0x57);
            }
            (i)++;
            if ((b & 0x0F) <= 0x09){
                Array.Resize(ref zv, zv.Length + 1);
                zv[i] = (byte)((b & 0x0F) + 0x30);
            }else{
                Array.Resize(ref zv, zv.Length + 1);
                zv[i] = (byte)((b & 0x0F) + 0x57);
            }
        }

        public void ZShHdr(ref byte[] zv, byte[] TxHdr, byte HdrType, ushort CRC){
            int i;

            Array.Resize(ref zv, zv.Length + 4);
			zv[0] = Convert.ToByte('*');
			zv[1] = Convert.ToByte('*');
			zv[2] = 0x18;
            zv[3] = Convert.ToByte('B');
			ZPutHex(ref zv, zv.Length, HdrType);
			CRC = Packet.UpdateCRC(HdrType, 0);
			for (i = 0; i <= 3; i++) {
                ZPutHex(ref zv, zv.Length, TxHdr[i]);
                CRC = Packet.UpdateCRC(TxHdr[i], CRC);
			}
            ZPutHex(ref zv, zv.Length, ((byte)((((CRC)) >> 8) & 0xff)));
			ZPutHex(ref zv, zv.Length, ((byte)(((CRC)) & 0xff)));
            Array.Resize(ref zv, zv.Length + 2);
            zv[zv.Length - 2] = 0x8D;
            zv[zv.Length - 1] = 0x8A;

            if ((HdrType != 8) && (HdrType != 3)){
                Array.Resize(ref zv, zv.Length + 1);
                zv[zv.Length - 1] = 0x11;
			}
        }

    }
}
