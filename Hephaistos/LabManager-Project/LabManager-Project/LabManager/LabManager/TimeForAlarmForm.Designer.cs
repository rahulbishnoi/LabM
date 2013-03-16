namespace LabManager
{
    partial class TimeForAlarmForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeForAlarmForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.c1TrueDBGridTimeForAlarm = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGridTimeForAlarm)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Error.ico");
            this.imageList1.Images.SetKeyName(1, "warning.ico");
            // 
            // c1TrueDBGridTimeForAlarm
            // 
            this.c1TrueDBGridTimeForAlarm.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.c1TrueDBGridTimeForAlarm.AllowUpdate = false;
            this.c1TrueDBGridTimeForAlarm.AlternatingRows = true;
            this.c1TrueDBGridTimeForAlarm.BackColor = System.Drawing.SystemColors.Control;
            this.c1TrueDBGridTimeForAlarm.Caption = "Warnings/Alarms ";
            this.c1TrueDBGridTimeForAlarm.ExtendRightColumn = true;
            this.c1TrueDBGridTimeForAlarm.FetchRowStyles = true;
            this.c1TrueDBGridTimeForAlarm.Images.Add(((System.Drawing.Image)(resources.GetObject("c1TrueDBGridTimeForAlarm.Images"))));
            this.c1TrueDBGridTimeForAlarm.Images.Add(((System.Drawing.Image)(resources.GetObject("c1TrueDBGridTimeForAlarm.Images1"))));
            this.c1TrueDBGridTimeForAlarm.Images.Add(((System.Drawing.Image)(resources.GetObject("c1TrueDBGridTimeForAlarm.Images2"))));
            this.c1TrueDBGridTimeForAlarm.Images.Add(((System.Drawing.Image)(resources.GetObject("c1TrueDBGridTimeForAlarm.Images3"))));
            this.c1TrueDBGridTimeForAlarm.Location = new System.Drawing.Point(12, 12);
            this.c1TrueDBGridTimeForAlarm.Name = "c1TrueDBGridTimeForAlarm";
            this.c1TrueDBGridTimeForAlarm.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1TrueDBGridTimeForAlarm.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1TrueDBGridTimeForAlarm.PreviewInfo.ZoomFactor = 75D;
            this.c1TrueDBGridTimeForAlarm.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("c1TrueDBGridTimeForAlarm.PrintInfo.PageSettings")));
            this.c1TrueDBGridTimeForAlarm.PropBag = resources.GetString("c1TrueDBGridTimeForAlarm.PropBag");
            this.c1TrueDBGridTimeForAlarm.RecordSelectors = false;
            this.c1TrueDBGridTimeForAlarm.Size = new System.Drawing.Size(532, 269);
            this.c1TrueDBGridTimeForAlarm.TabIndex = 19;
            this.c1TrueDBGridTimeForAlarm.Text = "c1TrueDBGrid3";
            this.c1TrueDBGridTimeForAlarm.FetchCellStyle += new C1.Win.C1TrueDBGrid.FetchCellStyleEventHandler(this.c1TrueDBGridTimeForAlarm_FetchCellStyle);
            this.c1TrueDBGridTimeForAlarm.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.c1TrueDBGridTimeForAlarm_FetchRowStyle_1);
            // 
            // TimeForAlarmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 293);
            this.Controls.Add(this.c1TrueDBGridTimeForAlarm);
            this.Name = "TimeForAlarmForm";
            this.Text = "TimeForAlarmForm";
            this.Load += new System.EventHandler(this.TimeForAlarmForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGridTimeForAlarm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1TrueDBGridTimeForAlarm;
    }
}