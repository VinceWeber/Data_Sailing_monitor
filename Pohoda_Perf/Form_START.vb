
Option Explicit On
Imports System.Math
Imports System.Net
Imports System.Text.Encoding


Public Class Form_START

    Dim suscriber As New Sockets.UdpClient(1024)

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UDP_Timer1.Tick

        Try
            Dim ep As IPEndPoint = New IPEndPoint(IPAddress.Any, 0)
            Dim rcvbytes() As Byte = suscriber.Receive(ep)
            ' Txt_UDP.Text = ASCII.GetString(rcvbytes)

            Mod_ANALYSE.Analyse_phrase_NMEA(ASCII.GetString(rcvbytes))

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Btn_Ecoute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Ecoute.Click
        Me.Btn_Ecoute.Enabled = False
        Me.chk_bdd_record.Enabled = False
        Me.Txt_horloge_int.Enabled = False
        Me.Txt_tps_limit.Enabled = False
        Me.Txt_Moyenne.Enabled = False
        Me.Txt_Sauvegarde.Enabled = False
        Me.Txt_Calcul.Enabled = False
        Me.chk_bus.Enabled = False
        Me.Chk_Calcul.Enabled = False
        Me.chk_bdd_calcul.Enabled = False
        Me.ProgressBar1.Visible = True
        Me.chk_valeurs.Enabled = True
        Me.Chk_Perf.Enabled = True
        Me.Chk_Reglopti.Enabled = True
        Mod_ANALYSE.demarrage_prgramme()
        UDP_Timer1.Enabled = True
        Timer_Calc_Val_active.Enabled = True
        Timer_Calc_Val_Minute.Enabled = True
        Refresh_variables()

    End Sub
    Private Sub Btn_Stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Stop.Click
        '  Form_Ecoute_port.fermeture_programme()
        Me.Btn_Ecoute.Enabled = True
        Me.chk_bdd_record.Enabled = True
        Me.Txt_horloge_int.Enabled = True
        Me.Txt_tps_limit.Enabled = True
        Me.Txt_Moyenne.Enabled = True
        Me.Txt_Sauvegarde.Enabled = True
        Me.Txt_Calcul.Enabled = True
        Me.chk_bus.Enabled = True
        Me.Chk_Calcul.Enabled = True
        Me.chk_bdd_calcul.Enabled = True
        Me.ProgressBar1.Visible = False
        Me.chk_valeurs.Enabled = False
        Me.Chk_Perf.Enabled = False
        Me.Chk_Reglopti.Enabled = False
        UDP_Timer1.Enabled = False
        Timer_Calc_Val_active.Enabled = False
        Timer_Calc_Val_Minute.Enabled = False

    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_valeurs.CheckedChanged
        Form_VAL_ACTIVES.Visible = Not (Form_VAL_ACTIVES.Visible)
    End Sub

    Private Sub HScrollBar1_ang_vent_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar1_ang_vent.Scroll
        Dim signe As String
        signe = ""
        Mod_ANALYSE.corr_dir_vent = HScrollBar1_ang_vent.Value - 50
        If Val(Mod_ANALYSE.corr_dir_vent) > 0 Then
            signe = "+"
        ElseIf Val(Mod_ANALYSE.corr_dir_vent) < 0 Then
            signe = "-"
        End If
        Lbl_correction_vent.Text = signe & " " & Abs(Val(Mod_ANALYSE.corr_dir_vent)) & " °"
        Lbl_correction_vent.Refresh()
    End Sub
    Private Sub Btn_update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Form_Graphique.Graph_update(Form_Graphique.zgc)
    End Sub
    Private Sub HScrollBar3_Compas_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar3_Compas.Scroll

        Dim signe As String
        signe = ""
        Mod_ANALYSE.corr_compas = HScrollBar3_Compas.Value - 50

        If Val(Mod_ANALYSE.corr_compas) > 0 Then
            signe = "+"
        ElseIf Val(Mod_ANALYSE.corr_compas) < 0 Then
            signe = "-"
        End If

        Lbl_corr_compas.Text = signe & " " & Abs(Val(Mod_ANALYSE.corr_compas)) & " °"
        Lbl_corr_compas.Refresh()
    End Sub
    Private Sub HScrollBar7_Dir_parcours_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar7_Dir_parcours.Scroll
        LBl_Dir_Parcours.Text = HScrollBar7_Dir_parcours.Value & " °"
        LBl_Dir_Parcours.Refresh()
    End Sub
    Private Sub HScrollBar2_vit_vent_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar2_vit_vent.Scroll
        Mod_ANALYSE.corr_vit_capteur_vent = 1 + (HScrollBar2_vit_vent.Value - 50) / 100
        Lbl_Corr_Vit_vent.Text = "x " & Round(Mod_ANALYSE.corr_vit_capteur_vent, 2)
        Lbl_Corr_Vit_vent.Refresh()
    End Sub
    Private Sub HScrollBar4_VS_Glob_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar4_VS_Glob.Scroll
        Mod_ANALYSE.Corr_speedo_glob = 1 + (HScrollBar4_VS_Glob.Value - 50) / 100
        Label8_VS_glob.Text = "x " & Round(Mod_ANALYSE.Corr_speedo_glob, 2)
        Label8_VS_glob.Refresh()
    End Sub
    Private Sub HScrollBar5_VSL_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar5_VSL.Scroll
        Mod_ANALYSE.Corr_speedo_Babord = 1 + (HScrollBar5_VSL.Value - 50) / 100
        Label9_VSL.Text = "x " & Round(Mod_ANALYSE.Corr_speedo_Babord, 2)
        Label9_VSL.Refresh()
    End Sub
    Private Sub HScrollBar6_VSR_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar6_VSR.Scroll
        Mod_ANALYSE.Corr_speedo_Tribord = 1 + (HScrollBar6_VSR.Value - 50) / 100
        Label10_VSR.Text = "x " & Round(Mod_ANALYSE.Corr_speedo_Tribord, 2)
        Label10_VSR.Refresh()
    End Sub
    Private Sub Refresh_variables()
        Dim signe As String
        signe = ""

        Mod_ANALYSE.corr_dir_vent = HScrollBar1_ang_vent.Value - 50
        Mod_ANALYSE.corr_compas = HScrollBar3_Compas.Value - 50
        LBl_Dir_Parcours.Text = HScrollBar7_Dir_parcours.Value & " °"
        Mod_ANALYSE.corr_vit_capteur_vent = 1 + (HScrollBar2_vit_vent.Value - 50) / 100
        Mod_ANALYSE.Corr_speedo_glob = 1 + (HScrollBar4_VS_Glob.Value - 50) / 100
        Mod_ANALYSE.Corr_speedo_Babord = 1 + (HScrollBar5_VSL.Value - 50) / 100
        Mod_ANALYSE.Corr_speedo_Tribord = 1 + (HScrollBar6_VSR.Value - 50) / 100

        Label10_VSR.Text = "x " & Round(Mod_ANALYSE.Corr_speedo_Tribord, 2)
        Label10_VSR.Refresh()
        Label9_VSL.Text = "x " & Round(Mod_ANALYSE.Corr_speedo_Babord, 2)
        Label9_VSL.Refresh()
        Label8_VS_glob.Text = "x " & Round(Mod_ANALYSE.Corr_speedo_glob, 2)
        Label8_VS_glob.Refresh()
        Lbl_Corr_Vit_vent.Text = "x " & Round(Mod_ANALYSE.corr_vit_capteur_vent, 2)
        Lbl_Corr_Vit_vent.Refresh()
        LBl_Dir_Parcours.Text = HScrollBar7_Dir_parcours.Value & " °"
        LBl_Dir_Parcours.Refresh()

        If Val(Mod_ANALYSE.corr_compas) > 0 Then
            signe = "+"
        ElseIf Val(Mod_ANALYSE.corr_compas) < 0 Then
            signe = "-"
        End If

        Lbl_corr_compas.Text = signe & " " & Abs(Val(Mod_ANALYSE.corr_compas)) & " °"
        Lbl_corr_compas.Refresh()

        If Val(Mod_ANALYSE.corr_dir_vent) > 0 Then
            signe = "+"
        ElseIf Val(Mod_ANALYSE.corr_dir_vent) < 0 Then
            signe = "-"
        End If
        Lbl_correction_vent.Text = signe & " " & Abs(Val(Mod_ANALYSE.corr_dir_vent)) & " °"
        Lbl_correction_vent.Refresh()

    End Sub

    Private Sub Form_START_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        suscriber.Client.Close()
    End Sub

    Private Sub Form_Start_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Refresh_variables()
        suscriber.Client.ReceiveTimeout = 100
        suscriber.Client.Blocking = False
    End Sub

    Private Sub Timer_Calc_Val_active_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Calc_Val_active.Tick
        Mod_ANALYSE.m_evenement_horloge_Calcul_donnees_actives()
        m_evenement_horloge_affiche_donnees_actives()
        Refresh_variables()
    End Sub

    Private Sub Timer_Calc_Val_Minute_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Calc_Val_Minute.Tick
        Mod_ANALYSE.m_evenement_horloge_Calcul_minute()
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chk_Reglopti.CheckedChanged
        Form_VAL_ACTIVES.Visible = Not (Form_VAL_ACTIVES.Visible)
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chk_Perf.CheckedChanged
        Form_ENR_Perf.Visible = Not (Form_ENR_Perf.Visible)
    End Sub
End Class
