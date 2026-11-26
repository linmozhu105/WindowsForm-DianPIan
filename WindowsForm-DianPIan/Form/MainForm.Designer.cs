namespace WindowsForm_DianPIan
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpZongTi = new System.Windows.Forms.TableLayoutPanel();
            this.tlpZuoCe = new System.Windows.Forms.TableLayoutPanel();
            this.tlpShuangTu = new System.Windows.Forms.TableLayoutPanel();
            this.lblYuanTu = new System.Windows.Forms.Label();
            this.lblChuLiTu = new System.Windows.Forms.Label();
            this.crdYuanTu = new Cognex.VisionPro.CogRecordDisplay();
            this.crdChuLiTu = new Cognex.VisionPro.CogRecordDisplay();
            this.dgvJianCeJiLu = new System.Windows.Forms.DataGridView();
            this.colShiJian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLeiXing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJieGuo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBeiZhu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpYouCe = new System.Windows.Forms.TableLayoutPanel();
            this.grpGongNengQu = new System.Windows.Forms.GroupBox();
            this.flpAnNiu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRuanChuFa = new System.Windows.Forms.Button();
            this.btnZhiXing = new System.Windows.Forms.Button();
            this.btnXuanZeTuXiang = new System.Windows.Forms.Button();
            this.grpJianCeJieGuo = new System.Windows.Forms.GroupBox();
            this.tlpJieGuo = new System.Windows.Forms.TableLayoutPanel();
            this.lblOK = new System.Windows.Forms.Label();
            this.txtOK = new System.Windows.Forms.TextBox();
            this.lblNG = new System.Windows.Forms.Label();
            this.txtNG = new System.Windows.Forms.TextBox();
            this.lblLiangPinLv = new System.Windows.Forms.Label();
            this.txtLiangPinLv = new System.Windows.Forms.TextBox();
            this.lblZongShu = new System.Windows.Forms.Label();
            this.txtZongShu = new System.Windows.Forms.TextBox();
            this.btnQingLing = new System.Windows.Forms.Button();
            this.btnJiaZaiShuJu = new System.Windows.Forms.Button();
            this.grpRiZhi = new System.Windows.Forms.GroupBox();
            this.lstRiZhi = new System.Windows.Forms.ListBox();
            this.mnsCaiDan = new System.Windows.Forms.MenuStrip();
            this.tsmiBianJiZuoYe = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCeLiangDianPian = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShuJu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiXiuGaiBaoCunShuJu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiXiangJi = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiXiuGaiXiangJiCanShu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiJiZhun = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSheZhiJiZhunDian = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBiaoDing = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSheZhiYinDaoBiaoDing = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTongXin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiXiuGaiTongXinFangShi = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpZongTi.SuspendLayout();
            this.tlpZuoCe.SuspendLayout();
            this.tlpShuangTu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crdYuanTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.crdChuLiTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJianCeJiLu)).BeginInit();
            this.tlpYouCe.SuspendLayout();
            this.grpGongNengQu.SuspendLayout();
            this.flpAnNiu.SuspendLayout();
            this.grpJianCeJieGuo.SuspendLayout();
            this.tlpJieGuo.SuspendLayout();
            this.grpRiZhi.SuspendLayout();
            this.mnsCaiDan.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpZongTi
            // 
            this.tlpZongTi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.tlpZongTi.ColumnCount = 2;
            this.tlpZongTi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.tlpZongTi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.tlpZongTi.Controls.Add(this.tlpZuoCe, 0, 0);
            this.tlpZongTi.Controls.Add(this.tlpYouCe, 1, 0);
            this.tlpZongTi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpZongTi.Location = new System.Drawing.Point(0, 36);
            this.tlpZongTi.Margin = new System.Windows.Forms.Padding(0);
            this.tlpZongTi.Name = "tlpZongTi";
            this.tlpZongTi.RowCount = 1;
            this.tlpZongTi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpZongTi.Size = new System.Drawing.Size(1340, 714);
            this.tlpZongTi.TabIndex = 0;
            // 
            // tlpZuoCe
            // 
            this.tlpZuoCe.ColumnCount = 1;
            this.tlpZuoCe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpZuoCe.Controls.Add(this.tlpShuangTu, 0, 0);
            this.tlpZuoCe.Controls.Add(this.dgvJianCeJiLu, 0, 1);
            this.tlpZuoCe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpZuoCe.Location = new System.Drawing.Point(10, 10);
            this.tlpZuoCe.Margin = new System.Windows.Forms.Padding(10, 10, 5, 10);
            this.tlpZuoCe.Name = "tlpZuoCe";
            this.tlpZuoCe.RowCount = 2;
            this.tlpZuoCe.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.tlpZuoCe.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tlpZuoCe.Size = new System.Drawing.Size(815, 694);
            this.tlpZuoCe.TabIndex = 0;
            this.tlpZuoCe.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpZuoCe_Paint);
            // 
            // tlpShuangTu
            // 
            this.tlpShuangTu.ColumnCount = 2;
            this.tlpShuangTu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpShuangTu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpShuangTu.Controls.Add(this.lblYuanTu, 0, 0);
            this.tlpShuangTu.Controls.Add(this.lblChuLiTu, 1, 0);
            this.tlpShuangTu.Controls.Add(this.crdYuanTu, 0, 1);
            this.tlpShuangTu.Controls.Add(this.crdChuLiTu, 1, 1);
            this.tlpShuangTu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpShuangTu.Location = new System.Drawing.Point(0, 0);
            this.tlpShuangTu.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.tlpShuangTu.Name = "tlpShuangTu";
            this.tlpShuangTu.RowCount = 2;
            this.tlpShuangTu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpShuangTu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpShuangTu.Size = new System.Drawing.Size(815, 436);
            this.tlpShuangTu.TabIndex = 1;
            // 
            // lblYuanTu
            // 
            this.lblYuanTu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYuanTu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblYuanTu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblYuanTu.Location = new System.Drawing.Point(2, 0);
            this.lblYuanTu.Margin = new System.Windows.Forms.Padding(2, 0, 6, 0);
            this.lblYuanTu.Name = "lblYuanTu";
            this.lblYuanTu.Size = new System.Drawing.Size(399, 30);
            this.lblYuanTu.TabIndex = 0;
            this.lblYuanTu.Text = "实时采集图像";
            this.lblYuanTu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblChuLiTu
            // 
            this.lblChuLiTu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChuLiTu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblChuLiTu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblChuLiTu.Location = new System.Drawing.Point(413, 0);
            this.lblChuLiTu.Margin = new System.Windows.Forms.Padding(6, 0, 2, 0);
            this.lblChuLiTu.Name = "lblChuLiTu";
            this.lblChuLiTu.Size = new System.Drawing.Size(400, 30);
            this.lblChuLiTu.TabIndex = 1;
            this.lblChuLiTu.Text = "处理结果图像";
            this.lblChuLiTu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // crdYuanTu
            // 
            this.crdYuanTu.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.crdYuanTu.ColorMapLowerRoiLimit = 0D;
            this.crdYuanTu.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.crdYuanTu.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.crdYuanTu.ColorMapUpperRoiLimit = 1D;
            this.crdYuanTu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crdYuanTu.DoubleTapZoomCycleLength = 2;
            this.crdYuanTu.DoubleTapZoomSensitivity = 2.5D;
            this.crdYuanTu.Location = new System.Drawing.Point(0, 30);
            this.crdYuanTu.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.crdYuanTu.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.crdYuanTu.MouseWheelSensitivity = 1D;
            this.crdYuanTu.Name = "crdYuanTu";
            this.crdYuanTu.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("crdYuanTu.OcxState")));
            this.crdYuanTu.Size = new System.Drawing.Size(401, 406);
            this.crdYuanTu.TabIndex = 2;
            // 
            // crdChuLiTu
            // 
            this.crdChuLiTu.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.crdChuLiTu.ColorMapLowerRoiLimit = 0D;
            this.crdChuLiTu.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.crdChuLiTu.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.crdChuLiTu.ColorMapUpperRoiLimit = 1D;
            this.crdChuLiTu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crdChuLiTu.DoubleTapZoomCycleLength = 2;
            this.crdChuLiTu.DoubleTapZoomSensitivity = 2.5D;
            this.crdChuLiTu.Location = new System.Drawing.Point(413, 30);
            this.crdChuLiTu.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.crdChuLiTu.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.crdChuLiTu.MouseWheelSensitivity = 1D;
            this.crdChuLiTu.Name = "crdChuLiTu";
            this.crdChuLiTu.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("crdChuLiTu.OcxState")));
            this.crdChuLiTu.Size = new System.Drawing.Size(402, 406);
            this.crdChuLiTu.TabIndex = 3;
            this.crdChuLiTu.Enter += new System.EventHandler(this.crdChuLiTu_Enter);
            // 
            // dgvJianCeJiLu
            // 
            this.dgvJianCeJiLu.AllowUserToAddRows = false;
            this.dgvJianCeJiLu.AllowUserToDeleteRows = false;
            this.dgvJianCeJiLu.AllowUserToResizeRows = false;
            this.dgvJianCeJiLu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvJianCeJiLu.BackgroundColor = System.Drawing.Color.White;
            this.dgvJianCeJiLu.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvJianCeJiLu.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvJianCeJiLu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvJianCeJiLu.ColumnHeadersHeight = 36;
            this.dgvJianCeJiLu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvJianCeJiLu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colShiJian,
            this.colLeiXing,
            this.colJieGuo,
            this.colBeiZhu});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvJianCeJiLu.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvJianCeJiLu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvJianCeJiLu.EnableHeadersVisualStyles = false;
            this.dgvJianCeJiLu.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.dgvJianCeJiLu.Location = new System.Drawing.Point(0, 444);
            this.dgvJianCeJiLu.Margin = new System.Windows.Forms.Padding(0);
            this.dgvJianCeJiLu.MultiSelect = false;
            this.dgvJianCeJiLu.Name = "dgvJianCeJiLu";
            this.dgvJianCeJiLu.RowHeadersVisible = false;
            this.dgvJianCeJiLu.RowHeadersWidth = 51;
            this.dgvJianCeJiLu.RowTemplate.Height = 32;
            this.dgvJianCeJiLu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJianCeJiLu.Size = new System.Drawing.Size(815, 250);
            this.dgvJianCeJiLu.TabIndex = 4;
            // 
            // colShiJian
            // 
            this.colShiJian.DataPropertyName = "Time";
            this.colShiJian.HeaderText = "时间";
            this.colShiJian.MinimumWidth = 6;
            this.colShiJian.Name = "colShiJian";
            // 
            // colLeiXing
            // 
            this.colLeiXing.DataPropertyName = "CsvCheckType";
            this.colLeiXing.HeaderText = "检测类型";
            this.colLeiXing.MinimumWidth = 6;
            this.colLeiXing.Name = "colLeiXing";
            // 
            // colJieGuo
            // 
            this.colJieGuo.DataPropertyName = "DataResult";
            this.colJieGuo.HeaderText = "结果";
            this.colJieGuo.MinimumWidth = 6;
            this.colJieGuo.Name = "colJieGuo";
            // 
            // colBeiZhu
            // 
            this.colBeiZhu.DataPropertyName = "Mark";
            this.colBeiZhu.HeaderText = "备注";
            this.colBeiZhu.MinimumWidth = 6;
            this.colBeiZhu.Name = "colBeiZhu";
            // 
            // tlpYouCe
            // 
            this.tlpYouCe.ColumnCount = 1;
            this.tlpYouCe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpYouCe.Controls.Add(this.grpGongNengQu, 0, 0);
            this.tlpYouCe.Controls.Add(this.grpJianCeJieGuo, 0, 1);
            this.tlpYouCe.Controls.Add(this.grpRiZhi, 0, 2);
            this.tlpYouCe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpYouCe.Location = new System.Drawing.Point(835, 10);
            this.tlpYouCe.Margin = new System.Windows.Forms.Padding(5, 10, 10, 10);
            this.tlpYouCe.Name = "tlpYouCe";
            this.tlpYouCe.RowCount = 3;
            this.tlpYouCe.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tlpYouCe.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 176F));
            this.tlpYouCe.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpYouCe.Size = new System.Drawing.Size(495, 694);
            this.tlpYouCe.TabIndex = 1;
            // 
            // grpGongNengQu
            // 
            this.grpGongNengQu.BackColor = System.Drawing.Color.White;
            this.grpGongNengQu.Controls.Add(this.flpAnNiu);
            this.grpGongNengQu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGongNengQu.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpGongNengQu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.grpGongNengQu.Location = new System.Drawing.Point(0, 0);
            this.grpGongNengQu.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.grpGongNengQu.Name = "grpGongNengQu";
            this.grpGongNengQu.Padding = new System.Windows.Forms.Padding(12, 6, 12, 10);
            this.grpGongNengQu.Size = new System.Drawing.Size(495, 96);
            this.grpGongNengQu.TabIndex = 0;
            this.grpGongNengQu.TabStop = false;
            this.grpGongNengQu.Text = "功能区";
            // 
            // flpAnNiu
            // 
            this.flpAnNiu.Controls.Add(this.btnRuanChuFa);
            this.flpAnNiu.Controls.Add(this.btnZhiXing);
            this.flpAnNiu.Controls.Add(this.btnXuanZeTuXiang);
            this.flpAnNiu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAnNiu.Location = new System.Drawing.Point(12, 30);
            this.flpAnNiu.Margin = new System.Windows.Forms.Padding(0);
            this.flpAnNiu.Name = "flpAnNiu";
            this.flpAnNiu.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.flpAnNiu.Size = new System.Drawing.Size(471, 56);
            this.flpAnNiu.TabIndex = 0;
            this.flpAnNiu.WrapContents = false;
            // 
            // btnRuanChuFa
            // 
            this.btnRuanChuFa.BackColor = System.Drawing.Color.White;
            this.btnRuanChuFa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRuanChuFa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnRuanChuFa.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(233)))), ((int)(((byte)(255)))));
            this.btnRuanChuFa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.btnRuanChuFa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRuanChuFa.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRuanChuFa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnRuanChuFa.Location = new System.Drawing.Point(2, 4);
            this.btnRuanChuFa.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnRuanChuFa.Name = "btnRuanChuFa";
            this.btnRuanChuFa.Size = new System.Drawing.Size(146, 42);
            this.btnRuanChuFa.TabIndex = 0;
            this.btnRuanChuFa.Text = "软触发";
            this.btnRuanChuFa.UseVisualStyleBackColor = false;
            // 
            // btnZhiXing
            // 
            this.btnZhiXing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnZhiXing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZhiXing.FlatAppearance.BorderSize = 0;
            this.btnZhiXing.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(88)))), ((int)(((byte)(200)))));
            this.btnZhiXing.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.btnZhiXing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZhiXing.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnZhiXing.ForeColor = System.Drawing.Color.White;
            this.btnZhiXing.Location = new System.Drawing.Point(158, 4);
            this.btnZhiXing.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnZhiXing.Name = "btnZhiXing";
            this.btnZhiXing.Size = new System.Drawing.Size(146, 42);
            this.btnZhiXing.TabIndex = 1;
            this.btnZhiXing.Text = "执行";
            this.btnZhiXing.UseVisualStyleBackColor = false;
            this.btnZhiXing.Click += new System.EventHandler(this.btnZhiXing_Click);
            // 
            // btnXuanZeTuXiang
            // 
            this.btnXuanZeTuXiang.BackColor = System.Drawing.Color.White;
            this.btnXuanZeTuXiang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuanZeTuXiang.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnXuanZeTuXiang.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(233)))), ((int)(((byte)(255)))));
            this.btnXuanZeTuXiang.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.btnXuanZeTuXiang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuanZeTuXiang.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnXuanZeTuXiang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnXuanZeTuXiang.Location = new System.Drawing.Point(314, 4);
            this.btnXuanZeTuXiang.Margin = new System.Windows.Forms.Padding(0);
            this.btnXuanZeTuXiang.Name = "btnXuanZeTuXiang";
            this.btnXuanZeTuXiang.Size = new System.Drawing.Size(146, 42);
            this.btnXuanZeTuXiang.TabIndex = 2;
            this.btnXuanZeTuXiang.Text = "选择图像";
            this.btnXuanZeTuXiang.UseVisualStyleBackColor = false;
            this.btnXuanZeTuXiang.Click += new System.EventHandler(this.btnXuanZeTuXiang_Click);
            // 
            // grpJianCeJieGuo
            // 
            this.grpJianCeJieGuo.BackColor = System.Drawing.Color.White;
            this.grpJianCeJieGuo.Controls.Add(this.tlpJieGuo);
            this.grpJianCeJieGuo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpJianCeJieGuo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpJianCeJieGuo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.grpJianCeJieGuo.Location = new System.Drawing.Point(0, 104);
            this.grpJianCeJieGuo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.grpJianCeJieGuo.Name = "grpJianCeJieGuo";
            this.grpJianCeJieGuo.Padding = new System.Windows.Forms.Padding(12, 6, 12, 12);
            this.grpJianCeJieGuo.Size = new System.Drawing.Size(495, 168);
            this.grpJianCeJieGuo.TabIndex = 1;
            this.grpJianCeJieGuo.TabStop = false;
            this.grpJianCeJieGuo.Text = "检测结果";
            // 
            // tlpJieGuo
            // 
            this.tlpJieGuo.ColumnCount = 4;
            this.tlpJieGuo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tlpJieGuo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpJieGuo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tlpJieGuo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpJieGuo.Controls.Add(this.lblOK, 0, 0);
            this.tlpJieGuo.Controls.Add(this.txtOK, 1, 0);
            this.tlpJieGuo.Controls.Add(this.lblNG, 2, 0);
            this.tlpJieGuo.Controls.Add(this.txtNG, 3, 0);
            this.tlpJieGuo.Controls.Add(this.lblLiangPinLv, 0, 1);
            this.tlpJieGuo.Controls.Add(this.txtLiangPinLv, 1, 1);
            this.tlpJieGuo.Controls.Add(this.lblZongShu, 2, 1);
            this.tlpJieGuo.Controls.Add(this.txtZongShu, 3, 1);
            this.tlpJieGuo.Controls.Add(this.btnQingLing, 3, 2);
            this.tlpJieGuo.Controls.Add(this.btnJiaZaiShuJu, 1, 2);
            this.tlpJieGuo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpJieGuo.Location = new System.Drawing.Point(12, 30);
            this.tlpJieGuo.Margin = new System.Windows.Forms.Padding(0);
            this.tlpJieGuo.Name = "tlpJieGuo";
            this.tlpJieGuo.RowCount = 3;
            this.tlpJieGuo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpJieGuo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpJieGuo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpJieGuo.Size = new System.Drawing.Size(471, 126);
            this.tlpJieGuo.TabIndex = 0;
            // 
            // lblOK
            // 
            this.lblOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOK.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblOK.Location = new System.Drawing.Point(0, 0);
            this.lblOK.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(60, 46);
            this.lblOK.TabIndex = 0;
            this.lblOK.Text = "OK：";
            this.lblOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOK
            // 
            this.txtOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOK.BackColor = System.Drawing.Color.White;
            this.txtOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOK.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.txtOK.Location = new System.Drawing.Point(70, 7);
            this.txtOK.Margin = new System.Windows.Forms.Padding(6, 0, 10, 0);
            this.txtOK.Name = "txtOK";
            this.txtOK.ReadOnly = true;
            this.txtOK.Size = new System.Drawing.Size(155, 32);
            this.txtOK.TabIndex = 1;
            this.txtOK.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNG
            // 
            this.lblNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNG.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblNG.Location = new System.Drawing.Point(241, 0);
            this.lblNG.Margin = new System.Windows.Forms.Padding(6, 0, 4, 0);
            this.lblNG.Name = "lblNG";
            this.lblNG.Size = new System.Drawing.Size(54, 46);
            this.lblNG.TabIndex = 2;
            this.lblNG.Text = "NG：";
            this.lblNG.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNG
            // 
            this.txtNG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNG.BackColor = System.Drawing.Color.White;
            this.txtNG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNG.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.txtNG.Location = new System.Drawing.Point(305, 7);
            this.txtNG.Margin = new System.Windows.Forms.Padding(6, 0, 4, 0);
            this.txtNG.Name = "txtNG";
            this.txtNG.ReadOnly = true;
            this.txtNG.Size = new System.Drawing.Size(162, 32);
            this.txtNG.TabIndex = 3;
            this.txtNG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblLiangPinLv
            // 
            this.lblLiangPinLv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLiangPinLv.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLiangPinLv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblLiangPinLv.Location = new System.Drawing.Point(0, 46);
            this.lblLiangPinLv.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.lblLiangPinLv.Name = "lblLiangPinLv";
            this.lblLiangPinLv.Size = new System.Drawing.Size(60, 46);
            this.lblLiangPinLv.TabIndex = 4;
            this.lblLiangPinLv.Text = "良品率：";
            this.lblLiangPinLv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLiangPinLv
            // 
            this.txtLiangPinLv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLiangPinLv.BackColor = System.Drawing.Color.White;
            this.txtLiangPinLv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLiangPinLv.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLiangPinLv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.txtLiangPinLv.Location = new System.Drawing.Point(70, 53);
            this.txtLiangPinLv.Margin = new System.Windows.Forms.Padding(6, 0, 10, 0);
            this.txtLiangPinLv.Name = "txtLiangPinLv";
            this.txtLiangPinLv.ReadOnly = true;
            this.txtLiangPinLv.Size = new System.Drawing.Size(155, 32);
            this.txtLiangPinLv.TabIndex = 5;
            this.txtLiangPinLv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblZongShu
            // 
            this.lblZongShu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblZongShu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblZongShu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblZongShu.Location = new System.Drawing.Point(241, 46);
            this.lblZongShu.Margin = new System.Windows.Forms.Padding(6, 0, 4, 0);
            this.lblZongShu.Name = "lblZongShu";
            this.lblZongShu.Size = new System.Drawing.Size(54, 46);
            this.lblZongShu.TabIndex = 6;
            this.lblZongShu.Text = "总数：";
            this.lblZongShu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtZongShu
            // 
            this.txtZongShu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZongShu.BackColor = System.Drawing.Color.White;
            this.txtZongShu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtZongShu.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtZongShu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.txtZongShu.Location = new System.Drawing.Point(305, 53);
            this.txtZongShu.Margin = new System.Windows.Forms.Padding(6, 0, 4, 0);
            this.txtZongShu.Name = "txtZongShu";
            this.txtZongShu.ReadOnly = true;
            this.txtZongShu.Size = new System.Drawing.Size(162, 32);
            this.txtZongShu.TabIndex = 7;
            this.txtZongShu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnQingLing
            // 
            this.btnQingLing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQingLing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(251)))));
            this.btnQingLing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQingLing.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.btnQingLing.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(236)))), ((int)(((byte)(241)))));
            this.btnQingLing.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnQingLing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQingLing.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQingLing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnQingLing.Location = new System.Drawing.Point(371, 98);
            this.btnQingLing.Margin = new System.Windows.Forms.Padding(0, 6, 4, 0);
            this.btnQingLing.Name = "btnQingLing";
            this.btnQingLing.Size = new System.Drawing.Size(96, 28);
            this.btnQingLing.TabIndex = 8;
            this.btnQingLing.Text = "清零";
            this.btnQingLing.UseVisualStyleBackColor = false;
            // 
            // btnJiaZaiShuJu
            // 
            this.btnJiaZaiShuJu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJiaZaiShuJu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(251)))));
            this.btnJiaZaiShuJu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJiaZaiShuJu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.btnJiaZaiShuJu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(236)))), ((int)(((byte)(241)))));
            this.btnJiaZaiShuJu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnJiaZaiShuJu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJiaZaiShuJu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnJiaZaiShuJu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnJiaZaiShuJu.Location = new System.Drawing.Point(135, 98);
            this.btnJiaZaiShuJu.Margin = new System.Windows.Forms.Padding(0, 6, 4, 0);
            this.btnJiaZaiShuJu.Name = "btnJiaZaiShuJu";
            this.btnJiaZaiShuJu.Size = new System.Drawing.Size(96, 28);
            this.btnJiaZaiShuJu.TabIndex = 9;
            this.btnJiaZaiShuJu.Text = "加载数据";
            this.btnJiaZaiShuJu.UseVisualStyleBackColor = false;
            this.btnJiaZaiShuJu.Click += new System.EventHandler(this.btnJiaZaiShuJu_Click);
            // 
            // grpRiZhi
            // 
            this.grpRiZhi.BackColor = System.Drawing.Color.White;
            this.grpRiZhi.Controls.Add(this.lstRiZhi);
            this.grpRiZhi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRiZhi.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpRiZhi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.grpRiZhi.Location = new System.Drawing.Point(0, 280);
            this.grpRiZhi.Margin = new System.Windows.Forms.Padding(0);
            this.grpRiZhi.Name = "grpRiZhi";
            this.grpRiZhi.Padding = new System.Windows.Forms.Padding(12, 6, 12, 12);
            this.grpRiZhi.Size = new System.Drawing.Size(495, 414);
            this.grpRiZhi.TabIndex = 2;
            this.grpRiZhi.TabStop = false;
            this.grpRiZhi.Text = "运行日志";
            // 
            // lstRiZhi
            // 
            this.lstRiZhi.BackColor = System.Drawing.Color.White;
            this.lstRiZhi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstRiZhi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRiZhi.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstRiZhi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lstRiZhi.FormattingEnabled = true;
            this.lstRiZhi.HorizontalScrollbar = true;
            this.lstRiZhi.IntegralHeight = false;
            this.lstRiZhi.ItemHeight = 20;
            this.lstRiZhi.Location = new System.Drawing.Point(12, 30);
            this.lstRiZhi.Margin = new System.Windows.Forms.Padding(0);
            this.lstRiZhi.Name = "lstRiZhi";
            this.lstRiZhi.Size = new System.Drawing.Size(471, 372);
            this.lstRiZhi.TabIndex = 0;
            // 
            // mnsCaiDan
            // 
            this.mnsCaiDan.BackColor = System.Drawing.Color.White;
            this.mnsCaiDan.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mnsCaiDan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.mnsCaiDan.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnsCaiDan.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBianJiZuoYe,
            this.tsmiShuJu,
            this.tsmiXiangJi,
            this.tsmiJiZhun,
            this.tsmiBiaoDing,
            this.tsmiTongXin});
            this.mnsCaiDan.Location = new System.Drawing.Point(0, 0);
            this.mnsCaiDan.Name = "mnsCaiDan";
            this.mnsCaiDan.Padding = new System.Windows.Forms.Padding(12, 6, 0, 6);
            this.mnsCaiDan.Size = new System.Drawing.Size(1340, 36);
            this.mnsCaiDan.TabIndex = 1;
            this.mnsCaiDan.Text = "mnsCaiDan";
            // 
            // tsmiBianJiZuoYe
            // 
            this.tsmiBianJiZuoYe.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCeLiangDianPian});
            this.tsmiBianJiZuoYe.Name = "tsmiBianJiZuoYe";
            this.tsmiBianJiZuoYe.Size = new System.Drawing.Size(83, 24);
            this.tsmiBianJiZuoYe.Text = "编辑作业";
            // 
            // tsmiCeLiangDianPian
            // 
            this.tsmiCeLiangDianPian.Name = "tsmiCeLiangDianPian";
            this.tsmiCeLiangDianPian.Size = new System.Drawing.Size(224, 26);
            this.tsmiCeLiangDianPian.Text = "测量垫片TB";
            this.tsmiCeLiangDianPian.Click += new System.EventHandler(this.tsmiCeLiangDianPian_Click);
            // 
            // tsmiShuJu
            // 
            this.tsmiShuJu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiXiuGaiBaoCunShuJu});
            this.tsmiShuJu.Name = "tsmiShuJu";
            this.tsmiShuJu.Size = new System.Drawing.Size(53, 24);
            this.tsmiShuJu.Text = "数据";
            // 
            // tsmiXiuGaiBaoCunShuJu
            // 
            this.tsmiXiuGaiBaoCunShuJu.Name = "tsmiXiuGaiBaoCunShuJu";
            this.tsmiXiuGaiBaoCunShuJu.Size = new System.Drawing.Size(182, 26);
            this.tsmiXiuGaiBaoCunShuJu.Text = "修改保存数据";
            // 
            // tsmiXiangJi
            // 
            this.tsmiXiangJi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiXiuGaiXiangJiCanShu});
            this.tsmiXiangJi.Name = "tsmiXiangJi";
            this.tsmiXiangJi.Size = new System.Drawing.Size(53, 24);
            this.tsmiXiangJi.Text = "相机";
            // 
            // tsmiXiuGaiXiangJiCanShu
            // 
            this.tsmiXiuGaiXiangJiCanShu.Name = "tsmiXiuGaiXiangJiCanShu";
            this.tsmiXiuGaiXiangJiCanShu.Size = new System.Drawing.Size(224, 26);
            this.tsmiXiuGaiXiangJiCanShu.Text = "修改相机参数";
            this.tsmiXiuGaiXiangJiCanShu.Click += new System.EventHandler(this.tsmiXiuGaiXiangJiCanShu_Click);
            // 
            // tsmiJiZhun
            // 
            this.tsmiJiZhun.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSheZhiJiZhunDian});
            this.tsmiJiZhun.Name = "tsmiJiZhun";
            this.tsmiJiZhun.Size = new System.Drawing.Size(53, 24);
            this.tsmiJiZhun.Text = "基准";
            // 
            // tsmiSheZhiJiZhunDian
            // 
            this.tsmiSheZhiJiZhunDian.Name = "tsmiSheZhiJiZhunDian";
            this.tsmiSheZhiJiZhunDian.Size = new System.Drawing.Size(167, 26);
            this.tsmiSheZhiJiZhunDian.Text = "设置基准点";
            // 
            // tsmiBiaoDing
            // 
            this.tsmiBiaoDing.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSheZhiYinDaoBiaoDing});
            this.tsmiBiaoDing.Name = "tsmiBiaoDing";
            this.tsmiBiaoDing.Size = new System.Drawing.Size(53, 24);
            this.tsmiBiaoDing.Text = "标定";
            // 
            // tsmiSheZhiYinDaoBiaoDing
            // 
            this.tsmiSheZhiYinDaoBiaoDing.Name = "tsmiSheZhiYinDaoBiaoDing";
            this.tsmiSheZhiYinDaoBiaoDing.Size = new System.Drawing.Size(182, 26);
            this.tsmiSheZhiYinDaoBiaoDing.Text = "设置引导标定";
            // 
            // tsmiTongXin
            // 
            this.tsmiTongXin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiXiuGaiTongXinFangShi});
            this.tsmiTongXin.Name = "tsmiTongXin";
            this.tsmiTongXin.Size = new System.Drawing.Size(53, 24);
            this.tsmiTongXin.Text = "通信";
            // 
            // tsmiXiuGaiTongXinFangShi
            // 
            this.tsmiXiuGaiTongXinFangShi.Name = "tsmiXiuGaiTongXinFangShi";
            this.tsmiXiuGaiTongXinFangShi.Size = new System.Drawing.Size(182, 26);
            this.tsmiXiuGaiTongXinFangShi.Text = "修改通信方式";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1340, 750);
            this.Controls.Add(this.tlpZongTi);
            this.Controls.Add(this.mnsCaiDan);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.mnsCaiDan;
            this.MinimumSize = new System.Drawing.Size(1150, 660);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检测主页";
            this.Load += new System.EventHandler(this.MainForm_Load_1);
            this.tlpZongTi.ResumeLayout(false);
            this.tlpZuoCe.ResumeLayout(false);
            this.tlpShuangTu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.crdYuanTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.crdChuLiTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJianCeJiLu)).EndInit();
            this.tlpYouCe.ResumeLayout(false);
            this.grpGongNengQu.ResumeLayout(false);
            this.flpAnNiu.ResumeLayout(false);
            this.grpJianCeJieGuo.ResumeLayout(false);
            this.tlpJieGuo.ResumeLayout(false);
            this.tlpJieGuo.PerformLayout();
            this.grpRiZhi.ResumeLayout(false);
            this.mnsCaiDan.ResumeLayout(false);
            this.mnsCaiDan.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpZongTi;
        private System.Windows.Forms.TableLayoutPanel tlpZuoCe;
        private System.Windows.Forms.TableLayoutPanel tlpShuangTu;
        private System.Windows.Forms.TableLayoutPanel tlpYouCe;
        private System.Windows.Forms.TableLayoutPanel tlpJieGuo;
        private Cognex.VisionPro.CogRecordDisplay crdYuanTu;
        private Cognex.VisionPro.CogRecordDisplay crdChuLiTu;
        private System.Windows.Forms.Label lblYuanTu;
        private System.Windows.Forms.Label lblChuLiTu;
        private System.Windows.Forms.GroupBox grpGongNengQu;
        private System.Windows.Forms.FlowLayoutPanel flpAnNiu;
        private System.Windows.Forms.Button btnRuanChuFa;
        private System.Windows.Forms.Button btnZhiXing;
        private System.Windows.Forms.GroupBox grpJianCeJieGuo;
        private System.Windows.Forms.Label lblOK;
        private System.Windows.Forms.Label lblNG;
        private System.Windows.Forms.Label lblLiangPinLv;
        private System.Windows.Forms.Label lblZongShu;
        private System.Windows.Forms.Button btnQingLing;
        private System.Windows.Forms.TextBox txtOK;
        private System.Windows.Forms.TextBox txtNG;
        private System.Windows.Forms.TextBox txtLiangPinLv;
        private System.Windows.Forms.TextBox txtZongShu;
        private System.Windows.Forms.GroupBox grpRiZhi;
        private System.Windows.Forms.ListBox lstRiZhi;
        private System.Windows.Forms.DataGridView dgvJianCeJiLu;
        private System.Windows.Forms.MenuStrip mnsCaiDan;
        private System.Windows.Forms.ToolStripMenuItem tsmiBianJiZuoYe;
        private System.Windows.Forms.ToolStripMenuItem tsmiShuJu;
        private System.Windows.Forms.ToolStripMenuItem tsmiXiangJi;
        private System.Windows.Forms.ToolStripMenuItem tsmiJiZhun;
        private System.Windows.Forms.ToolStripMenuItem tsmiBiaoDing;
        private System.Windows.Forms.ToolStripMenuItem tsmiTongXin;
        private System.Windows.Forms.ToolStripMenuItem tsmiCeLiangDianPian;
        private System.Windows.Forms.ToolStripMenuItem tsmiXiuGaiBaoCunShuJu;
        private System.Windows.Forms.ToolStripMenuItem tsmiXiuGaiXiangJiCanShu;
        private System.Windows.Forms.ToolStripMenuItem tsmiSheZhiJiZhunDian;
        private System.Windows.Forms.ToolStripMenuItem tsmiSheZhiYinDaoBiaoDing;
        private System.Windows.Forms.ToolStripMenuItem tsmiXiuGaiTongXinFangShi;
        private System.Windows.Forms.Button btnXuanZeTuXiang;
        private System.Windows.Forms.Button btnJiaZaiShuJu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShiJian;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLeiXing;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJieGuo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBeiZhu;
    }
}
