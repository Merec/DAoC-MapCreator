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

        private void UpdateSelectedMapsListBox()
        {
            selectedMapsListBox.DataSource = null;
            selectedMapsListBox.DataSource = SelectedZones.OrderBy(s => s.Name).ToList();
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

                foreach (string expansion in DataWrapper.GetExpansionsByRealm(realm).OrderBy(o => o.ToString()))
                {
                    TreeNode expansionNode = new TreeNode(expansion);

                    foreach (string mapType in DataWrapper.GetZoneTypesByRealmAndExpansion(realm, expansion).OrderBy(o => o.ToString()))
                    {
                        TreeNode mapTypeNode = new TreeNode(mapType);

                        foreach (KeyValuePair<string, string> zone in DataWrapper.GetZonesByRealmAndExpansionAndType(realm, expansion, mapType).OrderBy(o => o.Value))
                        {
                            TreeNode zoneNode = new TreeNode(zone.Value);
                            zoneNode.Tag = new ZoneSelection(zone.Key, zone.Value);

                            m_allNodes.Add(zoneNode);
                            mapTypeNode.Nodes.Add(zoneNode);
                        }

                        expansionNode.Nodes.Add(mapTypeNode);
                    }

                    realmNode.Nodes.Add(expansionNode);
                }

                mapsTreeView.Nodes.Add(realmNode);
            }
        }

        private void addAllButton_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in m_allNodes)
            {
                if (node.Tag is ZoneSelection)
                {
                    if (!SelectedZones.Contains((ZoneSelection)node.Tag))
                    {
                        SelectedZones.Add((ZoneSelection)node.Tag);
                    }
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
    }
}
