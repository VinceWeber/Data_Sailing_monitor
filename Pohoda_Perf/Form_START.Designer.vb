<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_START
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.HScrollBar7_Dir_parcours = New System.Windows.Forms.HScrollBar()
        Me.LBl_Dir_Parcours = New System.Windows.Forms.Label()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.HScrollBar6_VSR = New System.Windows.Forms.HScrollBar()
        Me.Label10_VSR = New System.Windows.Forms.Label()
        Me.HScrollBar5_VSL = New System.Windows.Forms.HScrollBar()
        Me.Label9_VSL = New System.Windows.Forms.Label()
        Me.HScrollBar4_VS_Glob = New System.Windows.Forms.HScrollBar()
        Me.Label8_VS_glob = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.HScrollBar3_Compas = New System.Windows.Forms.HScrollBar()
        Me.Lbl_corr_compas = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.HScrollBar2_vit_vent = New System.Windows.Forms.HScrollBar()
        Me.Lbl_Corr_Vit_vent = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.HScrollBar1_ang_vent = New System.Windows.Forms.HScrollBar()
        Me.Lbl_correction_vent = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chk_bdd_calcul = New System.Windows.Forms.CheckBox()
        Me.Chk_Calcul = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_Sauvegarde = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_Calcul = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_Moyenne = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_horloge_int = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_tps_limit = New System.Windows.Forms.TextBox()
        Me.chk_bdd_record = New System.Windows.Forms.CheckBox()
        Me.chk_bus = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chk_valeurs = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Btn_Ecoute = New System.Windows.Forms.Button()
        Me.Btn_Stop = New System.Windows.Forms.Button()
        Me.UDP_Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_Calc_Val_active = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_Calc_Val_Minute = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.Chk_Perf = New System.Windows.Forms.CheckBox()
        Me.Chk_Reglopti = New System.Windows.Forms.CheckBox()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GroupBox9)
        Me.GroupBox4.Controls.Add(Me.GroupBox8)
        Me.GroupBox4.Controls.Add(Me.GroupBox7)
        Me.GroupBox4.Controls.Add(Me.GroupBox5)
        Me.GroupBox4.Controls.Add(Me.GroupBox6)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 122)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(381, 154)
        Me.GroupBox4.TabIndex = 39
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Corrections capteurs"
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.HScrollBar7_Dir_parcours)
        Me.GroupBox9.Controls.Add(Me.LBl_Dir_Parcours)
        Me.GroupBox9.Location = New System.Drawing.Point(108, 113)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(267, 34)
        Me.GroupBox9.TabIndex = 39
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Direction du parcours"
        '
        'HScrollBar7_Dir_parcours
        '
        Me.HScrollBar7_Dir_parcours.Location = New System.Drawing.Point(6, 15)
        Me.HScrollBar7_Dir_parcours.Maximum = 368
        Me.HScrollBar7_Dir_parcours.Name = "HScrollBar7_Dir_parcours"
        Me.HScrollBar7_Dir_parcours.Size = New System.Drawing.Size(180, 16)
        Me.HScrollBar7_Dir_parcours.TabIndex = 2
        '
        'LBl_Dir_Parcours
        '
        Me.LBl_Dir_Parcours.AutoSize = True
        Me.LBl_Dir_Parcours.Location = New System.Drawing.Point(225, 15)
        Me.LBl_Dir_Parcours.Name = "LBl_Dir_Parcours"
        Me.LBl_Dir_Parcours.Size = New System.Drawing.Size(17, 13)
        Me.LBl_Dir_Parcours.TabIndex = 0
        Me.LBl_Dir_Parcours.Text = "0°"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.Label13)
        Me.GroupBox8.Controls.Add(Me.Label12)
        Me.GroupBox8.Controls.Add(Me.Label11)
        Me.GroupBox8.Controls.Add(Me.HScrollBar6_VSR)
        Me.GroupBox8.Controls.Add(Me.Label10_VSR)
        Me.GroupBox8.Controls.Add(Me.HScrollBar5_VSL)
        Me.GroupBox8.Controls.Add(Me.Label9_VSL)
        Me.GroupBox8.Controls.Add(Me.HScrollBar4_VS_Glob)
        Me.GroupBox8.Controls.Add(Me.Label8_VS_glob)
        Me.GroupBox8.Location = New System.Drawing.Point(200, 19)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(160, 88)
        Me.GroupBox8.TabIndex = 39
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Vit. Surf"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(8, 19)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(29, 13)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "Glob"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 62)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(15, 13)
        Me.Label12.TabIndex = 7
        Me.Label12.Text = "R"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 40)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(13, 13)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "L"
        '
        'HScrollBar6_VSR
        '
        Me.HScrollBar6_VSR.Location = New System.Drawing.Point(77, 59)
        Me.HScrollBar6_VSR.Name = "HScrollBar6_VSR"
        Me.HScrollBar6_VSR.Size = New System.Drawing.Size(80, 16)
        Me.HScrollBar6_VSR.TabIndex = 5
        Me.HScrollBar6_VSR.Value = 50
        '
        'Label10_VSR
        '
        Me.Label10_VSR.AutoSize = True
        Me.Label10_VSR.Location = New System.Drawing.Point(38, 62)
        Me.Label10_VSR.Name = "Label10_VSR"
        Me.Label10_VSR.Size = New System.Drawing.Size(36, 13)
        Me.Label10_VSR.TabIndex = 4
        Me.Label10_VSR.Text = "x 1.00"
        '
        'HScrollBar5_VSL
        '
        Me.HScrollBar5_VSL.Location = New System.Drawing.Point(77, 37)
        Me.HScrollBar5_VSL.Name = "HScrollBar5_VSL"
        Me.HScrollBar5_VSL.Size = New System.Drawing.Size(80, 16)
        Me.HScrollBar5_VSL.TabIndex = 3
        Me.HScrollBar5_VSL.Value = 50
        '
        'Label9_VSL
        '
        Me.Label9_VSL.AutoSize = True
        Me.Label9_VSL.Location = New System.Drawing.Point(38, 40)
        Me.Label9_VSL.Name = "Label9_VSL"
        Me.Label9_VSL.Size = New System.Drawing.Size(36, 13)
        Me.Label9_VSL.TabIndex = 2
        Me.Label9_VSL.Text = "x 1.00"
        '
        'HScrollBar4_VS_Glob
        '
        Me.HScrollBar4_VS_Glob.Location = New System.Drawing.Point(77, 16)
        Me.HScrollBar4_VS_Glob.Name = "HScrollBar4_VS_Glob"
        Me.HScrollBar4_VS_Glob.Size = New System.Drawing.Size(80, 16)
        Me.HScrollBar4_VS_Glob.TabIndex = 1
        Me.HScrollBar4_VS_Glob.Value = 50
        '
        'Label8_VS_glob
        '
        Me.Label8_VS_glob.AutoSize = True
        Me.Label8_VS_glob.Location = New System.Drawing.Point(38, 19)
        Me.Label8_VS_glob.Name = "Label8_VS_glob"
        Me.Label8_VS_glob.Size = New System.Drawing.Size(36, 13)
        Me.Label8_VS_glob.TabIndex = 0
        Me.Label8_VS_glob.Text = "x 1.00"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.HScrollBar3_Compas)
        Me.GroupBox7.Controls.Add(Me.Lbl_corr_compas)
        Me.GroupBox7.Location = New System.Drawing.Point(103, 19)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(91, 61)
        Me.GroupBox7.TabIndex = 38
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Déclinaison"
        '
        'HScrollBar3_Compas
        '
        Me.HScrollBar3_Compas.Location = New System.Drawing.Point(5, 42)
        Me.HScrollBar3_Compas.Name = "HScrollBar3_Compas"
        Me.HScrollBar3_Compas.Size = New System.Drawing.Size(80, 16)
        Me.HScrollBar3_Compas.TabIndex = 1
        Me.HScrollBar3_Compas.Value = 50
        '
        'Lbl_corr_compas
        '
        Me.Lbl_corr_compas.AutoSize = True
        Me.Lbl_corr_compas.Location = New System.Drawing.Point(32, 16)
        Me.Lbl_corr_compas.Name = "Lbl_corr_compas"
        Me.Lbl_corr_compas.Size = New System.Drawing.Size(17, 13)
        Me.Lbl_corr_compas.TabIndex = 0
        Me.Lbl_corr_compas.Text = "0°"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.HScrollBar2_vit_vent)
        Me.GroupBox5.Controls.Add(Me.Lbl_Corr_Vit_vent)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 86)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(91, 61)
        Me.GroupBox5.TabIndex = 37
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Vitesse Vent"
        '
        'HScrollBar2_vit_vent
        '
        Me.HScrollBar2_vit_vent.Location = New System.Drawing.Point(6, 42)
        Me.HScrollBar2_vit_vent.Name = "HScrollBar2_vit_vent"
        Me.HScrollBar2_vit_vent.Size = New System.Drawing.Size(80, 16)
        Me.HScrollBar2_vit_vent.TabIndex = 1
        Me.HScrollBar2_vit_vent.Value = 50
        '
        'Lbl_Corr_Vit_vent
        '
        Me.Lbl_Corr_Vit_vent.AutoSize = True
        Me.Lbl_Corr_Vit_vent.Location = New System.Drawing.Point(36, 16)
        Me.Lbl_Corr_Vit_vent.Name = "Lbl_Corr_Vit_vent"
        Me.Lbl_Corr_Vit_vent.Size = New System.Drawing.Size(36, 13)
        Me.Lbl_Corr_Vit_vent.TabIndex = 0
        Me.Lbl_Corr_Vit_vent.Text = "x 1.00"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.HScrollBar1_ang_vent)
        Me.GroupBox6.Controls.Add(Me.Lbl_correction_vent)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 19)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(91, 61)
        Me.GroupBox6.TabIndex = 36
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Angle Vent"
        '
        'HScrollBar1_ang_vent
        '
        Me.HScrollBar1_ang_vent.Location = New System.Drawing.Point(6, 42)
        Me.HScrollBar1_ang_vent.Name = "HScrollBar1_ang_vent"
        Me.HScrollBar1_ang_vent.Size = New System.Drawing.Size(80, 16)
        Me.HScrollBar1_ang_vent.TabIndex = 1
        Me.HScrollBar1_ang_vent.Value = 50
        '
        'Lbl_correction_vent
        '
        Me.Lbl_correction_vent.AutoSize = True
        Me.Lbl_correction_vent.Location = New System.Drawing.Point(36, 16)
        Me.Lbl_correction_vent.Name = "Lbl_correction_vent"
        Me.Lbl_correction_vent.Size = New System.Drawing.Size(17, 13)
        Me.Lbl_correction_vent.TabIndex = 0
        Me.Lbl_correction_vent.Text = "0°"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chk_bdd_calcul)
        Me.GroupBox1.Controls.Add(Me.Chk_Calcul)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Txt_Sauvegarde)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Txt_Calcul)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Txt_Moyenne)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_horloge_int)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Txt_tps_limit)
        Me.GroupBox1.Controls.Add(Me.chk_bdd_record)
        Me.GroupBox1.Controls.Add(Me.chk_bus)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 282)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(382, 150)
        Me.GroupBox1.TabIndex = 36
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Options"
        '
        'chk_bdd_calcul
        '
        Me.chk_bdd_calcul.AutoSize = True
        Me.chk_bdd_calcul.Location = New System.Drawing.Point(284, 42)
        Me.chk_bdd_calcul.Name = "chk_bdd_calcul"
        Me.chk_bdd_calcul.Size = New System.Drawing.Size(84, 17)
        Me.chk_bdd_calcul.TabIndex = 13
        Me.chk_bdd_calcul.Text = "BDD_Calcul"
        Me.chk_bdd_calcul.UseVisualStyleBackColor = True
        '
        'Chk_Calcul
        '
        Me.Chk_Calcul.AutoSize = True
        Me.Chk_Calcul.Checked = True
        Me.Chk_Calcul.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Chk_Calcul.Location = New System.Drawing.Point(138, 19)
        Me.Chk_Calcul.Name = "Chk_Calcul"
        Me.Chk_Calcul.Size = New System.Drawing.Size(112, 17)
        Me.Chk_Calcul.TabIndex = 12
        Me.Chk_Calcul.Text = "Calcul des valeurs"
        Me.Chk_Calcul.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(61, 123)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(151, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Sauvegarde des données (ms)"
        '
        'Txt_Sauvegarde
        '
        Me.Txt_Sauvegarde.Location = New System.Drawing.Point(4, 120)
        Me.Txt_Sauvegarde.Name = "Txt_Sauvegarde"
        Me.Txt_Sauvegarde.Size = New System.Drawing.Size(51, 20)
        Me.Txt_Sauvegarde.TabIndex = 10
        Me.Txt_Sauvegarde.Text = "1000"
        Me.Txt_Sauvegarde.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(237, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(122, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Calcul des données (ms)"
        '
        'Txt_Calcul
        '
        Me.Txt_Calcul.Location = New System.Drawing.Point(180, 91)
        Me.Txt_Calcul.Name = "Txt_Calcul"
        Me.Txt_Calcul.Size = New System.Drawing.Size(51, 20)
        Me.Txt_Calcul.TabIndex = 8
        Me.Txt_Calcul.Text = "1000"
        Me.Txt_Calcul.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(237, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(132, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Moy. données (nb valeurs)"
        '
        'Txt_Moyenne
        '
        Me.Txt_Moyenne.Location = New System.Drawing.Point(180, 65)
        Me.Txt_Moyenne.Name = "Txt_Moyenne"
        Me.Txt_Moyenne.Size = New System.Drawing.Size(51, 20)
        Me.Txt_Moyenne.TabIndex = 6
        Me.Txt_Moyenne.Text = "5"
        Me.Txt_Moyenne.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(60, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Horloge interne (ms)"
        '
        'Txt_horloge_int
        '
        Me.Txt_horloge_int.Location = New System.Drawing.Point(3, 94)
        Me.Txt_horloge_int.Name = "Txt_horloge_int"
        Me.Txt_horloge_int.Size = New System.Drawing.Size(51, 20)
        Me.Txt_horloge_int.TabIndex = 4
        Me.Txt_horloge_int.Text = "100"
        Me.Txt_horloge_int.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(61, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Temps limite (ms)"
        '
        'Txt_tps_limit
        '
        Me.Txt_tps_limit.Location = New System.Drawing.Point(4, 65)
        Me.Txt_tps_limit.Name = "Txt_tps_limit"
        Me.Txt_tps_limit.Size = New System.Drawing.Size(51, 20)
        Me.Txt_tps_limit.TabIndex = 2
        Me.Txt_tps_limit.Text = "4000"
        Me.Txt_tps_limit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chk_bdd_record
        '
        Me.chk_bdd_record.AutoSize = True
        Me.chk_bdd_record.Location = New System.Drawing.Point(284, 19)
        Me.chk_bdd_record.Name = "chk_bdd_record"
        Me.chk_bdd_record.Size = New System.Drawing.Size(90, 17)
        Me.chk_bdd_record.TabIndex = 1
        Me.chk_bdd_record.Text = "BDD_Record"
        Me.chk_bdd_record.UseVisualStyleBackColor = True
        '
        'chk_bus
        '
        Me.chk_bus.AutoSize = True
        Me.chk_bus.Checked = True
        Me.chk_bus.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_bus.Location = New System.Drawing.Point(5, 19)
        Me.chk_bus.Name = "chk_bus"
        Me.chk_bus.Size = New System.Drawing.Size(100, 17)
        Me.chk_bus.TabIndex = 0
        Me.chk_bus.Text = "Bus vers COM1"
        Me.chk_bus.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Chk_Reglopti)
        Me.GroupBox2.Controls.Add(Me.Chk_Perf)
        Me.GroupBox2.Controls.Add(Me.chk_valeurs)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 70)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(382, 46)
        Me.GroupBox2.TabIndex = 37
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Afficher"
        '
        'chk_valeurs
        '
        Me.chk_valeurs.AutoSize = True
        Me.chk_valeurs.Enabled = False
        Me.chk_valeurs.Location = New System.Drawing.Point(12, 19)
        Me.chk_valeurs.Name = "chk_valeurs"
        Me.chk_valeurs.Size = New System.Drawing.Size(61, 17)
        Me.chk_valeurs.TabIndex = 35
        Me.chk_valeurs.Text = "Valeurs"
        Me.chk_valeurs.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ProgressBar1)
        Me.GroupBox3.Controls.Add(Me.Btn_Ecoute)
        Me.GroupBox3.Controls.Add(Me.Btn_Stop)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(381, 52)
        Me.GroupBox3.TabIndex = 38
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Général"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(274, 19)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(100, 23)
        Me.ProgressBar1.TabIndex = 31
        Me.ProgressBar1.Visible = False
        '
        'Btn_Ecoute
        '
        Me.Btn_Ecoute.Location = New System.Drawing.Point(38, 16)
        Me.Btn_Ecoute.Name = "Btn_Ecoute"
        Me.Btn_Ecoute.Size = New System.Drawing.Size(111, 30)
        Me.Btn_Ecoute.TabIndex = 29
        Me.Btn_Ecoute.Text = "Start"
        Me.Btn_Ecoute.UseVisualStyleBackColor = True
        '
        'Btn_Stop
        '
        Me.Btn_Stop.Location = New System.Drawing.Point(157, 16)
        Me.Btn_Stop.Name = "Btn_Stop"
        Me.Btn_Stop.Size = New System.Drawing.Size(111, 30)
        Me.Btn_Stop.TabIndex = 30
        Me.Btn_Stop.Text = "Stop"
        Me.Btn_Stop.UseVisualStyleBackColor = True
        '
        'UDP_Timer1
        '
        Me.UDP_Timer1.Interval = 50
        '
        'Timer_Calc_Val_active
        '
        Me.Timer_Calc_Val_active.Interval = 1000
        '
        'Timer_Calc_Val_Minute
        '
        Me.Timer_Calc_Val_Minute.Interval = 60000
        '
        'Chk_Perf
        '
        Me.Chk_Perf.AutoSize = True
        Me.Chk_Perf.Enabled = False
        Me.Chk_Perf.Location = New System.Drawing.Point(79, 19)
        Me.Chk_Perf.Name = "Chk_Perf"
        Me.Chk_Perf.Size = New System.Drawing.Size(67, 17)
        Me.Chk_Perf.TabIndex = 36
        Me.Chk_Perf.Text = "Enr. Perf"
        Me.Chk_Perf.UseVisualStyleBackColor = True
        '
        'Chk_Reglopti
        '
        Me.Chk_Reglopti.AutoSize = True
        Me.Chk_Reglopti.Enabled = False
        Me.Chk_Reglopti.Location = New System.Drawing.Point(157, 19)
        Me.Chk_Reglopti.Name = "Chk_Reglopti"
        Me.Chk_Reglopti.Size = New System.Drawing.Size(88, 17)
        Me.Chk_Reglopti.TabIndex = 37
        Me.Chk_Reglopti.Text = "Reglage Opti"
        Me.Chk_Reglopti.UseVisualStyleBackColor = True
        '
        'Form_START
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 447)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "Form_START"
        Me.Text = "POHODA"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents HScrollBar7_Dir_parcours As System.Windows.Forms.HScrollBar
    Friend WithEvents LBl_Dir_Parcours As System.Windows.Forms.Label
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents HScrollBar6_VSR As System.Windows.Forms.HScrollBar
    Friend WithEvents Label10_VSR As System.Windows.Forms.Label
    Friend WithEvents HScrollBar5_VSL As System.Windows.Forms.HScrollBar
    Friend WithEvents Label9_VSL As System.Windows.Forms.Label
    Friend WithEvents HScrollBar4_VS_Glob As System.Windows.Forms.HScrollBar
    Friend WithEvents Label8_VS_glob As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents HScrollBar3_Compas As System.Windows.Forms.HScrollBar
    Friend WithEvents Lbl_corr_compas As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents HScrollBar2_vit_vent As System.Windows.Forms.HScrollBar
    Friend WithEvents Lbl_Corr_Vit_vent As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents HScrollBar1_ang_vent As System.Windows.Forms.HScrollBar
    Friend WithEvents Lbl_correction_vent As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chk_bdd_calcul As System.Windows.Forms.CheckBox
    Friend WithEvents Chk_Calcul As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Txt_Sauvegarde As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Txt_Calcul As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Txt_Moyenne As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Txt_horloge_int As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Txt_tps_limit As System.Windows.Forms.TextBox
    Friend WithEvents chk_bdd_record As System.Windows.Forms.CheckBox
    Friend WithEvents chk_bus As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chk_valeurs As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Btn_Ecoute As System.Windows.Forms.Button
    Friend WithEvents Btn_Stop As System.Windows.Forms.Button
    Friend WithEvents UDP_Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer_Calc_Val_active As System.Windows.Forms.Timer
    Friend WithEvents Timer_Calc_Val_Minute As System.Windows.Forms.Timer
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
    Friend WithEvents Chk_Reglopti As System.Windows.Forms.CheckBox
    Friend WithEvents Chk_Perf As System.Windows.Forms.CheckBox

End Class
