﻿namespace Merg.Cbus.Communications {
    public abstract class CbusMessageWithNodeNumberAndDeviceNumber : CbusMessageWithNodeNumber
    {
        protected CbusMessageWithNodeNumberAndDeviceNumber(Cbus.OpCodes opCode, byte[] data) : base(opCode, data)
        {
            EnsureDataLength(data, 4);
        }

        public ushort DeviceNumber => (ushort)((Data[2] << 8) + Data[3]);
    }
}