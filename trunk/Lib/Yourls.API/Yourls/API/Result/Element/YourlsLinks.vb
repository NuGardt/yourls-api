' NuGardt YOURLS API
' Copyright (C) 2013 NuGardt Software
' http://www.nugardt.com
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If not, see <http://www.gnu.org/licenses/>.
'
Imports System.Runtime.Serialization
Imports System.Text

Namespace Yourls.API.Result.Element
  ''' <summary>
  ''' Link 'pseudo-arrray' data.
  ''' </summary>
  ''' <remarks></remarks>
  <DataContract()>
  Public Class YourlsLinks
    'Since YOURLS outputs 'numbered objects' we have to do this. Would have been nicer to parse this as an array of objects.
    'Issue: NYA-7

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_1", EmitDefaultValue := False)>
    Public Link1 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_2", EmitDefaultValue := False)>
    Public Link2 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_3", EmitDefaultValue := False)>
    Public Link3 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_4", EmitDefaultValue := False)>
    Public Link4 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_5", EmitDefaultValue := False)>
    Public Link5 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_6", EmitDefaultValue := False)>
    Public Link6 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_7", EmitDefaultValue := False)>
    Public Link7 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_8", EmitDefaultValue := False)>
    Public Link8 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_9", EmitDefaultValue := False)>
    Public Link9 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_10", EmitDefaultValue := False)>
    Public Link10 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_11", EmitDefaultValue := False)>
    Public Link11 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_12", EmitDefaultValue := False)>
    Public Link12 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_13", EmitDefaultValue := False)>
    Public Link13 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_14", EmitDefaultValue := False)>
    Public Link14 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_15", EmitDefaultValue := False)>
    Public Link15 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_16", EmitDefaultValue := False)>
    Public Link16 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_17", EmitDefaultValue := False)>
    Public Link17 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_18", EmitDefaultValue := False)>
    Public Link18 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_19", EmitDefaultValue := False)>
    Public Link19 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_20", EmitDefaultValue := False)>
    Public Link20 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_21", EmitDefaultValue := False)>
    Public Link21 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_22", EmitDefaultValue := False)>
    Public Link22 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_23", EmitDefaultValue := False)>
    Public Link23 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_24", EmitDefaultValue := False)>
    Public Link24 As YourlsLinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_25", EmitDefaultValue := False)>
    Public Link25 As YourlsLinkData

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        If (Me.Link1 IsNot Nothing) Then Call .AppendLine(Me.Link1.ToString())
        If (Me.Link2 IsNot Nothing) Then Call .AppendLine(Me.Link2.ToString())
        If (Me.Link3 IsNot Nothing) Then Call .AppendLine(Me.Link3.ToString())
        If (Me.Link4 IsNot Nothing) Then Call .AppendLine(Me.Link4.ToString())
        If (Me.Link5 IsNot Nothing) Then Call .AppendLine(Me.Link5.ToString())
        If (Me.Link6 IsNot Nothing) Then Call .AppendLine(Me.Link6.ToString())
        If (Me.Link7 IsNot Nothing) Then Call .AppendLine(Me.Link7.ToString())
        If (Me.Link8 IsNot Nothing) Then Call .AppendLine(Me.Link8.ToString())
        If (Me.Link9 IsNot Nothing) Then Call .AppendLine(Me.Link9.ToString())
        If (Me.Link10 IsNot Nothing) Then Call .AppendLine(Me.Link10.ToString())
        If (Me.Link11 IsNot Nothing) Then Call .AppendLine(Me.Link11.ToString())
        If (Me.Link12 IsNot Nothing) Then Call .AppendLine(Me.Link12.ToString())
        If (Me.Link13 IsNot Nothing) Then Call .AppendLine(Me.Link13.ToString())
        If (Me.Link14 IsNot Nothing) Then Call .AppendLine(Me.Link14.ToString())
        If (Me.Link15 IsNot Nothing) Then Call .AppendLine(Me.Link15.ToString())
        If (Me.Link16 IsNot Nothing) Then Call .AppendLine(Me.Link16.ToString())
        If (Me.Link17 IsNot Nothing) Then Call .AppendLine(Me.Link17.ToString())
        If (Me.Link18 IsNot Nothing) Then Call .AppendLine(Me.Link18.ToString())
        If (Me.Link19 IsNot Nothing) Then Call .AppendLine(Me.Link19.ToString())
        If (Me.Link20 IsNot Nothing) Then Call .AppendLine(Me.Link20.ToString())
        If (Me.Link21 IsNot Nothing) Then Call .AppendLine(Me.Link21.ToString())
        If (Me.Link22 IsNot Nothing) Then Call .AppendLine(Me.Link22.ToString())
        If (Me.Link23 IsNot Nothing) Then Call .AppendLine(Me.Link23.ToString())
        If (Me.Link24 IsNot Nothing) Then Call .AppendLine(Me.Link24.ToString())
        If (Me.Link25 IsNot Nothing) Then Call .AppendLine(Me.Link25.ToString())
      End With

      Return SB.ToString()
    End Function
  End Class
End Namespace