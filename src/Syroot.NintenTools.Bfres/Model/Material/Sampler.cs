﻿using System;
using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;
using Syroot.NintenTools.Bfres.GX2;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents a <see cref="Texture"/> sampler in a <see cref="UserData"/> section, storing configuration on how to
    /// draw and interpolate textures.
    /// </summary>
    [DebuggerDisplay(nameof(Sampler) + " {" + nameof(Name) + "}")]
    public class Sampler : INamedResData
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private string _name;

        // ---- EVENTS -------------------------------------------------------------------------------------------------

        /// <summary>
        /// Raised when the <see cref="Name"/> property was changed.
        /// </summary>
        public event EventHandler NameChanged;

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        public TexSampler TexSampler { get; set; }

        /// <summary>
        /// The name with which the instance can be referenced uniquely in <see cref="INamedResDataList{Sampler}"/>
        /// instances.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (_name != value)
                {
                    _name = value;
                    NameChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            SamplerHead head = new SamplerHead(loader);
            TexSampler = new TexSampler(head.GX2Sampler);
            Name = loader.GetName(head.OfsName);
        }

        void IResData.Reference(ResFileLoader loader)
        {
        }
    }

    /// <summary>
    /// Represents the header of a <see cref="Sampler"/> instance.
    /// </summary>
    internal class SamplerHead
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        public uint[] GX2Sampler;
        public uint Handle;
        public uint OfsName;
        public byte Idx;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        internal SamplerHead(ResFileLoader loader)
        {
            GX2Sampler = loader.ReadUInt32s(3);
            Handle = loader.ReadUInt32();
            OfsName = loader.ReadOffset();
            Idx = loader.ReadByte();
            loader.Seek(3);
        }
    }
}