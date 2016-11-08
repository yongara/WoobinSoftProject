namespace MobiClickManager
{
    partial class ucProductInfo
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.productInfoDataGridView = new System.Windows.Forms.DataGridView();
            this.productInfoPanel = new System.Windows.Forms.Panel();
            this.addProductGroupBox = new System.Windows.Forms.GroupBox();
            this.createCodePanel = new System.Windows.Forms.Panel();
            this.createCodeSaleRadioButton = new System.Windows.Forms.RadioButton();
            this.createCodeTestRadioButton = new System.Windows.Forms.RadioButton();
            this.createKeyCodeEcecuteButton = new System.Windows.Forms.Button();
            this.createKeyCodeNewCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.createKeyCodeCountTailLabel = new System.Windows.Forms.Label();
            this.createKeyCodeCountLabel = new System.Windows.Forms.Label();
            this.etcInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.productNumberTextBox = new System.Windows.Forms.TextBox();
            this.productNumberTitleLabel = new System.Windows.Forms.Label();
            this.dealCodeTextBox = new System.Windows.Forms.TextBox();
            this.dealCodeTitleLabel = new System.Windows.Forms.Label();
            this.saleDateTextBox = new System.Windows.Forms.TextBox();
            this.saleDateTitleLabel = new System.Windows.Forms.Label();
            this.saleCodeTextBox = new System.Windows.Forms.TextBox();
            this.seleCodeTitleLabel = new System.Windows.Forms.Label();
            this.toDtLabel = new System.Windows.Forms.Label();
            this.toDtTextBox = new System.Windows.Forms.TextBox();
            this.fromDtLabel = new System.Windows.Forms.Label();
            this.fromDtTextBox = new System.Windows.Forms.TextBox();
            this.allowCountTextBox = new System.Windows.Forms.TextBox();
            this.allowCountLabel = new System.Windows.Forms.Label();
            this.allowCountTailLabel = new System.Windows.Forms.Label();
            this.macAddressGroupBox = new System.Windows.Forms.GroupBox();
            this.macAddressTextBox = new System.Windows.Forms.TextBox();
            this.keyCodeGroupBox = new System.Windows.Forms.GroupBox();
            this.keyCodeTextBox = new System.Windows.Forms.TextBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.userInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.userPasswordTextBox = new System.Windows.Forms.TextBox();
            this.userPasswordLabel = new System.Windows.Forms.Label();
            this.useridTextBox = new System.Windows.Forms.TextBox();
            this.userIdLabel = new System.Windows.Forms.Label();
            this.topProductPanel = new System.Windows.Forms.Panel();
            this.searchCountTitleLabel = new System.Windows.Forms.Label();
            this.searchCountTitleTailLabel = new System.Windows.Forms.Label();
            this.searchCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.searchKeywordlabel = new System.Windows.Forms.Label();
            this.searchExecuteButton = new System.Windows.Forms.Button();
            this.saleTypeCodeGroupBox = new System.Windows.Forms.GroupBox();
            this.etcTypeRadioButton = new System.Windows.Forms.RadioButton();
            this.testTypeRadioButton = new System.Windows.Forms.RadioButton();
            this.saleTypeRadioButton = new System.Windows.Forms.RadioButton();
            this.totalSaleRadioButton = new System.Windows.Forms.RadioButton();
            this.searchKeywordTextBox = new System.Windows.Forms.TextBox();
            this.searchProductTitleLabel = new System.Windows.Forms.Label();
            this.searchTypeComboBox = new System.Windows.Forms.ComboBox();
            this.bottomProductPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.productInfoDataGridView)).BeginInit();
            this.productInfoPanel.SuspendLayout();
            this.addProductGroupBox.SuspendLayout();
            this.createCodePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.createKeyCodeNewCountNumericUpDown)).BeginInit();
            this.etcInfoGroupBox.SuspendLayout();
            this.macAddressGroupBox.SuspendLayout();
            this.keyCodeGroupBox.SuspendLayout();
            this.userInfoGroupBox.SuspendLayout();
            this.topProductPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchCountNumericUpDown)).BeginInit();
            this.saleTypeCodeGroupBox.SuspendLayout();
            this.bottomProductPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // productInfoDataGridView
            // 
            this.productInfoDataGridView.AllowUserToAddRows = false;
            this.productInfoDataGridView.AllowUserToDeleteRows = false;
            this.productInfoDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.productInfoDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.productInfoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productInfoDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productInfoDataGridView.Location = new System.Drawing.Point(0, 0);
            this.productInfoDataGridView.MultiSelect = false;
            this.productInfoDataGridView.Name = "productInfoDataGridView";
            this.productInfoDataGridView.ReadOnly = true;
            this.productInfoDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.productInfoDataGridView.RowTemplate.Height = 23;
            this.productInfoDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.productInfoDataGridView.Size = new System.Drawing.Size(712, 697);
            this.productInfoDataGridView.TabIndex = 3;
            this.productInfoDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.productInfoDataGridView_CellContentClick);
            this.productInfoDataGridView.SelectionChanged += new System.EventHandler(this.productInfoDataGridView_SelectionChanged);
            // 
            // productInfoPanel
            // 
            this.productInfoPanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.productInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.productInfoPanel.Controls.Add(this.addProductGroupBox);
            this.productInfoPanel.Controls.Add(this.etcInfoGroupBox);
            this.productInfoPanel.Controls.Add(this.macAddressGroupBox);
            this.productInfoPanel.Controls.Add(this.keyCodeGroupBox);
            this.productInfoPanel.Controls.Add(this.deleteButton);
            this.productInfoPanel.Controls.Add(this.editButton);
            this.productInfoPanel.Controls.Add(this.userInfoGroupBox);
            this.productInfoPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.productInfoPanel.Location = new System.Drawing.Point(712, 0);
            this.productInfoPanel.Name = "productInfoPanel";
            this.productInfoPanel.Size = new System.Drawing.Size(330, 697);
            this.productInfoPanel.TabIndex = 4;
            // 
            // addProductGroupBox
            // 
            this.addProductGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addProductGroupBox.Controls.Add(this.createCodePanel);
            this.addProductGroupBox.Controls.Add(this.createKeyCodeEcecuteButton);
            this.addProductGroupBox.Controls.Add(this.createKeyCodeNewCountNumericUpDown);
            this.addProductGroupBox.Controls.Add(this.createKeyCodeCountTailLabel);
            this.addProductGroupBox.Controls.Add(this.createKeyCodeCountLabel);
            this.addProductGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addProductGroupBox.Location = new System.Drawing.Point(23, 533);
            this.addProductGroupBox.Name = "addProductGroupBox";
            this.addProductGroupBox.Size = new System.Drawing.Size(294, 136);
            this.addProductGroupBox.TabIndex = 7;
            this.addProductGroupBox.TabStop = false;
            this.addProductGroupBox.Text = "신규제품코드 추가";
            // 
            // createCodePanel
            // 
            this.createCodePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.createCodePanel.Controls.Add(this.createCodeSaleRadioButton);
            this.createCodePanel.Controls.Add(this.createCodeTestRadioButton);
            this.createCodePanel.Location = new System.Drawing.Point(32, 52);
            this.createCodePanel.Name = "createCodePanel";
            this.createCodePanel.Size = new System.Drawing.Size(218, 33);
            this.createCodePanel.TabIndex = 10;
            // 
            // createCodeSaleRadioButton
            // 
            this.createCodeSaleRadioButton.AutoSize = true;
            this.createCodeSaleRadioButton.Checked = true;
            this.createCodeSaleRadioButton.Location = new System.Drawing.Point(31, 6);
            this.createCodeSaleRadioButton.Name = "createCodeSaleRadioButton";
            this.createCodeSaleRadioButton.Size = new System.Drawing.Size(59, 16);
            this.createCodeSaleRadioButton.TabIndex = 4;
            this.createCodeSaleRadioButton.TabStop = true;
            this.createCodeSaleRadioButton.Text = "판매용";
            this.createCodeSaleRadioButton.UseVisualStyleBackColor = true;
            this.createCodeSaleRadioButton.CheckedChanged += new System.EventHandler(this.createCodeSaleRadioButton_CheckedChanged);
            // 
            // createCodeTestRadioButton
            // 
            this.createCodeTestRadioButton.AutoSize = true;
            this.createCodeTestRadioButton.Location = new System.Drawing.Point(113, 6);
            this.createCodeTestRadioButton.Name = "createCodeTestRadioButton";
            this.createCodeTestRadioButton.Size = new System.Drawing.Size(71, 16);
            this.createCodeTestRadioButton.TabIndex = 5;
            this.createCodeTestRadioButton.Text = "테스트용";
            this.createCodeTestRadioButton.UseVisualStyleBackColor = true;
            this.createCodeTestRadioButton.CheckedChanged += new System.EventHandler(this.createCodeTestRadioButton_CheckedChanged);
            // 
            // createKeyCodeEcecuteButton
            // 
            this.createKeyCodeEcecuteButton.Location = new System.Drawing.Point(32, 91);
            this.createKeyCodeEcecuteButton.Name = "createKeyCodeEcecuteButton";
            this.createKeyCodeEcecuteButton.Size = new System.Drawing.Size(152, 30);
            this.createKeyCodeEcecuteButton.TabIndex = 9;
            this.createKeyCodeEcecuteButton.Text = "제품코드 추가실행";
            this.createKeyCodeEcecuteButton.UseVisualStyleBackColor = true;
            this.createKeyCodeEcecuteButton.Click += new System.EventHandler(this.createKeyCodeEcecuteButton_Click);
            // 
            // createKeyCodeNewCountNumericUpDown
            // 
            this.createKeyCodeNewCountNumericUpDown.Location = new System.Drawing.Point(101, 20);
            this.createKeyCodeNewCountNumericUpDown.Name = "createKeyCodeNewCountNumericUpDown";
            this.createKeyCodeNewCountNumericUpDown.Size = new System.Drawing.Size(101, 21);
            this.createKeyCodeNewCountNumericUpDown.TabIndex = 7;
            this.createKeyCodeNewCountNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.createKeyCodeNewCountNumericUpDown.ValueChanged += new System.EventHandler(this.createKeyCodeNewCountNumericUpDown_ValueChanged);
            // 
            // createKeyCodeCountTailLabel
            // 
            this.createKeyCodeCountTailLabel.AutoSize = true;
            this.createKeyCodeCountTailLabel.Location = new System.Drawing.Point(213, 24);
            this.createKeyCodeCountTailLabel.Name = "createKeyCodeCountTailLabel";
            this.createKeyCodeCountTailLabel.Size = new System.Drawing.Size(17, 12);
            this.createKeyCodeCountTailLabel.TabIndex = 6;
            this.createKeyCodeCountTailLabel.Text = "건";
            // 
            // createKeyCodeCountLabel
            // 
            this.createKeyCodeCountLabel.AutoSize = true;
            this.createKeyCodeCountLabel.Location = new System.Drawing.Point(30, 24);
            this.createKeyCodeCountLabel.Name = "createKeyCodeCountLabel";
            this.createKeyCodeCountLabel.Size = new System.Drawing.Size(65, 12);
            this.createKeyCodeCountLabel.TabIndex = 0;
            this.createKeyCodeCountLabel.Text = "생성건수 : ";
            // 
            // etcInfoGroupBox
            // 
            this.etcInfoGroupBox.Controls.Add(this.productNumberTextBox);
            this.etcInfoGroupBox.Controls.Add(this.productNumberTitleLabel);
            this.etcInfoGroupBox.Controls.Add(this.dealCodeTextBox);
            this.etcInfoGroupBox.Controls.Add(this.dealCodeTitleLabel);
            this.etcInfoGroupBox.Controls.Add(this.saleDateTextBox);
            this.etcInfoGroupBox.Controls.Add(this.saleDateTitleLabel);
            this.etcInfoGroupBox.Controls.Add(this.saleCodeTextBox);
            this.etcInfoGroupBox.Controls.Add(this.seleCodeTitleLabel);
            this.etcInfoGroupBox.Controls.Add(this.toDtLabel);
            this.etcInfoGroupBox.Controls.Add(this.toDtTextBox);
            this.etcInfoGroupBox.Controls.Add(this.fromDtLabel);
            this.etcInfoGroupBox.Controls.Add(this.fromDtTextBox);
            this.etcInfoGroupBox.Controls.Add(this.allowCountTextBox);
            this.etcInfoGroupBox.Controls.Add(this.allowCountLabel);
            this.etcInfoGroupBox.Controls.Add(this.allowCountTailLabel);
            this.etcInfoGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.etcInfoGroupBox.Location = new System.Drawing.Point(23, 231);
            this.etcInfoGroupBox.Name = "etcInfoGroupBox";
            this.etcInfoGroupBox.Size = new System.Drawing.Size(294, 216);
            this.etcInfoGroupBox.TabIndex = 8;
            this.etcInfoGroupBox.TabStop = false;
            this.etcInfoGroupBox.Text = "기타정보";
            // 
            // productNumberTextBox
            // 
            this.productNumberTextBox.Location = new System.Drawing.Point(82, 176);
            this.productNumberTextBox.Name = "productNumberTextBox";
            this.productNumberTextBox.Size = new System.Drawing.Size(195, 21);
            this.productNumberTextBox.TabIndex = 30;
            // 
            // productNumberTitleLabel
            // 
            this.productNumberTitleLabel.AutoSize = true;
            this.productNumberTitleLabel.Location = new System.Drawing.Point(19, 180);
            this.productNumberTitleLabel.Name = "productNumberTitleLabel";
            this.productNumberTitleLabel.Size = new System.Drawing.Size(57, 12);
            this.productNumberTitleLabel.TabIndex = 29;
            this.productNumberTitleLabel.Text = "제품번호:";
            // 
            // dealCodeTextBox
            // 
            this.dealCodeTextBox.Location = new System.Drawing.Point(82, 150);
            this.dealCodeTextBox.Name = "dealCodeTextBox";
            this.dealCodeTextBox.Size = new System.Drawing.Size(195, 21);
            this.dealCodeTextBox.TabIndex = 28;
            // 
            // dealCodeTitleLabel
            // 
            this.dealCodeTitleLabel.AutoSize = true;
            this.dealCodeTitleLabel.Location = new System.Drawing.Point(19, 154);
            this.dealCodeTitleLabel.Name = "dealCodeTitleLabel";
            this.dealCodeTitleLabel.Size = new System.Drawing.Size(57, 12);
            this.dealCodeTitleLabel.TabIndex = 27;
            this.dealCodeTitleLabel.Text = "판매담당:";
            // 
            // saleDateTextBox
            // 
            this.saleDateTextBox.Location = new System.Drawing.Point(82, 124);
            this.saleDateTextBox.Name = "saleDateTextBox";
            this.saleDateTextBox.Size = new System.Drawing.Size(195, 21);
            this.saleDateTextBox.TabIndex = 26;
            // 
            // saleDateTitleLabel
            // 
            this.saleDateTitleLabel.AutoSize = true;
            this.saleDateTitleLabel.Location = new System.Drawing.Point(19, 128);
            this.saleDateTitleLabel.Name = "saleDateTitleLabel";
            this.saleDateTitleLabel.Size = new System.Drawing.Size(57, 12);
            this.saleDateTitleLabel.TabIndex = 25;
            this.saleDateTitleLabel.Text = "판매일자:";
            // 
            // saleCodeTextBox
            // 
            this.saleCodeTextBox.Location = new System.Drawing.Point(82, 98);
            this.saleCodeTextBox.Name = "saleCodeTextBox";
            this.saleCodeTextBox.Size = new System.Drawing.Size(195, 21);
            this.saleCodeTextBox.TabIndex = 24;
            // 
            // seleCodeTitleLabel
            // 
            this.seleCodeTitleLabel.AutoSize = true;
            this.seleCodeTitleLabel.Location = new System.Drawing.Point(19, 102);
            this.seleCodeTitleLabel.Name = "seleCodeTitleLabel";
            this.seleCodeTitleLabel.Size = new System.Drawing.Size(57, 12);
            this.seleCodeTitleLabel.TabIndex = 23;
            this.seleCodeTitleLabel.Text = "판매코드:";
            // 
            // toDtLabel
            // 
            this.toDtLabel.AutoSize = true;
            this.toDtLabel.Location = new System.Drawing.Point(19, 50);
            this.toDtLabel.Name = "toDtLabel";
            this.toDtLabel.Size = new System.Drawing.Size(57, 12);
            this.toDtLabel.TabIndex = 19;
            this.toDtLabel.Text = "시작일자:";
            // 
            // toDtTextBox
            // 
            this.toDtTextBox.Location = new System.Drawing.Point(82, 72);
            this.toDtTextBox.Name = "toDtTextBox";
            this.toDtTextBox.Size = new System.Drawing.Size(196, 21);
            this.toDtTextBox.TabIndex = 22;
            // 
            // fromDtLabel
            // 
            this.fromDtLabel.AutoSize = true;
            this.fromDtLabel.Location = new System.Drawing.Point(19, 76);
            this.fromDtLabel.Name = "fromDtLabel";
            this.fromDtLabel.Size = new System.Drawing.Size(57, 12);
            this.fromDtLabel.TabIndex = 21;
            this.fromDtLabel.Text = "종료일자:";
            // 
            // fromDtTextBox
            // 
            this.fromDtTextBox.Location = new System.Drawing.Point(82, 46);
            this.fromDtTextBox.Name = "fromDtTextBox";
            this.fromDtTextBox.Size = new System.Drawing.Size(195, 21);
            this.fromDtTextBox.TabIndex = 20;
            // 
            // allowCountTextBox
            // 
            this.allowCountTextBox.Location = new System.Drawing.Point(121, 20);
            this.allowCountTextBox.Name = "allowCountTextBox";
            this.allowCountTextBox.Size = new System.Drawing.Size(133, 21);
            this.allowCountTextBox.TabIndex = 16;
            // 
            // allowCountLabel
            // 
            this.allowCountLabel.AutoSize = true;
            this.allowCountLabel.Location = new System.Drawing.Point(14, 24);
            this.allowCountLabel.Name = "allowCountLabel";
            this.allowCountLabel.Size = new System.Drawing.Size(101, 12);
            this.allowCountLabel.TabIndex = 15;
            this.allowCountLabel.Text = "키워드 허용건수 :";
            // 
            // allowCountTailLabel
            // 
            this.allowCountTailLabel.AutoSize = true;
            this.allowCountTailLabel.Location = new System.Drawing.Point(260, 27);
            this.allowCountTailLabel.Name = "allowCountTailLabel";
            this.allowCountTailLabel.Size = new System.Drawing.Size(17, 12);
            this.allowCountTailLabel.TabIndex = 7;
            this.allowCountTailLabel.Text = "건";
            // 
            // macAddressGroupBox
            // 
            this.macAddressGroupBox.Controls.Add(this.macAddressTextBox);
            this.macAddressGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.macAddressGroupBox.Location = new System.Drawing.Point(23, 170);
            this.macAddressGroupBox.Name = "macAddressGroupBox";
            this.macAddressGroupBox.Size = new System.Drawing.Size(294, 55);
            this.macAddressGroupBox.TabIndex = 8;
            this.macAddressGroupBox.TabStop = false;
            this.macAddressGroupBox.Text = "맥주소";
            // 
            // macAddressTextBox
            // 
            this.macAddressTextBox.Location = new System.Drawing.Point(18, 20);
            this.macAddressTextBox.Name = "macAddressTextBox";
            this.macAddressTextBox.Size = new System.Drawing.Size(259, 21);
            this.macAddressTextBox.TabIndex = 14;
            // 
            // keyCodeGroupBox
            // 
            this.keyCodeGroupBox.Controls.Add(this.keyCodeTextBox);
            this.keyCodeGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.keyCodeGroupBox.Location = new System.Drawing.Point(23, 111);
            this.keyCodeGroupBox.Name = "keyCodeGroupBox";
            this.keyCodeGroupBox.Size = new System.Drawing.Size(294, 55);
            this.keyCodeGroupBox.TabIndex = 7;
            this.keyCodeGroupBox.TabStop = false;
            this.keyCodeGroupBox.Text = "제품코드";
            // 
            // keyCodeTextBox
            // 
            this.keyCodeTextBox.Location = new System.Drawing.Point(18, 20);
            this.keyCodeTextBox.Name = "keyCodeTextBox";
            this.keyCodeTextBox.Size = new System.Drawing.Size(259, 21);
            this.keyCodeTextBox.TabIndex = 4;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(238, 453);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(79, 30);
            this.deleteButton.TabIndex = 8;
            this.deleteButton.Text = "삭제";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(153, 454);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(79, 30);
            this.editButton.TabIndex = 7;
            this.editButton.Text = "수정";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // userInfoGroupBox
            // 
            this.userInfoGroupBox.Controls.Add(this.userPasswordTextBox);
            this.userInfoGroupBox.Controls.Add(this.userPasswordLabel);
            this.userInfoGroupBox.Controls.Add(this.useridTextBox);
            this.userInfoGroupBox.Controls.Add(this.userIdLabel);
            this.userInfoGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.userInfoGroupBox.Location = new System.Drawing.Point(23, 29);
            this.userInfoGroupBox.Name = "userInfoGroupBox";
            this.userInfoGroupBox.Size = new System.Drawing.Size(294, 79);
            this.userInfoGroupBox.TabIndex = 6;
            this.userInfoGroupBox.TabStop = false;
            this.userInfoGroupBox.Text = "사용자정보";
            // 
            // userPasswordTextBox
            // 
            this.userPasswordTextBox.Location = new System.Drawing.Point(112, 45);
            this.userPasswordTextBox.Name = "userPasswordTextBox";
            this.userPasswordTextBox.Size = new System.Drawing.Size(166, 21);
            this.userPasswordTextBox.TabIndex = 5;
            // 
            // userPasswordLabel
            // 
            this.userPasswordLabel.AutoSize = true;
            this.userPasswordLabel.Location = new System.Drawing.Point(6, 49);
            this.userPasswordLabel.Name = "userPasswordLabel";
            this.userPasswordLabel.Size = new System.Drawing.Size(101, 12);
            this.userPasswordLabel.TabIndex = 4;
            this.userPasswordLabel.Text = "사용자비밀번호 : ";
            // 
            // useridTextBox
            // 
            this.useridTextBox.Location = new System.Drawing.Point(111, 20);
            this.useridTextBox.Name = "useridTextBox";
            this.useridTextBox.Size = new System.Drawing.Size(166, 21);
            this.useridTextBox.TabIndex = 3;
            // 
            // userIdLabel
            // 
            this.userIdLabel.AutoSize = true;
            this.userIdLabel.Location = new System.Drawing.Point(16, 24);
            this.userIdLabel.Name = "userIdLabel";
            this.userIdLabel.Size = new System.Drawing.Size(89, 12);
            this.userIdLabel.TabIndex = 0;
            this.userIdLabel.Text = "사용자아이디 : ";
            // 
            // topProductPanel
            // 
            this.topProductPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.topProductPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.topProductPanel.Controls.Add(this.searchCountTitleLabel);
            this.topProductPanel.Controls.Add(this.searchCountTitleTailLabel);
            this.topProductPanel.Controls.Add(this.searchCountNumericUpDown);
            this.topProductPanel.Controls.Add(this.searchKeywordlabel);
            this.topProductPanel.Controls.Add(this.searchExecuteButton);
            this.topProductPanel.Controls.Add(this.saleTypeCodeGroupBox);
            this.topProductPanel.Controls.Add(this.searchKeywordTextBox);
            this.topProductPanel.Controls.Add(this.searchProductTitleLabel);
            this.topProductPanel.Controls.Add(this.searchTypeComboBox);
            this.topProductPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topProductPanel.Location = new System.Drawing.Point(0, 0);
            this.topProductPanel.Name = "topProductPanel";
            this.topProductPanel.Size = new System.Drawing.Size(1042, 62);
            this.topProductPanel.TabIndex = 9;
            // 
            // searchCountTitleLabel
            // 
            this.searchCountTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchCountTitleLabel.AutoSize = true;
            this.searchCountTitleLabel.Location = new System.Drawing.Point(941, 16);
            this.searchCountTitleLabel.Name = "searchCountTitleLabel";
            this.searchCountTitleLabel.Size = new System.Drawing.Size(53, 12);
            this.searchCountTitleLabel.TabIndex = 14;
            this.searchCountTitleLabel.Text = "검색건수";
            // 
            // searchCountTitleTailLabel
            // 
            this.searchCountTitleTailLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchCountTitleTailLabel.AutoSize = true;
            this.searchCountTitleTailLabel.Location = new System.Drawing.Point(1012, 38);
            this.searchCountTitleTailLabel.Name = "searchCountTitleTailLabel";
            this.searchCountTitleTailLabel.Size = new System.Drawing.Size(17, 12);
            this.searchCountTitleTailLabel.TabIndex = 13;
            this.searchCountTitleTailLabel.Text = "건";
            // 
            // searchCountNumericUpDown
            // 
            this.searchCountNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchCountNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.searchCountNumericUpDown.Location = new System.Drawing.Point(943, 31);
            this.searchCountNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.searchCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.searchCountNumericUpDown.Name = "searchCountNumericUpDown";
            this.searchCountNumericUpDown.Size = new System.Drawing.Size(66, 21);
            this.searchCountNumericUpDown.TabIndex = 12;
            this.searchCountNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.searchCountNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.searchCountNumericUpDown.ValueChanged += new System.EventHandler(this.searchCountNumericUpDown_ValueChanged);
            // 
            // searchKeywordlabel
            // 
            this.searchKeywordlabel.AutoSize = true;
            this.searchKeywordlabel.Location = new System.Drawing.Point(441, 16);
            this.searchKeywordlabel.Name = "searchKeywordlabel";
            this.searchKeywordlabel.Size = new System.Drawing.Size(65, 12);
            this.searchKeywordlabel.TabIndex = 11;
            this.searchKeywordlabel.Text = "검색키워드";
            // 
            // searchExecuteButton
            // 
            this.searchExecuteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchExecuteButton.Location = new System.Drawing.Point(871, 32);
            this.searchExecuteButton.Name = "searchExecuteButton";
            this.searchExecuteButton.Size = new System.Drawing.Size(66, 21);
            this.searchExecuteButton.TabIndex = 10;
            this.searchExecuteButton.Text = "검색";
            this.searchExecuteButton.UseVisualStyleBackColor = true;
            this.searchExecuteButton.Click += new System.EventHandler(this.searchExecuteButton_Click);
            // 
            // saleTypeCodeGroupBox
            // 
            this.saleTypeCodeGroupBox.Controls.Add(this.etcTypeRadioButton);
            this.saleTypeCodeGroupBox.Controls.Add(this.testTypeRadioButton);
            this.saleTypeCodeGroupBox.Controls.Add(this.saleTypeRadioButton);
            this.saleTypeCodeGroupBox.Controls.Add(this.totalSaleRadioButton);
            this.saleTypeCodeGroupBox.Location = new System.Drawing.Point(14, 7);
            this.saleTypeCodeGroupBox.Name = "saleTypeCodeGroupBox";
            this.saleTypeCodeGroupBox.Size = new System.Drawing.Size(276, 46);
            this.saleTypeCodeGroupBox.TabIndex = 9;
            this.saleTypeCodeGroupBox.TabStop = false;
            this.saleTypeCodeGroupBox.Text = "판매유형";
            // 
            // etcTypeRadioButton
            // 
            this.etcTypeRadioButton.AutoSize = true;
            this.etcTypeRadioButton.Location = new System.Drawing.Point(217, 20);
            this.etcTypeRadioButton.Name = "etcTypeRadioButton";
            this.etcTypeRadioButton.Size = new System.Drawing.Size(47, 16);
            this.etcTypeRadioButton.TabIndex = 3;
            this.etcTypeRadioButton.Text = "기타";
            this.etcTypeRadioButton.UseVisualStyleBackColor = true;
            this.etcTypeRadioButton.CheckedChanged += new System.EventHandler(this.etcTypeRadioButton_CheckedChanged);
            // 
            // testTypeRadioButton
            // 
            this.testTypeRadioButton.AutoSize = true;
            this.testTypeRadioButton.Location = new System.Drawing.Point(137, 20);
            this.testTypeRadioButton.Name = "testTypeRadioButton";
            this.testTypeRadioButton.Size = new System.Drawing.Size(71, 16);
            this.testTypeRadioButton.TabIndex = 2;
            this.testTypeRadioButton.Text = "테스트용";
            this.testTypeRadioButton.UseVisualStyleBackColor = true;
            this.testTypeRadioButton.CheckedChanged += new System.EventHandler(this.testTypeRadioButton_CheckedChanged);
            // 
            // saleTypeRadioButton
            // 
            this.saleTypeRadioButton.AutoSize = true;
            this.saleTypeRadioButton.Location = new System.Drawing.Point(69, 20);
            this.saleTypeRadioButton.Name = "saleTypeRadioButton";
            this.saleTypeRadioButton.Size = new System.Drawing.Size(59, 16);
            this.saleTypeRadioButton.TabIndex = 1;
            this.saleTypeRadioButton.Text = "판매용";
            this.saleTypeRadioButton.UseVisualStyleBackColor = true;
            this.saleTypeRadioButton.CheckedChanged += new System.EventHandler(this.saleTypeRadioButton_CheckedChanged);
            // 
            // totalSaleRadioButton
            // 
            this.totalSaleRadioButton.AutoSize = true;
            this.totalSaleRadioButton.Checked = true;
            this.totalSaleRadioButton.Location = new System.Drawing.Point(13, 20);
            this.totalSaleRadioButton.Name = "totalSaleRadioButton";
            this.totalSaleRadioButton.Size = new System.Drawing.Size(47, 16);
            this.totalSaleRadioButton.TabIndex = 0;
            this.totalSaleRadioButton.TabStop = true;
            this.totalSaleRadioButton.Text = "전체";
            this.totalSaleRadioButton.UseVisualStyleBackColor = true;
            this.totalSaleRadioButton.CheckedChanged += new System.EventHandler(this.totalSaleRadioButton_CheckedChanged);
            // 
            // searchKeywordTextBox
            // 
            this.searchKeywordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchKeywordTextBox.Location = new System.Drawing.Point(437, 32);
            this.searchKeywordTextBox.Name = "searchKeywordTextBox";
            this.searchKeywordTextBox.Size = new System.Drawing.Size(428, 21);
            this.searchKeywordTextBox.TabIndex = 3;
            this.searchKeywordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchKeywordTextBox_KeyDown);
            // 
            // searchProductTitleLabel
            // 
            this.searchProductTitleLabel.AutoSize = true;
            this.searchProductTitleLabel.Location = new System.Drawing.Point(324, 16);
            this.searchProductTitleLabel.Name = "searchProductTitleLabel";
            this.searchProductTitleLabel.Size = new System.Drawing.Size(53, 12);
            this.searchProductTitleLabel.TabIndex = 2;
            this.searchProductTitleLabel.Text = "검색유형";
            // 
            // searchTypeComboBox
            // 
            this.searchTypeComboBox.FormattingEnabled = true;
            this.searchTypeComboBox.Items.AddRange(new object[] {
            "전체",
            "사용자아이디",
            "제품키코드"});
            this.searchTypeComboBox.Location = new System.Drawing.Point(320, 32);
            this.searchTypeComboBox.Name = "searchTypeComboBox";
            this.searchTypeComboBox.Size = new System.Drawing.Size(111, 20);
            this.searchTypeComboBox.TabIndex = 1;
            this.searchTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.searchTypeComboBox_SelectedIndexChanged);
            // 
            // bottomProductPanel
            // 
            this.bottomProductPanel.Controls.Add(this.productInfoDataGridView);
            this.bottomProductPanel.Controls.Add(this.productInfoPanel);
            this.bottomProductPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomProductPanel.Location = new System.Drawing.Point(0, 62);
            this.bottomProductPanel.Name = "bottomProductPanel";
            this.bottomProductPanel.Size = new System.Drawing.Size(1042, 697);
            this.bottomProductPanel.TabIndex = 10;
            // 
            // ucProductInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bottomProductPanel);
            this.Controls.Add(this.topProductPanel);
            this.Name = "ucProductInfo";
            this.Size = new System.Drawing.Size(1042, 759);
            this.Load += new System.EventHandler(this.ucProductInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.productInfoDataGridView)).EndInit();
            this.productInfoPanel.ResumeLayout(false);
            this.addProductGroupBox.ResumeLayout(false);
            this.addProductGroupBox.PerformLayout();
            this.createCodePanel.ResumeLayout(false);
            this.createCodePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.createKeyCodeNewCountNumericUpDown)).EndInit();
            this.etcInfoGroupBox.ResumeLayout(false);
            this.etcInfoGroupBox.PerformLayout();
            this.macAddressGroupBox.ResumeLayout(false);
            this.macAddressGroupBox.PerformLayout();
            this.keyCodeGroupBox.ResumeLayout(false);
            this.keyCodeGroupBox.PerformLayout();
            this.userInfoGroupBox.ResumeLayout(false);
            this.userInfoGroupBox.PerformLayout();
            this.topProductPanel.ResumeLayout(false);
            this.topProductPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchCountNumericUpDown)).EndInit();
            this.saleTypeCodeGroupBox.ResumeLayout(false);
            this.saleTypeCodeGroupBox.PerformLayout();
            this.bottomProductPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView productInfoDataGridView;
        private System.Windows.Forms.Panel productInfoPanel;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.GroupBox userInfoGroupBox;
        private System.Windows.Forms.TextBox keyCodeTextBox;
        private System.Windows.Forms.TextBox useridTextBox;
        private System.Windows.Forms.Label userIdLabel;
        private System.Windows.Forms.TextBox userPasswordTextBox;
        private System.Windows.Forms.Label userPasswordLabel;
        private System.Windows.Forms.GroupBox macAddressGroupBox;
        private System.Windows.Forms.TextBox macAddressTextBox;
        private System.Windows.Forms.GroupBox etcInfoGroupBox;
        private System.Windows.Forms.TextBox toDtTextBox;
        private System.Windows.Forms.Label fromDtLabel;
        private System.Windows.Forms.TextBox fromDtTextBox;
        private System.Windows.Forms.Label toDtLabel;
        private System.Windows.Forms.TextBox allowCountTextBox;
        private System.Windows.Forms.Label allowCountLabel;
        private System.Windows.Forms.GroupBox keyCodeGroupBox;
        private System.Windows.Forms.GroupBox addProductGroupBox;
        private System.Windows.Forms.Label createKeyCodeCountLabel;
        private System.Windows.Forms.Panel topProductPanel;
        private System.Windows.Forms.Button searchExecuteButton;
        private System.Windows.Forms.GroupBox saleTypeCodeGroupBox;
        private System.Windows.Forms.RadioButton etcTypeRadioButton;
        private System.Windows.Forms.RadioButton testTypeRadioButton;
        private System.Windows.Forms.RadioButton saleTypeRadioButton;
        private System.Windows.Forms.RadioButton totalSaleRadioButton;
        private System.Windows.Forms.TextBox searchKeywordTextBox;
        private System.Windows.Forms.Label searchProductTitleLabel;
        private System.Windows.Forms.ComboBox searchTypeComboBox;
        private System.Windows.Forms.Panel bottomProductPanel;
        private System.Windows.Forms.Button createKeyCodeEcecuteButton;
        private System.Windows.Forms.NumericUpDown createKeyCodeNewCountNumericUpDown;
        private System.Windows.Forms.RadioButton createCodeTestRadioButton;
        private System.Windows.Forms.RadioButton createCodeSaleRadioButton;
        private System.Windows.Forms.Label createKeyCodeCountTailLabel;
        private System.Windows.Forms.Label allowCountTailLabel;
        private System.Windows.Forms.Panel createCodePanel;
        private System.Windows.Forms.TextBox dealCodeTextBox;
        private System.Windows.Forms.Label dealCodeTitleLabel;
        private System.Windows.Forms.TextBox saleDateTextBox;
        private System.Windows.Forms.Label saleDateTitleLabel;
        private System.Windows.Forms.TextBox saleCodeTextBox;
        private System.Windows.Forms.Label seleCodeTitleLabel;
        private System.Windows.Forms.TextBox productNumberTextBox;
        private System.Windows.Forms.Label productNumberTitleLabel;
        private System.Windows.Forms.Label searchKeywordlabel;
        private System.Windows.Forms.Label searchCountTitleLabel;
        private System.Windows.Forms.Label searchCountTitleTailLabel;
        private System.Windows.Forms.NumericUpDown searchCountNumericUpDown;
    }
}
