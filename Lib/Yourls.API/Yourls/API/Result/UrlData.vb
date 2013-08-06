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
  ''' URL data.
  ''' </summary>
  ''' <remarks></remarks>
  <DataContract()>
  Public Class UrlData
    ''' <summary>
    ''' Returns or sets the keyword of the short link.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "keyword")>
    Public Keyword As String

    ''' <summary>
    ''' Returns or sets the short URL.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "url")>
    Public Url As String

    ''' <summary>
    ''' Returns or sets the title of the short link.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "title")>
    Public Title As String

    ''' <summary>
    ''' Returns or sets the creation date of the short link.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "date")>
    Public [Date] As String

    ''' <summary>
    ''' Returns or sets the IP of the creator of the short link.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "ip")>
    Public Ip As String

    ''' <summary>
    ''' Returns or sets the total number of clicks of the short link.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "clicks")>
    Public Clicks As Int32
  End Class
End Namespace