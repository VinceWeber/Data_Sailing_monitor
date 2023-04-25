Public Class Form_VAL_ACTIVES
    Public Sub affiche_valeur_active()
        Dim text_moyennes As String

        text_moyennes = "Moyenne Active Vent Vrai :" & vbCrLf _
        & "Direction : " & Fix(Mod_ANALYSE.VENT_MOY_1min_ref_X.Direction_angle(Mod_ANALYSE.cpt_minute)) & "  -  " _
        & "Force : " & Fix(Mod_ANALYSE.VENT_MOY_1min_ref_X.Vitesse(Mod_ANALYSE.cpt_minute)) & vbCrLf

        text_moyennes = text_moyennes & _
        "Moyenne active Vent GEO :" & vbCrLf _
        & "Direction : " & Fix(Mod_ANALYSE.VENT_MOY_1min_GEO_ref_X.Direction_angle(Mod_ANALYSE.cpt_minute)) & "  -  " _
        & "Force : " & Fix(Mod_ANALYSE.VENT_MOY_1min_GEO_ref_X.Vitesse(Mod_ANALYSE.cpt_minute)) & vbCrLf

        For i = 0 To 28
            text_moyennes = text_moyennes & _
            "Moyenne - " & i + 1 & "min:" & "  -  " _
           & "Direction : " & Fix(Mod_ANALYSE.VENT_MOY_1min_ref_X.Direction_angle(Modulo(Mod_ANALYSE.cpt_minute - (i + 1), 30)) * 10) / 10 & "  -  " _
            & "Force : " & Fix(Mod_ANALYSE.VENT_MOY_1min_ref_X.Vitesse(Modulo(Mod_ANALYSE.cpt_minute - (i + 1), 30)) * 10) / 10 & vbCrLf
        Next i

        text_moyennes = text_moyennes & vbCrLf & _
            "Moyenne - des 30min" & "  -  " _
        & "Direction : " & Fix(Mod_ANALYSE.VENT_MOY_30min.Direction_angle(0)) & "  -  " _
        & "Force : " & Fix(Mod_ANALYSE.VENT_MOY_30min.Vitesse(0))

        Lbl_val_active.Text = _
        "GPS" & vbCrLf _
        & "Latitude: " & Mod_ANALYSE.valeur_active.GPS_1_Latitude & vbCrLf _
        & "Longitude: " & Mod_ANALYSE.valeur_active.GPS_2_Longitude & vbCrLf _
        & "Cap_fond: " & Mod_ANALYSE.valeur_active.GPS_4_Cap_fond & vbCrLf _
        & "Vitesse_fond: " & Mod_ANALYSE.valeur_active.GPS_5_Vitesse_fond & vbCrLf _
        & "Temps écoulé (ms): " & Environment.TickCount - Mod_ANALYSE.valeur_active.GPS_6_Heure_enregistrement & " - " & Mod_ANALYSE.valeur_active.GPS_7_Dernier_enregistrement & vbCrLf _
        & vbCrLf & "ANEMO" & vbCrLf _
        & "Vitesse vent Apparent: " & Mod_ANALYSE.valeur_active.ANE_1_Vit_Vapp & vbCrLf _
        & "Coté vent apparent: " & Mod_ANALYSE.valeur_active.ANE_2_Cote_Vapp & vbCrLf _
        & "Angle vent apparent: " & Mod_ANALYSE.valeur_active.ANE_3_Angle_Vapp & vbCrLf _
        & "Temps écoulé (ms): " & Environment.TickCount - Mod_ANALYSE.valeur_active.ANE_4_Heure_enregistrement & " - " & Mod_ANALYSE.valeur_active.ANE_5_Dernier_enregistrement & vbCrLf _
        & vbCrLf & "SPEEDO" & vbCrLf _
        & "Loch Global: " & Mod_ANALYSE.valeur_active.SPE_1_Loch_glob & vbCrLf _
        & "Loch Temp: " & Mod_ANALYSE.valeur_active.SPE_2_Loch_temp & vbCrLf _
        & "Température: " & Mod_ANALYSE.valeur_active.SPE_3_Temp & vbCrLf _
        & "Vitesse Surface: " & Mod_ANALYSE.valeur_active.SPE_4_Vit_Surf & vbCrLf _
        & "Profondeur: " & Mod_ANALYSE.valeur_active.SPE_5_Profondeur & vbCrLf _
        & "Temps écoulé (ms): " & Environment.TickCount - Mod_ANALYSE.valeur_active.SPE_6_Heure_enregistrement & " - " & Mod_ANALYSE.valeur_active.SPE_7_Dernier_enregistrement & vbCrLf _
        & vbCrLf & "PILOTE" & vbCrLf _
        & "Angle de barre: " & Mod_ANALYSE.valeur_active.PIL_1_Angle_barre & vbCrLf _
        & "Cap Magnetique: " & Mod_ANALYSE.valeur_active.PIL_2_Cap_Mag & vbCrLf _
        & "Temps écoulé (ms): " & Environment.TickCount - Mod_ANALYSE.valeur_active.PIL_3_Heure_enregistrement & " - " & Mod_ANALYSE.valeur_active.PIL_4_Dernier_enregistrement & vbCrLf _
        & vbCrLf

        '& "Direction Vent vrai (Ecart cap intégré): =" & Mod_Analyse.valeur_active.CAL_9_Cap_Vent_GEO & vbCrLf _

        Lbl_Val_moy.Text = "CALCUL" & vbCrLf _
        & "Vitesse vent vrai: " & Fix(Mod_ANALYSE.valeur_active.CAL_1_Vit_Vent_vrai) & vbCrLf _
        & "Angle Vent vrai: " & Fix(Mod_ANALYSE.valeur_active.CAL_2_Ang_Vent_vrai) & vbCrLf _
        & "Direction Vent vrai: =" & Fix(Mod_ANALYSE.valeur_active.CAL_3_Cap_Vent_vrai) & vbCrLf _
        & vbCrLf _
        & "Vitesse vent Géographique: " & Fix(Mod_ANALYSE.valeur_active.CAL_8_Vit_Vent_GEO) & vbCrLf _
        & "Direction Vent Géographique : " & Fix(Mod_ANALYSE.valeur_active.CAL_9_Cap_Vent_GEO) & vbCrLf _
        & vbCrLf _
        & "Le Courant porte au :" & Fix(Mod_ANALYSE.valeur_active.CAL_5_Cap_courant) & vbCrLf _
        & "Force Courant :" & Fix(Mod_ANALYSE.valeur_active.CAL_11_Force_courant * 100) / 100 & vbCrLf _
        & vbCrLf _
        & "VMG: " & Fix(Mod_ANALYSE.valeur_active.CAL_4_VMG * 100) / 100 & vbCrLf _
        & vbCrLf & "PERFORMANCE" & vbCrLf _
        & "Prct_Vopti: " & Fix(Mod_ANALYSE.valeur_active.CAL_6_Prct_Vit_opti * 100) & "%" & vbCrLf _
        & vbCrLf _
        & text_moyennes & vbCrLf _
       ' & "Vitesse Surface Force:" & Mod_Analyse.Variable_espion(0) & vbCrLf _
        ' & "Vitesse Surface Direction:" & Mod_Analyse.Variable_espion(1) & vbCrLf _
        ' & "Vitesse Fond Force:" & Mod_Analyse.Variable_espion(2) & vbCrLf _
        ' & "Vitesse Fond Direction:" & Mod_Analyse.Variable_espion(3) & vbCrLf _
        ' & "Vent apparent Force:" & Mod_Analyse.Variable_espion(4) & vbCrLf _
        ' & "Vent apparent Direction:" & Mod_Analyse.Variable_espion(5) & vbCrLf


    End Sub

    Private Sub Form_Valeurs_navigation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Form_Start.chk_valeurs.Checked = False
    End Sub
    Private Sub Form_Valeurs_navigation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Lbl_val_active.Text = ""
    End Sub
End Class