﻿using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres.GX2
{
    /// <summary>
    /// Represents GX2 settings controlling additional color blending options.
    /// </summary>
    public class ColorControl
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const int _multiWriteBit = 1;
        private const int _colorBufferBit = 4;
        private const int _blendEnableBit = 8, _blendEnableBits = 8;
        private const int _logicOpBit = 16, _logicOpBits = 8;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorControl"/> class.
        /// </summary>
        public ColorControl()
        {
        }

        internal ColorControl(uint value)
        {
            Value = value;
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        public bool MultiWriteEnabled
        {
            get { return Value.GetBit(_multiWriteBit); }
            set { Value = Value.SetBit(_multiWriteBit, value); }
        }

        public bool ColorBufferEnabled
        {
            get { return Value.GetBit(_colorBufferBit); }
            set { Value = Value.SetBit(_colorBufferBit, value); }
        }

        public byte BlendEnableMask
        {
            get { return (byte)Value.Decode(_blendEnableBit, _blendEnableBits); }
            set { Value = Value.Encode(value, _blendEnableBit, _blendEnableBits); }
        }

        public GX2LogicOp LogicOp
        {
            get { return (GX2LogicOp)Value.Decode(_logicOpBit, _logicOpBits); }
            set { Value = Value.Encode((uint)value, _logicOpBit, _logicOpBits); }
        }

        internal uint Value { get; set; }
    }
}