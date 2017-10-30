//
// MapCreator
// Copyright(C) 2015 Stefan Schäfer <merec@merec.org>
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MapCreator.data;

namespace MapCreator
{
    public partial class SelectMapsForm : Form
    {
        private List<ZoneSelection> m_selectedZones = new List<ZoneSelection>();

        public List<ZoneSelection> SelectedZones
        {
            get { return m_selectedZones; }
            set { m_selectedZones = value; }
        }

        public List<TreeNode> m_allNodes = new List<TreeNode>();

        public SelectMapsForm()
        {
            InitializeComponent();
            InitializeMapTreeView();
            
            selectedMapsListBox.DataSource = SelectedZones.OrderBy(s => s.Name).ToList();
            selectedMapsListBox.DisplayMember = "Name";
            selectedMapsListBox.ValueMember = "Id";

            // Load Presets
            presetsComboBox.DataSource = DataWrapper.GetPresetRows();
            presetsComboBox.DisplayMember = "Name";
            //presetsComboBox.ValueMember = "Id";
        }

        public void Preselect(List<ZoneSelection> preselect)
        {
            m_selectedZones = preselect;
            UpdateSelectedMapsListBox();
        }

        private void UpdateSelectedMapsListBox()
        {
            selectedMapsListBox.DataSource = null;
            selectedMapsListBox.DataSource = SelectedZones.OrderBy(s => s.Id).ToList();
        }

        private void UpdatePresets()
        {
            presetsComboBox.DataSource = DataWrapper.GetPresetRows();
        }

        private void InitializeMapTreeView()
        {
            foreach (string realm in DataWrapper.GetRealms())
            {
                TreeNode realmNode = new TreeNode(realm);

                foreach (string expansion in DataWrapper.GetExpansionsByRealm(realm))
                {
                    TreeNode expansionNode = new TreeNode(expansion);

                    foreach (string mapType in DataWrapper.GetZoneTypesByRealmAndExpansion(realm, expansion).OrderBy(o => o.ToString()))
                    {
                        TreeNode mapTypeNode = new TreeNode(mapType);

                        foreach (KeyValuePair<string, string> zone in DataWrapper.GetZonesByRealmAndExpansionAndType(realm, expansion, mapType).OrderBy(o => o.Key))
                        {
                            if (mapType == "Capitol" || mapType == "Indoor" || mapType == "Dungeons" || mapType == "Instances") continue;

                            ZoneSelection currentZone = new ZoneSelection(zone.Key, zone.Value, expansion, mapType);
                            TreeNode zoneNode = new TreeNode(currentZone.ToString());
                            zoneNode.Tag = currentZone;

                            m_allNodes.Add(zoneNode);
                            mapTypeNode.Nodes.Add(zoneNode);
                        }

                        if (mapTypeNode.Nodes.Count > 0)
                        {
                            expansionNode.Nodes.Add(mapTypeNode);
                        }
                    }

                    if (expansionNode.Nodes.Count == 1)
                    {
                        foreach (TreeNode node in expansionNode.Nodes[0].Nodes)
                        {
                            expansionNode.Nodes.Add(node);
                        }
                        expansionNode.Nodes.RemoveAt(0);
                        realmNode.Nodes.Add(expansionNode);
                    }
                    else if (expansionNode.Nodes.Count > 1)
                    {
                        realmNode.Nodes.Add(expansionNode);
                    }
                }

                mapsTreeView.Nodes.Add(realmNode);
            }
        }

        private void AddZonesRecursive(TreeNode node)
        {
            if(node.Tag is ZoneSelection)
            {
                if (!SelectedZones.Contains((ZoneSelection)node.Tag))
                {
                    SelectedZones.Add((ZoneSelection)node.Tag);
                }
            }
            else
            {
                foreach(TreeNode childNode in node.Nodes)
                {
                    AddZonesRecursive(childNode);
                }
            }
        }

        private void addAllButton_Click(object sender, EventArgs e)
        {
            if(mapsTreeView.SelectedNode != null && !(mapsTreeView.SelectedNode.Tag is ZoneSelection))
            {
                AddZonesRecursive(mapsTreeView.SelectedNode);
            }
            else if(mapsTreeView.SelNodes.Count > 0)
            {
                foreach(DictionaryEntry nodeEntry in mapsTreeView.SelNodes)
                {
                    AddZonesRecursive(((MWCommon.MWTreeNodeWrapper)nodeEntry.Value).Node);
                }
            }
            UpdateSelectedMapsListBox();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            foreach (DictionaryEntry entry in mapsTreeView.SelNodes)
            {
                MWCommon.MWTreeNodeWrapper nodeWrapper = (MWCommon.MWTreeNodeWrapper)entry.Value;
                if (nodeWrapper.Node.Tag is ZoneSelection)
                {
                    if (!SelectedZones.Contains((ZoneSelection)nodeWrapper.Node.Tag))
                    {
                        SelectedZones.Add((ZoneSelection)nodeWrapper.Node.Tag);
                    }
                }
            }
            UpdateSelectedMapsListBox();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            foreach (ZoneSelection selectedZone in selectedMapsListBox.SelectedItems)
            {
                SelectedZones.Remove(selectedZone);
            }
            UpdateSelectedMapsListBox();
        }

        private void removeAllButton_Click(object sender, EventArgs e)
        {
            SelectedZones.Clear();
            UpdateSelectedMapsListBox();
        }

        private void saveNewPresetButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(newPresetTextBox.Text))
            {
                List<string> zoneIds = new List<string>();
                foreach (ZoneSelection selectedZone in selectedMapsListBox.Items) zoneIds.Add(selectedZone.Id);
                DataWrapper.AddPresetRow(newPresetTextBox.Text, zoneIds);

                UpdatePresets();
                newPresetTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter a preset name.");
            }
        }

        private void loadPresetButton_Click(object sender, EventArgs e)
        {
            if (presetsComboBox.SelectedItem == null || !(presetsComboBox.SelectedItem is MapCreatorData.ZoneSelectionPresetsRow))
            {
                return;
            }

            MapCreatorData.ZoneSelectionPresetsRow preset = (MapCreatorData.ZoneSelectionPresetsRow)presetsComboBox.SelectedItem;

            SelectedZones.Clear();

            foreach (string zoneId in preset.Zones.Split(','))
            {
                var result = m_allNodes.Where(n => ((ZoneSelection)n.Tag).Id == zoneId).Select(n => (ZoneSelection)n.Tag);
                if (result.Count() > 0) SelectedZones.Add(result.First());
            }

            UpdateSelectedMapsListBox();
        }

        private void savePresetButton_Click(object sender, EventArgs e)
        {
            if (presetsComboBox.SelectedItem == null || !(presetsComboBox.SelectedItem is MapCreatorData.ZoneSelectionPresetsRow))
            {
                return;
            }

            MapCreatorData.ZoneSelectionPresetsRow preset = (MapCreatorData.ZoneSelectionPresetsRow)presetsComboBox.SelectedItem;

            List<string> zoneIds = new List<string>();
            foreach (ZoneSelection selectedZone in selectedMapsListBox.Items) zoneIds.Add(selectedZone.Id);
            preset.Zones = string.Join(",", zoneIds);

            DataWrapper.SavePresets();
        }

        private void deletePresetButton_Click(object sender, EventArgs e)
        {
            if (presetsComboBox.SelectedItem == null || !(presetsComboBox.SelectedItem is MapCreatorData.ZoneSelectionPresetsRow))
            {
                return;
            }

            MapCreatorData.ZoneSelectionPresetsRow preset = (MapCreatorData.ZoneSelectionPresetsRow)presetsComboBox.SelectedItem;
            DataWrapper.RemovePreset(preset);
            UpdatePresets();
        }

        private void selectedMapsListBox_DataSourceChanged(object sender, EventArgs e)
        {
            selectMapsCounterLabel.Text = SelectedZones.Count.ToString();
        }

        private void mapsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is ZoneSelection)
            {
                m_selectedZones.Add((ZoneSelection) e.Node.Tag);
                UpdateSelectedMapsListBox();
            }
        }
    }
}
