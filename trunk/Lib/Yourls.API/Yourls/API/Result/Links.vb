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

Namespace Yourls.API.Result
  ''' <summary>
  ''' Link 'pseudo-arrray' data.
  ''' </summary>
  ''' <remarks></remarks>
  <DataContract()>
  Public Class Links
    'Since YOURLS outputs 'numbered objects' we have to do this. Would have been nicer to parse this as an array of objects.
    'Issue: NYA-7

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_1")>
    Public Link1() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_2")>
    Public Link2() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_3")>
    Public Link3() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_4")>
    Public Link4() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_5")>
    Public Link5() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_6")>
    Public Link6() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_7")>
    Public Link7() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_8")>
    Public Link8() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_9")>
    Public Link9() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_10")>
    Public Link10() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_11")>
    Public Link11() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_12")>
    Public Link12() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_13")>
    Public Link13() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_14")>
    Public Link14() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_15")>
    Public Link15() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_16")>
    Public Link16() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_17")>
    Public Link17() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_18")>
    Public Link18() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_19")>
    Public Link19() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_20")>
    Public Link20() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_21")>
    Public Link21() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_22")>
    Public Link22() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_23")>
    Public Link23() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_24")>
    Public Link24() As LinkData

    ''' <summary>
    ''' Link
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "link_25")>
    Public Link25() As LinkData
  End Class
End Namespace