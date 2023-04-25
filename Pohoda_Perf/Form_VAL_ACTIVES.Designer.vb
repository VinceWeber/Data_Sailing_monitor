<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_VAL_ACTIVES
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
        Me.Lbl_Val_moy = New System.Windows.Forms.Label()
        Me.Lbl_val_active = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Lbl_Val_moy
        '
        Me.Lbl_Val_moy.AutoSize = True
        Me.Lbl_Val_moy.Location = New System.Drawing.Point(364, 24)
        Me.Lbl_Val_moy.Name = "Lbl_Val_moy"
        Me.Lbl_Val_moy.Size = New System.Drawing.Size(91, 13)
        Me.Lbl_Val_moy.TabIndex = 34
        Me.Lbl_Val_moy.Text = "Valeur_moyennes"
        '
        'Lbl_val_active
        '
        Me.Lbl_val_active.AutoSize = True
        Me.Lbl_val_active.Location = New System.Drawing.Point(24, 24)
        Me.Lbl_val_active.Name = "Lbl_val_active"
        Me.Lbl_val_active.Size = New System.Drawing.Size(76, 13)
        Me.Lbl_val_active.TabIndex = 33
        Me.Lbl_val_active.Text = "Lbl_val_active"
        '
        'Form_VAL_ACTIVES
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(542, 406)
        Me.Controls.Add(Me.Lbl_Val_moy)
        Me.Controls.Add(Me.Lbl_val_active)
        Me.Name = "Form_VAL_ACTIVES"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Lbl_Val_moy As System.Windows.Forms.Label
    Friend WithEvents Lbl_val_active As System.Windows.Forms.Label
End Class
