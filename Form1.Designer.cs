using System;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace NetworkMonitor
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private MaterialSkin.Controls.MaterialListView listViewProcesses;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderReceived;
        private System.Windows.Forms.ColumnHeader columnHeaderSent;
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
        private MaterialSkin.Controls.MaterialButton btnRefresh;
        private MaterialSkin.Controls.MaterialButton btnBlock;
        private MaterialSkin.Controls.MaterialButton btnUnblock;
        private MaterialSkin.Controls.MaterialButton btnUnblockAll;
        private MaterialSkin.Controls.MaterialButton btnToggleTheme;
        private Label lblTotalUpload;
        private Label lblTotalDownload;
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label lblDeveloperInfo;
        private ImageList imageList; // إضافة ImageList لعرض الأيقونات

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listViewProcesses = new MaterialSkin.Controls.MaterialListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderReceived = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnRefresh = new MaterialSkin.Controls.MaterialButton();
            this.btnBlock = new MaterialSkin.Controls.MaterialButton();
            this.btnUnblock = new MaterialSkin.Controls.MaterialButton();
            this.btnUnblockAll = new MaterialSkin.Controls.MaterialButton();
            this.lblTotalUpload = new System.Windows.Forms.Label();
            this.lblTotalDownload = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblDeveloperInfo = new System.Windows.Forms.Label();
            this.btnToggleTheme = new MaterialSkin.Controls.MaterialButton();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(12, 70);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(782, 563);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listViewProcesses);
            this.tabPage1.Controls.Add(this.lblTotalUpload);
            this.tabPage1.Controls.Add(this.lblTotalDownload);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(774, 537);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Processes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listViewProcesses
            // 
            this.listViewProcesses.AllowColumnReorder = true;
            this.listViewProcesses.AutoSizeTable = false;
            this.listViewProcesses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listViewProcesses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewProcesses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderReceived,
            this.columnHeaderSent,
            this.columnHeaderStatus});
            this.listViewProcesses.Depth = 0;
            this.listViewProcesses.FullRowSelect = true;
            this.listViewProcesses.HideSelection = false;
            this.listViewProcesses.Location = new System.Drawing.Point(6, 6);
            this.listViewProcesses.MinimumSize = new System.Drawing.Size(200, 100);
            this.listViewProcesses.MouseLocation = new System.Drawing.Point(-1, -1);
            this.listViewProcesses.MouseState = MaterialSkin.MouseState.OUT;
            this.listViewProcesses.Name = "listViewProcesses";
            this.listViewProcesses.OwnerDraw = true;
            this.listViewProcesses.Size = new System.Drawing.Size(764, 522);
            this.listViewProcesses.SmallImageList = this.imageList;
            this.listViewProcesses.TabIndex = 0;
            this.listViewProcesses.UseCompatibleStateImageBehavior = false;
            this.listViewProcesses.View = System.Windows.Forms.View.Details;
            this.listViewProcesses.SelectedIndexChanged += new System.EventHandler(this.listViewProcesses_SelectedIndexChanged);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Process Name";
            this.columnHeaderName.Width = 200;
            // 
            // columnHeaderReceived
            // 
            this.columnHeaderReceived.Text = "Bytes Received";
            this.columnHeaderReceived.Width = 200;
            // 
            // columnHeaderSent
            // 
            this.columnHeaderSent.Text = "Bytes Sent";
            this.columnHeaderSent.Width = 200;
            // 
            // columnHeaderStatus
            // 
            this.columnHeaderStatus.Text = "Status";
            this.columnHeaderStatus.Width = 150;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnRefresh
            // 
            this.btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefresh.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnRefresh.Depth = 0;
            this.btnRefresh.HighEmphasis = true;
            this.btnRefresh.Icon = null;
            this.btnRefresh.Location = new System.Drawing.Point(16, 642);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnRefresh.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnRefresh.Size = new System.Drawing.Size(84, 36);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnRefresh.UseAccentColor = false;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnBlock
            // 
            this.btnBlock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBlock.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnBlock.Depth = 0;
            this.btnBlock.HighEmphasis = true;
            this.btnBlock.Icon = null;
            this.btnBlock.Location = new System.Drawing.Point(233, 642);
            this.btnBlock.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnBlock.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnBlock.Name = "btnBlock";
            this.btnBlock.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnBlock.Size = new System.Drawing.Size(68, 36);
            this.btnBlock.TabIndex = 2;
            this.btnBlock.Text = "Block";
            this.btnBlock.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnBlock.UseAccentColor = false;
            this.btnBlock.UseVisualStyleBackColor = true;
            this.btnBlock.Click += new System.EventHandler(this.btnBlock_Click);
            // 
            // btnUnblock
            // 
            this.btnUnblock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUnblock.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnUnblock.Depth = 0;
            this.btnUnblock.HighEmphasis = true;
            this.btnUnblock.Icon = null;
            this.btnUnblock.Location = new System.Drawing.Point(309, 642);
            this.btnUnblock.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnUnblock.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnUnblock.Name = "btnUnblock";
            this.btnUnblock.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnUnblock.Size = new System.Drawing.Size(88, 36);
            this.btnUnblock.TabIndex = 3;
            this.btnUnblock.Text = "Unblock";
            this.btnUnblock.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnUnblock.UseAccentColor = false;
            this.btnUnblock.UseVisualStyleBackColor = true;
            this.btnUnblock.Click += new System.EventHandler(this.btnUnblock_Click);
            // 
            // btnUnblockAll
            // 
            this.btnUnblockAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUnblockAll.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnUnblockAll.Depth = 0;
            this.btnUnblockAll.HighEmphasis = true;
            this.btnUnblockAll.Icon = null;
            this.btnUnblockAll.Location = new System.Drawing.Point(405, 642);
            this.btnUnblockAll.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnUnblockAll.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnUnblockAll.Name = "btnUnblockAll";
            this.btnUnblockAll.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnUnblockAll.Size = new System.Drawing.Size(117, 36);
            this.btnUnblockAll.TabIndex = 4;
            this.btnUnblockAll.Text = "Unblock All";
            this.btnUnblockAll.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnUnblockAll.UseAccentColor = false;
            this.btnUnblockAll.UseVisualStyleBackColor = true;
            this.btnUnblockAll.Click += new System.EventHandler(this.btnUnblockAll_Click);
            // 
            // lblTotalUpload
            // 
            this.lblTotalUpload.AutoSize = true;
            this.lblTotalUpload.Location = new System.Drawing.Point(523, 28);
            this.lblTotalUpload.Name = "lblTotalUpload";
            this.lblTotalUpload.Size = new System.Drawing.Size(90, 13);
            this.lblTotalUpload.TabIndex = 6;
            this.lblTotalUpload.Text = "Total Upload: 0 B";
            // 
            // lblTotalDownload
            // 
            this.lblTotalDownload.AutoSize = true;
            this.lblTotalDownload.Location = new System.Drawing.Point(523, 53);
            this.lblTotalDownload.Name = "lblTotalDownload";
            this.lblTotalDownload.Size = new System.Drawing.Size(104, 13);
            this.lblTotalDownload.TabIndex = 7;
            this.lblTotalDownload.Text = "Total Download: 0 B";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblDeveloperInfo);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 537);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "About";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblDeveloperInfo
            // 
            this.lblDeveloperInfo.AutoSize = true;
            this.lblDeveloperInfo.Location = new System.Drawing.Point(23, 23);
            this.lblDeveloperInfo.Name = "lblDeveloperInfo";
            this.lblDeveloperInfo.Size = new System.Drawing.Size(158, 39);
            this.lblDeveloperInfo.TabIndex = 0;
            this.lblDeveloperInfo.Text = "Developer: Baseet\r\nDiscord: .Baseet\r\nVersion: 1.0.0";
            this.lblDeveloperInfo.Click += new System.EventHandler(this.lblDeveloperInfo_Click);
            // 
            // btnToggleTheme
            // 
            this.btnToggleTheme.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnToggleTheme.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnToggleTheme.Depth = 0;
            this.btnToggleTheme.HighEmphasis = true;
            this.btnToggleTheme.Icon = null;
            this.btnToggleTheme.Location = new System.Drawing.Point(686, 642);
            this.btnToggleTheme.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnToggleTheme.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnToggleTheme.Name = "btnToggleTheme";
            this.btnToggleTheme.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnToggleTheme.Size = new System.Drawing.Size(104, 36);
            this.btnToggleTheme.TabIndex = 5;
            this.btnToggleTheme.Text = "Dark Mode";
            this.btnToggleTheme.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnToggleTheme.UseAccentColor = false;
            this.btnToggleTheme.UseVisualStyleBackColor = true;
            this.btnToggleTheme.Click += new System.EventHandler(this.btnToggleTheme_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 687);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnToggleTheme);
            this.Controls.Add(this.btnUnblockAll);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUnblock);
            this.Controls.Add(this.btnBlock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Network Monitor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
