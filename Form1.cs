using OWOGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static OwoAdvancedSensationBuilder.AdvancedSensationBuilder;
using static OwoAdvancedSensationBuilder.AdvancedSensationBuilderMergeOptions;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using TrackBar = System.Windows.Forms.TrackBar;

namespace OwoAdvancedSensationBuilder {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            basicSensation = basicParse();
            advancedSensation = new AdvancedSensationBuilder(basicSensation).build();

            //Here we declare two baked sensations, Ball(0) and Dart(1)
            var auth = GameAuth.Parse("0~Ball~100,1,100,0,0,0,Impact|0%100,1%100,2%100,3%100,4%100,5%100~impact-0~#1~Dart~12,1,100,0,0,0,Impact|5%100~impact-1~");
            OWO.Configure(auth);

            updateVisualisation();
            OWO.AutoConnect();

            BakedSensation baked = basicSensation.Bake(3, "manual bake");

            managableSensations.Add("initial basic", basicSensation);
            managableSensations.Add("initial advanced", advancedSensation);
            managableSensations.Add("ball basic", basicSensation2);
            //managableSensations.Add("baked Ball", "0");
            //managableSensations.Add("baked Dart", "1");

            lbSensations.Items.Add("initial basic");
            lbSensations.Items.Add("initial advanced");
            lbSensations.Items.Add("ball basic");
            //lbSensations.Items.Add("baked Ball");
            //lbSensations.Items.Add("baked Dart");
            lbSensations.SelectedIndex = 0;
        }

        private void btnDebug_Click(object sender, EventArgs e) {

            OWO.Send("1");
        }

        private void btnToggleRain_Click(object sender, EventArgs e) {
            AdvancedSensationManager manager = AdvancedSensationManager.getInstance();
            if (manager.getPlayingSensationInstances().Keys.Contains("Rain Snippet")) {
                manager.stopSensation("Rain Snippet");
                updateVisualisationManager(true);
            } else {
                addRainRandom();
                updateVisualisationManager();
            }
        }

        private void addRainRandom() {

            Random r = new Random();

            AdvancedSensationManager manager = AdvancedSensationManager.getInstance();
            AdvancedSensationInstance instance = new AdvancedSensationInstance("Rain Snippet",
                    SensationsFactory.Create(20, 0.1f, 60, 0, 0, 0.3f).WithMuscles(Muscle.All[r.Next(0, Muscle.All.Length)]));
            instance.LastCalculationOfCycle += Instance_LastCalculationOfCycle;
            manager.playOnce(instance);
        }

        private void Instance_LastCalculationOfCycle(AdvancedSensationInstance instance) {
            addRainRandom();
        }

        private void btn20_Click(object sender, EventArgs e) {
            Sensation s = SensationsFactory.Create(100, 1, 20, 0, 0, 0).WithMuscles(Muscle.All.WithIntensity(100));
            OWO.Send(s);
        }

        private void btn20Adv_Click(object sender, EventArgs e) {
            Sensation s = new AdvancedSensationBuilder(SensationsFactory.Create(100, 1, 20, 0, 0, 0).WithMuscles(Muscle.All.WithIntensity(100))).build();
            OWO.Send(s);
        }

        private void btn120_Click(object sender, EventArgs e) {
            Sensation s = SensationsFactory.Create(100, 1, 100, 0, 0, 0).WithMuscles(Muscle.All.WithIntensity(20));
            OWO.Send(s);
        }

        private void btn120adv_Click(object sender, EventArgs e) {
            Sensation s = new AdvancedSensationBuilder(SensationsFactory.Create(100, 1, 100, 0, 0, 0).WithMuscles(Muscle.All.WithIntensity(20))).build();
            OWO.Send(s);
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
            AdvancedSensationBuilderOptions options = new AdvancedSensationBuilderOptions();
            options.muscles = Muscle.Front;
            Sensation advancedSensation = new AdvancedSensationBuilder(basicSensation, options).build();
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
            AdvancedSensationBuilderMergeOptions mergeOptions = new AdvancedSensationBuilderMergeOptions();
            mergeOptions.mode = MuscleMergeMode.MAX;

            advancedSensation = new AdvancedSensationBuilder(basicSensation).merge(basicSensation2, mergeOptions).build();
            updateVisualisation();
        }

        private void btnMergeDelayed_Click(object sender, EventArgs e) {
            AdvancedSensationBuilderMergeOptions mergeOptions = new AdvancedSensationBuilderMergeOptions();
            mergeOptions.mode = MuscleMergeMode.MAX;
            mergeOptions.delaySeconds = 1.5f;

            advancedSensation = new AdvancedSensationBuilder(basicSensation).merge(basicSensation2, mergeOptions).build();
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

        private void lbManager_SelectedIndexChanged(object sender, EventArgs e) {
            ListBox lb = sender as ListBox;
            string selected = lb.SelectedItem as string;

            lblName.Text = selected;
        }

        private void tbInensityMultiply_Scroll(object sender, EventArgs e) {

            string selected = lbSensations.SelectedItem as string;
            Sensation s = managableSensations[selected];

            TrackBar tb = sender as TrackBar;

            AdvancedSensationManager manager = AdvancedSensationManager.getInstance();
            manager.updateSensation(selected, s.MultiplyIntensityBy(tb.Value));
            updateVisualisationManager();
        }

        private void btnPlayNow_Click(object sender, EventArgs e) {
            string selected = lbSensations.SelectedItem as string;
            Sensation s = managableSensations[selected];

            AdvancedSensationManager manager = AdvancedSensationManager.getInstance();
            AdvancedSensationInstance instance = new AdvancedSensationInstance(selected, s);
            instance.LastCalculationOfCycle += Instance_LastCalculationOfCycle1;
            manager.playOnce(instance);
            updateVisualisationManager();
        }

        private void btnLoopNow_Click(object sender, EventArgs e) {
            string selected = lbSensations.SelectedItem as string;
            Sensation s = managableSensations[selected];

            AdvancedSensationManager manager = AdvancedSensationManager.getInstance();
            AdvancedSensationInstance instance = new AdvancedSensationInstance(selected, s);
            manager.playLoop(instance);
            updateVisualisationManager();
        }

        private void btnStopNow_Click(object sender, EventArgs e) {
            string selected = lbSensations.SelectedItem as string;

            AdvancedSensationManager manager = AdvancedSensationManager.getInstance();
            manager.stopSensation(selected);
            updateVisualisationManager(true);
        }

        private void btnRemoveAfter_Click(object sender, EventArgs e) {
            string selected = lbSensations.SelectedItem as string;

            AdvancedSensationManager manager = AdvancedSensationManager.getInstance();
            Dictionary<string, AdvancedSensationInstance> instances = manager.getPlayingSensationInstances();
            if (instances.ContainsKey(selected)) {
                instances[selected].LastCalculationOfCycle += Form1_LastCalculationOfCycle;
            }
        }

        private void Form1_LastCalculationOfCycle(AdvancedSensationInstance instance) {
            AdvancedSensationManager manager = AdvancedSensationManager.getInstance();
            manager.stopSensation(instance.name);
            updateVisualisationManager(true);
        }

        private void Instance_LastCalculationOfCycle1(AdvancedSensationInstance instance) {
            updateVisualisationManager(true);
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            updateVisualisationManager();
        }

        private void updateVisualisationManager(bool wait = false) {

            if (wait) {
                System.Timers.Timer timer = new System.Timers.Timer(150);
                timer.Elapsed += Timer_Elapsed;
                timer.AutoReset = false;
                timer.Enabled = true;
                return;
            }

            if (lbManager.InvokeRequired) {
                Action doInvoke = delegate { updateVisualisationManager(false); };
                lbManager.Invoke(doInvoke);
                return;
            }

            AdvancedSensationManager manager = AdvancedSensationManager.getInstance();

            List<string> names = new List<string>(manager.getPlayingSensationInstances().Keys);

            lbManager.Items.Clear();
            foreach (string name in names) {
                lbManager.Items.Add(name);
            }

            lblIntensityMultiplier.Text = tbInensityMultiply.Value.ToString() + "%";

            if (names.Count == 0) {
                tbInensityMultiply.Enabled = false;
                btnRemoveNow.Enabled = false;
                btnRemoveAfter.Enabled = false;
            } else {
                tbInensityMultiply.Enabled = true;
                btnRemoveNow.Enabled = true;
                btnRemoveAfter.Enabled = true;
            }

        }
    }
}
