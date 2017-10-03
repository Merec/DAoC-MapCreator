//
// MapCreator NifUtil Library
// Copyright(C) 2017 Stefan Schäfer <merec@merec.org>
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//

using System.Collections.Generic;
using Niflib;
using SharpDX;
using System;

namespace NifUtil
{
    public delegate bool IsNodeDrawableEventHandler(NiAVObject node);

    class Convert
    {
        /// <summary>
        /// The NIF File
        /// </summary>
        private NiFile m_file;

        /// <summary>
        /// The Nif File
        /// </summary>
        internal NiFile File
        {
            get { return m_file; }
            set { m_file = value; }
        }

        /// <summary>
        /// The exported text
        /// </summary>
        private List<string> m_export = new List<string>();

        /// <summary>
        /// The exported text
        /// </summary>
        internal List<string> Export
        {
            get { return m_export; }
            set { m_export = value; }
        }

        private List<string> ignoreNodeNames = new List<string>();
        internal List<string> IgnoreNodeNames
        {
            get { return ignoreNodeNames; }
            set { ignoreNodeNames = value; }
        }

        public event IsNodeDrawableEventHandler IsNodeDrawable;

        public Convert(NiFile niFile)
        {
            File = niFile;

            ignoreNodeNames.AddRange(new List<string> {
                "collidee",
                "bounding",
                "climb",
                "!lod_cullme",
                "!visible_damaged",
                "shadowcaster",
                "far"
            });

        }

        internal bool IsValidNode(NiAVObject node)
        {
            // Invisible Flag
            if ((node.Flags & 1) == 1) return false;

            // Ignore some node names
            foreach (string ignoreName in IgnoreNodeNames)
            {
                if (node.Name.Value.ToLower().StartsWith(ignoreName.ToLower())) return false;
            }

            // Additional Callback
            if(IsNodeDrawable != null)
            {
                return IsNodeDrawable(node);
            }

            return true;
        }

        internal Matrix ComputeWorldMatrix(NiAVObject obj)
        {
            var path = new List<NiAVObject>();
            var current = obj;
            while (current != null)
            {
                path.Add(current);
                current = current.Parent;
            }
            var worldMatrix = Matrix.Identity;
            for (var i = 0; i < path.Count; i++)
            {
                var node = path[i];
                worldMatrix *= node.Rotation;
                worldMatrix *= Matrix.Scaling(node.Scale, node.Scale, node.Scale);
                worldMatrix *= Matrix.Translation(node.Translation.X, node.Translation.Y, node.Translation.Z);
            }

            return worldMatrix;
        }

        public virtual void Write(string targetFile)
        {
        }
    }
}
