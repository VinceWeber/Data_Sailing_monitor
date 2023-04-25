Option Explicit On
Imports System.Math

Module Mod_ANALYSE

#Region " Déclarations variables"

#Region "Variables systeme"
    Dim inputData4 As String = ""
    Dim inputData5 As String = ""
    Dim inputData6 As String = ""
    Dim inputData7 As String = ""

#End Region
#Region "Variables Programme"
    Public premier_calcul As Boolean

#End Region
#Region "Variables  liées aux chaines de texte NMEA"
    'Variables  liées aux chaines de texte NMEA
    Public Tampon_bus As String
    Public chaine_ctrl, chaine_espace, tampon_com4, tampon_com5, tampon_com6, tampon_com7, tampon_com18 As String
#End Region
#Region "Variables liées aux valeurs actives et à calculer"
    'Variables liées aux valeurs actives et à calculer
    Public valeur_active As Valeur_Navigation
    Public Cpt_interne_minute, cpt_minute, corr_dir_vent, corr_compas As Integer
    Public Corr_speedo_glob, Corr_speedo_Babord, Corr_speedo_Tribord, corr_vit_capteur_vent As Single
    Public V_Polaire(2, 180), Vit_surf_brute(20), Vit_ventapp_brute(20), Angle_vent_brut(20) As Single
    Public VENT_MOY_1min_actuel, VENT_MOY_1min_ref_X, VENT_MOY_30min As Class_Vent
    Public VENT_MOY_1min_GEO_actuel, VENT_MOY_1min_GEO_ref_X, VENT_MOY_30min_GEO As Class_Vent
    Public time_ms_limit, nb_val, nb_val_minute, cpt_vit_surf, cpt_angle_vapp, cpt_vit_vent As Integer
    Public bPERF_WS(1000), bPERF_AWA(1000), bPERF_AWS(1000), bPERF_TWA(1000), bPERF_TWS(1000), bPERF_AngB(1000), bPERF_Perf(1000) As Single
    Public PERF_WS, PERF_AWA, PERF_AWS, PERF_TWA, PERF_TWS, PERF_AngB, PERF_Perf, cpt_perf, ENR_SEC As Single
    Public EC_WS, EC_AWA, EC_AWS, EC_TWA, EC_TWS, EC_PERF_AngB, EC_PERF As Single
    Public Variable_espion(10) As Single
    Public Vecteur_calcul As Vector2D
    Public Chemin_BDD As String
#End Region

#End Region

#Region "Evenements internes au programme "

    'Calcul données actives
    'calcul valeurs minute
    'calcul valeur moyenne depuis dernier RAZ

    Public Sub m_evenement_horloge_Calcul_donnees_actives()
        ' InitValActive() 'uniquement pour les besoin de la simulation du calcul du vent GPS
        ' calcul_vent_vrai()
        ' calcul_courant()
        ' calcul_vent_GEO()
        Calcul_vecteurs_vent_courant()
        calcul_VMG()
        calcul_prct_polaire()
        calcul_vent_vrai_MOY_30MIN_glissante_1min()
        Integration_Valeur_brutes_perf()
        calcul_Valeurs_Moyenne_Perf()
        ENR_SEC = ENR_SEC + 1
        If ENR_SEC >= Form_ENR_Perf.Txt_Lissage_Perf.Text Then Form_ENR_Perf.Btn_Sauv.Enabled = True
    End Sub
    Public Sub m_evenement_horloge_Calcul_minute()
        calcul_vent_vraimoyen_30min()
    End Sub
    Public Sub m_evenement_horloge_affiche_donnees_actives()
        Form_VAL_ACTIVES.affiche_valeur_active()
        Form_ENR_Perf.affiche_valeur_PERF()
    End Sub


#End Region

#Region "Analyse des Chaines NMEA"
    Public Sub Analyse_phrase_NMEA(ByVal Chaine As String)
        '********  ATTENTION CE CODE N'ANALYSE QU'UNE SEULE LIGNE DU TAMPON (corrigé) ****************
        Dim lg_chaine, i, j, pos_parametre(10), parametre As Integer
        Dim chr_deb, chr_fin, chr_sep, chr_checksum, checksum_phrase, phrase(10) As String
        Dim checksum_ok, debut_valide As Boolean
        lg_chaine = Len(Chaine)
        chr_deb = "$"
        chr_fin = Chr(10)
        chr_sep = ","
        chr_checksum = "*"
        checksum_ok = False
        checksum_phrase = ""
        debut_valide = False

        For i = 1 To lg_chaine
            If Mid(Chaine, i, 1) = chr_deb Then
                'Défini le début de la phrase NMEA
                For j = 0 To 10
                    phrase(j) = ""
                Next j
                pos_parametre(0) = i
                debut_valide = True
                parametre = 0
            ElseIf Mid(Chaine, i, 1) = chr_sep Then
                'Défini que l'on passe au prochain paramètre
                phrase(parametre) = Mid(Chaine, pos_parametre(parametre) + 1, i - pos_parametre(parametre) - 1)
                parametre = parametre + 1
                pos_parametre(parametre) = i
            ElseIf Mid(Chaine, i, 1) = chr_checksum Then
                If debut_valide = True Then
                    checksum_phrase = Mid(Chaine, i + 1, 2)
                    If GetChecksum(Mid(Chaine, pos_parametre(0), i - pos_parametre(0))) = checksum_phrase Then
                        'Défini que l'on passe au prochain paramètre
                        phrase(parametre) = Mid(Chaine, pos_parametre(parametre) + 1, i - pos_parametre(parametre) - 1)
                        parametre = parametre + 1
                        pos_parametre(parametre) = i
                        Traduction_NMEA(phrase) 'Traduit la phrase et renseigne la variable "Valeur active"
                    End If
                End If
                debut_valide = False
            ElseIf Mid(Chaine, i, 1) = chr_fin Then
                If debut_valide = True Then
                    phrase(parametre) = Mid(Chaine, pos_parametre(parametre) + 1, i - pos_parametre(parametre) - 1)
                    parametre = parametre + 1
                    pos_parametre(parametre) = i
                    Traduction_NMEA_GPS(phrase)
                End If
                debut_valide = False
            End If
            If parametre = 10 Then parametre = 9
        Next i

    End Sub
    Private Sub Traduction_NMEA(ByRef phrase)
        If phrase(0) = "VWVLW" Then 'loch
            valeur_active.SPE_1_Loch_glob = Val(phrase(1))
            valeur_active.SPE_2_Loch_temp = Val(phrase(3))
            valeur_active.SPE_7_Dernier_enregistrement = Environment.TickCount - valeur_active.SPE_6_Heure_enregistrement
            valeur_active.SPE_6_Heure_enregistrement = Environment.TickCount
            'valeur_active.SPE_6_Heure_enregistrement = Now
        ElseIf phrase(0) = "VWMTW" Then 'Température
            valeur_active.SPE_3_Temp = Val(phrase(1))
            ' valeur_active.SPE_6_Heure_enregistrement = Environment.TickCount
            'valeur_active.SPE_6_Heure_enregistrement = Now
        ElseIf phrase(0) = "VWVHW" Then ' Vitesse surface
            'Moyenne des données sur x valeurs
            cpt_vit_surf = Modulo(cpt_vit_surf + 1, nb_val)

            'Correction du capteur vitesse surface
            If valeur_active.ANE_2_Cote_Vapp = "L" Then
                Vit_surf_brute(cpt_vit_surf) = Val(phrase(5)) * Corr_speedo_glob * Corr_speedo_Babord
            ElseIf valeur_active.ANE_2_Cote_Vapp = "R" Then
                Vit_surf_brute(cpt_vit_surf) = Val(phrase(5)) * Corr_speedo_glob * Corr_speedo_Tribord
            Else
                Vit_surf_brute(cpt_vit_surf) = Val(phrase(5)) * Corr_speedo_glob
            End If

            valeur_active.SPE_4_Vit_Surf = Moyenne_donnees_brute(Vit_surf_brute, nb_val)
            'valeur_active.SPE_6_Heure_enregistrement = Environment.TickCount
            'valeur_active.SPE_6_Heure_enregistrement = Now
        ElseIf phrase(0) = "SDDBT" Then 'Profondeur
            valeur_active.SPE_5_Profondeur = Val(phrase(3))
            'valeur_active.SPE_6_Heure_enregistrement = Environment.TickCount
            'valeur_active.SPE_6_Heure_enregistrement = Now
        ElseIf phrase(0) = "APRSA" Then 'Angle de barre
            valeur_active.PIL_1_Angle_barre = Val(phrase(1))
            valeur_active.PIL_4_Dernier_enregistrement = Environment.TickCount - valeur_active.PIL_3_Heure_enregistrement
            valeur_active.PIL_3_Heure_enregistrement = Environment.TickCount
            'valeur_active.PIL_3_Heure_enregistrement = Now
        ElseIf phrase(0) = "APHDM" Then 'Cap magnétique
            valeur_active.PIL_2_Cap_Mag = Modulo(Val(phrase(1)) + corr_compas, 360)
            'valeur_active.PIL_3_Heure_enregistrement = Environment.TickCount
            'valeur_active.PIL_3_Heure_enregistrement = Now
        ElseIf phrase(0) = "IIVWR" Then ' Capteur vent

            Dim angle_corrige As Single
            Dim coté_corrige As String
            Dim vit_corrige As Single

            angle_corrige = Val(phrase(1))
            coté_corrige = phrase(2)
            vit_corrige = Val(phrase(3))

            If coté_corrige = "L" Or coté_corrige = "R" Then
                'Correction du capteur vent
                Correction_ANG_VAPP(angle_corrige, coté_corrige)
                Correction_VIT_VAPP(vit_corrige)

                'Enregistrement des données
                cpt_angle_vapp = Modulo(cpt_angle_vapp + 1, nb_val)
                cpt_vit_vent = Modulo(cpt_vit_vent + 1, nb_val)

                Vit_ventapp_brute(cpt_vit_vent) = vit_corrige
                Angle_vent_brut(cpt_angle_vapp) = angle_corrige

                valeur_active.ANE_3_Angle_Vapp = Moyenne_donnees_brute(Angle_vent_brut, nb_val)
                valeur_active.ANE_2_Cote_Vapp = coté_corrige
                valeur_active.ANE_1_Vit_Vapp = Moyenne_donnees_brute(Vit_ventapp_brute, nb_val)
                valeur_active.ANE_5_Dernier_enregistrement = Environment.TickCount - valeur_active.ANE_4_Heure_enregistrement
                valeur_active.ANE_4_Heure_enregistrement = Environment.TickCount
                'valeur_active.ANE_4_Heure_enregistrement = Now
            End If

        End If
    End Sub
    Private Sub Traduction_NMEA_GPS(ByRef phrase)
        If phrase(0) = "GPGLL" Then
            valeur_active.GPS_1_Latitude = phrase(1) & phrase(2)
            valeur_active.GPS_2_Longitude = phrase(3) & phrase(4)
            valeur_active.GPS_6_Heure_enregistrement = Environment.TickCount
            'valeur_active.GPS_6_Heure_enregistrement = Now
        ElseIf phrase(0) = "GPVTG" Then
            valeur_active.GPS_4_Cap_fond = Val(phrase(1))
            valeur_active.GPS_5_Vitesse_fond = Val(phrase(5))
            valeur_active.GPS_6_Heure_enregistrement = Environment.TickCount
            'valeur_active.GPS_6_Heure_enregistrement = Now
        End If
    End Sub
    Private Function Test_phrase_NMEA(ByVal chaine As String) As Boolean
        Dim lg_chaine, i As Integer
        Dim chr_deb, chr_fin, chr_checksum, checksum_phrase As String
        Dim checksum_ok, debut_valide As Boolean
        lg_chaine = Len(chaine)
        chr_deb = "$"
        chr_fin = Chr(10)
        chr_checksum = "*"
        checksum_ok = False
        checksum_phrase = ""
        debut_valide = False

        For i = 1 To lg_chaine
            If Mid(chaine, i, 1) = chr_deb Then
                'Défini le début de la phrase NMEA
                debut_valide = True
            ElseIf Mid(chaine, i, 1) = chr_checksum Then
                If debut_valide = True Then
                    checksum_phrase = Mid(chaine, i + 1, 2)
                    If GetChecksum(Mid(chaine, 0, i - 1)) = checksum_phrase Then
                        checksum_ok = True
                    End If
                End If
                debut_valide = False
            End If
        Next i
        Return checksum_ok
    End Function
    Public Function GetChecksum(ByVal sentence As String) As String
        ' Loop through all chars to get a checksum
        Dim Character As Char
        Dim Checksum As Integer
        On Error Resume Next
        For Each Character In sentence
            Select Case Character
                Case "$"c
                    ' Ignore the dollar sign
                Case "*"c
                    ' Stop processing before the asterisk
                    Exit For
                Case Else
                    ' Is this the first value for the checksum?
                    If Checksum = 0 Then
                        ' Yes. Set the checksum to the value
                        Checksum = Convert.ToByte(Character)
                    Else
                        ' No. XOR the checksum with this character's value
                        Checksum = Checksum Xor Convert.ToByte(Character)
                    End If
            End Select
        Next
        ' Return the checksum formatted as a two-character hexadecimal
        Return Checksum.ToString("X2")
    End Function
#End Region
#Region " Tests logiques"
    Public Function valeur_valide_SPE(ByVal valeur As Valeur_Navigation, ByVal ms_limit As Integer) As Boolean
        If Environment.TickCount - valeur.SPE_6_Heure_enregistrement < ms_limit Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function valeur_valide_GPS(ByVal valeur As Valeur_Navigation, ByVal ms_limit As Integer) As Boolean
        If Environment.TickCount - valeur_active.GPS_6_Heure_enregistrement < ms_limit Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function valeur_GPS_Stable()

    End Function
    Public Function valeur_valide_ANE(ByVal valeur As Valeur_Navigation, ByVal ms_limit As Integer) As Boolean
        If Environment.TickCount - valeur.ANE_4_Heure_enregistrement < ms_limit Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function valeur_valide_PIL(ByVal valeur As Valeur_Navigation, ByVal ms_limit As Integer) As Boolean
        If Environment.TickCount - valeur.PIL_3_Heure_enregistrement < ms_limit Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region
#Region " Correction des capteurs"
    Private Sub Correction_VIT_VAPP(ByRef VVApp As Single)
        VVApp = corr_vit_capteur_vent * VVApp
    End Sub
    Private Sub Correction_ANG_VAPP(ByRef angle As Single, ByRef coté As String)

        If coté = "R" Then
            angle = angle + corr_dir_vent
            If angle <= 0 Then
                angle = Abs(angle)
                coté = "L"
            ElseIf angle > 180 Then
                angle = 360 - angle
                coté = "L"
            End If

        ElseIf coté = "L" Then
            angle = angle - corr_dir_vent
            If angle <= 0 Then
                angle = Abs(angle)
                coté = "R"
            ElseIf angle > 180 Then
                angle = 360 - angle
                coté = "R"
            End If
        Else
            angle = angle + corr_dir_vent
            If angle <= 0 Then
                angle = Abs(angle)
                coté = "L"
            ElseIf angle > 0 Then
                angle = Abs(angle)
                coté = "R"
            End If
        End If
    End Sub
    Private Sub Correction_SPEEDO()

    End Sub
    Private Sub Correction_COMPAS()

    End Sub
#End Region

#Region "Calculs de valeurs"
    Private Sub calcul_vent_vrai()
        '******** Calcul du vent vrai (angle et vitesse) ************** SUB A SUPPRIMER (calcul vecteurs la remplace)
        ' Calcul effectué dans le référentiel "Bateau" sur la base d'un courant et d'une dérive négligeable
        Dim Vvent_apparent, Vvit_surf, Vvent_vrai, Vcourant, Vvent_geo, Vvit_fond As Vector2D
        Dim signe As Single

        Vvit_surf = New Vector2D
        Vvent_apparent = New Vector2D
        Vvent_vrai = New Vector2D
        Vcourant = New Vector2D
        Vvent_geo = New Vector2D
        Vvit_fond = New Vector2D

        Vvit_surf.x = valeur_active.SPE_4_Vit_Surf
        Vvit_surf.y = 0

        If valeur_active.ANE_2_Cote_Vapp = "L" Then
            signe = 1
        ElseIf valeur_active.ANE_2_Cote_Vapp = "R" Then
            signe = -1
        End If

        'Calcul du Vent vrai (référentiel bateau)
        Vvent_apparent.x = -valeur_active.ANE_1_Vit_Vapp * Cos(PI / 180 * valeur_active.ANE_3_Angle_Vapp)
        Vvent_apparent.y = -valeur_active.ANE_1_Vit_Vapp * Sin(signe * PI / 180 * valeur_active.ANE_3_Angle_Vapp)

        Vvent_vrai = AddVectors2D(Vvent_apparent, Vvit_surf)
        'Le vent vrai est ramené dans le référentiel géographique
        valeur_active.CAL_1_Vit_Vent_vrai = VecLen2D(Vvent_vrai)
        valeur_active.CAL_2_Ang_Vent_vrai = 180 / PI * AngleVec2D(Oppose_vec2D(Vvent_vrai))
        valeur_active.CAL_3_Cap_Vent_vrai = Modulo(valeur_active.PIL_2_Cap_Mag - valeur_active.CAL_2_Ang_Vent_vrai, 360)



    End Sub
    Private Sub calcul_vent_GEO()
        Dim vfond, vventapp, vventvrai As Vector2D 'SUB A SUPPRIMER (calcul vecteurs la remplace)
        Dim signe As Single

        vfond = New Vector2D
        vventapp = New Vector2D
        vventvrai = New Vector2D

        vfond.x = valeur_active.GPS_5_Vitesse_fond
        vfond.y = 0


        If valeur_active.ANE_2_Cote_Vapp = "L" Then
            signe = 1
        ElseIf valeur_active.ANE_2_Cote_Vapp = "R" Then
            signe = -1
        End If

        vventapp.x = -valeur_active.ANE_1_Vit_Vapp * Cos(PI / 180 * (valeur_active.ANE_3_Angle_Vapp + signe * valeur_active.CAL_5_Cap_courant))
        vventapp.y = -valeur_active.ANE_1_Vit_Vapp * Sin(signe * PI / 180 * (valeur_active.ANE_3_Angle_Vapp + signe * valeur_active.CAL_5_Cap_courant))


        vventvrai = AddVectors2D(vventapp, vfond)
        valeur_active.CAL_8_Vit_Vent_GEO = VecLen2D(vventvrai)
        valeur_active.CAL_10_Ang_vent_vrai_GEO = 180 / PI * AngleVec2D(Oppose_vec2D(vventvrai))
        valeur_active.CAL_9_Cap_Vent_GEO = Modulo(valeur_active.GPS_4_Cap_fond - valeur_active.CAL_10_Ang_vent_vrai_GEO, 360)

    End Sub
    Private Sub calcul_courant() 'SUB A SUPPRIMER (calcul vecteurs la remplace)
        Dim vsurf, vfond, vcourant As Vector2D
        vsurf = New Vector2D
        vfond = New Vector2D

        vsurf.x = valeur_active.SPE_4_Vit_Surf * Cos(Angle_from_Cap(valeur_active.PIL_2_Cap_Mag))
        vsurf.y = valeur_active.SPE_4_Vit_Surf * Sin(Angle_from_Cap(valeur_active.PIL_2_Cap_Mag))

        vfond.x = valeur_active.GPS_5_Vitesse_fond * Cos(Angle_from_Cap(valeur_active.GPS_4_Cap_fond))
        vfond.y = valeur_active.GPS_5_Vitesse_fond * Sin(Angle_from_Cap(valeur_active.GPS_4_Cap_fond))

        vcourant = AddVectors2D(vfond, Oppose_vec2D(vsurf))

        valeur_active.CAL_5_Cap_courant = Modulo(Cap_from_angle(AngleVec2D(vcourant)), 360)
        valeur_active.CAL_11_Force_courant = VecLen2D(vcourant)
    End Sub
    Private Sub calcul_VMG()
        ' Dim Vvreel, Vvitesse As Vector2D
        ' Vvreel = New Vector2D
        ' Vvitesse = New Vector2D

        'Vvreel.x = valeur_active.CAL_1_Vit_Vent_vrai * Cos(PI / 180 * valeur_active.CAL_2_Ang_Vent_vrai)
        ' Vvreel.y = valeur_active.CAL_1_Vit_Vent_vrai * Sin(PI / 180 * valeur_active.CAL_2_Ang_Vent_vrai)

        ' Vvitesse.x = valeur_active.SPE_4_Vit_Surf
        'Vvitesse.y = 0

        ' valeur_active.CAL_4_VMG = DotProduct2D(Vvreel, Vvitesse)
        valeur_active.CAL_4_VMG = Abs(valeur_active.SPE_4_Vit_Surf * Cos(PI / 180 * valeur_active.CAL_2_Ang_Vent_vrai))

    End Sub
    Private Sub Calcul_vecteurs_vent_courant()
        Dim Vvent_apparent, Vvent_apparent_bat, Vvit_surf, Vvit_surf_bat, Vvent_vrai, Vvent_vrai_bat, Vcourant, Vvent_geo, Vvit_fond, Vecteur_temp As Vector2D
        Dim signe, Angle_chgt_rep As Single

        Vvit_surf = New Vector2D
        Vvit_surf_bat = New Vector2D
        Vvent_apparent = New Vector2D
        Vvent_apparent_bat = New Vector2D
        Vvent_vrai = New Vector2D
        Vvent_vrai_bat = New Vector2D
        Vcourant = New Vector2D
        Vvent_geo = New Vector2D
        Vvit_fond = New Vector2D
        Vecteur_temp = New Vector2D

        If valeur_active.ANE_2_Cote_Vapp = "L" Then
            signe = 1
        ElseIf valeur_active.ANE_2_Cote_Vapp = "R" Then
            signe = -1
        End If

        'Vecteur Vent apparent dans le référentiel bateau
        Vecteur_temp.x = -valeur_active.ANE_1_Vit_Vapp * Cos(PI / 180 * valeur_active.ANE_3_Angle_Vapp)
        Vecteur_temp.y = -valeur_active.ANE_1_Vit_Vapp * Sin(signe * PI / 180 * valeur_active.ANE_3_Angle_Vapp)

        Vvent_apparent_bat = Vecteur_temp

        Angle_chgt_rep = PI / 180 * (valeur_active.PIL_2_Cap_Mag)

        'Vecteur Vitesse surface dans le référentiel Géographique
        Vvit_surf.x = valeur_active.SPE_4_Vit_Surf * Cos(Angle_from_Cap(valeur_active.PIL_2_Cap_Mag))
        Vvit_surf.y = valeur_active.SPE_4_Vit_Surf * Sin(Angle_from_Cap(valeur_active.PIL_2_Cap_Mag))

        'vecteur vitesse surface dans le référentiel bateau
        Vvit_surf_bat.x = valeur_active.SPE_4_Vit_Surf
        Vvit_surf_bat.y = 0

        'Vecteur Vitesse fond dans le référentiel Géographique
        Vvit_fond.x = valeur_active.GPS_5_Vitesse_fond * Cos(Angle_from_Cap(valeur_active.GPS_4_Cap_fond))
        Vvit_fond.y = valeur_active.GPS_5_Vitesse_fond * Sin(Angle_from_Cap(valeur_active.GPS_4_Cap_fond))

        'Changement de repère vers le référentiel Géographique
        Vvent_apparent.x = Vecteur_temp.x * Cos(Angle_chgt_rep) + Vecteur_temp.y * Sin(Angle_chgt_rep)
        Vvent_apparent.y = -Vecteur_temp.x * Sin(Angle_chgt_rep) + Vecteur_temp.y * Cos(Angle_chgt_rep)




        'Calcul du vecteur Courant (référentiel GEO)
        Vcourant = AddVectors2D(Vvit_fond, Oppose_vec2D(Vvit_surf))
        'Calcul du vecteur Vent vrai (référentiel GEO)
        ' Vvent_vrai = AddVectors2D(Vvent_apparent, Vvit_surf)

        'Calcul du vecteur vent vrai dans le référentiel bateau
        Vvent_vrai_bat = AddVectors2D(Vvent_apparent_bat, Vvit_surf_bat)

        'Changement de repère vers le référentiel Géographique appliqué au vent_vrai_bateau remplaçan tle calcul du vent vrai (référentiel GEO)
        Vvent_vrai.x = Vvent_vrai_bat.x * Cos(Angle_chgt_rep) + Vvent_vrai_bat.y * Sin(Angle_chgt_rep)
        Vvent_vrai.y = -Vvent_vrai_bat.x * Sin(Angle_chgt_rep) + Vvent_vrai_bat.y * Cos(Angle_chgt_rep)

        'Calcul du vecteur vent géographique (référentiel GEO)
        Vvent_geo = AddVectors2D(Vvent_vrai, Vcourant)


        'Intégration aux valeurs calculées

        valeur_active.CAL_1_Vit_Vent_vrai = VecLen2D(Vvent_vrai)
        valeur_active.CAL_2_Ang_Vent_vrai = AngleVec2D(Oppose_vec2D(Vvent_vrai_bat)) * 180 / PI
        valeur_active.CAL_3_Cap_Vent_vrai = -AngleVec2D(Oppose_vec2D(Vvent_vrai)) * 180 / PI

        valeur_active.CAL_8_Vit_Vent_GEO = VecLen2D(Vvent_geo)          'les résultats sont à vérifier
        valeur_active.CAL_9_Cap_Vent_GEO = -AngleVec2D(Oppose_vec2D(Vvent_geo)) * 180 / PI  'les résultats sont à vérifier

        valeur_active.CAL_5_Cap_courant = AngleVec2D((Vcourant)) * 180 / PI   'A priori ces résultats sont bon
        valeur_active.CAL_11_Force_courant = VecLen2D(Vcourant)   'A priori ces résultats sont bon

        'Sorties de controle
        'Variable_espion(0) = VecLen2D(Vvit_surf)
        ' Variable_espion(1) = Modulo(Cap_from_angle(AngleVec2D((Vvit_surf))), 360)

        ' Variable_espion(2) = VecLen2D(Vvit_fond)
        ' Variable_espion(3) = Modulo(Cap_from_angle(AngleVec2D((Vvit_fond))), 360)

        '  Variable_espion(4) = VecLen2D(Vvent_apparent)
        ' Variable_espion(5) = Modulo(Cap_from_angle(AngleVec2D((Vvent_apparent))) - 180, 360)

    End Sub
    Private Sub calcul_prct_polaire()
        Dim V_opt As Single
        Dim angle As Single

        angle = Abs(valeur_active.CAL_2_Ang_Vent_vrai)
        V_opt = V_Optimale(valeur_active.CAL_1_Vit_Vent_vrai, angle)
        valeur_active.CAL_6_Prct_Vit_opti = valeur_active.SPE_4_Vit_Surf / V_opt

    End Sub
    Private Function Moyenne_donnees_brute(ByVal donnee() As Single, ByVal n As Integer)
        Dim temp As Single
        For i = 0 To n - 1
            temp = temp + 1 / n * donnee(i)
        Next
        Return temp
    End Function
    Private Function Moyenne_donnees_vent(ByVal donnee() As Single, ByVal n As Integer)
        Dim temp As Single
        For i = 0 To n - 1
            If donnee(i) <> 0 Then
                temp = temp + 1 / n * donnee(i)
            Else
                n = n - 1
            End If
        Next
        Return temp
    End Function
    Private Function Ecart_type_donnees(ByVal donnee() As Single, ByVal n As Integer, ByVal val_moy As Single)
        Dim temp As Single
        For i = 0 To n - 1
            temp = temp + 1 / n * (donnee(i) - val_moy) ^ 2
        Next
        temp = temp ^ 0.5
        Return temp
    End Function



    Private Sub calcul_vent_vrai_MOY_30MIN_glissante_1min() 'calcul lancé toutes les secondes
        'Moyenne des données sur 1 minute
        Cpt_interne_minute = Modulo(Cpt_interne_minute + 1, nb_val_minute)

        '----------- CALCULS VENT MOYEN REFERENTIEL BATEAU  ------------------

        'Attention faire une exception si les capteurs ne sont pas stables
        VENT_MOY_1min_actuel.Vitesse(Cpt_interne_minute) = valeur_active.CAL_1_Vit_Vent_vrai
        VENT_MOY_1min_actuel.Direction_x(Cpt_interne_minute) = Cos(Angle_from_Cap(valeur_active.CAL_3_Cap_Vent_vrai))
        VENT_MOY_1min_actuel.Direction_y(Cpt_interne_minute) = Sin(Angle_from_Cap(valeur_active.CAL_3_Cap_Vent_vrai))

        VENT_MOY_1min_ref_X.Vitesse(cpt_minute) = Moyenne_donnees_vent(VENT_MOY_1min_actuel.Vitesse, nb_val_minute)
        VENT_MOY_1min_ref_X.Direction_x(cpt_minute) = Moyenne_donnees_vent(VENT_MOY_1min_actuel.Direction_x, nb_val_minute)
        VENT_MOY_1min_ref_X.Direction_y(cpt_minute) = Moyenne_donnees_vent(VENT_MOY_1min_actuel.Direction_y, nb_val_minute)

        Vecteur_calcul.x = VENT_MOY_1min_ref_X.Direction_x(cpt_minute)
        Vecteur_calcul.y = VENT_MOY_1min_ref_X.Direction_y(cpt_minute)

        VENT_MOY_1min_ref_X.Direction_angle(cpt_minute) = Modulo(Cap_from_angle(AngleVec2D(Vecteur_calcul)), 360)

        '----------- CALCULS VENT MOYEN REFERENTIEL GPS  ------------------

        VENT_MOY_1min_GEO_actuel.Vitesse(Cpt_interne_minute) = valeur_active.CAL_8_Vit_Vent_GEO
        VENT_MOY_1min_GEO_actuel.Direction_x(Cpt_interne_minute) = Cos(Angle_from_Cap(valeur_active.CAL_9_Cap_Vent_GEO))
        VENT_MOY_1min_GEO_actuel.Direction_y(Cpt_interne_minute) = Sin(Angle_from_Cap(valeur_active.CAL_9_Cap_Vent_GEO))

        VENT_MOY_1min_GEO_ref_X.Vitesse(cpt_minute) = Moyenne_donnees_vent(VENT_MOY_1min_GEO_actuel.Vitesse, nb_val_minute)
        VENT_MOY_1min_GEO_ref_X.Direction_x(cpt_minute) = Moyenne_donnees_vent(VENT_MOY_1min_GEO_actuel.Direction_x, nb_val_minute)
        VENT_MOY_1min_GEO_ref_X.Direction_y(cpt_minute) = Moyenne_donnees_vent(VENT_MOY_1min_GEO_actuel.Direction_y, nb_val_minute)

        Vecteur_calcul.x = VENT_MOY_1min_GEO_ref_X.Direction_x(cpt_minute)
        Vecteur_calcul.y = VENT_MOY_1min_GEO_ref_X.Direction_y(cpt_minute)

        VENT_MOY_1min_GEO_ref_X.Direction_angle(cpt_minute) = Modulo(Cap_from_angle(AngleVec2D(Vecteur_calcul)), 360)

    End Sub
    Private Sub calcul_vent_vraimoyen_30min() 'calcul lancé toutes les minutes
        cpt_minute = Modulo(cpt_minute + 1, 30)

        '------------ VENT REFERENTIEL BATEAU -------------

        VENT_MOY_30min.Direction_x(0) = Moyenne_donnees_vent(VENT_MOY_1min_ref_X.Direction_x, 30)
        VENT_MOY_30min.Direction_y(0) = Moyenne_donnees_vent(VENT_MOY_1min_ref_X.Direction_y, 30)
        VENT_MOY_30min.Vitesse(0) = Moyenne_donnees_vent(VENT_MOY_1min_ref_X.Vitesse, 30)

        Vecteur_calcul.x = VENT_MOY_30min.Direction_x(0)
        Vecteur_calcul.y = VENT_MOY_30min.Direction_y(0)

        VENT_MOY_30min.Direction_angle(0) = Modulo(Cap_from_angle(AngleVec2D(Vecteur_calcul)), 360)

        '------------ VENT REFERENTIEL GPS -------------

        VENT_MOY_30min_GEO.Direction_x(0) = Moyenne_donnees_vent(VENT_MOY_1min_GEO_ref_X.Direction_x, 30)
        VENT_MOY_30min_GEO.Direction_y(0) = Moyenne_donnees_vent(VENT_MOY_1min_GEO_ref_X.Direction_y, 30)
        VENT_MOY_30min_GEO.Vitesse(0) = Moyenne_donnees_vent(VENT_MOY_1min_GEO_ref_X.Vitesse, 30)

        Vecteur_calcul.x = VENT_MOY_30min_GEO.Direction_x(0)
        Vecteur_calcul.y = VENT_MOY_30min_GEO.Direction_y(0)

        VENT_MOY_30min_GEO.Direction_angle(0) = Modulo(Cap_from_angle(AngleVec2D(Vecteur_calcul)), 360)

        'Egalisation des moyennes non calculées à la valeur moyenne de la première minute
        If premier_calcul Then
            For i = 2 To nb_val_minute

                '------------ VENT REFERENTIEL BATEAU -------------

                VENT_MOY_1min_ref_X.Direction_x(i) = VENT_MOY_1min_ref_X.Direction_x(0)
                VENT_MOY_1min_ref_X.Direction_y(i) = VENT_MOY_1min_ref_X.Direction_y(0)
                VENT_MOY_1min_ref_X.Vitesse(i) = VENT_MOY_1min_ref_X.Vitesse(0)

                Vecteur_calcul.x = VENT_MOY_1min_ref_X.Direction_x(0)
                Vecteur_calcul.y = VENT_MOY_1min_ref_X.Direction_y(0)

                VENT_MOY_1min_ref_X.Direction_angle(i) = Modulo(Cap_from_angle(AngleVec2D(Vecteur_calcul)), 360)

                '------------ VENT REFERENTIEL GPS -------------

                VENT_MOY_1min_GEO_ref_X.Direction_x(i) = VENT_MOY_1min_GEO_ref_X.Direction_x(0)
                VENT_MOY_1min_GEO_ref_X.Direction_y(i) = VENT_MOY_1min_GEO_ref_X.Direction_y(0)
                VENT_MOY_1min_GEO_ref_X.Vitesse(i) = VENT_MOY_1min_GEO_ref_X.Vitesse(0)

                Vecteur_calcul.x = VENT_MOY_1min_GEO_ref_X.Direction_x(0)
                Vecteur_calcul.y = VENT_MOY_1min_GEO_ref_X.Direction_y(0)

                VENT_MOY_1min_GEO_ref_X.Direction_angle(i) = Modulo(Cap_from_angle(AngleVec2D(Vecteur_calcul)), 360)

                premier_calcul = False
            Next
        End If

    End Sub
    Public Function V_Optimale(ByVal Vvent As Single, ByVal Ang_vent_vrai As Single)
        Dim angle As Integer
        Dim Vopt As Single
        angle = Abs(Int(Ang_vent_vrai))
        If angle > 180 Then angle = 360 - angle
        If angle < 33 Then angle = 33
        If Vvent <= 5 Then
            Vopt = V_Polaire(0, angle) * Vvent / 5
        ElseIf Vvent > 5 And Vvent <= 10 Then
            Vopt = (V_Polaire(1, angle) - V_Polaire(0, angle)) / 5 * Vvent + 2 * V_Polaire(0, angle) - V_Polaire(1, angle)
        ElseIf Vvent > 10 Then
            Vopt = (V_Polaire(2, angle) - V_Polaire(1, angle)) / 10 * Vvent + 2 * V_Polaire(1, angle) - V_Polaire(2, angle)
        End If

        Return Vopt
    End Function

    Private Sub Integration_Valeur_brutes_perf()
        bPERF_WS(cpt_perf) = valeur_active.SPE_4_Vit_Surf
        bPERF_AWA(cpt_perf) = valeur_active.ANE_3_Angle_Vapp
        bPERF_AWS(cpt_perf) = valeur_active.ANE_1_Vit_Vapp
        bPERF_AngB(cpt_perf) = valeur_active.PIL_1_Angle_barre
        bPERF_Perf(cpt_perf) = valeur_active.CAL_6_Prct_Vit_opti
        bPERF_TWA(cpt_perf) = valeur_active.CAL_2_Ang_Vent_vrai
        bPERF_TWS(cpt_perf) = valeur_active.CAL_1_Vit_Vent_vrai
        cpt_perf = Modulo(cpt_perf + 1, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text))
    End Sub


    Private Sub calcul_Valeurs_Moyenne_Perf() 'Calcul lancé toutes les secondes
        'moyenner sur (2 ?) minutes les données suivantes : WS,AWA,AWS,TWA,TWS,Angle barre, Performances.
        PERF_WS = Moyenne_donnees_brute(bPERF_WS, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text))
        PERF_AWA = Moyenne_donnees_brute(bPERF_AWA, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text))
        PERF_AWS = Moyenne_donnees_brute(bPERF_AWS, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text))
        PERF_TWA = Moyenne_donnees_brute(bPERF_TWA, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text))
        PERF_TWS = Moyenne_donnees_brute(bPERF_TWS, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text))

        PERF_AngB = Moyenne_donnees_brute(bPERF_AngB, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text))
        EC_PERF_AngB = Ecart_type_donnees(bPERF_AngB, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text), PERF_AngB)

        PERF_Perf = Moyenne_donnees_brute(bPERF_Perf, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text))
        EC_WS = Ecart_type_donnees(bPERF_WS, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text), PERF_WS)
        EC_AWA = Ecart_type_donnees(bPERF_AWA, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text), PERF_AWA)
        EC_AWS = Ecart_type_donnees(bPERF_AWS, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text), PERF_AWS)
        EC_TWA = Ecart_type_donnees(bPERF_TWA, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text), PERF_TWA)
        EC_TWS = Ecart_type_donnees(bPERF_TWS, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text), PERF_TWS)

        EC_PERF = Ecart_type_donnees(bPERF_Perf, Val(Form_ENR_Perf.Txt_Lissage_Perf.Text), PERF_Perf)

    End Sub
#End Region


#Region "Sub Programme "
    Public Sub demarrage_prgramme()
        Dim horloge_int, coef_calcul, coef_calcul2, coef_ecriture As Single

        fermeture_programme()
        premier_calcul = True

        chaine_ctrl = Chr(10) '& Chr(13)
        chaine_espace = vbCrLf

        'initialisation des variables d'horloge
        time_ms_limit = Val(Form_Start.Txt_tps_limit.Text) ' Défini la validité d'une valeur à x secondes maxi, après elle est périmée
        horloge_int = Val(Form_Start.Txt_horloge_int.Text)
        coef_calcul = Int(Val(Form_Start.Txt_Calcul.Text) / horloge_int)
        coef_calcul2 = Int(60000 / horloge_int) 'lancement d'un calcul spécifique tt les minutes
        coef_ecriture = Int(Val(Form_Start.Txt_Sauvegarde.Text) / horloge_int)

        'Détermine le nombre de valeurs à moyenner pour le loch et anémo:
        nb_val = Int(Val(Form_Start.Txt_Moyenne.Text)) '-> sert à moyenner les valeurs des instruments
        nb_val_minute = Int(60000 / Val(Form_Start.Txt_Calcul.Text)) '-> Détermine l'intervalle de temps entre 2 calculs de moyenne de vent


        'Initialisation des variables de Vecteurs
        Initialisation_performances()

        'Initialisation chemin BDD enregistrement des performances"
        Chemin_BDD = "D:\Users\Vincent\Documents\06 - Bateau\Regate\Mesure performances\POHODA_BDD"

        ' InitValActive() ' initialise la valeur GPS par défaut pour la simulation !!!!

        Form_Start.Visible = True
        valeur_active = New Valeur_Navigation
        VENT_MOY_1min_actuel = New Class_Vent
        VENT_MOY_1min_ref_X = New Class_Vent
        VENT_MOY_30min = New Class_Vent

        VENT_MOY_1min_GEO_actuel = New Class_Vent
        VENT_MOY_1min_GEO_ref_X = New Class_Vent
        VENT_MOY_30min_GEO = New Class_Vent

        Vecteur_calcul = New Vector2D

    End Sub
    Public Sub Initialisation_performances()
        V_Polaire(0, 33) = 2.14546471537054
        V_Polaire(0, 34) = 2.24926491843317
        V_Polaire(0, 35) = 2.35306512149579
        V_Polaire(0, 36) = 2.46377335147863
        V_Polaire(0, 37) = 2.57448158146148
        V_Polaire(0, 38) = 2.68518981144432
        V_Polaire(0, 39) = 2.79589804142717
        V_Polaire(0, 40) = 2.90660627141001
        V_Polaire(0, 41) = 2.9851231242058
        V_Polaire(0, 42) = 3.06363997700158
        V_Polaire(0, 43) = 3.14215682979737
        V_Polaire(0, 44) = 3.22067368259316
        V_Polaire(0, 45) = 3.29919053538895
        V_Polaire(0, 46) = 3.3593842618852
        V_Polaire(0, 47) = 3.41957798838146
        V_Polaire(0, 48) = 3.47977171487771
        V_Polaire(0, 49) = 3.53996544137397
        V_Polaire(0, 50) = 3.60015916787022
        V_Polaire(0, 51) = 3.64232761280833
        V_Polaire(0, 52) = 3.68449605774643
        V_Polaire(0, 53) = 3.72666450268454
        V_Polaire(0, 54) = 3.76883294762265
        V_Polaire(0, 55) = 3.81100139256075
        V_Polaire(0, 56) = 3.84318175468381
        V_Polaire(0, 57) = 3.87536211680686
        V_Polaire(0, 58) = 3.90754247892992
        V_Polaire(0, 59) = 3.93972284105297
        V_Polaire(0, 60) = 3.97190320317603
        V_Polaire(0, 61) = 3.99619394057965
        V_Polaire(0, 62) = 4.02048467798328
        V_Polaire(0, 63) = 4.04477541538691
        V_Polaire(0, 64) = 4.06906615279053
        V_Polaire(0, 65) = 4.09335689019416
        V_Polaire(0, 66) = 4.11168898088171
        V_Polaire(0, 67) = 4.13002107156927
        V_Polaire(0, 68) = 4.14835316225682
        V_Polaire(0, 69) = 4.16668525294437
        V_Polaire(0, 70) = 4.18501734363192
        V_Polaire(0, 71) = 4.19858826624353
        V_Polaire(0, 72) = 4.21215918885514
        V_Polaire(0, 73) = 4.22573011146675
        V_Polaire(0, 74) = 4.23930103407836
        V_Polaire(0, 75) = 4.25287195668997
        V_Polaire(0, 76) = 4.262725445235
        V_Polaire(0, 77) = 4.27257893378004
        V_Polaire(0, 78) = 4.28243242232507
        V_Polaire(0, 79) = 4.2922859108701
        V_Polaire(0, 80) = 4.30213939941513
        V_Polaire(0, 81) = 4.30796795477516
        V_Polaire(0, 82) = 4.31379651013518
        V_Polaire(0, 83) = 4.31962506549521
        V_Polaire(0, 84) = 4.32545362085523
        V_Polaire(0, 85) = 4.33128217621526
        V_Polaire(0, 86) = 4.33472574097221
        V_Polaire(0, 87) = 4.33816930572916
        V_Polaire(0, 88) = 4.3416128704861
        V_Polaire(0, 89) = 4.34505643524305
        V_Polaire(0, 90) = 4.3485
        V_Polaire(0, 91) = 4.34571083490749
        V_Polaire(0, 92) = 4.34292166981498
        V_Polaire(0, 93) = 4.34013250472247
        V_Polaire(0, 94) = 4.33734333962996
        V_Polaire(0, 95) = 4.33455417453745
        V_Polaire(0, 96) = 4.32365197437145
        V_Polaire(0, 97) = 4.31274977420546
        V_Polaire(0, 98) = 4.30184757403947
        V_Polaire(0, 99) = 4.29094537387347
        V_Polaire(0, 100) = 4.28004317370748
        V_Polaire(0, 101) = 4.26054376070356
        V_Polaire(0, 102) = 4.24104434769963
        V_Polaire(0, 103) = 4.22154493469571
        V_Polaire(0, 104) = 4.20204552169179
        V_Polaire(0, 105) = 4.18254610868787
        V_Polaire(0, 106) = 4.14603645553518
        V_Polaire(0, 107) = 4.10952680238249
        V_Polaire(0, 108) = 4.0730171492298
        V_Polaire(0, 109) = 4.03650749607711
        V_Polaire(0, 110) = 3.99999784292442
        V_Polaire(0, 111) = 3.94902681679703
        V_Polaire(0, 112) = 3.89805579066964
        V_Polaire(0, 113) = 3.84708476454225
        V_Polaire(0, 114) = 3.79611373841487
        V_Polaire(0, 115) = 3.74514271228748
        V_Polaire(0, 116) = 3.69367358035786
        V_Polaire(0, 117) = 3.64220444842824
        V_Polaire(0, 118) = 3.59073531649862
        V_Polaire(0, 119) = 3.53926618456901
        V_Polaire(0, 120) = 3.48779705263939
        V_Polaire(0, 121) = 3.44813757153543
        V_Polaire(0, 122) = 3.40847809043147
        V_Polaire(0, 123) = 3.36881860932752
        V_Polaire(0, 124) = 3.32915912822356
        V_Polaire(0, 125) = 3.2894996471196
        V_Polaire(0, 126) = 3.25824782692765
        V_Polaire(0, 127) = 3.2269960067357
        V_Polaire(0, 128) = 3.19574418654374
        V_Polaire(0, 129) = 3.16449236635179
        V_Polaire(0, 130) = 3.13324054615984
        V_Polaire(0, 131) = 3.10851567019936
        V_Polaire(0, 132) = 3.08379079423887
        V_Polaire(0, 133) = 3.05906591827839
        V_Polaire(0, 134) = 3.03434104231791
        V_Polaire(0, 135) = 3.00961616635743
        V_Polaire(0, 136) = 2.98305326632069
        V_Polaire(0, 137) = 2.95649036628395
        V_Polaire(0, 138) = 2.92992746624722
        V_Polaire(0, 139) = 2.90336456621048
        V_Polaire(0, 140) = 2.87680166617374
        V_Polaire(0, 141) = 2.85245476181346
        V_Polaire(0, 142) = 2.82810785745318
        V_Polaire(0, 143) = 2.8037609530929
        V_Polaire(0, 144) = 2.77941404873263
        V_Polaire(0, 145) = 2.75506714437235
        V_Polaire(0, 146) = 2.73224871595131
        V_Polaire(0, 147) = 2.70943028753027
        V_Polaire(0, 148) = 2.68661185910923
        V_Polaire(0, 149) = 2.66379343068819
        V_Polaire(0, 150) = 2.64097500226715
        V_Polaire(0, 151) = 2.61874677616161
        V_Polaire(0, 152) = 2.59651855005606
        V_Polaire(0, 153) = 2.57429032395051
        V_Polaire(0, 154) = 2.55206209784496
        V_Polaire(0, 155) = 2.52983387173941
        V_Polaire(0, 156) = 2.50990708924405
        V_Polaire(0, 157) = 2.48998030674869
        V_Polaire(0, 158) = 2.47005352425333
        V_Polaire(0, 159) = 2.45012674175797
        V_Polaire(0, 160) = 2.43019995926261
        V_Polaire(0, 161) = 2.41282933914244
        V_Polaire(0, 162) = 2.39545871902228
        V_Polaire(0, 163) = 2.37808809890211
        V_Polaire(0, 164) = 2.36071747878194
        V_Polaire(0, 165) = 2.34334685866177
        V_Polaire(0, 166) = 2.32599591813293
        V_Polaire(0, 167) = 2.30864497760409
        V_Polaire(0, 168) = 2.29129403707525
        V_Polaire(0, 169) = 2.27394309654641
        V_Polaire(0, 170) = 2.25659215601756
        V_Polaire(0, 171) = 2.24162811580376
        V_Polaire(0, 172) = 2.22666407558996
        V_Polaire(0, 173) = 2.21170003537615
        V_Polaire(0, 174) = 2.19673599516235
        V_Polaire(0, 175) = 2.18177195494855
        V_Polaire(0, 176) = 2.16915156395884
        V_Polaire(0, 177) = 2.15653117296913
        V_Polaire(0, 178) = 2.14391078197942
        V_Polaire(0, 179) = 2.13129039098971
        V_Polaire(0, 180) = 2.11867
        V_Polaire(1, 33) = 3.14194966415759
        V_Polaire(1, 34) = 3.41018123260186
        V_Polaire(1, 35) = 3.67841280104613
        V_Polaire(1, 36) = 3.83788449861597
        V_Polaire(1, 37) = 3.99735619618582
        V_Polaire(1, 38) = 4.15682789375566
        V_Polaire(1, 39) = 4.31629959132551
        V_Polaire(1, 40) = 4.47577128889536
        V_Polaire(1, 41) = 4.62291222557513
        V_Polaire(1, 42) = 4.7700531622549
        V_Polaire(1, 43) = 4.91719409893467
        V_Polaire(1, 44) = 5.06433503561445
        V_Polaire(1, 45) = 5.21147597229422
        V_Polaire(1, 46) = 5.29890097414772
        V_Polaire(1, 47) = 5.38632597600122
        V_Polaire(1, 48) = 5.47375097785472
        V_Polaire(1, 49) = 5.56117597970822
        V_Polaire(1, 50) = 5.64860098156172
        V_Polaire(1, 51) = 5.70377934269807
        V_Polaire(1, 52) = 5.75895770383442
        V_Polaire(1, 53) = 5.81413606497077
        V_Polaire(1, 54) = 5.86931442610713
        V_Polaire(1, 55) = 5.92449278724348
        V_Polaire(1, 56) = 5.96019816747482
        V_Polaire(1, 57) = 5.99590354770615
        V_Polaire(1, 58) = 6.03160892793749
        V_Polaire(1, 59) = 6.06731430816883
        V_Polaire(1, 60) = 6.10301968840016
        V_Polaire(1, 61) = 6.12835431050227
        V_Polaire(1, 62) = 6.15368893260438
        V_Polaire(1, 63) = 6.17902355470649
        V_Polaire(1, 64) = 6.2043581768086
        V_Polaire(1, 65) = 6.22969279891071
        V_Polaire(1, 66) = 6.24457756290554
        V_Polaire(1, 67) = 6.25946232690036
        V_Polaire(1, 68) = 6.27434709089519
        V_Polaire(1, 69) = 6.28923185489001
        V_Polaire(1, 70) = 6.30411661888484
        V_Polaire(1, 71) = 6.31145913962191
        V_Polaire(1, 72) = 6.31880166035898
        V_Polaire(1, 73) = 6.32614418109605
        V_Polaire(1, 74) = 6.33348670183311
        V_Polaire(1, 75) = 6.34082922257018
        V_Polaire(1, 76) = 6.34786251716031
        V_Polaire(1, 77) = 6.35489581175044
        V_Polaire(1, 78) = 6.36192910634056
        V_Polaire(1, 79) = 6.36896240093069
        V_Polaire(1, 80) = 6.37599569552082
        V_Polaire(1, 81) = 6.38023516963731
        V_Polaire(1, 82) = 6.38447464375381
        V_Polaire(1, 83) = 6.3887141178703
        V_Polaire(1, 84) = 6.3929535919868
        V_Polaire(1, 85) = 6.39719306610329
        V_Polaire(1, 86) = 6.40016845288263
        V_Polaire(1, 87) = 6.40314383966197
        V_Polaire(1, 88) = 6.40611922644132
        V_Polaire(1, 89) = 6.40909461322066
        V_Polaire(1, 90) = 6.41207
        V_Polaire(1, 91) = 6.41147770782835
        V_Polaire(1, 92) = 6.41088541565671
        V_Polaire(1, 93) = 6.41029312348506
        V_Polaire(1, 94) = 6.40970083131342
        V_Polaire(1, 95) = 6.40910853914177
        V_Polaire(1, 96) = 6.40655173627408
        V_Polaire(1, 97) = 6.40399493340638
        V_Polaire(1, 98) = 6.40143813053868
        V_Polaire(1, 99) = 6.39888132767098
        V_Polaire(1, 100) = 6.39632452480329
        V_Polaire(1, 101) = 6.38959027316144
        V_Polaire(1, 102) = 6.38285602151959
        V_Polaire(1, 103) = 6.37612176987775
        V_Polaire(1, 104) = 6.3693875182359
        V_Polaire(1, 105) = 6.36265326659406
        V_Polaire(1, 106) = 6.35248019236605
        V_Polaire(1, 107) = 6.34230711813805
        V_Polaire(1, 108) = 6.33213404391004
        V_Polaire(1, 109) = 6.32196096968203
        V_Polaire(1, 110) = 6.31178789545403
        V_Polaire(1, 111) = 6.29688280611664
        V_Polaire(1, 112) = 6.28197771677925
        V_Polaire(1, 113) = 6.26707262744186
        V_Polaire(1, 114) = 6.25216753810448
        V_Polaire(1, 115) = 6.23726244876709
        V_Polaire(1, 116) = 6.21230717835611
        V_Polaire(1, 117) = 6.18735190794513
        V_Polaire(1, 118) = 6.16239663753416
        V_Polaire(1, 119) = 6.13744136712318
        V_Polaire(1, 120) = 6.1124860967122
        V_Polaire(1, 121) = 6.07738314305408
        V_Polaire(1, 122) = 6.04228018939596
        V_Polaire(1, 123) = 6.00717723573783
        V_Polaire(1, 124) = 5.97207428207971
        V_Polaire(1, 125) = 5.93697132842159
        V_Polaire(1, 126) = 5.89691472174616
        V_Polaire(1, 127) = 5.85685811507073
        V_Polaire(1, 128) = 5.81680150839531
        V_Polaire(1, 129) = 5.77674490171988
        V_Polaire(1, 130) = 5.73668829504445
        V_Polaire(1, 131) = 5.69379203127933
        V_Polaire(1, 132) = 5.65089576751421
        V_Polaire(1, 133) = 5.60799950374908
        V_Polaire(1, 134) = 5.56510323998396
        V_Polaire(1, 135) = 5.52220697621884
        V_Polaire(1, 136) = 5.48018741564715
        V_Polaire(1, 137) = 5.43816785507547
        V_Polaire(1, 138) = 5.39614829450379
        V_Polaire(1, 139) = 5.35412873393211
        V_Polaire(1, 140) = 5.31210917336043
        V_Polaire(1, 141) = 5.27221543866408
        V_Polaire(1, 142) = 5.23232170396774
        V_Polaire(1, 143) = 5.1924279692714
        V_Polaire(1, 144) = 5.15253423457505
        V_Polaire(1, 145) = 5.11264049987871
        V_Polaire(1, 146) = 5.07696361894259
        V_Polaire(1, 147) = 5.04128673800648
        V_Polaire(1, 148) = 5.00560985707037
        V_Polaire(1, 149) = 4.96993297613425
        V_Polaire(1, 150) = 4.93425609519814
        V_Polaire(1, 151) = 4.90278051123406
        V_Polaire(1, 152) = 4.87130492726997
        V_Polaire(1, 153) = 4.83982934330589
        V_Polaire(1, 154) = 4.8083537593418
        V_Polaire(1, 155) = 4.77687817537772
        V_Polaire(1, 156) = 4.75092769048911
        V_Polaire(1, 157) = 4.7249772056005
        V_Polaire(1, 158) = 4.69902672071188
        V_Polaire(1, 159) = 4.67307623582327
        V_Polaire(1, 160) = 4.64712575093466
        V_Polaire(1, 161) = 4.62530028755464
        V_Polaire(1, 162) = 4.60347482417461
        V_Polaire(1, 163) = 4.58164936079459
        V_Polaire(1, 164) = 4.55982389741457
        V_Polaire(1, 165) = 4.53799843403455
        V_Polaire(1, 166) = 4.52185402919303
        V_Polaire(1, 167) = 4.50570962435152
        V_Polaire(1, 168) = 4.48956521951
        V_Polaire(1, 169) = 4.47342081466848
        V_Polaire(1, 170) = 4.45727640982697
        V_Polaire(1, 171) = 4.44618311825416
        V_Polaire(1, 172) = 4.43508982668136
        V_Polaire(1, 173) = 4.42399653510855
        V_Polaire(1, 174) = 4.41290324353575
        V_Polaire(1, 175) = 4.40180995196294
        V_Polaire(1, 176) = 4.39500396157035
        V_Polaire(1, 177) = 4.38819797117776
        V_Polaire(1, 178) = 4.38139198078518
        V_Polaire(1, 179) = 4.37458599039259
        V_Polaire(1, 180) = 4.36778
        V_Polaire(2, 33) = 3.92889221660508
        V_Polaire(2, 34) = 4.04981654772691
        V_Polaire(2, 35) = 4.17074087884875
        V_Polaire(2, 36) = 4.36523053183118
        V_Polaire(2, 37) = 4.55972018481361
        V_Polaire(2, 38) = 4.75420983779604
        V_Polaire(2, 39) = 4.94869949077847
        V_Polaire(2, 40) = 5.1431891437609
        V_Polaire(2, 41) = 5.28401259299615
        V_Polaire(2, 42) = 5.42483604223141
        V_Polaire(2, 43) = 5.56565949146666
        V_Polaire(2, 44) = 5.70648294070191
        V_Polaire(2, 45) = 5.84730638993716
        V_Polaire(2, 46) = 5.90946532592643
        V_Polaire(2, 47) = 5.97162426191569
        V_Polaire(2, 48) = 6.03378319790495
        V_Polaire(2, 49) = 6.09594213389421
        V_Polaire(2, 50) = 6.15810106988348
        V_Polaire(2, 51) = 6.18977515737259
        V_Polaire(2, 52) = 6.22144924486171
        V_Polaire(2, 53) = 6.25312333235083
        V_Polaire(2, 54) = 6.28479741983995
        V_Polaire(2, 55) = 6.31647150732907
        V_Polaire(2, 56) = 6.33858075715289
        V_Polaire(2, 57) = 6.36069000697671
        V_Polaire(2, 58) = 6.38279925680053
        V_Polaire(2, 59) = 6.40490850662435
        V_Polaire(2, 60) = 6.42701775644816
        V_Polaire(2, 61) = 6.44544022983514
        V_Polaire(2, 62) = 6.46386270322211
        V_Polaire(2, 63) = 6.48228517660908
        V_Polaire(2, 64) = 6.50070764999605
        V_Polaire(2, 65) = 6.51913012338303
        V_Polaire(2, 66) = 6.53800017707363
        V_Polaire(2, 67) = 6.55687023076424
        V_Polaire(2, 68) = 6.57574028445484
        V_Polaire(2, 69) = 6.59461033814545
        V_Polaire(2, 70) = 6.61348039183606
        V_Polaire(2, 71) = 6.63691068709801
        V_Polaire(2, 72) = 6.66034098235996
        V_Polaire(2, 73) = 6.68377127762191
        V_Polaire(2, 74) = 6.70720157288386
        V_Polaire(2, 75) = 6.73063186814581
        V_Polaire(2, 76) = 6.75329649439939
        V_Polaire(2, 77) = 6.77596112065298
        V_Polaire(2, 78) = 6.79862574690656
        V_Polaire(2, 79) = 6.82129037316014
        V_Polaire(2, 80) = 6.84395499941372
        V_Polaire(2, 81) = 6.86432007646546
        V_Polaire(2, 82) = 6.88468515351719
        V_Polaire(2, 83) = 6.90505023056893
        V_Polaire(2, 84) = 6.92541530762067
        V_Polaire(2, 85) = 6.94578038467241
        V_Polaire(2, 86) = 6.96505830773793
        V_Polaire(2, 87) = 6.98433623080345
        V_Polaire(2, 88) = 7.00361415386896
        V_Polaire(2, 89) = 7.02289207693448
        V_Polaire(2, 90) = 7.04217
        V_Polaire(2, 91) = 7.05391613940204
        V_Polaire(2, 92) = 7.06566227880408
        V_Polaire(2, 93) = 7.07740841820612
        V_Polaire(2, 94) = 7.08915455760816
        V_Polaire(2, 95) = 7.1009006970102
        V_Polaire(2, 96) = 7.11341230956739
        V_Polaire(2, 97) = 7.12592392212458
        V_Polaire(2, 98) = 7.13843553468176
        V_Polaire(2, 99) = 7.15094714723895
        V_Polaire(2, 100) = 7.16345875979614
        V_Polaire(2, 101) = 7.17811442269763
        V_Polaire(2, 102) = 7.19277008559912
        V_Polaire(2, 103) = 7.20742574850061
        V_Polaire(2, 104) = 7.22208141140209
        V_Polaire(2, 105) = 7.23673707430358
        V_Polaire(2, 106) = 7.25213393095583
        V_Polaire(2, 107) = 7.26753078760808
        V_Polaire(2, 108) = 7.28292764426033
        V_Polaire(2, 109) = 7.29832450091258
        V_Polaire(2, 110) = 7.31372135756483
        V_Polaire(2, 111) = 7.32680030263304
        V_Polaire(2, 112) = 7.33987924770125
        V_Polaire(2, 113) = 7.35295819276945
        V_Polaire(2, 114) = 7.36603713783766
        V_Polaire(2, 115) = 7.37911608290587
        V_Polaire(2, 116) = 7.39201343866189
        V_Polaire(2, 117) = 7.40491079441792
        V_Polaire(2, 118) = 7.41780815017395
        V_Polaire(2, 119) = 7.43070550592997
        V_Polaire(2, 120) = 7.443602861686
        V_Polaire(2, 121) = 7.45387649819685
        V_Polaire(2, 122) = 7.46415013470771
        V_Polaire(2, 123) = 7.47442377121856
        V_Polaire(2, 124) = 7.48469740772941
        V_Polaire(2, 125) = 7.49497104424026
        V_Polaire(2, 126) = 7.49834897966673
        V_Polaire(2, 127) = 7.50172691509319
        V_Polaire(2, 128) = 7.50510485051965
        V_Polaire(2, 129) = 7.50848278594611
        V_Polaire(2, 130) = 7.51186072137257
        V_Polaire(2, 131) = 7.50945722146633
        V_Polaire(2, 132) = 7.50705372156009
        V_Polaire(2, 133) = 7.50465022165385
        V_Polaire(2, 134) = 7.50224672174761
        V_Polaire(2, 135) = 7.49984322184137
        V_Polaire(2, 136) = 7.49515856007172
        V_Polaire(2, 137) = 7.49047389830207
        V_Polaire(2, 138) = 7.48578923653242
        V_Polaire(2, 139) = 7.48110457476277
        V_Polaire(2, 140) = 7.47641991299312
        V_Polaire(2, 141) = 7.46616911636173
        V_Polaire(2, 142) = 7.45591831973034
        V_Polaire(2, 143) = 7.44566752309895
        V_Polaire(2, 144) = 7.43541672646757
        V_Polaire(2, 145) = 7.42516592983618
        V_Polaire(2, 146) = 7.40985919831413
        V_Polaire(2, 147) = 7.39455246679207
        V_Polaire(2, 148) = 7.37924573527001
        V_Polaire(2, 149) = 7.36393900374796
        V_Polaire(2, 150) = 7.3486322722259
        V_Polaire(2, 151) = 7.32710953748593
        V_Polaire(2, 152) = 7.30558680274596
        V_Polaire(2, 153) = 7.28406406800598
        V_Polaire(2, 154) = 7.26254133326601
        V_Polaire(2, 155) = 7.24101859852604
        V_Polaire(2, 156) = 7.2177811620444
        V_Polaire(2, 157) = 7.19454372556277
        V_Polaire(2, 158) = 7.17130628908113
        V_Polaire(2, 159) = 7.14806885259949
        V_Polaire(2, 160) = 7.12483141611786
        V_Polaire(2, 161) = 7.09874268867964
        V_Polaire(2, 162) = 7.07265396124142
        V_Polaire(2, 163) = 7.0465652338032
        V_Polaire(2, 164) = 7.02047650636498
        V_Polaire(2, 165) = 6.99438777892676
        V_Polaire(2, 166) = 6.9683528542758
        V_Polaire(2, 167) = 6.94231792962484
        V_Polaire(2, 168) = 6.91628300497388
        V_Polaire(2, 169) = 6.89024808032292
        V_Polaire(2, 170) = 6.86421315567196
        V_Polaire(2, 171) = 6.84231323308149
        V_Polaire(2, 172) = 6.82041331049102
        V_Polaire(2, 173) = 6.79851338790056
        V_Polaire(2, 174) = 6.77661346531009
        V_Polaire(2, 175) = 6.75471354271963
        V_Polaire(2, 176) = 6.7371008341757
        V_Polaire(2, 177) = 6.71948812563178
        V_Polaire(2, 178) = 6.70187541708785
        V_Polaire(2, 179) = 6.68426270854393
        V_Polaire(2, 180) = 6.66665

    End Sub
    Private Sub InitValActive()
        valeur_active = New Valeur_Navigation

        valeur_active.GPS_1_Latitude = "4740"
        valeur_active.GPS_2_Longitude = "325"
        valeur_active.GPS_4_Cap_fond = 280
        valeur_active.GPS_5_Vitesse_fond = 5
        valeur_active.GPS_6_Heure_enregistrement = Environment.TickCount
    End Sub
    Public Sub fermeture_programme()

    End Sub

#End Region



End Module
