
Public Class Valeur_Navigation
    '*********Valeurs du GPS
    Public GPS_1_Latitude As String
    Public GPS_2_Longitude As String
    Public GPS_3_Var_Mag As Single
    Public GPS_4_Cap_fond As Single
    Public GPS_5_Vitesse_fond As Single
    Public GPS_6_Heure_enregistrement As Integer
    Public GPS_7_Dernier_enregistrement As Integer
    '*********Valeurs Anémomètre
    Public ANE_1_Vit_Vapp As Single
    Public ANE_2_Cote_Vapp As String
    Public ANE_3_Angle_Vapp As Single
    Public ANE_4_Heure_enregistrement As Integer
    Public ANE_5_Dernier_enregistrement As Integer
    '*********Valeurs Speedo
    Public SPE_1_Loch_glob As Single
    Public SPE_2_Loch_temp As Single
    Public SPE_3_Temp As Single
    Public SPE_4_Vit_Surf As Single
    Public SPE_5_Profondeur As Single
    Public SPE_6_Heure_enregistrement As Integer
    Public SPE_7_Dernier_enregistrement As Integer
    '*********Valeurs Pilote
    Public PIL_1_Angle_barre As Single
    Public PIL_2_Cap_Mag As Single
    Public PIL_3_Heure_enregistrement As Integer
    Public PIL_4_Dernier_enregistrement As Integer
    '*********Valeurs CALCUL
    Public CAL_1_Vit_Vent_vrai As Single
    Public CAL_2_Ang_Vent_vrai As Single
    Public CAL_3_Cap_Vent_vrai As Single
    Public CAL_4_VMG As Single
    Public CAL_5_Cap_courant As Single
    Public CAL_6_Prct_Vit_opti As Single
    Public CAL_7_Prct_VMG_opti As Single
    Public CAL_8_Vit_Vent_GEO As Single
    Public CAL_9_Cap_Vent_GEO As Single
    Public CAL_10_Ang_vent_vrai_GEO As Single
    Public CAL_11_Force_courant As Single


    Public Sub New()
        GPS_1_Latitude = "00"
        GPS_2_Longitude = "00"
        GPS_3_Var_Mag = "00"
        GPS_4_Cap_fond = "00"
        GPS_5_Vitesse_fond = "00"
        GPS_6_Heure_enregistrement = Nothing
        '*********Valeurs Anémomètre
        ANE_1_Vit_Vapp = "0"
        ANE_2_Cote_Vapp = "0"
        ANE_3_Angle_Vapp = "0"
        ANE_4_Heure_enregistrement = Nothing
        '*********Valeurs Speedo
        SPE_1_Loch_glob = "0"
        SPE_2_Loch_temp = "0"
        SPE_3_Temp = "0"
        SPE_4_Vit_Surf = "0"
        SPE_5_Profondeur = "0"
        SPE_6_Heure_enregistrement = Nothing
        '*********Valeurs Pilote
        PIL_1_Angle_barre = "0"
        PIL_2_Cap_Mag = "0"
        PIL_3_Heure_enregistrement = Nothing
        '*********Valeurs CALCUL
        CAL_1_Vit_Vent_vrai = "0"
        CAL_2_Ang_Vent_vrai = "0"
        CAL_3_Cap_Vent_vrai = "0"
        CAL_9_Cap_Vent_GEO = "0"
        CAL_4_VMG = "0"
        CAL_5_Cap_courant = "0"
        CAL_6_Prct_Vit_opti = "0"
        CAL_7_Prct_VMG_opti = "0"
    End Sub
End Class
Public Class Class_Vent
    Public Direction_x(400) As Single
    Public Direction_y(400) As Single
    Public Direction_angle(400) As Single
    Public Vitesse(400) As Single
End Class
