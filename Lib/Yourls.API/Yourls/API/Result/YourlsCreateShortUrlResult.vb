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
  ''' YOURLS API response for creating a short link.
  ''' </summary>
  ''' <remarks></remarks>
  <DataContract()>
  Public Class YourlsCreateShortUrlResult
    Inherits YourlsResult

    'SUCESS:
    '{"status":"fail",
    '"code":"error:url",
    '"url":
    '{
    '"keyword":"2go8s",
    '"url":"http:\/\/www.nugardt.com",
    '"title":"Home | NuGardt Software",
    '"date":"2013-07-06 05:50:40",
    '"ip":"92.226.234.122",
    '"clicks":"1"
    '},
    '"message":"http:\/\/www.nugardt.com already exists in database",
    '"title":"Home | NuGardt Software",
    '"shorturl":"http:\/\/ls.nugardt.com\/2go8s",
    '"statusCode":200}

    'ERROR
    '{
    '"message":"Invalid username or password",
    '"errorCode":403,
    '"callback":""
    '}

    ''' <summary>
    ''' Returns or sets the status.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "status")>
    Public Status As String

    ''' <summary>
    ''' Returns or sets the code.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "code")>
    Public Code As String

    ''' <summary>
    ''' Returns or sets the title of the short link.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "title")>
    Public Title As String

    ''' <summary>
    ''' Returns or sets the short URL.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "shorturl")>
    Public ShortUrl As String

    ''' <summary>
    ''' Returns or sets the long URL.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "url")>
    Public Url As UrlData
  End Class
End Namespace