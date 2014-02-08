using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;

namespace MapCreator
{
    class TreeClusterRow
    {
        private string m_name;
        private string m_tree;
        private List<Vector3> m_treeInstances;

        #region Getter/Setter

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public string Tree
        {
            get { return m_tree; }
            set { m_tree = value; }
        }

        public List<Vector3> TreeInstances
        {
            get { return m_treeInstances; }
            set { m_treeInstances = value; }
        }

        #endregion

        public TreeClusterRow()
        {
        }
    }
}
