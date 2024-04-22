using OWOGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static OwoAdvancedSensationBuilder.AdvancedSensationBuilder;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace OwoAdvancedSensationBuilder {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            basicSensation = basicParse();
            advancedSensation = new AdvancedSensationBuilder(basicSensation).build();
            updateVisualisation();
            OWO.AutoConnect();


            managableSensations.Add("initial basic", basicSensation);
            managableSensations.Add("initial advanced", advancedSensation);
            managableSensations.Add("ball basic", basicSensation2);

            lbSensations.Items.Add("initial basic");
            lbSensations.Items.Add("initial advanced");
            lbSensations.Items.Add("ball basic");
            lbSensations.SelectedIndex = 0;
        }

        private void btnDebug_Click(object sender, EventArgs e) {
            // advancedSensation = new AdvancedSensationBuilder(advancedSensation).cutAtTime(0.5f, false).build();
            // advancedSensation = new AdvancedSensationBuilder(new List<int> { 0, 0, 0, 0, 100, 50, 20, 10, 50, 100 }).build();
            updateVisualisation();

            AdvancedSensationManager manager = AdvancedSensationManager.getInstance();
            manager.add("initial advanced", advancedSensation);
        }

        /*
         * CREATOR
         */

        Sensation basicSensation = null;
        Sensation basicSensation2 = Sensation.Ball;
        Sensation advancedSensation = null;

        Boolean intoS1 = true;

        private void btnConnect_Click(object sender, EventArgs e) {
            OWO.AutoConnect();
        }

        private void btnDisconnect_Click(object sender, EventArgs e) {
            OWO.Disconnect();
        }

        private void btnBasic_Click(object sender, EventArgs e) {
            OWO.Send(basicSensation);
        }

        private void btnBasic2_Click(object sender, EventArgs e) {
            OWO.Send(basicSensation2);
        }

        private void btnAdvanced_Click(object sender, EventArgs e) {
            OWO.Send(advancedSensation);
        }

        private void btnAdvancedMuscle_Click(object sender, EventArgs e) {
            Sensation advancedSensation = new AdvancedSensationBuilder(basicSensation, Muscle.Front).build();
            OWO.Send(advancedSensation);
        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            if (intoS1) {
                basicSensation = basicParse();
                advancedSensation = new AdvancedSensationBuilder(basicSensation).build();
            } else {
                basicSensation2 = basicParse();
                advancedSensation = new AdvancedSensationBuilder(basicSensation2).build();
            }
            updateVisualisation();
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            if (intoS1) {
                basicSensation = basicSensation.Append(basicParse());
                advancedSensation = new AdvancedSensationBuilder(basicSensation).build();
            } else {
                basicSensation2 = basicSensation2.Append(basicParse());
                advancedSensation = new AdvancedSensationBuilder(basicSensation2).build();
            }
            updateVisualisation();
        }

        private void btnMuscleNew_Click(object sender, EventArgs e) {
            Random r = new Random();
            if (intoS1) {
                basicSensation = basicParse().WithMuscles(Muscle.All[r.Next(0, Muscle.All.Length)]);
                advancedSensation = new AdvancedSensationBuilder(basicSensation).build();
            } else {
                basicSensation2 = basicParse().WithMuscles(Muscle.All[r.Next(0, Muscle.All.Length)]);
                advancedSensation = new AdvancedSensationBuilder(basicSensation2).build();
            }
            updateVisualisation();
        }

        private void btnMuscleAdd_Click(object sender, EventArgs e) {
            Random r = new Random();
            if (intoS1) {
                basicSensation = basicSensation.Append(basicParse().WithMuscles(Muscle.All[r.Next(0, Muscle.All.Length)]));
                advancedSensation = new AdvancedSensationBuilder(basicSensation).build();
            } else {
                basicSensation2 = basicSensation2.Append(basicParse().WithMuscles(Muscle.All[r.Next(0, Muscle.All.Length)]));
                advancedSensation = new AdvancedSensationBuilder(basicSensation2).build();
            }
            updateVisualisation();
        }

        private MicroSensation basicParse() {
            return SensationsFactory.Create(
                Int32.Parse(txtFrequency.Text),
                float.Parse(txtDuration.Text),
                Int32.Parse(txtIntensity.Text),
                float.Parse(txtRampUp.Text),
                float.Parse(txtRampDown.Text),
                float.Parse(txtExit.Text));
        }

        private void btnManualBuild_Click(object sender, EventArgs e) {
            basicSensation = Sensation.Parse(txtBasic1.Text);
            basicSensation2 = Sensation.Parse(txtBasic2.Text); ;
            if (intoS1) {
                advancedSensation = new AdvancedSensationBuilder(basicSensation).build();
            } else {
                advancedSensation = new AdvancedSensationBuilder(basicSensation2).build();
            }
            updateVisualisation();
        }

        private void updateVisualisation() {
            txtBasic1.Text = basicSensation.ToString();
            txtBasic2.Text = basicSensation2.ToString();
            txtAdvanced.Text = advancedSensation.ToString();

            if (intoS1) {
                lblToggle.Text = "1";
            } else {
                lblToggle.Text = "2";
            }
        }

        private void btnToggle_Click(object sender, EventArgs e) {
            intoS1 = !intoS1;
            updateVisualisation();
        }

        private void btnMerge_Click(object sender, EventArgs e) {
            advancedSensation = new AdvancedSensationBuilder(basicSensation).merge(basicSensation2, MuscleMergeMode.MAX).build();
            updateVisualisation();
        }

        private void btnMergeDelayed_Click(object sender, EventArgs e) {
            advancedSensation = new AdvancedSensationBuilder(basicSensation).merge(basicSensation2, MuscleMergeMode.MAX, 1.5f).build();
            updateVisualisation();
        }

        private void btnAddToManager_Click(object sender, EventArgs e) {

            string name = txtName.Text;
            int count = managableSensations.Count;
            managableSensations.Add(name, advancedSensation);
            if (managableSensations.Count > count) {
                // new
                lbSensations.Items.Add(name);
            }
        }

        /*
         * MANAGER
         */

        Dictionary<string, Sensation> managableSensations = new Dictionary<string, Sensation>();

        private void lbSensations_SelectedIndexChanged(object sender, EventArgs e) {
            ListBox lb = sender as ListBox;
            string selected = lb.SelectedItem as string;
            Sensation s = managableSensations[selected];

            lblName.Text = selected;
            txtAdvanced2.Text = s.ToString();
        }

        private void tbInensityMultiply_Scroll(object sender, EventArgs e) {

        }

        private void btnPlayNow_Click(object sender, EventArgs e) {

        }

        private void btnLoopNow_Click(object sender, EventArgs e) {

        }

        private void btnStopNow_Click(object sender, EventArgs e) {

        }
    }
}
