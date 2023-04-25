Imports System.Math

Public Class Vector2D
    Public x As Single
    Public y As Single
End Class
Public Class Position
    Public Latitude As Single
    Public Longitude As Single
    Public Var_Mag As Single
End Class
Public Class Vent
    Public Direction As Single
    Public Force As Single
End Class
Public Class Route
    Public Cap_Mag As Single
    Public Derive As Single
    Public Vitesse_fond As Single
    Public Vitesse_surf As Single
    Public Cap_fond As Single
End Class
Module Vecteurs
    Public Function AddVectors2D(ByVal v2dVec1 As Vector2D, ByVal v2dVec2 As Vector2D) As Vector2D
        AddVectors2D = New Vector2D
        'Addition de deux vecteurs u+v

        With AddVectors2D
            .x = v2dVec1.x + v2dVec2.x
            .y = v2dVec1.y + v2dVec2.y
        End With
    End Function
    Public Function VecLen2D(ByVal v2dVec As Vector2D) As Double

        'Norme d'un vecteur ||v||

        VecLen2D = Sqrt((v2dVec.x ^ 2) + (v2dVec.y ^ 2))
    End Function
    Public Function AngleCosVec2D(ByVal v2dVec1 As Vector2D, ByVal v2dVec2 As Vector2D) As Double

        'Renvoie le cosinus de l'angle entre deux vecteurs cos(u,v)

        AngleCosVec2D = DotProduct2D(v2dVec1, v2dVec2) / (VecLen2D(v2dVec1) * VecLen2D(v2dVec2))
    End Function
    Public Function AngleVec2D(ByVal v2dVec1 As Vector2D) As Double
        Dim sortie As Single
        'Renvoie le l'angle d'un vecteur cos
        If v2dVec1.x = 0 Then
            If v2dVec1.y > 0 Then sortie = PI / 2
            If v2dVec1.y < 0 Then sortie = 3 * PI / 2
        Else
            If v2dVec1.x > 0 Then sortie = Atan(v2dVec1.y / v2dVec1.x)
            If v2dVec1.x < 0 Then sortie = Atan(v2dVec1.y / v2dVec1.x) + PI
        End If
        AngleVec2D = sortie
    End Function
    Public Function Translation_zeroPI_moinsPIPI(ByVal Angle As Double)
        Dim sortie As Single
        If Angle > 0 And Angle <= PI Then
            sortie = Angle
        Else
            sortie = Angle - 2 * PI
        End If
        Translation_zeroPI_moinsPIPI = sortie
    End Function

    Public Function IsVecNull2D(ByVal v2dVec As Vector2D) As Boolean

        'Renvoie une valeur définissant si un vecteur est nul v=0

        IsVecNull2D = (DotProduct2D(v2dVec, v2dVec) = 0)
    End Function
    Public Function Oppose_vec2D(ByVal v2dVec As Vector2D) As Vector2D
        Dim sortie As Vector2D
        sortie = New Vector2D
        'Renvoie le vecteur opposé
        sortie.x = -v2dVec.x
        sortie.y = -v2dVec.y

        Oppose_vec2D = sortie
    End Function
    Public Function AreColinear2D(ByVal v2dVec1 As Vector2D, ByVal v2dVec2 As Vector2D) As Boolean

        'Renvoie une valeur définissant si deux
        'vecteurs sont colinéaires (angle = 180° ou 0°)
        'u=kv

        AreColinear2D = IIf(Abs(AngleCosVec2D(v2dVec1, v2dVec2)) = 1, True, False)
    End Function
    Public Function DotProduct2D(ByVal v2dVec1 As Vector2D, ByVal v2dVec2 As Vector2D) As Double

        'Produit scalaire dans 2 dimensions
        'u.v

        DotProduct2D = v2dVec1.x * v2dVec2.x + v2dVec1.y * v2dVec2.y
    End Function
    Public Function MulV2D(ByVal v2dVec As Vector2D, ByVal dFactor As Double) As Vector2D

        'Multiplication d'un vecteur par un réel
        'ku
        MulV2D = New Vector2D
        With MulV2D
            .x = v2dVec.x * dFactor
            .y = v2dVec.y * dFactor
        End With
    End Function
    Public Function Normalize(ByVal v2dVec As Vector2D) As Vector2D

        'Normalisation d'un vecteur
        Dim bbX As Vector2D
        Dim bbY As Vector2D

        bbX = New Vector2D
        bbY = New Vector2D
        Normalize = New Vector2D

        bbX.x = 1 : bbY.y = 1

        Normalize.x = AngleCosVec2D(v2dVec, bbX)
        Normalize.y = AngleCosVec2D(v2dVec, bbY)
    End Function
    Public Function Cauchy_Schwarz(ByVal v2dVec1 As Vector2D, ByVal v2dVec2 As Vector2D) As Boolean
        'Inégalité triangulaire de Chauchy-Schwarz, qui
        'permet de vérifier les fonctions qui gèrent
        'les vecteurs en 2 dimensions
        '
        'Elle est vérifiée si ||u+v|| <= ||u|| + ||v||

        Cauchy_Schwarz = (VecLen2D(AddVectors2D(v2dVec1, v2dVec2)) <= (VecLen2D(v2dVec1) + VecLen2D(v2dVec2)))
    End Function
    Public Function Angle_from_Cap(ByVal Cap As Single) As Single
        Return PI * (Cap) / 180
    End Function
    Public Function Cap_from_angle(ByVal Angle As Single) As Single
        Return 180 / PI * Angle
    End Function

    Public Function Modulo(ByVal Nombre As Single, ByVal Modul As Single) As Single
        Return Nombre - (Int(Nombre / Modul)) * Modul
    End Function
    'Public Function Angle_from_Cap(ByVal Cap As Single) As Single 'Ancienne formule
    '     Return -PI * Cap / 180
    ' End Function
    'Public Function Cap_from_angle(ByVal Angle As Single) As Single 'Ancienne formule
    '    Return -180 / PI * Angle
    'End Function


#Region "Sub utiles, mais inclassable !!"
    Public Sub hbwait(ByVal ms_to_wait As Long)
        Dim endwait As Double
        endwait = Environment.TickCount + ms_to_wait
        While Environment.TickCount < endwait
            System.Threading.Thread.Sleep(1)
            Application.DoEvents()
        End While
    End Sub
#End Region


End Module