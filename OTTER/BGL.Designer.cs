namespace OTTER
{
    partial class BGL
    {


        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.syncRate = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panelState = new System.Windows.Forms.Panel();
            this.lblStress = new System.Windows.Forms.Label();
            this.lblPhysical = new System.Windows.Forms.Label();
            this.lblCharisma = new System.Windows.Forms.Label();
            this.lblKnowledge = new System.Windows.Forms.Label();
            this.panelOpening = new System.Windows.Forms.Panel();
            this.panelStart = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.buttonExitGame = new System.Windows.Forms.Button();
            this.buttonLoadGame = new System.Windows.Forms.Button();
            this.panelState.SuspendLayout();
            this.panelOpening.SuspendLayout();
            this.panelStart.SuspendLayout();
            this.SuspendLayout();
            // 
            // syncRate
            // 
            this.syncRate.AutoSize = true;
            this.syncRate.BackColor = System.Drawing.Color.Transparent;
            this.syncRate.Location = new System.Drawing.Point(12, 394);
            this.syncRate.Name = "syncRate";
            this.syncRate.Size = new System.Drawing.Size(71, 52);
            this.syncRate.TabIndex = 0;
            this.syncRate.Text = "60";
            this.syncRate.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 17;
            this.timer1.Tick += new System.EventHandler(this.Update);
            // 
            // timer2
            // 
            this.timer2.Interval = 250;
            this.timer2.Tick += new System.EventHandler(this.updateFrameRate);
            // 
            // panelState
            // 
            this.panelState.Controls.Add(this.lblStress);
            this.panelState.Controls.Add(this.lblPhysical);
            this.panelState.Controls.Add(this.lblCharisma);
            this.panelState.Controls.Add(this.lblKnowledge);
            this.panelState.Location = new System.Drawing.Point(3, 491);
            this.panelState.Name = "panelState";
            this.panelState.Size = new System.Drawing.Size(550, 105);
            this.panelState.TabIndex = 5;
            this.panelState.Visible = false;
            // 
            // lblStress
            // 
            this.lblStress.AutoSize = true;
            this.lblStress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStress.Location = new System.Drawing.Point(257, 60);
            this.lblStress.Name = "lblStress";
            this.lblStress.Size = new System.Drawing.Size(64, 25);
            this.lblStress.TabIndex = 3;
            this.lblStress.Text = "label2";
            // 
            // lblPhysical
            // 
            this.lblPhysical.AutoSize = true;
            this.lblPhysical.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhysical.Location = new System.Drawing.Point(257, 10);
            this.lblPhysical.Name = "lblPhysical";
            this.lblPhysical.Size = new System.Drawing.Size(64, 25);
            this.lblPhysical.TabIndex = 2;
            this.lblPhysical.Text = "label2";
            // 
            // lblCharisma
            // 
            this.lblCharisma.AutoSize = true;
            this.lblCharisma.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharisma.Location = new System.Drawing.Point(26, 60);
            this.lblCharisma.Name = "lblCharisma";
            this.lblCharisma.Size = new System.Drawing.Size(64, 25);
            this.lblCharisma.TabIndex = 1;
            this.lblCharisma.Text = "label2";
            // 
            // lblKnowledge
            // 
            this.lblKnowledge.AutoSize = true;
            this.lblKnowledge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKnowledge.Location = new System.Drawing.Point(26, 10);
            this.lblKnowledge.Name = "lblKnowledge";
            this.lblKnowledge.Size = new System.Drawing.Size(64, 25);
            this.lblKnowledge.TabIndex = 0;
            this.lblKnowledge.Text = "label2";
            // 
            // panelOpening
            // 
            this.panelOpening.BackColor = System.Drawing.Color.Maroon;
            this.panelOpening.BackgroundImage = global::OTTER.Properties.Resources.uni_sim;
            this.panelOpening.Controls.Add(this.panelStart);
            this.panelOpening.Controls.Add(this.buttonStartGame);
            this.panelOpening.Controls.Add(this.buttonExitGame);
            this.panelOpening.Controls.Add(this.buttonLoadGame);
            this.panelOpening.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpening.Location = new System.Drawing.Point(0, 0);
            this.panelOpening.Name = "panelOpening";
            this.panelOpening.Size = new System.Drawing.Size(598, 499);
            this.panelOpening.TabIndex = 4;
            // 
            // panelStart
            // 
            this.panelStart.BackgroundImage = global::OTTER.Properties.Resources.unisim_bg;
            this.panelStart.Controls.Add(this.label5);
            this.panelStart.Controls.Add(this.label4);
            this.panelStart.Controls.Add(this.label3);
            this.panelStart.Controls.Add(this.label2);
            this.panelStart.Controls.Add(this.buttonStart);
            this.panelStart.Controls.Add(this.txtName);
            this.panelStart.Controls.Add(this.label1);
            this.panelStart.Controls.Add(this.rbFemale);
            this.panelStart.Controls.Add(this.rbMale);
            this.panelStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStart.Location = new System.Drawing.Point(0, 0);
            this.panelStart.Name = "panelStart";
            this.panelStart.Size = new System.Drawing.Size(598, 499);
            this.panelStart.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(115, 365);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(266, 35);
            this.label5.TabIndex = 8;
            this.label5.Text = "◦ To save game, click \"Q\".";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(162, 321);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(355, 35);
            this.label4.TabIndex = 7;
            this.label4.Text = "you have to touch them and click \"T\".";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(115, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(263, 35);
            this.label3.TabIndex = 6;
            this.label3.Text = "◦ To interact with objects,";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(115, 251);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(336, 35);
            this.label2.TabIndex = 5;
            this.label2.Text = "◦ For stats window, click \"Space\".";
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.ForeColor = System.Drawing.Color.Black;
            this.buttonStart.Location = new System.Drawing.Point(218, 173);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(115, 39);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "Start game";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click_1);
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Agency FB", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(218, 44);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 47);
            this.txtName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(104, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFemale.Location = new System.Drawing.Point(286, 115);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(99, 39);
            this.rbFemale.TabIndex = 1;
            this.rbFemale.TabStop = true;
            this.rbFemale.Text = "Female";
            this.rbFemale.UseVisualStyleBackColor = true;
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Checked = true;
            this.rbMale.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMale.Location = new System.Drawing.Point(128, 115);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(78, 39);
            this.rbMale.TabIndex = 0;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "Male";
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartGame.ForeColor = System.Drawing.Color.Black;
            this.buttonStartGame.Location = new System.Drawing.Point(209, 208);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(115, 39);
            this.buttonStartGame.TabIndex = 1;
            this.buttonStartGame.Text = "Start game";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonExitGame
            // 
            this.buttonExitGame.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExitGame.ForeColor = System.Drawing.Color.Black;
            this.buttonExitGame.Location = new System.Drawing.Point(209, 411);
            this.buttonExitGame.Name = "buttonExitGame";
            this.buttonExitGame.Size = new System.Drawing.Size(115, 43);
            this.buttonExitGame.TabIndex = 3;
            this.buttonExitGame.Text = "Exit Game";
            this.buttonExitGame.UseVisualStyleBackColor = true;
            this.buttonExitGame.Click += new System.EventHandler(this.buttonExitGame_Click);
            // 
            // buttonLoadGame
            // 
            this.buttonLoadGame.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoadGame.ForeColor = System.Drawing.Color.Black;
            this.buttonLoadGame.Location = new System.Drawing.Point(209, 310);
            this.buttonLoadGame.Name = "buttonLoadGame";
            this.buttonLoadGame.Size = new System.Drawing.Size(115, 43);
            this.buttonLoadGame.TabIndex = 2;
            this.buttonLoadGame.Text = "Load Game";
            this.buttonLoadGame.UseVisualStyleBackColor = true;
            this.buttonLoadGame.Click += new System.EventHandler(this.buttonLoadGame_Click);
            // 
            // BGL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(26F, 52F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(598, 499);
            this.Controls.Add(this.panelState);
            this.Controls.Add(this.panelOpening);
            this.Controls.Add(this.syncRate);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.MaximizeBox = false;
            this.Name = "BGL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UniversitySimulator";
            this.Load += new System.EventHandler(this.startTimer);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Draw);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mouseClicked);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseUp);
            this.panelState.ResumeLayout(false);
            this.panelState.PerformLayout();
            this.panelOpening.ResumeLayout(false);
            this.panelStart.ResumeLayout(false);
            this.panelStart.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label syncRate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.Button buttonLoadGame;
        private System.Windows.Forms.Button buttonExitGame;
        private System.Windows.Forms.Panel panelOpening;
        private System.Windows.Forms.Panel panelStart;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Panel panelState;
        private System.Windows.Forms.Label lblStress;
        private System.Windows.Forms.Label lblPhysical;
        private System.Windows.Forms.Label lblCharisma;
        private System.Windows.Forms.Label lblKnowledge;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
    }
}

