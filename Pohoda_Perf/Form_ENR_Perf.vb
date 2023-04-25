Imports System.IO
Public Class Form_ENR_Perf
    Public texte_enregistrement As String

    Public Sub affiche_valeur_PERF()

        Me.Lbl_METEO_TWS.Text = Fix(valeur_active.CAL_1_Vit_Vent_vrai * 10) / 10
        Me.Lbl_All_TWA.Text = Fix(valeur_active.CAL_2_Ang_Vent_vrai * 10) / 10
        Me.Lbl_Val_Ang.Text = Fix(PERF_AngB * 10) / 10
        Me.Lbl_Val_AWA.Text = Fix(PERF_AWA * 10) / 10
        Me.Lbl_Val_AWS.Text = Fix(PERF_AWS * 10) / 10
        Me.Lbl_Val_Perf.Text = Fix(PERF_Perf * 1000) / 10
        Me.Lbl_Val_TWA.Text = Fix(PERF_TWA * 10) / 10
        Me.Lbl_Val_TWS.Text = Fix(PERF_TWS * 10) / 10
        Me.Lbl_Val_WS.Text = Fix(PERF_WS * 100) / 100
        Me.Lbl_Cot_Vapp.Text = valeur_active.ANE_2_Cote_Vapp
        Me.Lbl_DATE.Text = Now
        'Me.Txt_Commentaires.Text = "(DATE /METEO: TWS;EM /ALL:T WA;BORD /BATEAU: MASSE;POS.CG;MATOSSAGE /GREEMENT:P;BHAV;BHAR;GH /GV:D;B;E;C;T /GEN:D;E;C;T /SPI:B;BB;E;BBE;B;HB;T /VALM:BS;AWA;AWS;TWA;TWS;AB;PERF" & vbCrLf
        ' Me.Txt_Commentaires.Text = Now & ";/;" & valeur_active.CAL_1_Vit_Vent_vrai & ";" & Me.Txt_EM.Text & _
        '    ";/ALL:;" & PERF_TWA & ";" & valeur_active.ANE_2_Cote_Vapp & _
        '    ";/BATEAU:;" & Me.Txt_Masse.Text & ";" & Txt_Pos_Cg.Text & ";" & Me.Txt_Matossage.Text & vbCrLf & _
        '    ";/GREEMENT:;" & Me.Txt_Pataras.Text & ";" & Me.Txt_BHAV.Text & ";" & Me.Txt_BHAR.Text & ";" & Me.Txt_Galhauban.Text & vbCrLf & _
        '    ";/GV:;" & Me.Txt_Drisse_GV.Text & ";" & Me.Txt_Bordure_GV.Text & ";" & Me.Txt_Ecoute_GV.Text & ";" & Me.Txt_Chariot_GV.Text & ";" & Me.Txt_Type_GV.Text & vbCrLf & _
        '    ";/GEN:;" & Me.Txt_Drisse_Genois.Text & ";" & Me.Txt_Ecoute_Genois.Text & ";" & Me.Txt_Chariot_Genois.Text & ";" & Me.Txt_Type_Genois.Text & vbCrLf & _
        '    ";/SPI:;" & Me.Txt_Bras_Spi.Text & ";" & Me.Txt_Barber_Bras.Text & ";" & Me.Txt_Ecoute_SPI.Text & ";" & Me.Txt_Barber_Ecoute.Text & ";" & Me.Txt_Balancine_Tangon.Text & ";" & Me.Txt_HB_Tangon.Text & ";" & Me.Txt_Type_SPI.Text & vbCrLf & _
        '    ";/VALM:;" & PERF_WS & ";" & PERF_AWA & ";" & PERF_AWS & ";" & PERF_TWA & ";" & PERF_TWS & ";" & PERF_AngB & ";" & PERF_Perf & vbCrLf & _
        '    ";/VALEC:;" & EC_WS & ";" & EC_AWA & ";" & EC_AWS & ";" & EC_TWA & ";" & EC_TWS & ";" & EC_PERF_AngB & ";" & EC_PERF & vbCrLf
        Me.Lbl_enr_sec.Text = ENR_SEC

        'Affiche les Ecarts types

        Me.Lbl_EC_WS.Text = Fix(EC_WS * 100) / 100
        Me.Lbl_EC_AWA.Text = Fix(EC_AWA * 10) / 10
        Me.Lbl_EC_AWS.Text = Fix(EC_AWS * 10) / 10
        Me.Lbl_EC_TWA.Text = Fix(EC_TWA * 10) / 10
        Me.Lbl_EC_TWS.Text = Fix(EC_TWS * 10) / 10
        Me.Lbl_EC_AB.Text = Fix(EC_PERF_AngB * 10) / 10
        Me.Lbl_EC_PERF.Text = Fix(EC_PERF * 10) / 10


        texte_enregistrement = "DATE;TWS;Etat de la mer;TWA;Amure;Masse bateau;Position Cg;Matossage;Pataras;BHAV;BAHAR;Galhauban;Drisse GV;Bordure GV;Ecoute GV;Chariot GV;Type GV;Drisse Genois;Ecoute Genois;Chariot Genois;Type Genois;Bras Spi;Barber Bras; Ecoute Spi;Barber Ecoute;Balancine Tangon;HB Tangon;Type Spi;WS;EC_WS;AWA;EC_AWA;AWS;EC_AWS;TWA;EC_TWA;TWS;EC_TWS;AngB;EC_AngB;Perf;EC_PerF;Commentaire" & vbCrLf & _
            Now & ";" & valeur_active.CAL_1_Vit_Vent_vrai & ";" & Me.Txt_EM.Text & _
            ";" & PERF_TWA & ";" & valeur_active.ANE_2_Cote_Vapp & _
            ";" & Me.Txt_Masse.Text & ";" & Txt_Pos_Cg.Text & ";" & Me.Txt_Matossage.Text & _
            ";" & Me.Txt_Pataras.Text & ";" & Me.Txt_BHAV.Text & ";" & Me.Txt_BHAR.Text & ";" & Me.Txt_Galhauban.Text & _
            ";" & Me.Txt_Drisse_GV.Text & ";" & Me.Txt_Bordure_GV.Text & ";" & Me.Txt_Ecoute_GV.Text & ";" & Me.Txt_Chariot_GV.Text & ";" & Me.Txt_Type_GV.Text & _
            ";" & Me.Txt_Drisse_Genois.Text & ";" & Me.Txt_Ecoute_Genois.Text & ";" & Me.Txt_Chariot_Genois.Text & ";" & Me.Txt_Type_Genois.Text & _
            ";" & Me.Txt_Bras_Spi.Text & ";" & Me.Txt_Barber_Bras.Text & ";" & Me.Txt_Ecoute_SPI.Text & ";" & Me.Txt_Barber_Ecoute.Text & ";" & Me.Txt_Balancine_Tangon.Text & ";" & Me.Txt_HB_Tangon.Text & ";" & Me.Txt_Type_SPI.Text & _
            ";" & PERF_WS & ";" & EC_WS & ";" & PERF_AWA & ";" & EC_AWA & ";" & PERF_AWS & ";" & EC_AWS & ";" & PERF_TWA & ";" & EC_TWA & ";" & PERF_TWS & ";" & EC_TWS & ";" & PERF_AngB & ";" & EC_PERF_AngB & ";" & PERF_Perf & ";" & EC_PERF & ";" & Me.Txt_Commentaires.Text

    End Sub


    Private Sub Form_ENR_Perf_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Form_START.Chk_Perf.Checked = False
    End Sub

    Private Sub Txt_EM_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Form_ENR_Perf_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Lbl_Chemin.Text = Chemin_BDD
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Sauv.Click
        Dim date_actuelle As String

        date_actuelle = Now

        date_actuelle = Replace(date_actuelle, "/", "-")
        date_actuelle = Replace(date_actuelle, ":", ",")

        Dim sw As New StreamWriter(Chemin_BDD & "\" & date_actuelle & ".txt")
        'écriture        
        sw.WriteLine(texte_enregistrement)
        sw.Close()
    End Sub

    Private Sub Txt_Lissage_Perf_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Lissage_Perf.TextChanged
        If Txt_Lissage_Perf.Text = "" Then Txt_Lissage_Perf.Text = 1
        If Not IsNumeric(Txt_Lissage_Perf.Text) Then Txt_Lissage_Perf.Text = 1
        If Txt_Lissage_Perf.Text <= 0 Then Txt_Lissage_Perf.Text = 1
        ENR_SEC = 0
        PERF_WS = 0
        PERF_AWA = 0
        PERF_AWS = 0
        PERF_TWA = 0
        PERF_TWS = 0
        PERF_AngB = 0
        PERF_Perf = 0
        Me.Btn_Sauv.Enabled = False
    End Sub


    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        With FolderBrowserDialog1
            .SelectedPath = Chemin_BDD
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Chemin_BDD = .SelectedPath
            End If
        End With
        Me.Lbl_Chemin.Text = Chemin_BDD
    End Sub
End Class