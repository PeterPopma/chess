
using Chess.CustomControls;

namespace Chess.Forms
{
    partial class FormMain
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
            this.displayMonogame = new Display();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownMaxMoveTime = new System.Windows.Forms.NumericUpDown();
            this.checkBoxSliding = new System.Windows.Forms.CheckBox();
            this.labelMoveTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelGameTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelMessage = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxMoveTime)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // displayMonogame
            // 
            this.displayMonogame.ActivePlayer = false;
            this.displayMonogame.Location = new System.Drawing.Point(0, 0);
            this.displayMonogame.Name = "displayMonogame";
            this.displayMonogame.ParentForm = null;
            this.displayMonogame.Size = new System.Drawing.Size(1417, 1016);
            this.displayMonogame.SlidingMoves = false;
            this.displayMonogame.TabIndex = 0;
            this.displayMonogame.Text = "displayMonogame";
            this.displayMonogame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.displayMonogame_MouseClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.numericUpDownMaxMoveTime);
            this.panel2.Controls.Add(this.checkBoxSliding);
            this.panel2.Controls.Add(this.labelMoveTime);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.labelGameTime);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.buttonUndo);
            this.panel2.Controls.Add(this.buttonReset);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(1037, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(365, 332);
            this.panel2.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Maximum move time (s)";
            // 
            // numericUpDownMaxMoveTime
            // 
            this.numericUpDownMaxMoveTime.Location = new System.Drawing.Point(271, 160);
            this.numericUpDownMaxMoveTime.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownMaxMoveTime.Name = "numericUpDownMaxMoveTime";
            this.numericUpDownMaxMoveTime.Size = new System.Drawing.Size(73, 20);
            this.numericUpDownMaxMoveTime.TabIndex = 14;
            this.numericUpDownMaxMoveTime.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownMaxMoveTime.ValueChanged += new System.EventHandler(this.numericUpDownMaxMoveTime_ValueChanged);
            // 
            // checkBoxSliding
            // 
            this.checkBoxSliding.AutoSize = true;
            this.checkBoxSliding.Location = new System.Drawing.Point(28, 161);
            this.checkBoxSliding.Name = "checkBoxSliding";
            this.checkBoxSliding.Size = new System.Drawing.Size(91, 17);
            this.checkBoxSliding.TabIndex = 13;
            this.checkBoxSliding.Text = "Sliding moves";
            this.checkBoxSliding.UseVisualStyleBackColor = true;
            this.checkBoxSliding.CheckedChanged += new System.EventHandler(this.checkBoxSliding_CheckedChanged);
            // 
            // labelMoveTime
            // 
            this.labelMoveTime.AutoSize = true;
            this.labelMoveTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMoveTime.Location = new System.Drawing.Point(140, 102);
            this.labelMoveTime.Name = "labelMoveTime";
            this.labelMoveTime.Size = new System.Drawing.Size(66, 24);
            this.labelMoveTime.TabIndex = 12;
            this.labelMoveTime.Text = "XXXX";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Move time:";
            // 
            // labelGameTime
            // 
            this.labelGameTime.AutoSize = true;
            this.labelGameTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGameTime.Location = new System.Drawing.Point(140, 68);
            this.labelGameTime.Name = "labelGameTime";
            this.labelGameTime.Size = new System.Drawing.Size(66, 24);
            this.labelGameTime.TabIndex = 10;
            this.labelGameTime.Text = "XXXX";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "Game time:";
            // 
            // buttonUndo
            // 
            this.buttonUndo.Location = new System.Drawing.Point(200, 15);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(144, 38);
            this.buttonUndo.TabIndex = 8;
            this.buttonUndo.Text = "Undo last move";
            this.buttonUndo.UseVisualStyleBackColor = true;
            this.buttonUndo.Click += new System.EventHandler(this.buttonUndo_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(20, 15);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(144, 38);
            this.buttonReset.TabIndex = 6;
            this.buttonReset.Text = "Reset game";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.labelMessage);
            this.panel1.Location = new System.Drawing.Point(20, 216);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(324, 94);
            this.panel1.TabIndex = 7;
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMessage.Location = new System.Drawing.Point(22, 17);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(167, 51);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.Text = "XXXXX";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1419, 1015);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.displayMonogame);
            this.Name = "FormMain";
            this.Text = "Chess v1.0 (c) P.Popma 2018";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxMoveTime)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.Display displayMonogame;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Label labelGameTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonUndo;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label labelMessage;
        internal System.Windows.Forms.Label labelMoveTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxMoveTime;
        private System.Windows.Forms.CheckBox checkBoxSliding;

        public Display DisplayMonogame { get => displayMonogame; set => displayMonogame = value; }
    }
}