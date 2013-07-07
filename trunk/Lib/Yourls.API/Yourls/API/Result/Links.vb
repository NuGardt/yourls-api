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
  ''' Link data.
  ''' </summary>
  ''' <remarks></remarks>
    <DataContract()>
  Public Class Links
    'Since YOURLS outputs 'numbered objects' we have to do this. Would have been nicer to parse this as an array of objects.
    'Suggested improvement https://github.com/YOURLS/YOURLS/issues/1448

    <DataMember(Name := "link_1")>
    Public Link1() As LinkData

    <DataMember(Name := "link_2")>
    Public Link2() As LinkData

    <DataMember(Name := "link_3")>
    Public Link3() As LinkData

    <DataMember(Name := "link_4")>
    Public Link4() As LinkData

    <DataMember(Name := "link_5")>
    Public Link5() As LinkData

    <DataMember(Name := "link_6")>
    Public Link6() As LinkData

    <DataMember(Name := "link_7")>
    Public Link7() As LinkData

    <DataMember(Name := "link_8")>
    Public Link8() As LinkData

    <DataMember(Name := "link_9")>
    Public Link9() As LinkData

    <DataMember(Name := "link_10")>
    Public Link10() As LinkData
  End Class
End Namespace