namespace OwoAdvancedSensationBuilder {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnBasic = new System.Windows.Forms.Button();
            this.btnAdvanced = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtFrequency = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIntensity = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtExit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRampDown = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRampUp = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnMuscleAdd = new System.Windows.Forms.Button();
            this.btnMuscleNew = new System.Windows.Forms.Button();
            this.btnAdvancedMuscle = new System.Windows.Forms.Button();
            this.txtBasic1 = new System.Windows.Forms.TextBox();
            this.txtAdvanced = new System.Windows.Forms.TextBox();
            this.txtBasic2 = new System.Windows.Forms.TextBox();
            this.lblOne = new System.Windows.Forms.Label();
            this.lblTwo = new System.Windows.Forms.Label();
            this.lblToggle = new System.Windows.Forms.Label();
            this.btnToggle = new System.Windows.Forms.Button();
            this.btnBasic2 = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnMergeDelayed = new System.Windows.Forms.Button();
            this.btnManualBuild = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(151, 24);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(169, 12);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(151, 24);
            this.btnDisconnect.TabIndex = 1;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnBasic
            // 
            this.btnBasic.Location = new System.Drawing.Point(140, 376);
            this.btnBasic.Name = "btnBasic";
            this.btnBasic.Size = new System.Drawing.Size(212, 26);
            this.btnBasic.TabIndex = 2;
            this.btnBasic.Text = "Basic Sensation 1";
            this.btnBasic.UseVisualStyleBackColor = true;
            this.btnBasic.Click += new System.EventHandler(this.btnBasic_Click);
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.Location = new System.Drawing.Point(358, 376);
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.Size = new System.Drawing.Size(212, 62);
            this.btnAdvanced.TabIndex = 3;
            this.btnAdvanced.Text = "Advanced Sensation";
            this.btnAdvanced.UseVisualStyleBackColor = true;
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(528, 53);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(127, 33);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "New Sensation";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtFrequency
            // 
            this.txtFrequency.Location = new System.Drawing.Point(432, 27);
            this.txtFrequency.Name = "txtFrequency";
            this.txtFrequency.Size = new System.Drawing.Size(53, 20);
            this.txtFrequency.TabIndex = 5;
            this.txtFrequency.Text = "100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(429, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "frequency";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(486, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "duration";
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(489, 27);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(53, 20);
            this.txtDuration.TabIndex = 7;
            this.txtDuration.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(545, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "intensity";
            // 
            // txtIntensity
            // 
            this.txtIntensity.Location = new System.Drawing.Point(548, 27);
            this.txtIntensity.Name = "txtIntensity";
            this.txtIntensity.Size = new System.Drawing.Size(53, 20);
            this.txtIntensity.TabIndex = 9;
            this.txtIntensity.Text = "20";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(720, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "exit";
            // 
            // txtExit
            // 
            this.txtExit.Location = new System.Drawing.Point(723, 27);
            this.txtExit.Name = "txtExit";
            this.txtExit.Size = new System.Drawing.Size(53, 20);
            this.txtExit.TabIndex = 15;
            this.txtExit.Text = "0,100";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(661, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "ramp down";
            // 
            // txtRampDown
            // 
            this.txtRampDown.Location = new System.Drawing.Point(664, 27);
            this.txtRampDown.Name = "txtRampDown";
            this.txtRampDown.Size = new System.Drawing.Size(53, 20);
            this.txtRampDown.TabIndex = 13;
            this.txtRampDown.Text = "0,300";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(604, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "ramp up";
            // 
            // txtRampUp
            // 
            this.txtRampUp.Location = new System.Drawing.Point(607, 27);
            this.txtRampUp.Name = "txtRampUp";
            this.txtRampUp.Size = new System.Drawing.Size(53, 20);
            this.txtRampUp.TabIndex = 11;
            this.txtRampUp.Text = "0,300";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(661, 53);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(127, 33);
            this.btnAdd.TabIndex = 17;
            this.btnAdd.Text = "Add Sensation";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnMuscleAdd
            // 
            this.btnMuscleAdd.Location = new System.Drawing.Point(661, 92);
            this.btnMuscleAdd.Name = "btnMuscleAdd";
            this.btnMuscleAdd.Size = new System.Drawing.Size(127, 33);
            this.btnMuscleAdd.TabIndex = 18;
            this.btnMuscleAdd.Text = "Add (with Muscle)";
            this.btnMuscleAdd.UseVisualStyleBackColor = true;
            this.btnMuscleAdd.Click += new System.EventHandler(this.btnMuscleAdd_Click);
            // 
            // btnMuscleNew
            // 
            this.btnMuscleNew.Location = new System.Drawing.Point(528, 92);
            this.btnMuscleNew.Name = "btnMuscleNew";
            this.btnMuscleNew.Size = new System.Drawing.Size(127, 33);
            this.btnMuscleNew.TabIndex = 19;
            this.btnMuscleNew.Text = "New (with Muscle)";
            this.btnMuscleNew.UseVisualStyleBackColor = true;
            this.btnMuscleNew.Click += new System.EventHandler(this.btnMuscleNew_Click);
            // 
            // btnAdvancedMuscle
            // 
            this.btnAdvancedMuscle.Location = new System.Drawing.Point(576, 376);
            this.btnAdvancedMuscle.Name = "btnAdvancedMuscle";
            this.btnAdvancedMuscle.Size = new System.Drawing.Size(212, 62);
            this.btnAdvancedMuscle.TabIndex = 20;
            this.btnAdvancedMuscle.Text = "Advanced Sensation (Muscle Override)";
            this.btnAdvancedMuscle.UseVisualStyleBackColor = true;
            this.btnAdvancedMuscle.Click += new System.EventHandler(this.btnAdvancedMuscle_Click);
            // 
            // txtBasic1
            // 
            this.txtBasic1.Location = new System.Drawing.Point(28, 170);
            this.txtBasic1.Name = "txtBasic1";
            this.txtBasic1.Size = new System.Drawing.Size(692, 20);
            this.txtBasic1.TabIndex = 21;
            // 
            // txtAdvanced
            // 
            this.txtAdvanced.Location = new System.Drawing.Point(12, 238);
            this.txtAdvanced.Multiline = true;
            this.txtAdvanced.Name = "txtAdvanced";
            this.txtAdvanced.ReadOnly = true;
            this.txtAdvanced.Size = new System.Drawing.Size(776, 132);
            this.txtAdvanced.TabIndex = 22;
            // 
            // txtBasic2
            // 
            this.txtBasic2.Location = new System.Drawing.Point(28, 196);
            this.txtBasic2.Name = "txtBasic2";
            this.txtBasic2.Size = new System.Drawing.Size(692, 20);
            this.txtBasic2.TabIndex = 24;
            // 
            // lblOne
            // 
            this.lblOne.AutoSize = true;
            this.lblOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOne.Location = new System.Drawing.Point(9, 173);
            this.lblOne.Name = "lblOne";
            this.lblOne.Size = new System.Drawing.Size(13, 13);
            this.lblOne.TabIndex = 26;
            this.lblOne.Text = "1";
            // 
            // lblTwo
            // 
            this.lblTwo.AutoSize = true;
            this.lblTwo.Location = new System.Drawing.Point(9, 199);
            this.lblTwo.Name = "lblTwo";
            this.lblTwo.Size = new System.Drawing.Size(13, 13);
            this.lblTwo.TabIndex = 27;
            this.lblTwo.Text = "2";
            // 
            // lblToggle
            // 
            this.lblToggle.AutoSize = true;
            this.lblToggle.Location = new System.Drawing.Point(188, 63);
            this.lblToggle.Name = "lblToggle";
            this.lblToggle.Size = new System.Drawing.Size(13, 13);
            this.lblToggle.TabIndex = 28;
            this.lblToggle.Text = "1";
            // 
            // btnToggle
            // 
            this.btnToggle.Location = new System.Drawing.Point(12, 53);
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(173, 33);
            this.btnToggle.TabIndex = 29;
            this.btnToggle.Text = "Toggle Modify Sensation";
            this.btnToggle.UseVisualStyleBackColor = true;
            this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
            // 
            // btnBasic2
            // 
            this.btnBasic2.Location = new System.Drawing.Point(140, 412);
            this.btnBasic2.Name = "btnBasic2";
            this.btnBasic2.Size = new System.Drawing.Size(212, 26);
            this.btnBasic2.TabIndex = 30;
            this.btnBasic2.Text = "Basic Sensation 2";
            this.btnBasic2.UseVisualStyleBackColor = true;
            this.btnBasic2.Click += new System.EventHandler(this.btnBasic2_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(12, 92);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(173, 33);
            this.btnMerge.TabIndex = 31;
            this.btnMerge.Text = "Merge into Advanced";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 222);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(170, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Last modified advanced Sensation";
            // 
            // btnMergeDelayed
            // 
            this.btnMergeDelayed.Location = new System.Drawing.Point(12, 131);
            this.btnMergeDelayed.Name = "btnMergeDelayed";
            this.btnMergeDelayed.Size = new System.Drawing.Size(173, 33);
            this.btnMergeDelayed.TabIndex = 33;
            this.btnMergeDelayed.Text = "Merge (delayed)";
            this.btnMergeDelayed.UseVisualStyleBackColor = true;
            this.btnMergeDelayed.Click += new System.EventHandler(this.btnMergeDelayed_Click);
            // 
            // btnManualBuild
            // 
            this.btnManualBuild.Location = new System.Drawing.Point(726, 170);
            this.btnManualBuild.Name = "btnManualBuild";
            this.btnManualBuild.Size = new System.Drawing.Size(62, 46);
            this.btnManualBuild.TabIndex = 34;
            this.btnManualBuild.Text = "Manual Parse";
            this.btnManualBuild.UseVisualStyleBackColor = true;
            this.btnManualBuild.Click += new System.EventHandler(this.btnManualBuild_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnManualBuild);
            this.Controls.Add(this.btnMergeDelayed);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.btnBasic2);
            this.Controls.Add(this.btnToggle);
            this.Controls.Add(this.lblToggle);
            this.Controls.Add(this.lblTwo);
            this.Controls.Add(this.lblOne);
            this.Controls.Add(this.txtBasic2);
            this.Controls.Add(this.txtAdvanced);
            this.Controls.Add(this.txtBasic1);
            this.Controls.Add(this.btnAdvancedMuscle);
            this.Controls.Add(this.btnMuscleNew);
            this.Controls.Add(this.btnMuscleAdd);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtExit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRampDown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtRampUp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtIntensity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDuration);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFrequency);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdvanced);
            this.Controls.Add(this.btnBasic);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnBasic;
        private System.Windows.Forms.Button btnAdvanced;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtFrequency;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIntensity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtExit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRampDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRampUp;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnMuscleAdd;
        private System.Windows.Forms.Button btnMuscleNew;
        private System.Windows.Forms.Button btnAdvancedMuscle;
        private System.Windows.Forms.TextBox txtBasic1;
        private System.Windows.Forms.TextBox txtAdvanced;
        private System.Windows.Forms.TextBox txtBasic2;
        private System.Windows.Forms.Label lblOne;
        private System.Windows.Forms.Label lblTwo;
        private System.Windows.Forms.Button btnToggle;
        private System.Windows.Forms.Label lblToggle;
        private System.Windows.Forms.Button btnBasic2;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnMergeDelayed;
        private System.Windows.Forms.Button btnManualBuild;
    }
}

